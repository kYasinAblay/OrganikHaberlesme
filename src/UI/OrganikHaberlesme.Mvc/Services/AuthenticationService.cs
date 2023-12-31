﻿using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using AutoMapper;

using OrganikHaberlesme.Mvc.Contracts;
using OrganikHaberlesme.Mvc.Models.User;
using OrganikHaberlesme.Mvc.Services.Base;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

using IAuthenticationService = OrganikHaberlesme.Mvc.Contracts.IAuthenticationService;
using Microsoft.AspNetCore.Identity;
using OrganikHaberlesme.Application.Models.Email;
using Newtonsoft.Json;
using SendGrid;
using NuGet.Protocol;
using System.Text.Json;
using System;

namespace OrganikHaberlesme.Mvc.Services
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public AuthenticationService(
            ILocalStorageService localStorage,
            IClient client,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
            : base(localStorage, client)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _tokenHandler = new JwtSecurityTokenHandler();
        }
        public async Task<bool?> AuthenticateOtp(AuthOptions options)
        {
            try
            {
                var authResponse = await _client.OtpLoginAsync(options);
               
                // Get Claims from token and Build auth user object
                var tokenContent = _tokenHandler.ReadJwtToken(authResponse.Token);
               
                var claims = ParseClaims(tokenContent);
                var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

                if (_httpContextAccessor.HttpContext == null)
                {
                    return false;
                }

                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
                _localStorage.SetStorageValue("token", authResponse.Token);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool?> Authenticate(string email, string password)
        {
            try
            {
                var authRequest = new AuthRequest { Email = email, Password = password };
                var authResponse = await _client.LoginAsync(authRequest);

                var result = ToDictionary<bool>(authResponse.Data);

                if (result != null && result["requiresTwoFactor"] && !result["succeeded"])
                {
                    return null;
                }

                if (string.IsNullOrEmpty(authResponse.Token))
                {
                    return false;
                }

                // Get Claims from token and Build auth user object
                var tokenContent = _tokenHandler.ReadJwtToken(authResponse.Token);
                var claims = ParseClaims(tokenContent);
                var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

                if (_httpContextAccessor.HttpContext == null)
                {
                    return false;
                }

                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
                _localStorage.SetStorageValue("token", authResponse.Token);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Register(RegisterVm register)
        {
            try
            {
                var registrationRequest = _mapper.Map<RegistrationRequest>(register);

                var response = await _client.RegisterAsync(registrationRequest);

                if (string.IsNullOrEmpty(response.UserId))
                {
                    return false;
                }

                await Authenticate(register.Email, register.Password);
            }
            catch (Exception ex)
            {
                _localStorage.SetStorageValue<string>("Register", ex.Message);
                return false;
            }
           
            return true;
        }

        public async Task Logout()
        {
            _localStorage.ClearStorage(new List<string> { "token" });

            if (_httpContextAccessor.HttpContext != null)
            {
                await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }

        private static IEnumerable<Claim> ParseClaims(JwtSecurityToken tokenContent)
        {
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            return claims;
        }

        public async Task<VerificationNotify> GetLogin2FACode(string provider) => await _client.GenerateCodeAsync(provider);
        public static Dictionary<string, TValue> ToDictionary<TValue>(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, TValue>>(json);
            return dictionary;
        }
    }
}


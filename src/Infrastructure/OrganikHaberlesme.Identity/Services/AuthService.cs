using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using OrganikHaberlesme.Application.Constants;
using OrganikHaberlesme.Application.Contracts.Identity;
using OrganikHaberlesme.Application.Models.Identity;
using OrganikHaberlesme.Identity.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrganikHaberlesme.Application.Responses;
using Microsoft.AspNetCore.Http;
using OrganikHaberlesme.Application.Models.VerificationCode;

namespace OrganikHaberlesme.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new Exception($"User with '{request.Email}' not found.");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, false);


            if (result.RequiresTwoFactor && !result.Succeeded)
            {
                return new AuthResponse
                {
                    Data = result,
                };
            }

            if (!result.Succeeded)
            {
                throw new Exception($"Credentials for '{request.Email}' aren't valid.");
            }

            var jwtSecurityToken = await GenerateToken(user);

            var response = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName,
            };

            return response;
        }
        public async Task<AuthResponse> OtpLogin(AuthOptions options)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            var result = await _signInManager.TwoFactorSignInAsync(options.Provider, options.Code, options.IsPersistence, options.RememberClient);


            var jwtSecurityToken = await GenerateToken(user);

            var response = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName,
            };

            return response;
        }
        public async Task<VerificationNotify> GenerateCode(string provider)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new Exception($"User '{user.Email}' not found.");
            }
            var providers = await _userManager.GetValidTwoFactorProvidersAsync(user);

            var token = await _userManager.GenerateTwoFactorTokenAsync(user, provider);

            return new VerificationNotify { Code = token, MailTo = user.Email };
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);
            if (existingUser != null)
            {
                throw new Exception($"Username '{request.UserName}' already exists.");
            }

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingEmail != null)
            {
                throw new Exception($"Email '{request.Email} already exists.");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserName = request.UserName,
                FirstName = "",
                LastName = "",
                EmailConfirmed = true,
                TwoFactorEnabled = true
            };
            try
            {
                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Employee");
                    return new RegistrationResponse { UserId = user.Id };
                }
                throw new Exception($"{result.Errors}");
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken
            (
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
            );

            return jwtSecurityToken;
        }


    }
}

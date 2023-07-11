using System;
using System.Text;

using OrganikHaberlesme.Application.Contracts.Identity;
using OrganikHaberlesme.Application.Models.Identity;
using OrganikHaberlesme.Identity.Models;
using OrganikHaberlesme.Identity.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OrganikHaberlesme.Identity.CustomValidations;

namespace OrganikHaberlesme.Identity
{
    public static class IdentityServiceRegistration
    {
        public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services
                .AddDbContext<OrganikIdentityDbContext>(options =>
                {
                    options.UseSqlServer(
                        configuration.GetConnectionString("OrganikHaberlesmeIdentityConnectionString"),
                        b => b.MigrationsAssembly(typeof(OrganikIdentityDbContext).Assembly.FullName));
                });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<OrganikIdentityDbContext>().AddUserValidator<CustomUserValidator>().AddDefaultTokenProviders();

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                    };
                });

            return services;
        }
    }
}

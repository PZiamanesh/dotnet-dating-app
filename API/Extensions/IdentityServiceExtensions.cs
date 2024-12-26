using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(
            this IServiceCollection services,
            IConfiguration configuration
            )
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    var secret = configuration["JwtSettings:Key"] ?? throw new Exception("token not found to validate.");

                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                    };
                });

            return services;
        }
    }
}

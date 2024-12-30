using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration
            )
        {
            #region app services
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPhotoService, PhotoService>();
            #endregion

            services.AddControllers();

            services.AddDbContext<DatingDbContextEF>(opt =>
            {
                opt.UseSqlServer(configuration["ConnectionStrings:DatingAppConnString"]);
            });

            services.AddCors();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.Configure<CloudinarySetting>(configuration.GetSection("CloudinarySettings"));

            return services;
        }
    }
}

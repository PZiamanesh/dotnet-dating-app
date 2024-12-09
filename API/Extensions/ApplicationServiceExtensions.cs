using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

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
            #endregion

            services.AddControllers();

            services.AddDbContext<DatingDbContextEF>(opt =>
            {
                opt.UseSqlServer(configuration["ConnectionStrings:DatingAppConnString"]);
            });

            services.AddCors();

            return services;
        }
    }
}

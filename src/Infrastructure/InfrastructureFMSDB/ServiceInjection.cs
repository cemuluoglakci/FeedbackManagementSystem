using ApplicationFMS.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfrastructureFMSDB
{
    public static class ServiceInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("FMSDatabase");
            services.AddDbContext<FMSDataContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddScoped<IFMSDataContext>(provider => provider.GetService<FMSDataContext>());

            return services;
        }


    }
}

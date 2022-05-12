using ApplicationFMS.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ApplicationFMS.Helpers
{
    public static class ServiceInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerf<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestVal<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggerBehaviour<,>));
            return services;
        }

    }
}

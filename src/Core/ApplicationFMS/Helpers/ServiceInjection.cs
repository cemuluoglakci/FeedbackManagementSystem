using ApplicationFMS.Behaviours;
using MediatR;
using MediatR.Pipeline;
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

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggerBehaviour<,>));

            services.AddTransient(typeof(IRequestExceptionHandler<,>), typeof(ExceptionBehaviourTest<,,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));           

            return services;
        }

    }
}

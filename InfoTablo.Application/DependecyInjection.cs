using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;

namespace InfoTablo.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}

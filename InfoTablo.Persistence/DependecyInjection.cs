using InfoTablo.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfoTablo.Persistence
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<InfoTabloDbContext>(options =>
            options.UseNpgsql(connectionString)
            .UseLazyLoadingProxies());
            services.AddScoped<IInfoTabloDbContext>(provider =>
                provider.GetService<InfoTabloDbContext>());

            services.AddMemoryCache();
            services.AddScoped<IWeatherService, WeatherService>();
            return services;
        }
    }
}

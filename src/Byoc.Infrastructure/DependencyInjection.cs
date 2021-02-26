using Byoc.Application.Interfaces;
using Byoc.Infrastructure.Repositories;
using Byoc.Infrastructure.Services;

using Microsoft.Extensions.DependencyInjection;

namespace Byoc.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IRandomProvider, RandomProvider>();
            services.AddSingleton<IForecastSummaryProvider, ForecastSummaryProvider>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IStringRepository, ForecastSummaryRepository>();
            services.AddTransient<IWeatherService, NswWeatherService>();
        }
    }
}

using System.Threading.Tasks;

using Byoc.Application.Interfaces;
using Byoc.Application.Models;

namespace Byoc.Infrastructure.Apis
{
    public class WeatherForecastApiService : IWeatherForecastApiService
    {
        private readonly IDateTimeProvider _dateTime;
        private readonly IRandomProvider _random;
        private readonly IForecastSummaryProvider _forecastSummary;

        public WeatherForecastApiService(IDateTimeProvider dateTime, IRandomProvider random, IForecastSummaryProvider forecastSummary)
        {
            _dateTime = dateTime;
            _random = random;
            _forecastSummary = forecastSummary;
        }

        public async Task<WeatherForecast> GetAsync(int day)
        {
            // Normally calls an external api
            var result = new WeatherForecast
            {
                Date = _dateTime.Now().AddDays(day),
                TemperatureC = _random.NextInt(-20, 55),
                Summary = _forecastSummary.Get()
            };

            return await Task.FromResult(result);
        }
    }
}

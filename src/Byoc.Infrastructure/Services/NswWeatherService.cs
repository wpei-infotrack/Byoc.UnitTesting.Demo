using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Byoc.Application.Interfaces;
using Byoc.Application.Models;

namespace Byoc.Infrastructure.Services
{
    public class NswWeatherService : IWeatherService
    {
        private readonly IForecastSummaryProvider _forecastSummary;
        private readonly IRandomProvider _random;
        private readonly IDateTimeProvider _dateTime;

        public NswWeatherService(IForecastSummaryProvider forecastSummary, IRandomProvider random, IDateTimeProvider dateTime)
        {
            _forecastSummary = forecastSummary ?? throw new ArgumentNullException(nameof(forecastSummary));
            _random = random ?? throw new ArgumentNullException(nameof(random));
            _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
        }

        public async Task<IEnumerable<WeatherForecast>> GetForecastForNextFiveDaysAsync()
        {
            WeatherForecast[] result = Enumerable.Range(1, 5)
                                                 .Select(index => new WeatherForecast
                                                  {
                                                      Date = _dateTime.Now().AddDays(index),
                                                      TemperatureC = _random.NextInt(-20, 55),
                                                      Summary = _forecastSummary.Get()
                                                  })
                                                 .ToArray();

            return await Task.FromResult(result);
        }

        public async Task<WeatherForecast> GetForecastForAsync(int day)
        {
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

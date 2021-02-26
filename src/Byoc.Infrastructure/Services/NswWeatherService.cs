using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Byoc.Application.Interfaces;
using Byoc.Application.Models;

namespace Byoc.Infrastructure.Services
{
    public class NswWeatherService : IWeatherService
    {
        private readonly IWeatherForecastApiService _weatherForecastApi;

        public NswWeatherService(IWeatherForecastApiService weatherForecastApi)
        {
            _weatherForecastApi = weatherForecastApi;
        }

        public async Task<IEnumerable<WeatherForecast>> GetForecastForNextFiveDaysAsync()
        {
            IEnumerable<int> daysList = Enumerable.Range(1, 5);

            var result = new List<WeatherForecast>();
            foreach (int i in daysList)
            {
                result.Add(await _weatherForecastApi.GetAsync(i));
            }

            return result;
        }

        public async Task<WeatherForecast> GetForecastForAsync(int day)
        {
            return await _weatherForecastApi.GetAsync(day);
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

using Byoc.Application.Models;

namespace Byoc.Application.Interfaces
{
    public interface IWeatherService
    {
        Task<IEnumerable<WeatherForecast>> GetForecastForNextFiveDaysAsync();

        Task<WeatherForecast> GetForecastForAsync(int day);
    }
}

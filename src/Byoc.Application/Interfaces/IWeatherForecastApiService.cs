using System.Threading.Tasks;

using Byoc.Application.Models;

namespace Byoc.Application.Interfaces
{
    public interface IWeatherForecastApiService
    {
        Task<WeatherForecast> GetAsync(int day);
    }
}

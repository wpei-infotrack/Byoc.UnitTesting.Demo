using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Byoc.Application.Interfaces;
using Byoc.Application.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Byoc.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherForecastController(IWeatherService weatherService)
        {
            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WeatherForecast>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            IEnumerable<WeatherForecast> result = await _weatherService.GetForecastForNextFiveDaysAsync();

            return Ok(result);
        }

        [HttpGet("for")]
        [ProducesResponseType(typeof(WeatherForecast), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFor([FromQuery] int day)
        {
            WeatherForecast result = await _weatherService.GetForecastForAsync(day);

            return Ok(result);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using AutoFixture.Xunit2;

using Byoc.Api;
using Byoc.Api.Controllers;
using Byoc.Application.Interfaces;
using Byoc.Application.Models;

using FluentAssertions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;

using Newtonsoft.Json;

using NSubstitute;

using Xunit;

namespace Byoc.UnitTesting.Demo
{
    public class WeatherForecastControllerTests
    {
        private readonly IWebHostBuilder _webHostBuilder;

        public WeatherForecastControllerTests()
        {
            _webHostBuilder = new WebHostBuilder().UseStartup<Startup>();
        }

        [Fact]
        public async Task WhenCallingWeatherForecastEndpoint_ThenReturnForecastsForNextFiveDays()
        {
            // ARRANGE
            using var server = new TestServer(_webHostBuilder);
            using HttpClient sut = server.CreateClient();

            // ACT
            string result = await sut.GetStringAsync("/WeatherForecast");

            // ASSERT
            result.Should().NotBeNullOrEmpty();
            var json = JsonConvert.DeserializeObject<IEnumerable<WeatherForecast>>(result);
            json.Count().Should().Be(5, "There should be forecasts for the next 5 days");
        }

        [Theory, AutoData]
        public async Task GivenAnyDay_WhenGettingWeatherForecast_ThenReturnForecastForThatDay(int day)
        {
            // ARRANGE
            using var server = new TestServer(_webHostBuilder);
            using HttpClient sut = server.CreateClient();

            // ACT
            string result = await sut.GetStringAsync($"/WeatherForecast/For?day={day}");

            // ASSERT
            result.Should().NotBeNullOrEmpty();
            var json = JsonConvert.DeserializeObject<WeatherForecast>(result);
            json.Date.Day.Should().Be(DateTime.Now.AddDays(day).Day);
        }
    }
}

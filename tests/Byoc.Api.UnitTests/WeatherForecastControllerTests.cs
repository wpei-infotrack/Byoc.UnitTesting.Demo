using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoFixture.Xunit2;

using Byoc.Api.Controllers;
using Byoc.Application.Interfaces;
using Byoc.Application.Models;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using NSubstitute;

using Xunit;

namespace Byoc.Api.UnitTests
{
    public class WeatherForecastControllerTests
    {
        [Theory, AutoData]
        public async Task WhenGettingForecastForTheNextFiveDays_ThenReturnExpectedForecasts(IEnumerable<WeatherForecast> expected)
        {
            // ARRANGE
            var fakeWeatherService = Substitute.For<IWeatherService>();

            List<WeatherForecast> expectedAsList = expected.ToList();
            fakeWeatherService.GetForecastForNextFiveDaysAsync().Returns(expectedAsList);

            var sut = new WeatherForecastController(fakeWeatherService);

            // ACT
            IActionResult actionResult = await sut.Get();

            // ASSERT
            var okResult = actionResult as OkObjectResult;
            okResult.Should().NotBeNull();

            var content = okResult?.Value as IEnumerable<WeatherForecast>;
            content.Should().BeEquivalentTo(expectedAsList);
        }
    }
}

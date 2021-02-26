using System;
using System.Threading.Tasks;

using AutoFixture;
using AutoFixture.Xunit2;

using Byoc.Application.Interfaces;
using Byoc.Application.Models;
using Byoc.Infrastructure.Services;

using FluentAssertions;

using NSubstitute;

using Xunit;

namespace Byoc.Infrastructure.UnitTests.Services
{
    public class NswWeatherServiceTests
    {
        private readonly Fixture _fixture;

        private readonly IWeatherForecastApiService _apiService;
        private readonly IWeatherService _sut;

        public NswWeatherServiceTests()
        {
            _fixture = new Fixture();
            _apiService = Substitute.For<IWeatherForecastApiService>();
            _sut = new NswWeatherService(_apiService);
        }

        [Theory]
        [InlineAutoData]
        [InlineAutoData]
        [InlineAutoData]
        [InlineAutoData]
        [InlineAutoData]
        public async Task GivenAnyDay_WhenGettingForecastForDay_ThenReturnExpectedForecast(string summary, int temperatureC, DateTime dateTime, int day)
        {
            // ARRANGE
            WeatherForecast expected = _fixture.Build<WeatherForecast>()
                                               .With(x => x.Date, dateTime.AddDays(day))
                                               .With(x => x.Summary, summary)
                                               .With(x => x.TemperatureC, temperatureC)
                                               .Create();

            _apiService.GetAsync(Arg.Any<int>()).Returns(expected);

            // ACT
            WeatherForecast result = await _sut.GetForecastForAsync(day);

            // ASSERT
            result.Should().BeEquivalentTo(expected);
        }
    }
}

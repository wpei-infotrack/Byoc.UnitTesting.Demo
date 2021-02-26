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

        private readonly IForecastSummaryProvider _forecastSummary;
        private readonly IRandomProvider _random;
        private readonly IDateTimeProvider _dateTime;
        private readonly IWeatherService _sut;

        public NswWeatherServiceTests()
        {
            _fixture = new Fixture();
            _random = Substitute.For<IRandomProvider>();
            _dateTime = Substitute.For<IDateTimeProvider>();
            _forecastSummary = Substitute.For<IForecastSummaryProvider>();
            _sut = new NswWeatherService(_forecastSummary, _random, _dateTime);
        }

        [Theory]
        [InlineAutoData]
        [InlineAutoData]
        [InlineAutoData]
        [InlineAutoData]
        [InlineAutoData]
        public async Task GivenAnyDay_WhenGettingForecastForDay_ThenReturnExpectedForecast(string summary, int temperatureC, DateTime dateTime, int days)
        {
            // ARRANGE
            _dateTime.Now().Returns(dateTime);
            _random.NextInt(Arg.Any<int>(), Arg.Any<int>()).Returns(temperatureC);
            _forecastSummary.Get().Returns(summary);

            WeatherForecast expected = _fixture.Build<WeatherForecast>()
                                               .With(x => x.Date, dateTime.AddDays(days))
                                               .With(x => x.Summary, summary)
                                               .With(x => x.TemperatureC, temperatureC)
                                               .Create();

            // ACT
            WeatherForecast result = await _sut.GetForecastForAsync(days);

            // ASSERT
            result.Should().BeEquivalentTo(expected);
        }
    }
}

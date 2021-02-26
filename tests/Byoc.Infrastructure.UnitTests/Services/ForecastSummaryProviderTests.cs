using AutoFixture.Xunit2;

using Byoc.Application.Interfaces;
using Byoc.Infrastructure.Services;

using FluentAssertions;

using NSubstitute;

using Xunit;

namespace Byoc.Infrastructure.UnitTests.Services
{
    public class ForecastSummaryProviderTests
    {
        private readonly IStringRepository _repository;
        private readonly IForecastSummaryProvider _sut;

        public ForecastSummaryProviderTests()
        {
            var random = Substitute.For<IRandomProvider>();
            _repository = Substitute.For<IStringRepository>();
            _sut = new ForecastSummaryProvider(random, _repository);
        }

        [Theory, AutoData]
        public void GivenAnySetOfSummaries_WhenGettingForecastSummary_ThenReturnSummaryFromSetOfSummaries(string[] summary)
        {
            // ARRANGE
            _repository.GetAll().Returns(summary);

            // ACT
            string result = _sut.Get();

            // ASSERT
            result.Should().BeOneOf(summary);
        }
    }
}

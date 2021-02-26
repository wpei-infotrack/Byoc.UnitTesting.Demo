using Byoc.Application.Interfaces;

namespace Byoc.Infrastructure.Services
{
    public class ForecastSummaryProvider : IForecastSummaryProvider
    {
        private readonly IRandomProvider _random;
        private readonly IStringRepository _repository;

        public ForecastSummaryProvider(IRandomProvider random, IStringRepository repository)
        {
            _random = random;
            _repository = repository;
        }

        public string Get()
        {
            string[] summaries = _repository.GetAll();
            return summaries[_random.NextInt(summaries.Length)];
        }
    }
}

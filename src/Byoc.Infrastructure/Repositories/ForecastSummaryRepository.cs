using Byoc.Application.Interfaces;

namespace Byoc.Infrastructure.Repositories
{
    public class ForecastSummaryRepository : IStringRepository
    {
        private static readonly string[] Summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public string[] GetAll()
        {
            return Summaries;
        }
    }
}

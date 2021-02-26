using System;

using Byoc.Application.Interfaces;

namespace Byoc.Infrastructure.Services
{
    public class RandomProvider : IRandomProvider
    {
        private readonly Random _random;

        public RandomProvider()
        {
            _random = new Random();
        }

        public int NextInt(int maxValue)
        {
            return _random.Next(maxValue);
        }

        public int NextInt(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
    }
}

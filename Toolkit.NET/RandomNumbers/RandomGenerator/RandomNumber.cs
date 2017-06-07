using System;

namespace Toolkit.NET.RandomNumbers.RandomGenerator
{
    public abstract class RandomNumber<T>
    {
        protected readonly Random Random;

        protected RandomNumber(int paSeed)
        {
            Random = new Random(paSeed);
        }

        protected RandomNumber(Random paRandom)
        {
            Random = paRandom;
        }

        public abstract T Next();
    }
}

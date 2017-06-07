using System;

namespace Toolkit.NET.RandomNumbers.RandomGenerator
{
    public class RandomInt : RandomNumber<int>
    {
        private int _min;
        private int _max;
        public RandomInt(int paSeed, int paMin, int paMax) : base(paSeed)
        {
            _min = paMin;
            _max = paMax + 1;
        }

        public RandomInt(int paSeed, int paMax) : base(paSeed)
        {
            _min = 0;
            _max = paMax + 1;
        }

        public RandomInt(Random paRandom, int paMin, int paMax) : base(paRandom)
        {
            _min = paMin;
            _max = paMax;
        }

        public RandomInt(Random paRandom, int paMax) : base(paRandom)
        {
            _min = 0;
            _max = paMax;
        }

        public int NextInt()
        {
            return base.Random.Next(_min, _max);
        }

        public override int Next()
        {
            return this.NextInt();
        }
    }
}

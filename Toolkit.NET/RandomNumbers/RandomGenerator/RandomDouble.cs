using System;

namespace Toolkit.NET.RandomNumbers.RandomGenerator
{
    public class RandomDouble : RandomNumber<double>
    {
        private readonly double _min;
        private readonly double _max;

        public RandomDouble(int paSeed, double paMin = 0, double paMax = 1) : base(paSeed)
        {
            _min = paMin;
            _max = paMax;
            if (_max <= _min)
                throw new ArgumentException($"Min is bigger or equal to max value. Min: {_min}, Max: {_max}");
        }

        public RandomDouble(Random paRandom, double paMin = 0, double paMax = 1) : base(paRandom)
        {
            _min = paMin;
            _max = paMax;
            if (_max <= _min)
                throw new ArgumentException($"Min is bigger or equal to max value. Min: {_min}, Max: {_max}");
        }

        public double NextDouble()
        {
            return ((base.Random.NextDouble() * (_max - _min)) + _min);
        }

        public override double Next()
        {
            return this.NextDouble();
        }
    }
}

using System;

namespace Toolkit.NET.RandomNumbers.RandomGenerator
{
    public class RandomTriangular : RandomNumber<double>
    {
        private readonly double _min;
        private readonly double _max;
        private readonly double _middle;

        public RandomTriangular(int paSeed, double paMin, double paMax, double paMiddle) : base(paSeed)
        {
            _min = paMin;
            _max = paMax;
            _middle = paMiddle;
        }

        public RandomTriangular(Random paRandom, double paMin, double paMax, double paMiddle) : base(paRandom)
        {
            _min = paMin;
            _max = paMax;
            _middle = paMiddle;
        }

        public override double Next()
        {
            double f = (_middle - _min) / (_max - _min);
            double rand = base.Random.NextDouble();
            if (rand < f)
                return _min + Math.Sqrt(rand * (_max - _min) * (_middle - _min));
            else
                return _max - Math.Sqrt((1 - rand) * (_max - _min) * (_max - _middle));
        }
    }
}

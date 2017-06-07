using System;

namespace Toolkit.NET.RandomNumbers.RandomGenerator
{
    public class RandomExponential : RandomNumber<double>
    {
        private readonly double _lambda;

        public RandomExponential(int paSeed, double paLambda) : base(paSeed)
        {
            _lambda = paLambda;
        }

        public RandomExponential(Random paRandom, double paLambda) : base(paRandom)
        {
            _lambda = paLambda;
        }

        public override double Next()
        {
            return Math.Log(1 - base.Random.NextDouble())/(-_lambda);
        }
    }
}

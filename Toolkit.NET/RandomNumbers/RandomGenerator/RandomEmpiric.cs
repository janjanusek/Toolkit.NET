using System;
using System.Linq;

namespace Toolkit.NET.RandomNumbers.RandomGenerator
{
    public class RandomEmpiric<T> : RandomNumber<T>
    {
        private EmpiricRange<T>[] _empiricRange;

        public RandomEmpiric(int paSeed, params EmpiricRange<T>[] paRanges) : base(paSeed)
        {
            this.Init(paRanges);
        }

        private void Init(EmpiricRange<T>[] paRanges)
        {
            if (paRanges.Sum(r => r.Probability) != 1.0)
                throw new Exception("Sum of probabilities must be 1.");
            _empiricRange = paRanges.OrderBy(r => r.Probability).ToArray();
        }

        public RandomEmpiric(Random paRandom, params EmpiricRange<T>[] paRanges) : base(paRandom)
        {
            this.Init(paRanges);
        }

        public override T Next()
        {
            var probability = base.Random.NextDouble();
            var lowerRange = 0.0;
            foreach (var upperRange in _empiricRange)
            {
                if (lowerRange <= probability && probability < lowerRange + upperRange.Probability)
                    return upperRange.Value;
                lowerRange += upperRange.Probability;
            }
            throw new Exception($"Couldn't find coresponding value for probability: {probability}");
        }
    }

    public class EmpiricRange<T>
    {
        public T Value { get; private set; }
        public double Probability { get; private set; }

        public EmpiricRange(T paValue, double paProbability)
        {
            Value = paValue;
            Probability = paProbability;
        }
    }
}

using System;

namespace Toolkit.NET.RandomNumbers.RandomGenerator
{
    public class SeedGenerator
    {
        private static SeedGenerator _instance;
        public static SeedGenerator Instance => _instance ?? (_instance = new SeedGenerator());

        private readonly Random _seedGenerator;

        private SeedGenerator()
        {
#if DEBUG
            _seedGenerator = new Random(12345);
#else
            _seedGenerator = new Random(Environment.TickCount);
#endif
        }

        public Random GetNewRandomInstance()
        {
            return new Random(_seedGenerator.Next());
        }

        public RandomInt GetNewRandomIntegerInstance(int paMin, int paMax)
        {
            return new RandomInt(_seedGenerator.Next(), paMin, paMax);
        }

        public RandomInt GetNewRandomIntegerInstance(int paMax)
        {
            return new RandomInt(_seedGenerator.Next(), paMax);
        }

        public RandomDouble GetNewRandomDoubleInstance(double paMin = 0, double paMax = 1)
        {
            return new RandomDouble(_seedGenerator.Next(), paMin, paMax);
        }

        public RandomEmpiric<T> GetNewRandomEmpiricInstance<T>(params EmpiricRange<T>[] paEmpiricRanges)
        {
            return new RandomEmpiric<T>(_seedGenerator.Next(), paEmpiricRanges);
        }

        public RandomTriangular GetNewRandomTriangularInstance(double paMin, double paMax, double paMiddle)
        {
            return new RandomTriangular(_seedGenerator.Next(), paMin, paMax, paMiddle);
        }

        public RandomExponential GetNewRandomExponentialInstance(double paLambda)
        {
            return new RandomExponential(_seedGenerator.Next(), paLambda);
        }

        public RandomExponential GetNewRandomExponentialInstanceE(double paEValue)
        {
            return new RandomExponential(_seedGenerator.Next(), 1.0 / paEValue);
        }

    }
}

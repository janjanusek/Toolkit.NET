using System;

namespace Toolkit.NET.Simulations.EventOrientedSimulationCore.StatisticCollections
{
    public class ConfidenceInterval<TData>
    {
        private readonly Func<TData, double> _conversionFunction;

        private double _xiFirstPart;
        private double _xiSecondPart;
        private int _n;
        private double _sum;
        private readonly double _tAlpha;


        public ConfidenceInterval(Func<TData, double> paConversionFunction, double paTAlpha)
        {
            _tAlpha = paTAlpha;
            _xiFirstPart = _xiSecondPart = _n = 0;
            _conversionFunction = paConversionFunction;
        }

        public void AddValue(TData paTData)
        {
            var value = this._conversionFunction.Invoke(paTData);
            _xiFirstPart += value * value;//Math.Pow(value, 2);
            _xiSecondPart += value;
            _sum += value;
            _n++;
        }

        public double[] CalculateConfidenceInterval()
        {
            //var s = Math.Sqrt((_xiFirstPart / _n) - Math.Pow(_xiSecondPart / _n, 2));
            var sPowTwo = _xiFirstPart/_n - ((_xiSecondPart/_n)*(_xiSecondPart/_n));
            var s = Math.Sqrt(sPowTwo);
            var mean = _sum / _n;
            return new[] { mean - (_tAlpha * s) / Math.Sqrt(_n - 1), mean + (_tAlpha * s) / Math.Sqrt(_n - 1) };
        }
    }

    public class ConfidenceInterval
    {
        private double _xiFirstPart;
        private double _xiSecondPart;
        private int _n;
        private double _sum;
        private readonly double _tAlpha;


        public ConfidenceInterval(double paTAlpha)
        {
            _tAlpha = paTAlpha;
            _xiFirstPart = _xiSecondPart = _n = 0;
        }

        public void AddValue(double paTData)
        {
            var value = paTData;
            _xiFirstPart += value * value;//Math.Pow(value, 2);
            _xiSecondPart += value;
            _sum += value;
            _n++;
        }

        public double[] CalculateConfidenceInterval()
        {
            //var s = Math.Sqrt((_xiFirstPart / _n) - Math.Pow(_xiSecondPart / _n, 2));
            var sPowTwo = _xiFirstPart / _n - ((_xiSecondPart / _n) * (_xiSecondPart / _n));
            var s = Math.Sqrt(sPowTwo);
            var mean = _sum / _n;
            return new[] { mean - (_tAlpha * s) / Math.Sqrt(_n - 1), mean + (_tAlpha * s) / Math.Sqrt(_n - 1) };
        }
    }
}

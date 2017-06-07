using System;

namespace Toolkit.NET.Simulations.EventOrientedSimulationCore.StatisticCollections
{
    public class MeanValue<TData>
    {
        private readonly Func<TData, double> _conversionFunction;
        private int _count;
        private double _sum;
        public double Mean => double.IsNaN(_sum / _count) == false ? _sum / _count : 0;

        public MeanValue(Func<TData, double> paConversion)
        {
            _conversionFunction = paConversion;
            _sum = _count = 0;
        }

        public void AddValue(TData paData)
        {
            _sum += _conversionFunction.Invoke(paData);
            _count++;
        }
    }

    public class MeanValue
    {
        private int _count;
        private double _sum;
        public double Mean => double.IsNaN(_sum / _count) == false ? _sum / _count : 0;

        public MeanValue()
        {
            _sum = _count = 0;
        }

        public void AddValue(double paData)
        {
            _sum += paData;
            _count++;
        }
    }
}

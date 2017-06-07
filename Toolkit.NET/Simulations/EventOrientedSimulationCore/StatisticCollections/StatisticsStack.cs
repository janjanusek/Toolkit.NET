using System.Collections;
using System.Collections.Generic;
using Toolkit.NET.Simulations.EventOrientedSimulationCore.EventSimulationCore;

namespace Toolkit.NET.Simulations.EventOrientedSimulationCore.StatisticCollections
{
    public class StatisticsStack<TData> : IEnumerable<TData>
    {
        private readonly EventSimulation _eventSimulation;
        private readonly Stack<TData> _stack;
        private double _sum;
        private int _count;
        private double _timeOfLastChange;
        public double MeanTimeInQueue => double.IsNaN(_sum / _count) == false ? _sum / _count : 0;
        public double MeanCountInStack => double.IsNaN(_sum / _eventSimulation.SimulationTime) == false ? _sum / _eventSimulation.SimulationTime : 0;
        public int Count => _stack.Count;

        public StatisticsStack(EventSimulation paEventSimulation)
        {
            _eventSimulation = paEventSimulation;
            _stack = new Stack<TData>();
            _sum = _timeOfLastChange = 0;
        }

        public void Push(TData paData)
        {
            _sum += _stack.Count * (_eventSimulation.CurrentSimulationTime - _timeOfLastChange);
            _timeOfLastChange = _eventSimulation.CurrentSimulationTime;
            _stack.Push(paData);
            _count++;
        }

        public TData Pop()
        {
            _sum += _stack.Count * (_eventSimulation.CurrentSimulationTime - _timeOfLastChange);
            _timeOfLastChange = _eventSimulation.CurrentSimulationTime;
            return _stack.Pop();
        }

        public TData Peek() => _stack.Peek();

        public void Clear()
        {
            //_sum += _stack.Count * (_eventSimulation.CurrentSimulationTime - _timeOfLastChange);
            //_timeOfLastChange = _eventSimulation.CurrentSimulationTime;
            _stack.Clear();
        }

        public IEnumerator<TData> GetEnumerator()
        {
            return _stack.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return $"Count = {this.Count}";
        }
    }
}

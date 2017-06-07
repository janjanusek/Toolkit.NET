using System.Collections;
using System.Collections.Generic;
using Toolkit.NET.Simulations.EventOrientedSimulationCore.EventSimulationCore;

namespace Toolkit.NET.Simulations.EventOrientedSimulationCore.StatisticCollections
{
    public class StatisticsQueue<TData> : IEnumerable<TData>
    {
        private readonly EventSimulation _eventSimulation;
        private readonly Queue<TData> _queue;
        private double _sum;
        private int _count;
        private double _timeOfLastChange;
        public double MeanTimeInQueue => double.IsNaN(_sum / _count) == false ? _sum / _count : 0;
        public double MeanCountInQueue => double.IsNaN(_sum / _eventSimulation.SimulationTime) == false ? _sum / _eventSimulation.SimulationTime : 0;
        public int Count => _queue.Count;

        public StatisticsQueue(EventSimulation paEventSimulation)
        {
            _eventSimulation = paEventSimulation;
            _queue = new Queue<TData>();
            _sum = _timeOfLastChange = 0;
        }

        public void Enqueue(TData paData)
        {
            _sum += _queue.Count * (_eventSimulation.CurrentSimulationTime - _timeOfLastChange);
            _queue.Enqueue(paData);
            _timeOfLastChange = _eventSimulation.CurrentSimulationTime;
            _count++;
        }

        public TData Dequeue()
        {
            _sum += _queue.Count * (_eventSimulation.CurrentSimulationTime - _timeOfLastChange);
            _timeOfLastChange = _eventSimulation.CurrentSimulationTime;
            return _queue.Dequeue();
        }

        public void Clear()
        {
            //_sum += _queue.Count * (_eventSimulation.CurrentSimulationTime - _timeOfLastChange);
            //_timeOfLastChange = _eventSimulation.CurrentSimulationTime;
            _queue.Clear();
        }

        public IEnumerator<TData> GetEnumerator()
        {
            return _queue.GetEnumerator();
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

using System;
using System.ComponentModel;
using System.Threading;
using Priority_Queue;

namespace Toolkit.NET.Simulations.EventOrientedSimulationCore.EventSimulationCore
{
    public class EventFinishedEventArgs : EventArgs
    {
        public string DebugMessage { get; set; }
    }

    public abstract class EventSimulation
    {
        public event EventHandler<EventFinishedEventArgs> EventIsDone;
        public event EventHandler SimulationStarted;
        public event EventHandler SimulationEnded;
        private BackgroundWorker _backgroundWorker;

        public double CurrentSimulationTime { get; private set; }
        public double SimulationTime { get; private set; }
        private readonly SimplePriorityQueue<Event, double> _timeAxis;
        private bool _pause;
        private bool _isRunning;
        private int _simulationDelay;

        protected EventSimulation(double paSimulationTime)
        {
            SimulationTime = paSimulationTime;
            CurrentSimulationTime = _simulationDelay = 0;
            _timeAxis = new SimplePriorityQueue<Event,double>();//(new EventComparer());
            _backgroundWorker = new BackgroundWorker()
            {
                WorkerSupportsCancellation = true
            };
            _backgroundWorker.DoWork += BackgroundWorkerOnDoWork;
            _backgroundWorker.RunWorkerCompleted += BackgroundWorkerOnRunWorkerCompleted;
            _isRunning = _pause = false;
        }

        private void BackgroundWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            OnSimulationEnded();
            _isRunning = false;
        }

        private void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            var worker = (BackgroundWorker)sender;
            Event tempEvent;
            double tempEventTime;
            while (_timeAxis.Count != 0 && CurrentSimulationTime < SimulationTime)
            {
                if (worker.CancellationPending)
                    break;
                while (_pause)
                    Thread.Sleep(250);
                tempEvent = _timeAxis.Dequeue();
                tempEventTime = tempEvent.EventTime;
                if (CurrentSimulationTime > tempEventTime)
                    throw new Exception($"Event traveling in time. Current Event is planed for:{tempEventTime} but current simulation time is {CurrentSimulationTime}");
                CurrentSimulationTime = tempEventTime;//tempEventTime;
                tempEvent.Execute();
                OnEventIsDone(new EventFinishedEventArgs() { DebugMessage = tempEvent.ToString() });
                if (_simulationDelay != 0)
                    Thread.Sleep(_simulationDelay);
            }
        }

        public void PlanEvent(Event paEvent, double paTime)
        {
            if (paTime < CurrentSimulationTime)
                throw new ArgumentException($"Cannot plan event on time which is in past. Your time: {paTime}, simulation time: {CurrentSimulationTime}");
            _timeAxis.Enqueue(paEvent, paTime);
        }

        public void DoSimulationOnCurrentThread()
        {
            Event tempEvent;
            double tempEventTime;
            while (_timeAxis.Count != 0 && CurrentSimulationTime < SimulationTime)
            {
                tempEvent = _timeAxis.Dequeue();
                tempEventTime = tempEvent.EventTime;
                if (CurrentSimulationTime > tempEventTime)
                    throw new Exception($"Event traveling in time. Current Event is planed for:{tempEventTime} but current simulation time is {CurrentSimulationTime}");
                CurrentSimulationTime = tempEventTime;
                tempEvent.Execute();
            }
        }

        public void DoSimulationAsync()
        {
            if (_pause)
            {
                _pause = false;
                return;
            }
            if (_isRunning == false)
            {
                this.OnSimulationStarted();
                _backgroundWorker.RunWorkerAsync();
            }
        }

        public void Stop()
        {
            _isRunning = false;
            //if (_backgroundWorker.IsBusy)
            _backgroundWorker.CancelAsync();
        }

        public void Pause() => _pause = true;
        public void UnPause() => _pause = false;

        public bool IsRunning => _isRunning;
        public bool IsPaused => _pause;

        public void ChangeSimulationDelay(int paDelay) => _simulationDelay = Math.Max(0, paDelay);

        protected virtual void OnEventIsDone(EventFinishedEventArgs e)
        {
            EventIsDone?.Invoke(this, e);
        }

        protected virtual void OnSimulationStarted()
        {
            _isRunning = true;
            SimulationStarted?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnSimulationEnded()
        {
            SimulationEnded?.Invoke(this, EventArgs.Empty);
        }
    }

    public class EventSimulationEventArgs : EventArgs
    {
        public Event DoneEvent { get; set; }
    }
}

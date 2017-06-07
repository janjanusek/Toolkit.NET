using System;

namespace Toolkit.NET.Simulations.MonteCarloCore
{
    public class MonteCarloEventArgs<TData> : EventArgs
    {
        public int ReplicationNumber { get; set; }
        public bool ExperimentIsRunning { get; set; }
        public bool ResultsUpdated { get; set; }
        public TData Data { get; set; }

        public MonteCarloEventArgs()
        {
            ExperimentIsRunning = false;
            ReplicationNumber = 0;
        }
    }

    public class MonteCarloStartedEventArgs : EventArgs
    {

    }

    public class MonteCarloStoppedEventArgs<TData> : EventArgs
    {
        public int ReplicationNumber { get; set; }
        public TData Data { get; set; }

        public MonteCarloStoppedEventArgs(MonteCarloEventArgs<TData> paMonteCarloEventArgs)
        {
            ReplicationNumber = paMonteCarloEventArgs.ReplicationNumber;
            Data = paMonteCarloEventArgs.Data;
        }
    }
}
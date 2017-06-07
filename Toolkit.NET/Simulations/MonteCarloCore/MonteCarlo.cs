using System;
using System.ComponentModel;

namespace Toolkit.NET.Simulations.MonteCarloCore
{
    public abstract class MonteCarlo<TDataType>
    {
        public event EventHandler<MonteCarloStartedEventArgs> ExperimentStarted;
        public event EventHandler<MonteCarloStoppedEventArgs<TDataType>> ExperimentStopped;

        public event EventHandler<MonteCarloEventArgs<TDataType>> ReplicationFinished;
        private bool _experimentApproved;
        protected readonly int CountOfReplications;
        private readonly int _howManySkip;
        protected TDataType ReplicationResults;
        private BackgroundWorker _backgroundWorker;

        protected MonteCarlo(int paCountOfReplications, int paHowManySkip = 0)
        {
            _howManySkip = paHowManySkip;
            this.CountOfReplications = paCountOfReplications;
        }

        private void InitWorker()
        {
            _backgroundWorker?.Dispose();
            _backgroundWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _backgroundWorker.DoWork += _backgroundWorker_DoWork;
            _backgroundWorker.ProgressChanged += _backgroundWorker_ProgressChanged;
        }

        private void _backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is MonteCarloEventArgs<TDataType>)
            {
                var args = (MonteCarloEventArgs<TDataType>)e.UserState;
                this.OnReplicationFinished(args);
            }
            else if (e.UserState is MonteCarloStartedEventArgs)
            {
                var args = (MonteCarloStartedEventArgs)e.UserState;
                this.OnExperimentStarted(args);
            }
            else if(e.UserState is MonteCarloStoppedEventArgs<TDataType>)
            {
                var args = (MonteCarloStoppedEventArgs<TDataType>)e.UserState;
                this.OnExperimentStopped(args);
            }
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.StartExperiment();
            var eventArgs = new MonteCarloEventArgs<TDataType>()
            {
                ExperimentIsRunning = true
            };
            var worker = sender as BackgroundWorker;
            //worker.ReportProgress(0, );
            OnExperimentStarted(new MonteCarloStartedEventArgs());
            var nthElement = (int)e.Argument;
            var lastResult = CountOfReplications;
            var feedOutCondition = false;
            for (var i = 0; i < this.CountOfReplications; i++)
            {
                if (worker.CancellationPending)
                {
                    lastResult = i;
                    break;
                }
                this.DoReplication();
                eventArgs = new MonteCarloEventArgs<TDataType>() { ExperimentIsRunning = true };

                feedOutCondition = i >= _howManySkip && i % nthElement == 0;
                if (feedOutCondition)
                {
                    eventArgs.Data = this.ReplicationResults;
                    eventArgs.ResultsUpdated = true;
                }
                else
                    eventArgs.ResultsUpdated = false;
                if (i % nthElement == 0)
                {
                    eventArgs.ReplicationNumber = 1 + i;
                    //worker.ReportProgress(0, eventArgs);
                    OnReplicationFinished(eventArgs);
                }
            }
            eventArgs.ExperimentIsRunning = false;
            if (worker.CancellationPending == false || eventArgs.ReplicationNumber == CountOfReplications || (CountOfReplications - 1) % nthElement != 0)
            {
                eventArgs.ResultsUpdated = true;
                eventArgs.ReplicationNumber = lastResult;
                eventArgs.Data = this.ReplicationResults;
            }
            OnExperimentStopped(new MonteCarloStoppedEventArgs<TDataType>(eventArgs));
            //worker.ReportProgress(0, );
        }

        protected abstract void DoReplication();

        public void RunExperimentInBackground(int paShowOnlyNthReplication)
        {
            if (_backgroundWorker?.IsBusy == true)
                return;
            this.InitWorker();
            _backgroundWorker.RunWorkerAsync(paShowOnlyNthReplication);
        }

        protected void OnReplicationFinished(MonteCarloEventArgs<TDataType> e)
        {
            ReplicationFinished?.Invoke(this, e);
        }

        protected void StartExperiment() => this._experimentApproved = true;
        public void StopExperiment()
        {
            _backgroundWorker.CancelAsync();
            this._experimentApproved = false;
        }
        public bool ExperimentCanceled => this._experimentApproved;

        protected virtual void OnExperimentStarted(MonteCarloStartedEventArgs e)
        {
            ExperimentStarted?.Invoke(this, e);
        }

        protected virtual void OnExperimentStopped(MonteCarloStoppedEventArgs<TDataType> e)
        {
            _backgroundWorker?.Dispose();
            _backgroundWorker = null;
            ExperimentStopped?.Invoke(this, e);
        }
    }
}

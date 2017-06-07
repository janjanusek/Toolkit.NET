using System;
using System.ComponentModel;
using System.Threading;

namespace Toolkit.NET.Parrallel
{
    public class ParallelExecutor<TArrayType> : IDisposable
    {
        public event EventHandler ExecutionStarted;
        public event EventHandler ExecutionIsDone;
        private readonly Mutex _mutex;
        private readonly IndexedBackgroundWorker[] _backgroundWorkers;
        private int _countOfWorkingThreads;
        public bool IsExecuting { get; private set; }

        public ParallelExecutor(int paCountOfThreads)
        {
            _mutex = new Mutex();
            _backgroundWorkers = new IndexedBackgroundWorker[paCountOfThreads];
            for (int i = 0; i < _backgroundWorkers.Length; i++)
            {
                _backgroundWorkers[i] = new IndexedBackgroundWorker()
                {
                    Index = i
                };
                _backgroundWorkers[i].DoWork += OnDoWork;
                _backgroundWorkers[i].RunWorkerCompleted += OnRunWorkerCompleted;
            }
            _countOfWorkingThreads = 0;
        }

        public void Execute(TArrayType[] paArray, Action<TArrayType> paExecution)
        {
            ExecuteAsync(paArray, paExecution);
            while (IsExecuting) { }
        }

        public void ExecuteAsync(TArrayType[] paArray, Action<TArrayType> paExecution)
        {
            if (IsExecuting)
                throw new Exception("Parallel executor is already executing.");
            IsExecuting = true;
            this.OnExecutionStarted();
            var perThread = (int)Math.Ceiling(1.0 * paArray.Length / _backgroundWorkers.Length);
            var countOfThreads = _backgroundWorkers.Length;
            if (paArray.Length < _backgroundWorkers.Length)
                countOfThreads = perThread;
            _countOfWorkingThreads = countOfThreads;
            for (int i = 0; i < countOfThreads; i++)
            {
                _backgroundWorkers[i].RunWorkerAsync(new WorkerParam<TArrayType>()
                {
                    StartIndex = i * perThread,
                    EndIndexExclusive = (i + 1) * perThread,
                    Array = paArray,
                    Action = paExecution
                });
            }
        }

        private void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            _mutex.WaitOne();
            if (--_countOfWorkingThreads == 0)
            {
                IsExecuting = false;
                this.OnExecutionIsDone();
            }
            _mutex.ReleaseMutex();
        }

        private void OnDoWork(object o, DoWorkEventArgs doWorkEventArgs)
        {
            var parameter = (WorkerParam<TArrayType>)doWorkEventArgs.Argument;

            for (int j = parameter.StartIndex; j < parameter.EndIndexExclusive && j < parameter.Array.Length; j++)
            {
                parameter.Action.Invoke(parameter.Array[j]);
            }
        }

        public void Dispose()
        {
            foreach (var worker in _backgroundWorkers)
            {
                worker.Dispose();
            }
            _mutex.Dispose();
        }

        protected virtual void OnExecutionIsDone()
        {
            ExecutionIsDone?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnExecutionStarted()
        {
            ExecutionStarted?.Invoke(this, EventArgs.Empty);
        }
    }

    internal class WorkerParam<TArrayType>
    {
        public TArrayType[] Array { get; set; }
        public int StartIndex { get; set; }
        public int EndIndexExclusive { get; set; }
        public Action<TArrayType> Action { get; set; }
    }
}

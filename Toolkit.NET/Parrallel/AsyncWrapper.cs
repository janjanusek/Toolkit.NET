using System;
using System.ComponentModel;

namespace Toolkit.NET.Parrallel
{
    public class AsyncWrapper<TResult>
    {
        public event EventHandler<TResult> AsyncTaskFinished;
        private TResult _result;
        private readonly BackgroundWorker _backgroundWorker;
        public bool IsRunning { get; private set; }

        public AsyncWrapper(Func<object, TResult> paFunc, bool paSupportedCancelation = false)
        {
            IsRunning = false;
            _backgroundWorker = new BackgroundWorker()
            {
                WorkerSupportsCancellation = paSupportedCancelation
            };
            _backgroundWorker.DoWork += (sender, args) =>
            {
                _result = paFunc.Invoke(args.Argument);
            };
            _backgroundWorker.RunWorkerCompleted += (sender, args) =>
            {
                IsRunning = false;
                OnAsyncTaskFinished(_result);
            };
        }

        public void RunAsync(object paParameter = null)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                _backgroundWorker.RunWorkerAsync(paParameter);
            }
            else
                throw new Exception("Async wrapper is already running.");
        }

        protected virtual void OnAsyncTaskFinished(TResult e)
        {
            AsyncTaskFinished?.Invoke(this, e);
        }
    }
}

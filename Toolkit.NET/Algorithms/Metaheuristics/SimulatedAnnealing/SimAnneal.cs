using System;
using System.ComponentModel;
using System.Threading;

namespace Toolkit.NET.Algorithms.Metaheuristics.SimulatedAnnealing
{
    public abstract class SimAnneal<TFinalResult>
    {
        public event EventHandler SimulatedAnnealingFinished;
        private ISolution _bestYet;
        private readonly Mutex _mutex;
        public ISolution BestYet
        {
            get
            {
                _mutex.WaitOne();
                var temp = _bestYet;
                _mutex.ReleaseMutex();
                return temp;
            }
            set
            {
                _mutex.WaitOne();
                _bestYet = value;
                _mutex.ReleaseMutex();
            }
        }

        public TFinalResult Result { get; protected set; }

        public double Temperature { get; set; }
        public double MinTemperature { get; set; }
        public double Alpha { get; set; }
        public int Iterations { get; set; }
        public int MaximumIterations { get; set; }

        private readonly BackgroundWorker _backgroundWorker;
        public bool IsAsyncRunning { get; private set; }

        protected SimAnneal()
        {
            _mutex = new Mutex();
            _backgroundWorker = new BackgroundWorker()
            {
                WorkerSupportsCancellation = true
            };
            _backgroundWorker.DoWork += (sender, args) =>
            {
                Run();
            };
            _backgroundWorker.RunWorkerCompleted += (sender, args) =>
            {
                IsAsyncRunning = false;
                OnSimulatedAnnealingFinished();
            };
        }

        public void Run()
        {
            this.Init();
            BestYet = GetInitialSolution();
            var oldSol = BestYet;
            var random = new Random((int)(DateTime.Now.Ticks % int.MaxValue));
            var iterations = 0;
            while (Temperature > MinTemperature && iterations < MaximumIterations)
            {
                for (int i = 0; i < Iterations; iterations += i++)
                {
                    var newSol = this.NextSolution(oldSol);
                    if (AcceptanceProbability(oldSol, newSol) > random.NextDouble())
                    {
                        oldSol = newSol;
                        if (BestYet.Cost > newSol.Cost)
                            BestYet = newSol;
                    }
                }
                Temperature *= Alpha;
            }
            GC.Collect();
            PrepareFinalSolution();
            if (!IsAsyncRunning)
                OnSimulatedAnnealingFinished();
            GC.Collect();
        }

        private double AcceptanceProbability(ISolution paNew, ISolution paOld)
        {
            return paNew.Cost < paOld.Cost ? 1 : Math.Pow(Math.E, (paNew.Cost - paOld.Cost) / Temperature);
        }

        protected abstract void Init();

        protected abstract ISolution GetInitialSolution();

        protected abstract ISolution NextSolution(ISolution paOldSolution);

        protected abstract void PrepareFinalSolution();

        public bool RunAsync()
        {
            if (IsAsyncRunning)
                return false;
            IsAsyncRunning = true;
            _backgroundWorker.RunWorkerAsync();
            return true;
        }

        protected virtual void OnSimulatedAnnealingFinished()
        {
            SimulatedAnnealingFinished?.Invoke(this, EventArgs.Empty);
        }
    }
}

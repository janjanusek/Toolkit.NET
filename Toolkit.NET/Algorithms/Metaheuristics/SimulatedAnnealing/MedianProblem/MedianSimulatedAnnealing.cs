using System;
using System.Collections.Generic;
using System.Linq;
using IoaHeuristicOptimalizer.Algorithms.PathFindingAlghorithms.Dijkstra;
using Toolkit.NET.Algorithms.PathFinding;
using Toolkit.NET.Parrallel;

namespace Toolkit.NET.Algorithms.Metaheuristics.SimulatedAnnealing.MedianProblem
{
    public class MedianSimulatedAnnealing : SimAnneal<MedianResult>
    {
        private double[][] _dist;
        private readonly INode[] _candidates;
        private readonly INode[] _customers;
        private readonly int _p;
        private readonly Random _random;
        private readonly IGraph _graph;

        public MedianSimulatedAnnealing(int paP, INode[] paCandidates, INode[] paCustomers, IGraph paGraph)
        {
            _p = paP;
            _random = new Random((int)DateTime.Now.Ticks);
            _graph = paGraph;
            _candidates = paCandidates;
            _customers = paCustomers;
        }

        private int RandomNumber() => _random.Next(0, _candidates.Length);

        protected override void Init()
        {
            var count = 0;
            foreach (var node in _graph.Nodes)
            {
                node.Index = count++;
            }
            _dist = new double[_candidates.Length][];//[_customers.Length];
            for (int i = 0; i < _candidates.Length; i++)
            {
                _dist[i] = new double[_customers.Length];
            }
            using (var exec = new ParallelExecutor<double[]>(Math.Max(1, Environment.ProcessorCount - 1)))
            {
                exec.Execute(_dist, (doubles, i) =>
                {
                    var disjtrasAlg = new DijkstrasAlgorithm();
                    var distances = disjtrasAlg.FindPaths(_graph, _candidates[i]).ToArray();

                    for (int j = 0; j < _customers.Length; j++)
                    {
                        _dist[i][j] = distances[_customers[j].Index].Distance;
                    }
                });
            }
        }

        protected override ISolution GetInitialSolution()
        {
            var candidates = new int[_p]; //Enumerable.Range(0, _p).ToArray();
            var costs = new double[_p];
            for (int i = 0; i < _p; i++)
            {
                var index = RandomNumber()/*_currentStep*/ % _candidates.Length;
                for (int j = 0; j < candidates.Length; j++)
                {
                    if (candidates[j] == index)
                    {
                        //index++;
                        index = RandomNumber()/*_currentStep*/ % _candidates.Length;
                        j = 0;
                    }
                }
                candidates[i] = index;
            }
            var sol = new MedianSolution()
            {
                PickedCandidates = candidates,
                PickedCandidatesCosts = costs,
                Cost = this.CountCost(candidates, costs)
            };

            return NextSolution(sol);
        }

        protected override ISolution NextSolution(ISolution paOldSolution)
        {
            var medianSol = (MedianSolution)paOldSolution;
            var step = /*_currentStep*/ RandomNumber() % _p;
            var picked = new int[_p];
            var costs = new double[_p];
            for (int i = 0; i < _p; i++)
            {
                if (step != i)
                    picked[i] = medianSol.PickedCandidates[i];
                else
                {
                    var index = RandomNumber()/*_currentStep*/ % _candidates.Length;
                    for (int j = 0; j < medianSol.PickedCandidates.Length; j++)
                    {
                        if (medianSol.PickedCandidates[j] == index)
                        {
                            //index++;
                            index = RandomNumber()/*_currentStep*/ % _candidates.Length;
                            j = 0;
                        }
                    }
                    //index = index % _candidates.Length;
                    picked[i] = index;
                }
            }
            var sol = new MedianSolution()
            {
                PickedCandidates = picked,
                PickedCandidatesCosts = costs,
                Cost = CountCost(picked, costs)
            };
            return sol;
        }

        protected override void PrepareFinalSolution()
        {
            Result = new MedianResult();
            var best = (MedianSolution)BestYet;

            for (int i = 0; i < best.PickedCandidates.Length; i++)
            {
                Result.Results.Add(_candidates[best.PickedCandidates[i]], new LinkedList<INode>());
            }

            for (int i = 0; i < _customers.Length; i++)
            {
                var customer = _customers[i];
                var min = double.MaxValue;
                var index = -1;
                for (int j = 0; j < best.PickedCandidates.Length; j++)
                {
                    if (min > _dist[best.PickedCandidates[j]][i])
                    {
                        index = j;
                        min = _dist[best.PickedCandidates[j]][i];
                    }
                }
                if (index != -1)
                {
                    Result.Results[_candidates[best.PickedCandidates[index]]].AddLast(_customers[i]);
                    Result.Cost += min;
                }
                else
                    Result.NotServedCustomers.AddLast(_customers[i]);
            }
        }

        private double CountCost(int[] paPicked, double[] paPickedCost)
        {
            var cost = 0.0;
            for (int i = 0; i < _customers.Length; i++)
            {
                var customer = _customers[i];
                var min = double.MaxValue;
                var index = -1;
                for (int j = 0; j < paPicked.Length; j++)
                {
                    if (min > _dist[paPicked[j]][i])
                    {
                        index = j;
                        min = _dist[paPicked[j]][i];
                    }
                }
                if (index == -1)
                {
                    cost += double.NegativeInfinity;
                    paPickedCost[index != -1 ? index : 0] += double.NegativeInfinity;
                }
                else
                {
                    cost += min;
                    paPickedCost[index != -1 ? index : 0] += min;
                }
            }
            return cost;
        }

    }
}

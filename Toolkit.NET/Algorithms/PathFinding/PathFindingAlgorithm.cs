using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Toolkit.NET.Algorithms.PathFinding;
using Path = Toolkit.NET.Algorithms.PathFinding.Path;

namespace IoaHeuristicOptimalizer.Algorithms.PathFindingAlghorithms
{
    public abstract class PathFindingAlgorithm
    {
        public TimeSpan DurationOfLastSearch { get; private set; }
        private readonly Stopwatch _stopwatch;
        public int CountOfSteps { get; protected set; }

        protected PathFindingAlgorithm()
        {
            _stopwatch = new Stopwatch();
        }

        public float[] Find(IGraph paGraph, INode paSourceNode)
        {
            this.CountOfSteps = 0;
            _stopwatch.Restart();
            _stopwatch.Start();
            var result = this.FindImplementation(paGraph, paSourceNode);
            _stopwatch.Stop();
            DurationOfLastSearch = _stopwatch.Elapsed;
            return result;
        }

        protected abstract float[] FindImplementation(IGraph paGraph, INode paSourceNode);

        public IEnumerable<Path> FindPaths(IGraph paGraph, INode paSourceNode)
        {
            var pathsInArray = this.Find(paGraph, paSourceNode);
            var paths = new LinkedList<Path>();
            var fromNode = paSourceNode;
            var wrongPath = new Path() { From = fromNode, Distance = float.PositiveInfinity };
            for (int i = 0; i < pathsInArray.Length; i++)
            {
                if (paGraph.Nodes.Count() > i)
                {
                    var path = new Path()
                    {
                        From = fromNode,
                        To = paGraph.NodeByIndex(i),
                        Distance = pathsInArray[i]
                    };
                    paths.AddLast(path);
                }
                else
                    paths.AddLast(wrongPath);
            }
            return paths;
        }

        public virtual Path FindPath(IGraph paGraph, INode paSourceNode, INode paDestinationNode)
        {
            return this.FindPaths(paGraph, paSourceNode).First(p => p.To.Index == paDestinationNode.Index);
        }
    }
}

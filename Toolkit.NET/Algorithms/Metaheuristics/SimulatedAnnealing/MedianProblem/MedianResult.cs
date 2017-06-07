using System.Collections.Generic;
using Toolkit.NET.Algorithms.PathFinding;

namespace Toolkit.NET.Algorithms.Metaheuristics.SimulatedAnnealing.MedianProblem
{
    public class MedianResult
    {
        public Dictionary<INode, LinkedList<INode>> Results { get; set; }
        public LinkedList<INode> NotServedCustomers { get; set; }
        public double Cost { get; set; }

        public bool TotalRequirements { get; set; }

        public MedianResult()
        {
            Results = new Dictionary<INode, LinkedList<INode>>();
            NotServedCustomers = new LinkedList<INode>();
        }
    }
}
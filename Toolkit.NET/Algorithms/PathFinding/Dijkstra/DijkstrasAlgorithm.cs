using System.Linq;
using Priority_Queue;
using Toolkit.NET.Algorithms.PathFinding;

namespace IoaHeuristicOptimalizer.Algorithms.PathFindingAlghorithms.Dijkstra
{
    public class DijkstrasAlgorithm : PathFindingAlgorithm
    {
        protected override float[] FindImplementation(IGraph paGraph, INode paSourceNode)
        {
            var sourceNode = paSourceNode;
            var queue = new SimplePriorityQueue<INode,float>();
            var dist = new float[paGraph.Nodes.Count()];
            var previous = new INode[paGraph.Nodes.Count()];

            dist[sourceNode.Index] = 0;
            var nodeIndex = 0;
            foreach (var node in paGraph.Nodes)
            {
                nodeIndex = node.Index;
                if (node != sourceNode)
                {
                    dist[nodeIndex] = float.PositiveInfinity;
                }
                queue.Enqueue(node, dist[nodeIndex]);
            }

            while (queue.Count != 0)
            {
                var bestNode = queue.Dequeue();
                base.CountOfSteps++;
                foreach (var pathToNeighbor in bestNode.IncidentNodes)
                {
                    var neighborNode = pathToNeighbor.Oposite(bestNode);
                    var alt = dist[bestNode.Index] + pathToNeighbor.Distance;
                    if (alt < dist[neighborNode.Index])
                    {
                        dist[neighborNode.Index] = alt;
                        previous[neighborNode.Index] = bestNode;
                        queue.UpdatePriority(neighborNode, alt);
                    }
                }
            }
            return dist;
        }
    }
}

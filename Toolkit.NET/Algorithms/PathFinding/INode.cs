using System.Collections.Generic;

namespace Toolkit.NET.Algorithms.PathFinding
{
    public interface INode
    {
        int Index { get; }
        IEnumerable<Path> IncidentNodes { get; }
    }
}
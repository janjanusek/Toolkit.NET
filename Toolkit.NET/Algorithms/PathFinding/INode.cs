using System.Collections.Generic;

namespace Toolkit.NET.Algorithms.PathFinding
{
    public interface INode
    {
        int Index { get; set; }
        IEnumerable<Path> IncidentNodes { get; }
    }
}
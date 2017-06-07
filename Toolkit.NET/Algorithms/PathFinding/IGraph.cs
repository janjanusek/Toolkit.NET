using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.NET.Algorithms.PathFinding
{
    public interface IGraph
    {
        IEnumerable<INode> Nodes { get; }
        INode NodeByIndex(int paIndex);
    }
}

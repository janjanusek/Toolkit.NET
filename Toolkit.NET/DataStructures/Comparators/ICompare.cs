using System.Collections.Generic;

namespace Toolkit.NET.DataStructures.Comparators
{
    public interface ICompare<TData> : IComparer<TData>
    {
        int Compare(TData paCompareWith);
    }
}

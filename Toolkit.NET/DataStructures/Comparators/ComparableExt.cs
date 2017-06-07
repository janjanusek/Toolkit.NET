using System.Collections.Generic;

namespace Toolkit.NET.DataStructures.Comparators
{
    public static class ComparableExt
    {
        public static Compare CompareTo<TCompareType>(this IComparer<TCompareType> paComparer, TCompareType paX, TCompareType paY)
        {
            return (Compare) paComparer.Compare(paX, paY);
        }
    }
}

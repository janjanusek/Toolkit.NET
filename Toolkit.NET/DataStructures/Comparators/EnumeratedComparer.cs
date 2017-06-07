using System.Collections.Generic;

namespace Toolkit.NET.DataStructures.Comparators
{
    public class EnumeratedComparer<TKey> : IComparer<TKey>
    {
        private readonly IComparer<TKey> _comparer;

        public EnumeratedComparer(IComparer<TKey> paComparer)
        {
            _comparer = paComparer;
        }

        public Compare CompareTo(TKey paCompareKey, TKey paCompareKeyWith)
        {
            return (Compare) _comparer.Compare(paCompareKey, paCompareKeyWith);
        }

        public int Compare(TKey paCompareKey, TKey paCompareKeyWith)
        {
            return _comparer.Compare(paCompareKey, paCompareKeyWith);
        }
    }
}

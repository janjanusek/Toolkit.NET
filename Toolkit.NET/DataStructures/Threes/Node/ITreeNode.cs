using Toolkit.NET.DataStructures.Comparators;

namespace Toolkit.NET.DataStructures.Threes.Node
{
    public interface ITreeNode<TKey, TData>
    {
        ITreeNode<TKey, TData> Parent { get; set; }
        ITreeNode<TKey, TData> LeftChild { get; set; }
        ITreeNode<TKey, TData> RightChild { get; set; }

        TKey Key { get; set; }
        TData Data { get; set; }

        bool Insert(ITreeNode<TKey, TData> paInsertNode, EnumeratedComparer<TKey> paComparer);

        ITreeNode<TKey, TData> Remove();
    }
}

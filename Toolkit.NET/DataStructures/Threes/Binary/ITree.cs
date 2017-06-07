using Toolkit.NET.DataStructures.Threes.Node;

namespace Toolkit.NET.DataStructures.Threes.Binary
{
    public interface ITree<TKey, TData>
    {
        int Count { get; }
        ITreeNode<TKey, TData> Root { get; }

        TData Search(TKey paKey);
        bool Insert(TKey paKey, TData paData);
        TData Delete(TKey paKey);
    }
}

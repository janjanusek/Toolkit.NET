using Toolkit.NET.DataStructures.Threes.Binary;

namespace Toolkit.NET.DataStructures.Threes.Node
{
    public abstract class NodeBase<TKey, TData>
    {
        #region Referecnes on other nodes
        
        public ITreeNode<TKey, TData> Parent { get; set; }
        public ITreeNode<TKey, TData> LeftChild { get; set; }
        public ITreeNode<TKey, TData> RightChild { get; set; }

        public TKey Key { get; set; }
        public TData Data { get; set; }

        #endregion
        
        public ChildrensSide IamMyParents_SideChildren()
        {
            if(Parent == null)
                return ChildrensSide.Unknown;
            return this == Parent.LeftChild ? ChildrensSide.LeftChild : ChildrensSide.RightChild;
        }
    }
}

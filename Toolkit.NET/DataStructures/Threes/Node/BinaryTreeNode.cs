using Toolkit.NET.DataStructures.Comparators;
using Toolkit.NET.DataStructures.Threes.Binary;

namespace Toolkit.NET.DataStructures.Threes.Node
{
    public class BinaryTreeNode<TKey, TData> : NodeBase<TKey, TData>, ITreeNode<TKey, TData>
    {
        public bool Insert(ITreeNode<TKey, TData> paInsertNode, EnumeratedComparer<TKey> paComparer)
        {
            ITreeNode<TKey, TData> temp = this;
            while (true)
            {
                var compareResult = paComparer.CompareTo(temp.Key, paInsertNode.Key);
                if (compareResult == Compare.Equal)
                    return false;

                if (compareResult == Compare.LessThan)
                {
                    if (temp.RightChild != null)
                        temp = temp.RightChild;
                    else
                    {
                        paInsertNode.Parent = temp;
                        temp.RightChild = paInsertNode;
                        return true;
                    }
                }
                else if (compareResult == Compare.GreaterThan)
                {
                    if (temp.LeftChild != null)
                        temp = temp.LeftChild;
                    else
                    {
                        paInsertNode.Parent = temp;
                        temp.LeftChild = paInsertNode;
                        return true;
                    }
                }
            }
        }

        public ITreeNode<TKey, TData> Remove()
        {
            var side = IamMyParents_SideChildren();

            if (this.RightChild == null)
            {
                if (side == ChildrensSide.LeftChild)
                    this.Parent.LeftChild = this.LeftChild;
                else if(side == ChildrensSide.RightChild)
                    this.Parent.RightChild = this.LeftChild;
                if (this.LeftChild != null)
                    this.LeftChild.Parent = this.Parent;
                return this.LeftChild;
            }
            else if (this.RightChild != null && this.LeftChild == null)
            {
                if (side == ChildrensSide.LeftChild)
                    this.Parent.LeftChild = this.RightChild;
                else if (side == ChildrensSide.RightChild)
                    this.Parent.RightChild = this.RightChild;
                this.RightChild = this.Parent;
                return this.RightChild;
            }
            else if (this.LeftChild != null && this.RightChild != null)
            {
                ITreeNode<TKey, TData> minNode = this.RightChild;
                while (minNode.LeftChild != null)
                    minNode = minNode.LeftChild;
                if (minNode.RightChild != null)
                {
                    minNode.RightChild.Parent = minNode.Parent;
                    minNode.Parent.LeftChild = minNode.RightChild;
                }
                else
                    minNode.Parent.LeftChild = null;

                if (side == ChildrensSide.LeftChild)
                    this.Parent.LeftChild = minNode;
                else if (side == ChildrensSide.RightChild)
                    this.Parent.RightChild = minNode;
                minNode.Parent = this.Parent;
                minNode.RightChild = this.RightChild;
                if (minNode.RightChild != null)
                    minNode.RightChild.Parent = minNode;
                minNode.LeftChild = this.LeftChild;
                if (minNode.LeftChild != null)
                    minNode.LeftChild.Parent = minNode;
                return minNode;
            }
            return null;
        }
    }
}

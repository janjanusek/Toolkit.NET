using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Toolkit.NET.DataStructures.Comparators;
using Toolkit.NET.DataStructures.Threes.Node;

namespace Toolkit.NET.DataStructures.Threes.Binary
{
    public class BinaryTree<TKey, TData> : ITree<TKey, TData>, IEnumerable<TData>
    {

        #region Properties

        public int Count { get; private set; }
        public ITreeNode<TKey, TData> Root { get; set; }
        public EnumeratedComparer<TKey> Comparator { get; set; }

        #endregion

        #region Constructor

        public BinaryTree(IComparer<TKey> paComparable)
        {
            Comparator = paComparable != null ? new EnumeratedComparer<TKey>(paComparable) : null;
            Root = null;
        }

        #endregion

        #region Search methods

        public virtual TData Search(TKey paKey)
        {
            return Search(paKey, Root);
        }

        public virtual bool ContainsKey(TKey paKey) => SearchInternally(paKey, Root) != null;

        private TData Search(TKey paKey, ITreeNode<TKey, TData> paBinaryTreeNodeWhereIsSearching)
        {
            var result = SearchInternally(paKey, paBinaryTreeNodeWhereIsSearching);
            if (result != null)
                return result.Data;
            return default(TData);
        }

        protected virtual ITreeNode<TKey, TData> SearchInternally(TKey paKey, ITreeNode<TKey, TData> paNodeWhereIsSearching)
        {
            var tempNode = paNodeWhereIsSearching;
            if (tempNode == null)
                return null;
            var result = Comparator.CompareTo(paKey, tempNode.Key);
            if (result == Compare.LessThan)
                return SearchInternally(paKey, tempNode.LeftChild);
            if (result == Compare.Equal)
                return tempNode;
            return SearchInternally(paKey, tempNode.RightChild);
        }

        #endregion

        #region Insert methods

        public virtual bool Insert(TKey paKey, TData paData)
        {
            var result = Insert(new BinaryTreeNode<TKey, TData>
            {
                Key = paKey,
                Data = paData
            });
            return result != null;
        }

        protected virtual ITreeNode<TKey, TData> Insert(ITreeNode<TKey, TData> paTreeNode)
        {
            var result = false;
            if (Root == null)
            {
                Root = paTreeNode;
                result = true;
            }
            else
            {
                result = Root.Insert(paTreeNode, Comparator);
            }
            if (result)
                Count++;
            return result ? paTreeNode : null;
        }

        #endregion

        #region Delete methods

        public virtual TData Delete(TKey paKey)
        {
            var searchResult = this.SearchInternally(paKey, Root);
            var result = Delete(searchResult);
            if (result != null)
                return result.Data;
            return default(TData);
        }

        protected virtual ITreeNode<TKey, TData> Delete(ITreeNode<TKey, TData> paTreeNode)
        {
            var node = paTreeNode;
            if (node != null)
            {
                var replaceRoot = node == Root;
                var possibleRoot = node.Remove();
                if (replaceRoot)
                    Root = possibleRoot;
                Count--;
                return node;
            }
            return null;
        }

        #endregion

        #region Enumeration methods

        public IEnumerator<TData> GetEnumerator()
        {
            var enumerator = new BinaryPreOrderEnumerator<TKey, TData>(this);
            return enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            var output = string.Empty;
            InOrder(Root, ref output, 0, "R");
            if (output.Any())
                output = output.Remove(0, 2);
            return output;
        }

        private void InOrder(ITreeNode<TKey, TData> paParent, ref string paOutput, int paLevel, string paAddSide)
        {
            if (paParent != null)
            {
                paOutput += $", [{paLevel}]{paAddSide}-{paParent.Key} P:{(paParent.Parent ?? new BinaryTreeNode<TKey, TData>()).Key}";
                if (paParent.LeftChild != null)
                    InOrder(paParent.LeftChild, ref paOutput, (paLevel + 1), "L");
                if (paParent.RightChild != null)
                    InOrder(paParent.RightChild, ref paOutput, (paLevel + 1), "P");
            }
        }

        #endregion

    }

    public class BinaryTree<TData> : BinaryTree<TData, TData> where TData : ICompare<TData>
    {
        public BinaryTree() : base(null)
        {
        }

        public bool Insert(TData paData)
        {
            SetComparator(paData);
            return base.Insert(paData, paData);
        }

        private void SetComparator(TData paData)
        {
            if (Comparator == null)
                base.Comparator = new EnumeratedComparer<TData>(paData as ICompare<TData>);
        }
    }
}

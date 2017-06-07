using System.Collections.Generic;
using Toolkit.NET.DataStructures.Threes.Binary;
using Toolkit.NET.DataStructures.Threes.Node;

namespace Toolkit.NET.DataStructures.Threes
{
    public class AvlBinaryTree<TKey, TData> : BinaryTree<TKey, TData>
    {
        public AvlBinaryTree(IComparer<TKey> paComparable) : base(paComparable)
        {
        }

        public override bool Insert(TKey paKey, TData paData)
        {
            var inserted = base.Insert(new BinaryTreeNode<TKey, TData>
            {
                Key = paKey,
                Data = paData
            });
            if(inserted != null)
                ReorderTreeAfterInsert(inserted);

            return inserted != null;
        }

        private void ReorderTreeAfterInsert(ITreeNode<TKey, TData> paLastAddedTreeNode)
        {
            
        }

        private void ReorderTreeAfterDelete(ITreeNode<TKey, TData> paParentOfLastRemovedTreeNode)
        {

        }

        public override TData Delete(TKey paKey)
        {
            var searchResult = base.SearchInternally(paKey, Root);
            var result = base.Delete(searchResult);
            ReorderTreeAfterDelete(result);
            if (result != null)
                return result.Data;
            return default(TData);
        }
    }
}

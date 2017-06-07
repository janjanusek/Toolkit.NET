using System.Collections;
using System.Collections.Generic;
using Toolkit.NET.DataStructures.Threes.Node;

namespace Toolkit.NET.DataStructures.Threes.Binary
{
    public class BinaryPreOrderEnumerator<TKey, TData> : IEnumerator<TData>
    {
        public ITree<TKey, TData> Backup { get; private set; }
        public bool First { get; set; }
        public ITreeNode<TKey, TData> CurrentHelper { get; private set; }
        public int Count { get; set; }
        public TData Current => CurrentHelper.Data;

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public BinaryPreOrderEnumerator(BinaryTree<TKey, TData> paThree)
        {
            Backup = paThree;
            Reset();
        }

        public void Dispose()
        {
            CurrentHelper = null;
        }

        public bool MoveNext()
        {
            if (CurrentHelper == null)
                return false;

            if (First)
                First = !First;
            else if (Count == Backup.Count)
                return false;
            else
            {
                if (CanGoLeft)
                    CurrentHelper = CurrentHelper?.LeftChild;
                else if (CanGoRight)
                    CurrentHelper = CurrentHelper?.RightChild;
                else
                    _mustGoUp = true;

                if (_mustGoUp)
                {
                    bool repeat = true;
                    ITreeNode<TKey, TData> parent = CurrentHelper?.Parent;
                    while (repeat)
                    {
                        if (parent?.RightChild != null)
                        {
                            repeat = false;
                            CurrentHelper = parent?.RightChild;
                            _mustGoUp = !_mustGoUp;
                        }
                        else if (parent == null)
                        {
                            repeat = false;
                        }
                        else
                        {
                            parent = parent.Parent;
                        }
                    }
                }
            }
            if (CurrentHelper != null)
                Count++;
            return CurrentHelper != null;
        }

        private bool CanGoLeft => CurrentHelper?.LeftChild != null;
        private bool CanGoRight => CurrentHelper?.RightChild != null;
        private bool _mustGoUp;

        public void Reset()
        {
            CurrentHelper = Backup.Root;
            Count = 0;
            First = true;
        }
    }
}

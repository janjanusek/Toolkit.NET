using System;

namespace Toolkit.NET.DllMemoryManagement
{
    internal class IntPtrWrapper : IPointerAllocation
    {
        public bool IsAllocated => true;
        public IntPtr Pointer { get; private set; }

        public IntPtrWrapper(IntPtr paIntPtr)
        {
            Pointer = paIntPtr;
        }

        public void Allocate()
        {

        }

        public void Free()
        {

        }
    }
}
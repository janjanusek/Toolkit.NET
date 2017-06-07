using System;

namespace Toolkit.NET.DllMemoryManagement
{
    public interface IPointerAllocation
    {
        bool IsAllocated { get; }
        IntPtr Pointer { get; }
        void Allocate();
        void Free();
    }
}
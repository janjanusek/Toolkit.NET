using System;
using System.Runtime.InteropServices;

namespace Toolkit.NET.DllMemoryManagement
{
    public class ValuePointer<TData> : IPointerAllocation where TData : struct, IComparable, IFormattable, IConvertible,
        IComparable<TData>, IEquatable<TData>
    {
        public bool IsAllocated => _pointerHandle.IsAllocated;
        private readonly TData _value;
        private IntPtr _pointer;
        private GCHandle _pointerHandle;
        public IntPtr Pointer => _pointerHandle.IsAllocated ? _pointer : IntPtr.Zero;

        public ValuePointer(ref TData paValue, bool paAllocate = false)
        {
            _value = paValue;
            if (paAllocate)
                Allocate();
        }

        ~ValuePointer()
        {
            Free();
        }

        public void Allocate()
        {
            if (_pointerHandle.IsAllocated)
                return;
            _pointerHandle = GCHandle.Alloc(_value, GCHandleType.Pinned);
            _pointer = _pointerHandle.AddrOfPinnedObject();
        }

        public void Free()
        {
            if (!_pointerHandle.IsAllocated)
                return;
            _pointerHandle.Free();
        }

    }

    public class DynamicValuePointer : IPointerAllocation
    {
        public bool IsAllocated => _pointerHandle.IsAllocated;
        private readonly dynamic _value;
        private IntPtr _pointer;
        private GCHandle _pointerHandle;
        public IntPtr Pointer => _pointerHandle.IsAllocated ? _pointer : IntPtr.Zero;

        public DynamicValuePointer(ref dynamic paValue, bool paAllocate = false)
        {
            _value = paValue;
            if (paAllocate)
                Allocate();
        }

        ~DynamicValuePointer()
        {
            Free();
        }

        public void Allocate()
        {
            if (_pointerHandle.IsAllocated)
                return;
            _pointerHandle = GCHandle.Alloc(_value, GCHandleType.Pinned);
            _pointer = _pointerHandle.AddrOfPinnedObject();
        }

        public void Free()
        {
            if (!_pointerHandle.IsAllocated)
                return;
            _pointerHandle.Free();
        }

    }
}too
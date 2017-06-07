using System;
using System.Runtime.InteropServices;

namespace Toolkit.NET.DllMemoryManagement
{
    public class JaggedPointer<TData> : IPointerAllocation
    {
        public IntPtr Pointer { get; private set; }

        public bool IsAllocated => _pointerHandle.IsAllocated;

        private readonly TData[][] _array;
        public TData[][] Array => _array;

        private GCHandle _pointerHandle;
        private GCHandle[] _arrayGcHandles;
        private IntPtr[] _arrayPointers;

        public JaggedPointer(ref TData[][] paJaggedArray)
        {
            _array = paJaggedArray;
        }

        ~JaggedPointer()
        {
            Free();
        }

        public void Allocate()
        {
            if (IsAllocated)
                return;
            _arrayGcHandles = new GCHandle[_array.Length];
            _arrayPointers = new IntPtr[_array.Length];
            for (int i = 0; i < _arrayGcHandles.Length; i++)
            {
                _arrayGcHandles[i] = GCHandle.Alloc(_array[i], GCHandleType.Pinned);
                _arrayPointers[i] = _arrayGcHandles[i].AddrOfPinnedObject();
            }
            _pointerHandle = GCHandle.Alloc(_arrayPointers, GCHandleType.Pinned);
            Pointer = _pointerHandle.AddrOfPinnedObject();
        }

        public void Free()
        {
            if (!IsAllocated)
                return;
            for (int i = 0; i < _arrayGcHandles.Length; i++)
            {
                if (_arrayGcHandles[i].IsAllocated)
                    _arrayGcHandles[i].Free();
            }
            _pointerHandle.Free();
            _arrayGcHandles = null;
            _arrayPointers = null;
        }
    }

    public class DynamicJaggedPointer : IPointerAllocation
    {
        public IntPtr Pointer { get; private set; }

        public bool IsAllocated => _pointerHandle.IsAllocated;

        private readonly dynamic[][] _array;
        public dynamic[][] Array => _array;

        private GCHandle _pointerHandle;
        private GCHandle[] _arrayGcHandles;
        private IntPtr[] _arrayPointers;

        public DynamicJaggedPointer(ref dynamic[][] paJaggedArray)
        {
            _array = paJaggedArray;
        }

        ~DynamicJaggedPointer()
        {
            Free();
        }

        public void Allocate()
        {
            if (IsAllocated)
                return;
            _arrayGcHandles = new GCHandle[_array.Length];
            _arrayPointers = new IntPtr[_array.Length];
            for (int i = 0; i < _arrayGcHandles.Length; i++)
            {
                _arrayGcHandles[i] = GCHandle.Alloc(_array[i], GCHandleType.Pinned);
                _arrayPointers[i] = _arrayGcHandles[i].AddrOfPinnedObject();
            }
            _pointerHandle = GCHandle.Alloc(_arrayPointers, GCHandleType.Pinned);
            Pointer = _pointerHandle.AddrOfPinnedObject();
        }

        public void Free()
        {
            if (!IsAllocated)
                return;
            for (int i = 0; i < _arrayGcHandles.Length; i++)
            {
                if (_arrayGcHandles[i].IsAllocated)
                    _arrayGcHandles[i].Free();
            }
            _pointerHandle.Free();
            _arrayGcHandles = null;
            _arrayPointers = null;
        }
    }
}
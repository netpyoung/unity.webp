
using System;
using System.Runtime.InteropServices;

#pragma warning disable 1591

namespace WebP.NativeWrapper.Dec
{
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct WebPYUVABuffer
    {

        /// uint8_t*
        public IntPtr y;

        /// uint8_t*
        public IntPtr u;

        /// uint8_t*
        public IntPtr v;

        /// uint8_t*
        public IntPtr a;

        /// int
        public int y_stride;

        /// int
        public int u_stride;

        /// int
        public int v_stride;

        /// int
        public int a_stride;

        /// size_t->unsigned int
        public UIntPtr y_size;

        /// size_t->unsigned int
        public UIntPtr u_size;

        /// size_t->unsigned int
        public UIntPtr v_size;

        /// size_t->unsigned int
        public UIntPtr a_size;
    }
}

#pragma warning restore 1591

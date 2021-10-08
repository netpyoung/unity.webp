
using System;
using System.Runtime.InteropServices;

#pragma warning disable 1591

namespace WebP.NativeWrapper.Dec
{
    //------------------------------------------------------------------------------
    // WebPDecBuffer: Generic structure for describing the output sample buffer.

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct WebPRGBABuffer
    {

        /// uint8_t*
        public IntPtr rgba;

        /// int
        public int stride;

        /// size_t->unsigned int
        public UIntPtr size;
    }
}

#pragma warning restore 1591

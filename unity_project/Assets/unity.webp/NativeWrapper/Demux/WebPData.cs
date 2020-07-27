using System;
using System.Runtime.InteropServices;

#pragma warning disable 1591

namespace WebP.NativeWrapper.Demux
{
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct WebPData
    {
        public IntPtr bytes;
        public UIntPtr size;
    };
}
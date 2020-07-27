using System.Runtime.InteropServices;

#pragma warning disable 1591

namespace WebP.NativeWrapper.Demux
{
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct WebPAnimInfo
    {
        public int canvas_width;
        public int canvas_height;
        public int loop_count;
        public int bgcolor;
        public int frame_count;
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
        public uint[] pad;       // Padding for later use.
    };
}
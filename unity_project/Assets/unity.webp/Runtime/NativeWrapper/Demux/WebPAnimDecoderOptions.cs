using System.Runtime.InteropServices;
using WebP.NativeWrapper.Dec;

#pragma warning disable 1591

namespace WebP.NativeWrapper.Demux
{
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct WebPAnimDecoderOptions
    {
        // Output colorspace. Only the following modes are supported:
        // MODE_RGBA, MODE_BGRA, MODE_rgbA and MODE_bgrA.
        public WEBP_CSP_MODE color_mode;
        public int use_threads;           // If true, use multi-threaded decoding.
        /// uint32_t[7]
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.U4)]
        public uint[] padding;       // Padding for later use.
    };
}
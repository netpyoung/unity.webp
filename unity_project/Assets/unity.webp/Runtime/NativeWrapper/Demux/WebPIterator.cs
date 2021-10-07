using System;
using System.Runtime.InteropServices;

namespace WebP.NativeWrapper.Demux
{
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct WebPIterator
    {
        public int frame_num;
        public int num_frames; // equivalent to WEBP_FF_FRAME_COUNT.
        public int x_offset; // offset relative to the canvas.
        public int y_offset;
        public int width; // dimensions of this frame.
        public int height;
        public int duration; // display duration in milliseconds.
        public WebPMuxAnimDispose dispose_method; // dispose method for the frame.
        public int complete; // true if 'fragment' contains a full frame. partial images may still be decoded with the WebP incremental decoder.
        public WebPData fragment; // The frame given by 'frame_num'. Note for historical reasons this is called a fragment.
        public int has_alpha; // True if the frame contains transparency.
        public WebPMuxAnimBlend blend_method; // Blend operation for the frame.
        // uint32_t
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.U4)]
        public uint[] pad; // padding for later use.
        // void*
        public IntPtr private_; // for internal use only.
    }

}
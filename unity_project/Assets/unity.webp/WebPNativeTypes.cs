using System;
using System.Runtime.InteropServices;

namespace WebP
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

    // Dispose method (animation only). Indicates how the area used by the current
    // frame is to be treated before rendering the next frame on the canvas.
    public enum WebPMuxAnimDispose
    {
        WEBP_MUX_DISPOSE_NONE, // Do not dispose.
        WEBP_MUX_DISPOSE_BACKGROUND // Dispose to background color.
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct WebPData
    {
        // uint8_t*
        public IntPtr bytes;
        // size_t
        public UIntPtr size;
    }

    // Blend operation (animation only). Indicates how transparent pixels of the
    // current frame are blended with those of the previous canvas.
    public enum WebPMuxAnimBlend
    {
        WEBP_MUX_BLEND, // Blend.
        WEBP_MUX_NO_BLEND // Do not blend.
    }

}
using System;
using System.Runtime.InteropServices;
using WebP.NativeWrapper.Dec;

namespace WebP.NativeWrapper.Demux
{
    [StructLayoutAttribute(LayoutKind.Sequential, Pack = 1)]
    public struct WebPAnimDecoder
    {
        public IntPtr demux_;             // Demuxer created from given WebP bitstream.
        public WebPDecoderConfig config_;       // Decoder config.
                                                // Note: we use a pointer to a function blending multiple pixels at a time to
                                                // allow possible inlining of per-pixel blending function.
        public IntPtr blend_func_;        // Pointer to the chose blend row function.
        public WebPAnimInfo info_;              // Global info about the animation.
        public IntPtr curr_frame_;            // Current canvas (not disposed).
        public IntPtr prev_frame_disposed_;   // Previous canvas (properly disposed).
        public int prev_frame_timestamp_;       // Previous frame timestamp (milliseconds).
        public WebPIterator prev_iter_;         // Iterator object for previous frame.
        public int prev_frame_was_keyframe_;    // True if previous frame was a keyframe.
        public int next_frame_;                 // Index of the next frame to be decoded
                                                // (starting from 1).
    };

}
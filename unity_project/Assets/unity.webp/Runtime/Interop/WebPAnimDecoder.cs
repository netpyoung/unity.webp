using System;

namespace unity.libwebp.Interop
{
    public partial struct WebPAnimDecoder
    {
    }

    public unsafe partial struct WebPAnimDecoder
    {
        public WebPDemuxer* demux_;

        public WebPDecoderConfig config_;

        [NativeTypeName("BlendRowFunc")]
        public IntPtr blend_func_;

        public WebPAnimInfo info_;

        [NativeTypeName("uint8_t *")]
        public byte* curr_frame_;

        [NativeTypeName("uint8_t *")]
        public byte* prev_frame_disposed_;

        public int prev_frame_timestamp_;

        public WebPIterator prev_iter_;

        public int prev_frame_was_keyframe_;

        public int next_frame_;
    }
}

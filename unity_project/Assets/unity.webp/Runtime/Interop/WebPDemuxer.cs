using System;

namespace unity.libwebp.Interop
{
    public partial struct WebPDemuxer
    {
    }

    public unsafe partial struct WebPDemuxer
    {
        public MemBuffer mem_;

        public WebPDemuxState state_;

        public int is_ext_format_;

        [NativeTypeName("uint32_t")]
        public uint feature_flags_;

        public int canvas_width_;

        public int canvas_height_;

        public int loop_count_;

        [NativeTypeName("uint32_t")]
        public uint bgcolor_;

        public int num_frames_;

        public Frame* frames_;

        public Frame** frames_tail_;

        public Chunk* chunks_;

        public Chunk** chunks_tail_;
    }
}

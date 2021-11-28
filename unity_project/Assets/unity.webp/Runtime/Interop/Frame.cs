using System;

namespace unity.libwebp.Interop
{
    public unsafe partial struct Frame
    {
        public int x_offset_;

        public int y_offset_;

        public int width_;

        public int height_;

        public int has_alpha_;

        public int duration_;

        public WebPMuxAnimDispose dispose_method_;

        public WebPMuxAnimBlend blend_method_;

        public int frame_num_;

        public int complete_;

        [NativeTypeName("ChunkData [2]")]
        public _img_components__e__FixedBuffer img_components_;

        [NativeTypeName("struct Frame *")]
        public Frame* next_;

        public partial struct _img_components__e__FixedBuffer
        {
            public ChunkData e0;
            public ChunkData e1;

            public unsafe ref ChunkData this[int index]
            {
                get
                {
                    fixed (ChunkData* pThis = &e0)
                    {
                        return ref pThis[index];
                    }
                }
            }
        }
    }
}

namespace unity.libwebp.Interop
{
    public unsafe partial struct WebPAnimInfo
    {
        [NativeTypeName("uint32_t")]
        public uint canvas_width;

        [NativeTypeName("uint32_t")]
        public uint canvas_height;

        [NativeTypeName("uint32_t")]
        public uint loop_count;

        [NativeTypeName("uint32_t")]
        public uint bgcolor;

        [NativeTypeName("uint32_t")]
        public uint frame_count;

        [NativeTypeName("uint32_t [4]")]
        public fixed uint pad[4];
    }
}

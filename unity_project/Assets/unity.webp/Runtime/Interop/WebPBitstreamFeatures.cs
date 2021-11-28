namespace unity.libwebp.Interop
{
    public unsafe partial struct WebPBitstreamFeatures
    {
        public int width;

        public int height;

        public int has_alpha;

        public int has_animation;

        public int format;

        [NativeTypeName("uint32_t [5]")]
        public fixed uint pad[5];
    }
}

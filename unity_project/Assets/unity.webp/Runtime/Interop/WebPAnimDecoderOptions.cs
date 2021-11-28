namespace unity.libwebp.Interop
{
    public unsafe partial struct WebPAnimDecoderOptions
    {
        public WEBP_CSP_MODE color_mode;

        public int use_threads;

        [NativeTypeName("uint32_t [7]")]
        public fixed uint padding[7];
    }
}

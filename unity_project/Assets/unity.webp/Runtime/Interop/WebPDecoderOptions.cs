namespace unity.libwebp.Interop
{
    public unsafe partial struct WebPDecoderOptions
    {
        public int bypass_filtering;

        public int no_fancy_upsampling;

        public int use_cropping;

        public int crop_left;

        public int crop_top;

        public int crop_width;

        public int crop_height;

        public int use_scaling;

        public int scaled_width;

        public int scaled_height;

        public int use_threads;

        public int dithering_strength;

        public int flip;

        public int alpha_dithering_strength;

        [NativeTypeName("uint32_t [5]")]
        public fixed uint pad[5];
    }
}

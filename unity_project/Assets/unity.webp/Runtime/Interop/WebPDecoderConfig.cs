using System;

namespace unity.libwebp.Interop
{
    public partial struct WebPDecoderConfig
    {
        public WebPBitstreamFeatures input;

        public WebPDecBuffer output;

        public WebPDecoderOptions options;
    }
}

using System.Runtime.InteropServices;

#pragma warning disable 1591

namespace WebP.NativeWrapper.Dec
{
    [StructLayoutAttribute(LayoutKind.Sequential, Pack = 1)]
    public struct WebPDecoderConfig
    {
        public WebPBitstreamFeatures input;
        public WebPDecBuffer output;
        public WebPDecoderOptions options;
    }
}

#pragma warning restore 1591

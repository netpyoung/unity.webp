using unity.libwebp;

namespace WebP
{
    public class Info
    {
        public static string GetDecoderVersion()
        {
            uint v = (uint)NativeLibwebp.WebPGetDecoderVersion();
            uint revision = v % 256;
            uint minor = (v >> 8) % 256;
            uint major = (v >> 16) % 256;
            return $"{major}.{minor}.{revision}";
        }

        public static string GetEncoderVersion()
        {
            uint v = (uint)NativeLibwebp.WebPGetEncoderVersion();
            uint revision = v % 256;
            uint minor = (v >> 8) % 256;
            uint major = (v >> 16) % 256;
            return $"{major}.{minor}.{revision}";
        }
    }
}

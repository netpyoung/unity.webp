namespace unity.libwebp.Interop
{
    [NativeTypeName("unsigned int")]
    public enum WebPEncCSP : uint
    {
        WEBP_YUV420 = 0,
        WEBP_YUV420A = 4,
        WEBP_CSP_UV_MASK = 3,
        WEBP_CSP_ALPHA_BIT = 4,
    }
}

using System;

namespace unity.libwebp.Interop
{
    public unsafe partial struct WebPRGBABuffer
    {
        [NativeTypeName("uint8_t *")]
        public byte* rgba;

        public int stride;

        [NativeTypeName("size_t")]
        public UIntPtr size;
    }
}

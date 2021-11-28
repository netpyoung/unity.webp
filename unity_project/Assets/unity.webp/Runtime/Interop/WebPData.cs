using System;

namespace unity.libwebp.Interop
{
    public unsafe partial struct WebPData
    {
        [NativeTypeName("const uint8_t *")]
        public byte* bytes;

        [NativeTypeName("size_t")]
        public UIntPtr size;
    }
}

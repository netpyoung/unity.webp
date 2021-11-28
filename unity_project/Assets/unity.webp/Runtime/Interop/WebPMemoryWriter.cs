using System;

namespace unity.libwebp.Interop
{
    public unsafe partial struct WebPMemoryWriter
    {
        [NativeTypeName("uint8_t *")]
        public byte* mem;

        [NativeTypeName("size_t")]
        public UIntPtr size;

        [NativeTypeName("size_t")]
        public UIntPtr max_size;

        [NativeTypeName("uint32_t [1]")]
        public fixed uint pad[1];
    }
}

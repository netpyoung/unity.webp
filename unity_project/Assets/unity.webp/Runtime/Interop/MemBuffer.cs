using System;

namespace unity.libwebp.Interop
{
    public unsafe partial struct MemBuffer
    {
        [NativeTypeName("size_t")]
        public UIntPtr start_;

        [NativeTypeName("size_t")]
        public UIntPtr end_;

        [NativeTypeName("size_t")]
        public UIntPtr riff_end_;

        [NativeTypeName("size_t")]
        public UIntPtr buf_size_;

        [NativeTypeName("const uint8_t *")]
        public byte* buf_;
    }
}

using System;

namespace unity.libwebp.Interop
{
    public partial struct ChunkData
    {
        [NativeTypeName("size_t")]
        public UIntPtr offset_;

        [NativeTypeName("size_t")]
        public UIntPtr size_;
    }
}

using System;

namespace unity.libwebp.Interop
{
    public unsafe partial struct Chunk
    {
        public ChunkData data_;

        [NativeTypeName("struct Chunk *")]
        public Chunk* next_;
    }
}

using System;

namespace unity.libwebp.Interop
{
    public unsafe partial struct WebPChunkIterator
    {
        public int chunk_num;

        public int num_chunks;

        public WebPData chunk;

        [NativeTypeName("uint32_t [6]")]
        public fixed uint pad[6];

        public void* private_;
    }
}

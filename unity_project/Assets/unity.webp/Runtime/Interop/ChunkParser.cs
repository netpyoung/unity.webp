using System;

namespace unity.libwebp.Interop
{
    public unsafe partial struct ChunkParser
    {
        [NativeTypeName("uint8_t [4]")]
        public fixed byte id[4];

        [NativeTypeName("ParseStatus (*)(WebPDemuxer *const)")]
        public IntPtr parse;

        [NativeTypeName("int (*)(const WebPDemuxer *const)")]
        public IntPtr valid;
    }
}

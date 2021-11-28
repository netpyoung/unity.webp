using System;

namespace unity.libwebp.Interop
{
    public unsafe partial struct WebPYUVABuffer
    {
        [NativeTypeName("uint8_t *")]
        public byte* y;

        [NativeTypeName("uint8_t *")]
        public byte* u;

        [NativeTypeName("uint8_t *")]
        public byte* v;

        [NativeTypeName("uint8_t *")]
        public byte* a;

        public int y_stride;

        public int u_stride;

        public int v_stride;

        public int a_stride;

        [NativeTypeName("size_t")]
        public UIntPtr y_size;

        [NativeTypeName("size_t")]
        public UIntPtr u_size;

        [NativeTypeName("size_t")]
        public UIntPtr v_size;

        [NativeTypeName("size_t")]
        public UIntPtr a_size;
    }
}

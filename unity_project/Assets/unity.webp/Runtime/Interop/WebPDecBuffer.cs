using System;
using System.Runtime.InteropServices;

namespace unity.libwebp.Interop
{
    public unsafe partial struct WebPDecBuffer
    {
        public WEBP_CSP_MODE colorspace;

        public int width;

        public int height;

        public int is_external_memory;

        [NativeTypeName("union (anonymous union at libwebp/src/webp/decode.h:207:3)")]
        public _u_e__Union u;

        [NativeTypeName("uint32_t [4]")]
        public fixed uint pad[4];

        [NativeTypeName("uint8_t *")]
        public byte* private_memory;

        [StructLayout(LayoutKind.Explicit)]
        public partial struct _u_e__Union
        {
            [FieldOffset(0)]
            public WebPRGBABuffer RGBA;

            [FieldOffset(0)]
            public WebPYUVABuffer YUVA;
        }
    }
}

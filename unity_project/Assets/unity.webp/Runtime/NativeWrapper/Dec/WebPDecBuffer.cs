
using System;
using System.Runtime.InteropServices;

#pragma warning disable 1591

namespace WebP.NativeWrapper.Dec
{
    [StructLayoutAttribute(LayoutKind.Sequential, Pack = 1)]
    public struct WebPDecBuffer
    {

        /// WEBP_CSP_MODE->Anonymous_cb136f5b_1d5d_49a0_aca4_656a79e9d159
        public WEBP_CSP_MODE colorspace;

        /// int
        public int width;

        /// int
        public int height;

        /// int
        public int is_external_memory;

        /// Anonymous_690ed5ec_4c3d_40c6_9bd0_0747b5a28b54
        public AnonymousWebpBuffer u;

        /// uint32_t[4]
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
        public uint[] pad;

        /// uint8_t*
        public IntPtr private_memory;
    }



    [StructLayoutAttribute(LayoutKind.Explicit)]
    public struct AnonymousWebpBuffer
    {

        /// WebPRGBABuffer->Anonymous_47cdec86_3c1a_4b39_ab93_76bc7499076a
        [FieldOffsetAttribute(0)]
        public WebPRGBABuffer RGBA;

        /// WebPYUVABuffer->Anonymous_70de6e8e_c3ae_4506_bef0_c17f17a7e678
        [FieldOffsetAttribute(0)]
        public WebPYUVABuffer YUVA;
    }
}

#pragma warning restore 1591

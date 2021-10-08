
using System;
using System.Runtime.InteropServices;

#pragma warning disable 1591

namespace WebP.NativeWrapper.Dec
{
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct WebPPicture
    {

        /// int
        public int use_argb;

        /// WebPEncCSP->Anonymous_84ce7065_fe91_48b4_93d8_1f0e84319dba
        public WebPEncCSP colorspace;

        /// int
        public int width;

        /// int
        public int height;

        /// uint8_t*
        public IntPtr y;

        /// uint8_t*
        public IntPtr u;

        /// uint8_t*
        public IntPtr v;

        /// int
        public int y_stride;

        /// int
        public int uv_stride;

        /// uint8_t*
        public IntPtr a;

        /// int
        public int a_stride;

        /// uint32_t[2]
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.U4)]
        public uint[] pad1;

        /// uint32_t*
        public IntPtr argb;

        /// int
        public int argb_stride;

        /// uint32_t[3]
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.U4)]
        public uint[] pad2;

        /// WebPWriterFunction
        public WebPWriterFunction writer;

        /// void*
        public IntPtr custom_ptr;

        /// int
        public int extra_info_type;

        /// uint8_t*
        public IntPtr extra_info;

        /// WebPAuxStats*
        public IntPtr stats;

        /// WebPEncodingError->Anonymous_8b714d63_f91b_46af_b0d0_667c703ed356
        public WebPEncodingError error_code;

        /// WebPProgressHook
        public WebPProgressHook progress_hook;

        /// void*
        public IntPtr user_data;

        /// uint32_t[3]
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.U4)]
        public uint[] pad3;

        /// uint8_t*
        public IntPtr u0;

        /// uint8_t*
        public IntPtr v0;

        /// int
        public int uv0_stride;

        /// uint32_t[7]
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.U4)]
        public uint[] pad4;

        /// void*
        public IntPtr memory_;

        /// void*
        public IntPtr memory_argb_;

        /// void*[2]
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.SysUInt)]
        public IntPtr[] pad5;
    }
}

#pragma warning restore 1591

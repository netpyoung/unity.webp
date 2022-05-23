using System;

namespace unity.libwebp.Interop
{
    public unsafe partial struct WebPPicture
    {
        public int use_argb;

        public WebPEncCSP colorspace;

        public int width;

        public int height;

        [NativeTypeName("uint8_t *")]
        public byte* y;

        [NativeTypeName("uint8_t *")]
        public byte* u;

        [NativeTypeName("uint8_t *")]
        public byte* v;

        public int y_stride;

        public int uv_stride;

        [NativeTypeName("uint8_t *")]
        public byte* a;

        public int a_stride;

        [NativeTypeName("uint32_t [2]")]
        public fixed uint pad1[2];

        [NativeTypeName("uint32_t *")]
        public uint* argb;

        public int argb_stride;

        [NativeTypeName("uint32_t [3]")]
        public fixed uint pad2[3];

        [NativeTypeName("WebPWriterFunction")]
        public IntPtr writer;

        public void* custom_ptr;

        public int extra_info_type;

        [NativeTypeName("uint8_t *")]
        public byte* extra_info;

        public WebPAuxStats* stats;

        public WebPEncodingError error_code;

        [NativeTypeName("WebPProgressHook")]
        public IntPtr progress_hook;

        public void* user_data;

        [NativeTypeName("uint32_t [3]")]
        public fixed uint pad3[3];

        [NativeTypeName("uint8_t *")]
        public byte* pad4;

        [NativeTypeName("uint8_t *")]
        public byte* pad5;

        [NativeTypeName("uint32_t [8]")]
        public fixed uint pad6[8];

        public void* memory_;

        public void* memory_argb_;

        [NativeTypeName("void *[2]")]
        public _pad7_e__FixedBuffer pad7;

        public unsafe partial struct _pad7_e__FixedBuffer
        {
            public void* e0;
            public void* e1;

            // NOTE(pyoung): Comment out to avoid compile error
            //   - ref: https://github.com/netpyoung/unity.webp/issues/44#issuecomment-1134680004
            //public ref void* this[int index]
            //{
            //    get
            //    {
            //        fixed (void** pThis = &e0)
            //        {
            //            return ref pThis[index];
            //        }
            //    }
            //}
        }
    }

    public partial struct WebPPicture
    {
    }
}

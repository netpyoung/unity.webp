using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unity.libwebp.Interop;

namespace unity.libwebp
{

    public static unsafe partial class NativeLibwebpdemux
    {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        const string DLL_NAME = "libwebpdemux";
#elif UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
        const string DLL_NAME = "webpdemux";
#elif UNITY_ANDROID
		const string DLL_NAME = "webpdemux";
#elif UNITY_IOS
		const string DLL_NAME = "__Internal";
#elif UNITY_WEBGL
		const string DLL_NAME = "__Internal";
#endif

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPGetDemuxVersion();

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern WebPDemuxer* WebPDemuxInternal([NativeTypeName("const WebPData *")] WebPData* param0, int param1, WebPDemuxState* param2, int param3);

        public static WebPDemuxer* WebPDemux([NativeTypeName("const WebPData *")] WebPData* data)
        {
            return WebPDemuxInternal(data, 0, null, 0x0107);
        }

        public static WebPDemuxer* WebPDemuxPartial([NativeTypeName("const WebPData *")] WebPData* data, WebPDemuxState* state)
        {
            return WebPDemuxInternal(data, 1, state, 0x0107);
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void WebPDemuxDelete(WebPDemuxer* dmux);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint WebPDemuxGetI([NativeTypeName("const WebPDemuxer *")] WebPDemuxer* dmux, WebPFormatFeature feature);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPDemuxGetFrame([NativeTypeName("const WebPDemuxer *")] WebPDemuxer* dmux, int frame_number, WebPIterator* iter);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPDemuxNextFrame(WebPIterator* iter);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPDemuxPrevFrame(WebPIterator* iter);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void WebPDemuxReleaseIterator(WebPIterator* iter);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPDemuxGetChunk([NativeTypeName("const WebPDemuxer *")] WebPDemuxer* dmux, [NativeTypeName("const char [4]")] sbyte* fourcc, int chunk_number, WebPChunkIterator* iter);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPDemuxNextChunk(WebPChunkIterator* iter);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPDemuxPrevChunk(WebPChunkIterator* iter);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void WebPDemuxReleaseChunkIterator(WebPChunkIterator* iter);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPAnimDecoderOptionsInitInternal(WebPAnimDecoderOptions* param0, int param1);

        public static int WebPAnimDecoderOptionsInit(WebPAnimDecoderOptions* dec_options)
        {
            return WebPAnimDecoderOptionsInitInternal(dec_options, 0x0107);
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern WebPAnimDecoder* WebPAnimDecoderNewInternal([NativeTypeName("const WebPData *")] WebPData* param0, [NativeTypeName("const WebPAnimDecoderOptions *")] WebPAnimDecoderOptions* param1, int param2);

        public static WebPAnimDecoder* WebPAnimDecoderNew([NativeTypeName("const WebPData *")] WebPData* webp_data, [NativeTypeName("const WebPAnimDecoderOptions *")] WebPAnimDecoderOptions* dec_options)
        {
            return WebPAnimDecoderNewInternal(webp_data, dec_options, 0x0107);
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPAnimDecoderGetInfo([NativeTypeName("const WebPAnimDecoder *")] WebPAnimDecoder* dec, WebPAnimInfo* info);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPAnimDecoderGetNext(WebPAnimDecoder* dec, [NativeTypeName("uint8_t **")] byte** buf, int* timestamp);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPAnimDecoderHasMoreFrames([NativeTypeName("const WebPAnimDecoder *")] WebPAnimDecoder* dec);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void WebPAnimDecoderReset(WebPAnimDecoder* dec);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const WebPDemuxer *")]
        public static extern WebPDemuxer* WebPAnimDecoderGetDemuxer([NativeTypeName("const WebPAnimDecoder *")] WebPAnimDecoder* dec);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void WebPAnimDecoderDelete(WebPAnimDecoder* dec);

        [NativeTypeName("#define WEBP_DEMUX_ABI_VERSION 0x0107")]
        public const int WEBP_DEMUX_ABI_VERSION = 0x0107;

        public static void WebPDataInit(WebPData* webp_data)
        {
            if (webp_data != null)
            {
                Unsafe.InitBlockUnaligned(webp_data, 0, (uint)sizeof(WebPData));
            }
        }

        public static void WebPDataClear(WebPData* webp_data)
        {
            if (webp_data != null)
            {
                WebPFree((void*)(webp_data->bytes));
                WebPDataInit(webp_data);
            }
        }

        public static int WebPDataCopy([NativeTypeName("const WebPData *")] WebPData* src, WebPData* dst)
        {
            if (src == null || dst == null)
            {
                return 0;
            }

            WebPDataInit(dst);
            if (src->bytes != null && (uint)src->size != 0)
            {
                dst->bytes = (byte*)(WebPMalloc(src->size));
                if (dst->bytes == null)
                {
                    return 0;
                }

                Unsafe.CopyBlockUnaligned((void*)(dst->bytes), src->bytes, (uint)src->size);
                dst->size = src->size;
            }

            return 1;
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void* WebPMalloc([NativeTypeName("size_t")] UIntPtr size);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void WebPFree(void* ptr);
    }
}

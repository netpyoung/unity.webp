using System;
using System.Runtime.InteropServices;

#pragma warning disable 1591

namespace WebP.NativeWrapper.Demux
{

    public class Demux
    {
        public const int WEBP_DEMUX_ABI_VERSION = 0x0107;
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        const string DLL_NAME = "libwebpdemux";
#elif UNITY_EDITOR || UNITY_STANDALONE_OSX
        const string DLL_NAME = "webpdemux";
#elif UNITY_ANDROID
		const string DLL_NAME = "webpdemux";
#elif UNITY_IOS
		const string DLL_NAME = "__Internal";
#endif

        // WEBP_EXTERN int WebPAnimDecoderOptionsInitInternal(WebPAnimDecoderOptions*, int);
        [DllImport(DLL_NAME, EntryPoint = "WebPAnimDecoderOptionsInitInternal")]
        public static extern void WebPAnimDecoderOptionsInitInternal(ref WebPAnimDecoderOptions data, int version);
        public static void WebPAnimDecoderOptionsInit(ref WebPAnimDecoderOptions data)
        {
            WebPAnimDecoderOptionsInitInternal(ref data, WEBP_DEMUX_ABI_VERSION);
        }

        // WEBP_EXTERN WebPAnimDecoder* WebPAnimDecoderNewInternal(const WebPData*, const WebPAnimDecoderOptions*, int);
        [DllImport(DLL_NAME, EntryPoint = "WebPAnimDecoderNewInternal")]
        public static extern IntPtr WebPAnimDecoderNewInternal(ref WebPData data, ref WebPAnimDecoderOptions option, int version);
        public static IntPtr WebPAnimDecoderNew(ref WebPData data, ref WebPAnimDecoderOptions option)
        {
            return WebPAnimDecoderNewInternal(ref data, ref option, WEBP_DEMUX_ABI_VERSION);
        }

        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPAnimDecoderGetNext")]
        public static extern int WebPAnimDecoderGetNext(IntPtr dec, ref IntPtr p, ref int timestamp);

        //         void WebPDemuxReleaseIterator(WebPIterator* iter);
        [DllImport(DLL_NAME, EntryPoint = "WebPDemuxReleaseIterator")]
        public static extern void WebPDemuxReleaseIterator(ref WebPIterator dec);

        // WEBP_EXTERN void WebPDemuxReleaseChunkIterator(WebPChunkIterator* iter);
        [DllImport(DLL_NAME, EntryPoint = "WebPDemuxReleaseChunkIterator")]
        public static extern void WebPDemuxReleaseChunkIterator(IntPtr webpChunkIterator);

        // WEBP_EXTERN int WebPDemuxPrevFrame(WebPIterator* iter);
        [DllImport(DLL_NAME, EntryPoint = "WebPDemuxPrevFrame")]
        public static extern int WebPDemuxPrevFrame(IntPtr webpIterator);

        //         WEBP_EXTERN int WebPDemuxPrevChunk(WebPChunkIterator* iter);
        [DllImport(DLL_NAME, EntryPoint = "WebPDemuxPrevChunk")]
        public static extern int WebPDemuxPrevChunk(IntPtr webpChunkIterator);

        // WEBP_EXTERN int WebPDemuxNextFrame(WebPIterator* iter);
        [DllImport(DLL_NAME, EntryPoint = "WebPDemuxNextFrame")]
        public static extern int WebPDemuxNextFrame(IntPtr webpIterator);

        [DllImport(DLL_NAME, EntryPoint = "WebPDemuxNextFrame")]
        public static extern int WebPDemuxNextFrame(ref WebPIterator iter);

        // WEBP_EXTERN int WebPDemuxNextChunk(WebPChunkIterator* iter);
        [DllImport(DLL_NAME, EntryPoint = "WebPDemuxNextChunk")]
        public static extern int WebPDemuxNextChunk(IntPtr webpChunkIterator);

        // WEBP_EXTERN WebPDemuxer* WebPDemuxInternal(const WebPData*, int, WebPDemuxState*, int);
        [DllImport(DLL_NAME, EntryPoint = "WebPDemuxInternal")]
        public static extern IntPtr WebPDemuxInternal(IntPtr webpdata, int a, IntPtr state, int version);

        // WEBP_EXTERN uint32_t WebPDemuxGetI(const WebPDemuxer* dmux, WebPFormatFeature feature);
        [DllImport(DLL_NAME, EntryPoint = "WebPDemuxGetI")]
        public static extern uint WebPDemuxGetI(IntPtr demux, WebPFormatFeature feature);

        // WEBP_EXTERN int WebPDemuxGetFrame(const WebPDemuxer* dmux, int frame_number, WebPIterator* iter);
        [DllImport(DLL_NAME, EntryPoint = "WebPDemuxGetFrame")]
        public static extern int WebPDemuxGetFrame(IntPtr demux, int frame_number, ref WebPIterator iter);
        //public static extern int WebPDemuxGetFrame(IntPtr demux, int frame_number, IntPtr iter);

        // WEBP_EXTERN int WebPDemuxGetChunk(const WebPDemuxer* dmux, const char fourcc[4], int chunk_number, WebPChunkIterator* iter);
        [DllImport(DLL_NAME, EntryPoint = "WebPDemuxGetChunk")]
        public static extern int WebPDemuxGetChunk(IntPtr dmux, char[] fourcc, int chunk_number, IntPtr iter);

        // WEBP_EXTERN void WebPDemuxDelete(WebPDemuxer* dmux);
        [DllImport(DLL_NAME, EntryPoint = "WebPDemuxDelete")]
        public static extern void WebPDemuxDelete(IntPtr demux);

        // WEBP_EXTERN void WebPAnimDecoderReset(WebPAnimDecoder* dec);
        [DllImport(DLL_NAME, EntryPoint = "WebPAnimDecoderReset")]
        public static extern void WebPAnimDecoderReset(IntPtr dec);

        // WEBP_EXTERN int WebPAnimDecoderHasMoreFrames(const WebPAnimDecoder* dec);
        [DllImport(DLL_NAME, EntryPoint = "WebPAnimDecoderHasMoreFrames")]
        public static extern int WebPAnimDecoderHasMoreFrames(IntPtr dec);

        // WEBP_EXTERN int WebPAnimDecoderGetNext(WebPAnimDecoder* dec, uint8_t** buf, int* timestamp);
        [DllImport(DLL_NAME, EntryPoint = "WebPAnimDecoderGetNext")]
        public static extern int WebPAnimDecoderGetNext(IntPtr dec, IntPtr buf, ref int timestamp);

        // WEBP_EXTERN int WebPAnimDecoderGetInfo(const WebPAnimDecoder* dec, WebPAnimInfo* info);
        [DllImport(DLL_NAME, EntryPoint = "WebPAnimDecoderGetInfo")]
        public static extern int WebPAnimDecoderGetInfo(IntPtr dec, ref WebPAnimInfo info);

        // WEBP_EXTERN const WebPDemuxer* WebPAnimDecoderGetDemuxer(const WebPAnimDecoder* dec);
        [DllImport(DLL_NAME, EntryPoint = "WebPAnimDecoderGetDemuxer")]
        public static extern IntPtr WebPAnimDecoderGetDemuxer(IntPtr dec);

        //WEBP_EXTERN void WebPAnimDecoderDelete(WebPAnimDecoder* dec);
        [DllImport(DLL_NAME, EntryPoint = "WebPAnimDecoderDelete")]
        public static extern void WebPAnimDecoderDelete(IntPtr dec);
    }
}
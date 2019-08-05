using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#pragma warning disable 1591

namespace WebP.Extern
{
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct WebPData
    {
        public IntPtr bytes;
        public UIntPtr size;
    };

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct WebPAnimInfo
    {
        public int canvas_width;
        public int canvas_height;
        public int loop_count;
        public int bgcolor;
        public int frame_count;
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
        public uint[] pad;       // Padding for later use.
    };

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct WebPAnimDecoderOptions
    {
        // Output colorspace. Only the following modes are supported:
        // MODE_RGBA, MODE_BGRA, MODE_rgbA and MODE_bgrA.
        public WEBP_CSP_MODE color_mode;
        public int use_threads;           // If true, use multi-threaded decoding.
        /// uint32_t[7]
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.U4)]
        public uint[] padding;       // Padding for later use.
    };

    public class libwebpdemux
    {
        const int WEBP_DEMUX_ABI_VERSION = 0x0107;
#if UNITY_EDITOR
        const string DLL_NAME = "libwebpdemux";

#elif UNITY_ANDROID
		const string DLL_NAME = "webpdemux";
#elif UNITY_IOS
		const string DLL_NAME = "__Internal";
#endif
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPAnimDecoderOptionsInitInternal")]
        public static extern void WebPAnimDecoderOptionsInitInternal(ref WebPAnimDecoderOptions data, int version);
        public static void WebPAnimDecoderOptionsInit(ref WebPAnimDecoderOptions data)
        {
            WebPAnimDecoderOptionsInitInternal(ref data, WEBP_DEMUX_ABI_VERSION);
        }
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPAnimDecoderNewInternal")]
        public static extern IntPtr WebPAnimDecoderNewInternal(ref WebPData data, ref WebPAnimDecoderOptions option, int version);
        public static IntPtr WebPAnimDecoderNew(ref WebPData data, ref WebPAnimDecoderOptions option)
        {
            return WebPAnimDecoderNewInternal(ref data, ref option, WEBP_DEMUX_ABI_VERSION);
        }

        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPAnimDecoderDelete")]
        public static extern void WebPAnimDecoderDelete(IntPtr dec);

        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPAnimDecoderGetInfo")]
        public static extern int WebPAnimDecoderGetInfo(IntPtr dec, ref WebPAnimInfo info);


        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPAnimDecoderGetNext")]
        public static extern int WebPAnimDecoderGetNext(IntPtr dec, ref IntPtr p, ref int timestamp);
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPAnimDecoderReset")]
        public static extern void WebPAnimDecoderReset(IntPtr dec);

        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPAnimDecoderHasMoreFrames")]
        public static extern bool WebPAnimDecoderHasMoreFrames(IntPtr dec);


    }
}
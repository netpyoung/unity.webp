using System;
using System.Runtime.InteropServices;
using static unity.libwebp.Interop.WEBP_CSP_MODE;
using static unity.libwebp.Interop.WebPPreset;
using unity.libwebp.Interop;

namespace unity.libwebp
{
    public static unsafe partial class NativeLibwebp
    {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        const string DLL_NAME = "libwebp";
#elif UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
        const string DLL_NAME = "webp";
#elif UNITY_ANDROID
		const string DLL_NAME = "webp";
#elif UNITY_IOS
		const string DLL_NAME = "__Internal";
#elif UNITY_WEBGL
        const string DLL_NAME = "__Internal";
#endif
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPGetDecoderVersion();

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPGetInfo([NativeTypeName("const uint8_t *")] byte* data, [NativeTypeName("size_t")] UIntPtr data_size, int* width, int* height);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint8_t *")]
        public static extern byte* WebPDecodeRGBA([NativeTypeName("const uint8_t *")] byte* data, [NativeTypeName("size_t")] UIntPtr data_size, int* width, int* height);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint8_t *")]
        public static extern byte* WebPDecodeARGB([NativeTypeName("const uint8_t *")] byte* data, [NativeTypeName("size_t")] UIntPtr data_size, int* width, int* height);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint8_t *")]
        public static extern byte* WebPDecodeBGRA([NativeTypeName("const uint8_t *")] byte* data, [NativeTypeName("size_t")] UIntPtr data_size, int* width, int* height);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint8_t *")]
        public static extern byte* WebPDecodeRGB([NativeTypeName("const uint8_t *")] byte* data, [NativeTypeName("size_t")] UIntPtr data_size, int* width, int* height);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint8_t *")]
        public static extern byte* WebPDecodeBGR([NativeTypeName("const uint8_t *")] byte* data, [NativeTypeName("size_t")] UIntPtr data_size, int* width, int* height);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint8_t *")]
        public static extern byte* WebPDecodeYUV([NativeTypeName("const uint8_t *")] byte* data, [NativeTypeName("size_t")] UIntPtr data_size, int* width, int* height, [NativeTypeName("uint8_t **")] byte** u, [NativeTypeName("uint8_t **")] byte** v, int* stride, int* uv_stride);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint8_t *")]
        public static extern byte* WebPDecodeRGBAInto([NativeTypeName("const uint8_t *")] byte* data, [NativeTypeName("size_t")] UIntPtr data_size, [NativeTypeName("uint8_t *")] byte* output_buffer, [NativeTypeName("size_t")] UIntPtr output_buffer_size, int output_stride);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint8_t *")]
        public static extern byte* WebPDecodeARGBInto([NativeTypeName("const uint8_t *")] byte* data, [NativeTypeName("size_t")] UIntPtr data_size, [NativeTypeName("uint8_t *")] byte* output_buffer, [NativeTypeName("size_t")] UIntPtr output_buffer_size, int output_stride);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint8_t *")]
        public static extern byte* WebPDecodeBGRAInto([NativeTypeName("const uint8_t *")] byte* data, [NativeTypeName("size_t")] UIntPtr data_size, [NativeTypeName("uint8_t *")] byte* output_buffer, [NativeTypeName("size_t")] UIntPtr output_buffer_size, int output_stride);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint8_t *")]
        public static extern byte* WebPDecodeRGBInto([NativeTypeName("const uint8_t *")] byte* data, [NativeTypeName("size_t")] UIntPtr data_size, [NativeTypeName("uint8_t *")] byte* output_buffer, [NativeTypeName("size_t")] UIntPtr output_buffer_size, int output_stride);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint8_t *")]
        public static extern byte* WebPDecodeBGRInto([NativeTypeName("const uint8_t *")] byte* data, [NativeTypeName("size_t")] UIntPtr data_size, [NativeTypeName("uint8_t *")] byte* output_buffer, [NativeTypeName("size_t")] UIntPtr output_buffer_size, int output_stride);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint8_t *")]
        public static extern byte* WebPDecodeYUVInto([NativeTypeName("const uint8_t *")] byte* data, [NativeTypeName("size_t")] UIntPtr data_size, [NativeTypeName("uint8_t *")] byte* luma, [NativeTypeName("size_t")] UIntPtr luma_size, int luma_stride, [NativeTypeName("uint8_t *")] byte* u, [NativeTypeName("size_t")] UIntPtr u_size, int u_stride, [NativeTypeName("uint8_t *")] byte* v, [NativeTypeName("size_t")] UIntPtr v_size, int v_stride);

        public static int WebPIsPremultipliedMode(WEBP_CSP_MODE mode)
        {
            return (mode == MODE_rgbA || mode == MODE_bgrA || mode == MODE_Argb || mode == MODE_rgbA_4444) ? 1 : 0;
        }

        public static int WebPIsAlphaMode(WEBP_CSP_MODE mode)
        {
            return (mode == MODE_RGBA || mode == MODE_BGRA || mode == MODE_ARGB || mode == MODE_RGBA_4444 || mode == MODE_YUVA || (WebPIsPremultipliedMode(mode)) != 0) ? 1 : 0;
        }

        public static int WebPIsRGBMode(WEBP_CSP_MODE mode)
        {
            return ((int)mode < (int)(MODE_YUV)) ? 1 : 0;
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPInitDecBufferInternal(WebPDecBuffer* param0, int param1);

        public static int WebPInitDecBuffer(WebPDecBuffer* buffer)
        {
            return WebPInitDecBufferInternal(buffer, 0x0209);
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void WebPFreeDecBuffer(WebPDecBuffer* buffer);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern WebPIDecoder* WebPINewDecoder(WebPDecBuffer* output_buffer);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern WebPIDecoder* WebPINewRGB(WEBP_CSP_MODE csp, [NativeTypeName("uint8_t *")] byte* output_buffer, [NativeTypeName("size_t")] UIntPtr output_buffer_size, int output_stride);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern WebPIDecoder* WebPINewYUVA([NativeTypeName("uint8_t *")] byte* luma, [NativeTypeName("size_t")] UIntPtr luma_size, int luma_stride, [NativeTypeName("uint8_t *")] byte* u, [NativeTypeName("size_t")] UIntPtr u_size, int u_stride, [NativeTypeName("uint8_t *")] byte* v, [NativeTypeName("size_t")] UIntPtr v_size, int v_stride, [NativeTypeName("uint8_t *")] byte* a, [NativeTypeName("size_t")] UIntPtr a_size, int a_stride);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern WebPIDecoder* WebPINewYUV([NativeTypeName("uint8_t *")] byte* luma, [NativeTypeName("size_t")] UIntPtr luma_size, int luma_stride, [NativeTypeName("uint8_t *")] byte* u, [NativeTypeName("size_t")] UIntPtr u_size, int u_stride, [NativeTypeName("uint8_t *")] byte* v, [NativeTypeName("size_t")] UIntPtr v_size, int v_stride);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void WebPIDelete(WebPIDecoder* idec);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VP8StatusCode WebPIAppend(WebPIDecoder* idec, [NativeTypeName("const uint8_t *")] byte* data, [NativeTypeName("size_t")] UIntPtr data_size);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VP8StatusCode WebPIUpdate(WebPIDecoder* idec, [NativeTypeName("const uint8_t *")] byte* data, [NativeTypeName("size_t")] UIntPtr data_size);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint8_t *")]
        public static extern byte* WebPIDecGetRGB([NativeTypeName("const WebPIDecoder *")] WebPIDecoder* idec, int* last_y, int* width, int* height, int* stride);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint8_t *")]
        public static extern byte* WebPIDecGetYUVA([NativeTypeName("const WebPIDecoder *")] WebPIDecoder* idec, int* last_y, [NativeTypeName("uint8_t **")] byte** u, [NativeTypeName("uint8_t **")] byte** v, [NativeTypeName("uint8_t **")] byte** a, int* width, int* height, int* stride, int* uv_stride, int* a_stride);

        [return: NativeTypeName("uint8_t *")]
        public static byte* WebPIDecGetYUV([NativeTypeName("const WebPIDecoder *")] WebPIDecoder* idec, int* last_y, [NativeTypeName("uint8_t **")] byte** u, [NativeTypeName("uint8_t **")] byte** v, int* width, int* height, int* stride, int* uv_stride)
        {
            return WebPIDecGetYUVA(idec, last_y, u, v, null, width, height, stride, uv_stride, null);
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const WebPDecBuffer *")]
        public static extern WebPDecBuffer* WebPIDecodedArea([NativeTypeName("const WebPIDecoder *")] WebPIDecoder* idec, int* left, int* top, int* width, int* height);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VP8StatusCode WebPGetFeaturesInternal([NativeTypeName("const uint8_t *")] byte* param0, [NativeTypeName("size_t")] UIntPtr param1, WebPBitstreamFeatures* param2, int param3);

        public static VP8StatusCode WebPGetFeatures([NativeTypeName("const uint8_t *")] byte* data, [NativeTypeName("size_t")] UIntPtr data_size, WebPBitstreamFeatures* features)
        {
            return WebPGetFeaturesInternal(data, data_size, features, 0x0209);
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPInitDecoderConfigInternal(WebPDecoderConfig* param0, int param1);

        public static int WebPInitDecoderConfig(WebPDecoderConfig* config)
        {
            return WebPInitDecoderConfigInternal(config, 0x0209);
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern WebPIDecoder* WebPIDecode([NativeTypeName("const uint8_t *")] byte* data, [NativeTypeName("size_t")] UIntPtr data_size, WebPDecoderConfig* config);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VP8StatusCode WebPDecode([NativeTypeName("const uint8_t *")] byte* data, [NativeTypeName("size_t")] UIntPtr data_size, WebPDecoderConfig* config);

        [NativeTypeName("#define WEBP_DECODER_ABI_VERSION 0x0209")]
        public const int WEBP_DECODER_ABI_VERSION = 0x0209;

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPGetEncoderVersion();

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern UIntPtr WebPEncodeRGB([NativeTypeName("const uint8_t *")] byte* rgb, int width, int height, int stride, float quality_factor, [NativeTypeName("uint8_t **")] byte** output);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern UIntPtr WebPEncodeBGR([NativeTypeName("const uint8_t *")] byte* bgr, int width, int height, int stride, float quality_factor, [NativeTypeName("uint8_t **")] byte** output);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern UIntPtr WebPEncodeRGBA([NativeTypeName("const uint8_t *")] byte* rgba, int width, int height, int stride, float quality_factor, [NativeTypeName("uint8_t **")] byte** output);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern UIntPtr WebPEncodeBGRA([NativeTypeName("const uint8_t *")] byte* bgra, int width, int height, int stride, float quality_factor, [NativeTypeName("uint8_t **")] byte** output);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern UIntPtr WebPEncodeLosslessRGB([NativeTypeName("const uint8_t *")] byte* rgb, int width, int height, int stride, [NativeTypeName("uint8_t **")] byte** output);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern UIntPtr WebPEncodeLosslessBGR([NativeTypeName("const uint8_t *")] byte* bgr, int width, int height, int stride, [NativeTypeName("uint8_t **")] byte** output);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern UIntPtr WebPEncodeLosslessRGBA([NativeTypeName("const uint8_t *")] byte* rgba, int width, int height, int stride, [NativeTypeName("uint8_t **")] byte** output);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern UIntPtr WebPEncodeLosslessBGRA([NativeTypeName("const uint8_t *")] byte* bgra, int width, int height, int stride, [NativeTypeName("uint8_t **")] byte** output);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPConfigInitInternal(WebPConfig* param0, WebPPreset param1, float param2, int param3);

        public static int WebPConfigInit(WebPConfig* config)
        {
            return WebPConfigInitInternal(config, WEBP_PRESET_DEFAULT, 75.0f, 0x020f);
        }

        public static int WebPConfigPreset(WebPConfig* config, WebPPreset preset, float quality)
        {
            return WebPConfigInitInternal(config, preset, quality, 0x020f);
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPConfigLosslessPreset(WebPConfig* config, int level);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPValidateConfig([NativeTypeName("const WebPConfig *")] WebPConfig* config);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void WebPMemoryWriterInit(WebPMemoryWriter* writer);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void WebPMemoryWriterClear(WebPMemoryWriter* writer);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPMemoryWrite([NativeTypeName("const uint8_t *")] byte* data, [NativeTypeName("size_t")] UIntPtr data_size, [NativeTypeName("const WebPPicture *")] WebPPicture* picture);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPPictureInitInternal(WebPPicture* param0, int param1);

        public static int WebPPictureInit(WebPPicture* picture)
        {
            return WebPPictureInitInternal(picture, 0x020f);
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPPictureAlloc(WebPPicture* picture);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void WebPPictureFree(WebPPicture* picture);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPPictureCopy([NativeTypeName("const WebPPicture *")] WebPPicture* src, WebPPicture* dst);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPPlaneDistortion([NativeTypeName("const uint8_t *")] byte* src, [NativeTypeName("size_t")] UIntPtr src_stride, [NativeTypeName("const uint8_t *")] byte* @ref, [NativeTypeName("size_t")] UIntPtr ref_stride, int width, int height, [NativeTypeName("size_t")] UIntPtr x_step, int type, float* distortion, float* result);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPPictureDistortion([NativeTypeName("const WebPPicture *")] WebPPicture* src, [NativeTypeName("const WebPPicture *")] WebPPicture* @ref, int metric_type, [NativeTypeName("float [5]")] float* result);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPPictureCrop(WebPPicture* picture, int left, int top, int width, int height);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPPictureView([NativeTypeName("const WebPPicture *")] WebPPicture* src, int left, int top, int width, int height, WebPPicture* dst);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPPictureIsView([NativeTypeName("const WebPPicture *")] WebPPicture* picture);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPPictureRescale(WebPPicture* pic, int width, int height);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPPictureImportRGB(WebPPicture* picture, [NativeTypeName("const uint8_t *")] byte* rgb, int rgb_stride);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPPictureImportRGBA(WebPPicture* picture, [NativeTypeName("const uint8_t *")] byte* rgba, int rgba_stride);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPPictureImportRGBX(WebPPicture* picture, [NativeTypeName("const uint8_t *")] byte* rgbx, int rgbx_stride);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPPictureImportBGR(WebPPicture* picture, [NativeTypeName("const uint8_t *")] byte* bgr, int bgr_stride);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPPictureImportBGRA(WebPPicture* picture, [NativeTypeName("const uint8_t *")] byte* bgra, int bgra_stride);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPPictureImportBGRX(WebPPicture* picture, [NativeTypeName("const uint8_t *")] byte* bgrx, int bgrx_stride);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPPictureARGBToYUVA(WebPPicture* picture, WebPEncCSP param1);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPPictureARGBToYUVADithered(WebPPicture* picture, WebPEncCSP colorspace, float dithering);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPPictureSharpARGBToYUVA(WebPPicture* picture);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPPictureSmartARGBToYUVA(WebPPicture* picture);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPPictureYUVAToARGB(WebPPicture* picture);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void WebPCleanupTransparentArea(WebPPicture* picture);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPPictureHasTransparency([NativeTypeName("const WebPPicture *")] WebPPicture* picture);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void WebPBlendAlpha(WebPPicture* pic, [NativeTypeName("uint32_t")] uint background_rgb);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPEncode([NativeTypeName("const WebPConfig *")] WebPConfig* config, WebPPicture* picture);

        [NativeTypeName("#define WEBP_ENCODER_ABI_VERSION 0x020f")]
        public const int WEBP_ENCODER_ABI_VERSION = 0x020f;

        [NativeTypeName("#define WEBP_MAX_DIMENSION 16383")]
        public const int WEBP_MAX_DIMENSION = 16383;

        //public static int CheckSizeOverflow([NativeTypeName("uint64_t")] ulong size)
        //{
        //    return (size == (UIntPtr)(size)) ? 1 : 0;
        //}

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void* WebPSafeMalloc([NativeTypeName("uint64_t")] ulong nmemb, [NativeTypeName("size_t")] UIntPtr size);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void* WebPSafeCalloc([NativeTypeName("uint64_t")] ulong nmemb, [NativeTypeName("size_t")] UIntPtr size);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void WebPSafeFree([NativeTypeName("void *const")] void* ptr);

        //[return: NativeTypeName("uint32_t")]
        //public static uint WebPMemToUint32([NativeTypeName("const uint8_t *const")] byte* ptr)
        //{
        //    uint A;

        //    Unsafe.CopyBlockUnaligned(&A, ptr, sizeof(uint));
        //    return A;
        //}

        //public static void WebPUint32ToMem([NativeTypeName("uint8_t *const")] byte* ptr, [NativeTypeName("uint32_t")] uint val)
        //{
        //    Unsafe.CopyBlockUnaligned(ptr, &val, sizeof(uint));
        //}

        //public static int GetLE16([NativeTypeName("const uint8_t *const")] byte* data)
        //{
        //    return (int)(data[0] << 0) | (data[1] << 8);
        //}

        //public static int GetLE24([NativeTypeName("const uint8_t *const")] byte* data)
        //{
        //    return GetLE16(data) | (data[2] << 16);
        //}

        //[return: NativeTypeName("uint32_t")]
        //public static uint GetLE32([NativeTypeName("const uint8_t *const")] byte* data)
        //{
        //    return GetLE16(data) | ((uint)(GetLE16(data + 2)) << 16);
        //}

        //public static void PutLE16([NativeTypeName("uint8_t *const")] byte* data, int val)
        //{
        //    (void)((!!(val < (1 << 16))) || (_wassert(, , (uint)(98)), 0) != 0);
        //    data[0] = (val >> 0) & 0xff;
        //    data[1] = (val >> 8) & 0xff;
        //}

        //public static void PutLE24([NativeTypeName("uint8_t *const")] byte* data, int val)
        //{
        //    (void)((!!(val < (1 << 24))) || (_wassert(, , (uint)(104)), 0) != 0);
        //    PutLE16(data, val & 0xffff);
        //    data[2] = (val >> 16) & 0xff;
        //}

        //public static void PutLE32([NativeTypeName("uint8_t *const")] byte* data, [NativeTypeName("uint32_t")] uint val)
        //{
        //    PutLE16(data, (int)(val & 0xffff));
        //    PutLE16(data + 2, (int)(val >> 16));
        //}

        //public static int BitsLog2Floor([NativeTypeName("uint32_t")] uint n)
        //{
        //    UIntPtr first_set_bit;

        //    _BitScanReverse(&first_set_bit, n);
        //    return first_set_bit;
        //}

        //public static int BitsCtz([NativeTypeName("uint32_t")] uint n)
        //{
        //    UIntPtr first_set_bit;

        //    _BitScanForward(&first_set_bit, n);
        //    return first_set_bit;
        //}

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void WebPCopyPlane([NativeTypeName("const uint8_t *")] byte* src, int src_stride, [NativeTypeName("uint8_t *")] byte* dst, int dst_stride, int width, int height);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void WebPCopyPixels([NativeTypeName("const struct WebPPicture *const")] WebPPicture* src, [NativeTypeName("struct WebPPicture *const")] WebPPicture* dst);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPGetColorPalette([NativeTypeName("const struct WebPPicture *const")] WebPPicture* pic, [NativeTypeName("uint32_t *const")] uint* palette);

        [NativeTypeName("#define WEBP_MAX_ALLOCABLE_MEMORY (1ULL << 34)")]
        public const ulong WEBP_MAX_ALLOCABLE_MEMORY = (1UL << 34);

        [NativeTypeName("#define WEBP_ALIGN_CST 31")]
        public const int WEBP_ALIGN_CST = 31;
    }
}


using System;
using System.Runtime.InteropServices;

#pragma warning disable 1591

namespace WebP.NativeWrapper.Dec
{
    /// Return Type: int
    ///data: uint8_t*
    ///data_size: size_t->unsigned int
    ///picture: WebPPicture*
    public delegate int WebPWriterFunction([InAttribute()] IntPtr data, UIntPtr data_size, ref WebPPicture picture);

    /// Return Type: int
    ///percent: int
    ///picture: WebPPicture*
    public delegate int WebPProgressHook(int percent, ref WebPPicture picture);

    public class Decode
    {

        /// WEBP_DECODER_ABI_VERSION 0x0203    // MAJOR(8b) + MINOR(8b)
        public const int WEBP_DECODER_ABI_VERSION = 515;

        /// WEBP_ENCODER_ABI_VERSION 0x0202    // MAJOR(8b) + MINOR(8b)
        public const int WEBP_ENCODER_ABI_VERSION = 514;

        /// <summary>
        /// The maximum length of any dimension of a WebP image is 16383
        /// </summary>
        public const int WEBP_MAX_DIMENSION = 16383;

        #region NATIVE_WRAPPERS


#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        const string DLL_NAME = "libwebp";
#elif UNITY_EDITOR || UNITY_STANDALONE_OSX
        const string DLL_NAME = "webp";
#elif UNITY_ANDROID
		const string DLL_NAME = "webp";
#elif UNITY_IOS
		const string DLL_NAME = "__Internal";
#endif

        /// Return Type: int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPGetDecoderVersion")]
        public static extern int WebPGetDecoderVersion();

        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPSafeFree")]
        public static extern void WebPSafeFree(IntPtr toDeallocate);


        /// <summary>
        /// Retrieve basic header information: width, height.
        /// This function will also validate the header and return 0 in
        /// case of formatting error.
        /// Pointers 'width' and 'height' can be passed NULL if deemed irrelevant.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="data_size"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPGetInfo")]
        public static extern int WebPGetInfo([InAttribute()] IntPtr data, UIntPtr data_size, ref int width, ref int height);


        /// Return Type: uint8_t*
        ///data: uint8_t*
        ///data_size: size_t->unsigned int
        ///width: int*
        ///height: int*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPDecodeRGBA")]
        public static extern IntPtr WebPDecodeRGBA([InAttribute()] IntPtr data, UIntPtr data_size, ref int width, ref int height);


        /// Return Type: uint8_t*
        ///data: uint8_t*
        ///data_size: size_t->unsigned int
        ///width: int*
        ///height: int*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPDecodeARGB")]
        public static extern IntPtr WebPDecodeARGB([InAttribute()] IntPtr data, UIntPtr data_size, ref int width, ref int height);


        /// Return Type: uint8_t*
        ///data: uint8_t*
        ///data_size: size_t->unsigned int
        ///width: int*
        ///height: int*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPDecodeBGRA")]
        public static extern IntPtr WebPDecodeBGRA([InAttribute()] IntPtr data, UIntPtr data_size, ref int width, ref int height);


        /// Return Type: uint8_t*
        ///data: uint8_t*
        ///data_size: size_t->unsigned int
        ///width: int*
        ///height: int*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPDecodeRGB")]
        public static extern IntPtr WebPDecodeRGB([InAttribute()] IntPtr data, UIntPtr data_size, ref int width, ref int height);


        /// Return Type: uint8_t*
        ///data: uint8_t*
        ///data_size: size_t->unsigned int
        ///width: int*
        ///height: int*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPDecodeBGR")]
        public static extern IntPtr WebPDecodeBGR([InAttribute()] IntPtr data, UIntPtr data_size, ref int width, ref int height);


        /// Return Type: uint8_t*
        ///data: uint8_t*
        ///data_size: size_t->unsigned int
        ///width: int*
        ///height: int*
        ///u: uint8_t**
        ///v: uint8_t**
        ///stride: int*
        ///uv_stride: int*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPDecodeYUV")]
        public static extern IntPtr WebPDecodeYUV([InAttribute()] IntPtr data, UIntPtr data_size, ref int width, ref int height, ref IntPtr u, ref IntPtr v, ref int stride, ref int uv_stride);


        /// Return Type: uint8_t*
        ///data: uint8_t*
        ///data_size: size_t->unsigned int
        ///output_buffer: uint8_t*
        ///output_buffer_size: size_t->unsigned int
        ///output_stride: int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPDecodeRGBAInto")]
        public static extern IntPtr WebPDecodeRGBAInto([InAttribute()] IntPtr data, UIntPtr data_size, IntPtr output_buffer, UIntPtr output_buffer_size, int output_stride);


        /// Return Type: uint8_t*
        ///data: uint8_t*
        ///data_size: size_t->unsigned int
        ///output_buffer: uint8_t*
        ///output_buffer_size: size_t->unsigned int
        ///output_stride: int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPDecodeARGBInto")]
        public static extern IntPtr WebPDecodeARGBInto([InAttribute()] IntPtr data, UIntPtr data_size, IntPtr output_buffer, UIntPtr output_buffer_size, int output_stride);


        /// Return Type: uint8_t*
        ///data: uint8_t*
        ///data_size: size_t->unsigned int
        ///output_buffer: uint8_t*
        ///output_buffer_size: size_t->unsigned int
        ///output_stride: int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPDecodeBGRAInto")]
        public static extern IntPtr WebPDecodeBGRAInto([InAttribute()] IntPtr data, UIntPtr data_size, IntPtr output_buffer, UIntPtr output_buffer_size, int output_stride);


        /// Return Type: uint8_t*
        ///data: uint8_t*
        ///data_size: size_t->unsigned int
        ///output_buffer: uint8_t*
        ///output_buffer_size: size_t->unsigned int
        ///output_stride: int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPDecodeRGBInto")]
        public static extern IntPtr WebPDecodeRGBInto([InAttribute()] IntPtr data, UIntPtr data_size, IntPtr output_buffer, UIntPtr output_buffer_size, int output_stride);


        /// Return Type: uint8_t*
        ///data: uint8_t*
        ///data_size: size_t->unsigned int
        ///output_buffer: uint8_t*
        ///output_buffer_size: size_t->unsigned int
        ///output_stride: int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPDecodeBGRInto")]
        public static extern IntPtr WebPDecodeBGRInto([InAttribute()] IntPtr data, UIntPtr data_size, IntPtr output_buffer, UIntPtr output_buffer_size, int output_stride);


        /// Return Type: uint8_t*
        ///data: uint8_t*
        ///data_size: size_t->unsigned int
        ///luma: uint8_t*
        ///luma_size: size_t->unsigned int
        ///luma_stride: int
        ///u: uint8_t*
        ///u_size: size_t->unsigned int
        ///u_stride: int
        ///v: uint8_t*
        ///v_size: size_t->unsigned int
        ///v_stride: int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPDecodeYUVInto")]
        public static extern IntPtr WebPDecodeYUVInto([InAttribute()] IntPtr data, UIntPtr data_size, IntPtr luma, UIntPtr luma_size, int luma_stride, IntPtr u, UIntPtr u_size, int u_stride, IntPtr v, UIntPtr v_size, int v_stride);


        /// Return Type: int
        ///param0: WebPDecBuffer*
        ///param1: int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPInitDecBufferInternal")]
        public static extern int WebPInitDecBufferInternal(ref WebPDecBuffer param0, int param1);


        /// Return Type: void
        ///buffer: WebPDecBuffer*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPFreeDecBuffer")]
        public static extern void WebPFreeDecBuffer(ref WebPDecBuffer buffer);


        /// Return Type: WebPIDecoder*
        ///output_buffer: WebPDecBuffer*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPINewDecoder")]
        public static extern IntPtr WebPINewDecoder(ref WebPDecBuffer output_buffer);


        /// Return Type: WebPIDecoder*
        ///csp: WEBP_CSP_MODE->Anonymous_cb136f5b_1d5d_49a0_aca4_656a79e9d159
        ///output_buffer: uint8_t*
        ///output_buffer_size: size_t->unsigned int
        ///output_stride: int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPINewRGB")]
        public static extern IntPtr WebPINewRGB(WEBP_CSP_MODE csp, IntPtr output_buffer, UIntPtr output_buffer_size, int output_stride);


        /// Return Type: WebPIDecoder*
        ///luma: uint8_t*
        ///luma_size: size_t->unsigned int
        ///luma_stride: int
        ///u: uint8_t*
        ///u_size: size_t->unsigned int
        ///u_stride: int
        ///v: uint8_t*
        ///v_size: size_t->unsigned int
        ///v_stride: int
        ///a: uint8_t*
        ///a_size: size_t->unsigned int
        ///a_stride: int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPINewYUVA")]
        public static extern IntPtr WebPINewYUVA(IntPtr luma, UIntPtr luma_size, int luma_stride, IntPtr u, UIntPtr u_size, int u_stride, IntPtr v, UIntPtr v_size, int v_stride, IntPtr a, UIntPtr a_size, int a_stride);


        /// Return Type: WebPIDecoder*
        ///luma: uint8_t*
        ///luma_size: size_t->unsigned int
        ///luma_stride: int
        ///u: uint8_t*
        ///u_size: size_t->unsigned int
        ///u_stride: int
        ///v: uint8_t*
        ///v_size: size_t->unsigned int
        ///v_stride: int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPINewYUV")]
        public static extern IntPtr WebPINewYUV(IntPtr luma, UIntPtr luma_size, int luma_stride, IntPtr u, UIntPtr u_size, int u_stride, IntPtr v, UIntPtr v_size, int v_stride);


        /// Return Type: void
        ///idec: WebPIDecoder*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPIDelete")]
        public static extern void WebPIDelete(ref WebPIDecoder idec);


        /// Return Type: VP8StatusCode->Anonymous_b244cc15_fbc7_4c41_8884_71fe4f515cd6
        ///idec: WebPIDecoder*
        ///data: uint8_t*
        ///data_size: size_t->unsigned int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPIAppend")]
        public static extern VP8StatusCode WebPIAppend(ref WebPIDecoder idec, [InAttribute()] IntPtr data, UIntPtr data_size);


        /// Return Type: VP8StatusCode->Anonymous_b244cc15_fbc7_4c41_8884_71fe4f515cd6
        ///idec: WebPIDecoder*
        ///data: uint8_t*
        ///data_size: size_t->unsigned int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPIUpdate")]
        public static extern VP8StatusCode WebPIUpdate(ref WebPIDecoder idec, [InAttribute()] IntPtr data, UIntPtr data_size);


        /// Return Type: uint8_t*
        ///idec: WebPIDecoder*
        ///last_y: int*
        ///width: int*
        ///height: int*
        ///stride: int*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPIDecGetRGB")]
        public static extern IntPtr WebPIDecGetRGB(ref WebPIDecoder idec, ref int last_y, ref int width, ref int height, ref int stride);


        /// Return Type: uint8_t*
        ///idec: WebPIDecoder*
        ///last_y: int*
        ///u: uint8_t**
        ///v: uint8_t**
        ///a: uint8_t**
        ///width: int*
        ///height: int*
        ///stride: int*
        ///uv_stride: int*
        ///a_stride: int*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPIDecGetYUVA")]
        public static extern IntPtr WebPIDecGetYUVA(ref WebPIDecoder idec, ref int last_y, ref IntPtr u, ref IntPtr v, ref IntPtr a, ref int width, ref int height, ref int stride, ref int uv_stride, ref int a_stride);


        /// Return Type: WebPDecBuffer*
        ///idec: WebPIDecoder*
        ///left: int*
        ///top: int*
        ///width: int*
        ///height: int*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPIDecodedArea")]
        public static extern IntPtr WebPIDecodedArea(ref WebPIDecoder idec, ref int left, ref int top, ref int width, ref int height);


        /// Return Type: VP8StatusCode->Anonymous_b244cc15_fbc7_4c41_8884_71fe4f515cd6
        ///param0: uint8_t*
        ///param1: size_t->unsigned int
        ///param2: WebPBitstreamFeatures*
        ///param3: int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPGetFeaturesInternal")]
        public static extern VP8StatusCode WebPGetFeaturesInternal([InAttribute()] IntPtr param0, UIntPtr param1, ref WebPBitstreamFeatures param2, int param3);


        /// Return Type: int
        ///param0: WebPDecoderConfig*
        ///param1: int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPInitDecoderConfigInternal")]
        public static extern int WebPInitDecoderConfigInternal(ref WebPDecoderConfig param0, int param1);


        /// Return Type: WebPIDecoder*
        ///data: uint8_t*
        ///data_size: size_t->unsigned int
        ///config: WebPDecoderConfig*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPIDecode")]
        public static extern IntPtr WebPIDecode([InAttribute()] IntPtr data, UIntPtr data_size, ref WebPDecoderConfig config);


        /// Return Type: VP8StatusCode->Anonymous_b244cc15_fbc7_4c41_8884_71fe4f515cd6
        ///data: uint8_t*
        ///data_size: size_t->unsigned int
        ///config: WebPDecoderConfig*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPDecode")]
        public static extern VP8StatusCode WebPDecode([InAttribute()] IntPtr data, UIntPtr data_size, IntPtr config);

        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPDecode")]
        public static extern VP8StatusCode WebPDecode([InAttribute()] IntPtr data, UIntPtr data_size, ref WebPDecoderConfig config);


        /// Return Type: int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPGetEncoderVersion")]
        public static extern int WebPGetEncoderVersion();


        /// Return Type: size_t->unsigned int
        ///rgb: uint8_t*
        ///width: int
        ///height: int
        ///stride: int
        ///quality_factor: float
        ///output: uint8_t**
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPEncodeRGB")]
        public static extern UIntPtr WebPEncodeRGB([InAttribute()] IntPtr rgb, int width, int height, int stride, float quality_factor, ref IntPtr output);


        /// Return Type: size_t->unsigned int
        ///bgr: uint8_t*
        ///width: int
        ///height: int
        ///stride: int
        ///quality_factor: float
        ///output: uint8_t**
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPEncodeBGR")]
        public static extern UIntPtr WebPEncodeBGR([InAttribute()] IntPtr bgr, int width, int height, int stride, float quality_factor, ref IntPtr output);


        /// Return Type: size_t->unsigned int
        ///rgba: uint8_t*
        ///width: int
        ///height: int
        ///stride: int
        ///quality_factor: float
        ///output: uint8_t**
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPEncodeRGBA")]
        public static extern UIntPtr WebPEncodeRGBA([InAttribute()] IntPtr rgba, int width, int height, int stride, float quality_factor, ref IntPtr output);


        /// Return Type: size_t->unsigned int
        ///bgra: uint8_t*
        ///width: int
        ///height: int
        ///stride: int
        ///quality_factor: float
        ///output: uint8_t**
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPEncodeBGRA")]
        public static extern IntPtr WebPEncodeBGRA([InAttribute()] IntPtr bgra, int width, int height, int stride, float quality_factor, ref IntPtr output);


        /// Return Type: size_t->unsigned int
        ///rgb: uint8_t*
        ///width: int
        ///height: int
        ///stride: int
        ///output: uint8_t**
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPEncodeLosslessRGB")]
        public static extern UIntPtr WebPEncodeLosslessRGB([InAttribute()] IntPtr rgb, int width, int height, int stride, ref IntPtr output);


        /// Return Type: size_t->unsigned int
        ///bgr: uint8_t*
        ///width: int
        ///height: int
        ///stride: int
        ///output: uint8_t**
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPEncodeLosslessBGR")]
        public static extern UIntPtr WebPEncodeLosslessBGR([InAttribute()] IntPtr bgr, int width, int height, int stride, ref IntPtr output);


        /// Return Type: size_t->unsigned int
        ///rgba: uint8_t*
        ///width: int
        ///height: int
        ///stride: int
        ///output: uint8_t**
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPEncodeLosslessRGBA")]
        public static extern UIntPtr WebPEncodeLosslessRGBA([InAttribute()] IntPtr rgba, int width, int height, int stride, ref IntPtr output);


        /// Return Type: size_t->unsigned int
        ///bgra: uint8_t*
        ///width: int
        ///height: int
        ///stride: int
        ///output: uint8_t**
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPEncodeLosslessBGRA")]
        public static extern UIntPtr WebPEncodeLosslessBGRA([InAttribute()] IntPtr bgra, int width, int height, int stride, ref IntPtr output);


        /// Return Type: int
        ///param0: WebPConfig*
        ///param1: WebPPreset->Anonymous_017d4167_f53e_4b3d_b029_592ff5c3f80b
        ///param2: float
        ///param3: int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPConfigInitInternal")]
        public static extern int WebPConfigInitInternal(ref WebPConfig param0, WebPPreset param1, float param2, int param3);


        /// Return Type: int
        ///config: WebPConfig*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPValidateConfig")]
        public static extern int WebPValidateConfig(ref WebPConfig config);


        /// Return Type: void
        ///writer: WebPMemoryWriter*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPMemoryWriterInit")]
        public static extern void WebPMemoryWriterInit(ref WebPMemoryWriter writer);


        /// Return Type: int
        ///data: uint8_t*
        ///data_size: size_t->unsigned int
        ///picture: WebPPicture*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPMemoryWrite")]
        public static extern int WebPMemoryWrite([InAttribute()] IntPtr data, UIntPtr data_size, ref WebPPicture picture);


        /// Return Type: int
        ///param0: WebPPicture*
        ///param1: int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPPictureInitInternal")]
        public static extern int WebPPictureInitInternal(ref WebPPicture param0, int param1);


        /// Return Type: int
        ///picture: WebPPicture*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPPictureAlloc")]
        public static extern int WebPPictureAlloc(ref WebPPicture picture);


        /// Return Type: void
        ///picture: WebPPicture*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPPictureFree")]
        public static extern void WebPPictureFree(ref WebPPicture picture);


        /// Return Type: int
        ///src: WebPPicture*
        ///dst: WebPPicture*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPPictureCopy")]
        public static extern int WebPPictureCopy(ref WebPPicture src, ref WebPPicture dst);


        /// Return Type: int
        ///pic1: WebPPicture*
        ///pic2: WebPPicture*
        ///metric_type: int
        ///result: float* result[5]
        ///

        /// <summary>
        /// Compute PSNR, SSIM or LSIM distortion metric between two pictures.
        /// Result is in dB, stores in result[] in the Y/U/V/Alpha/All order.
        /// Returns false in case of error (src and ref don't have same dimension, ...)
        /// Warning: this function is rather CPU-intensive.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="reference"></param>
        /// <param name="metric_type">0 = PSNR, 1 = SSIM, 2 = LSIM</param>
        /// <param name="result"></param>
        /// <returns></returns>
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPPictureDistortion")]
        public static extern int WebPPictureDistortion(ref WebPPicture src, ref WebPPicture reference, int metric_type, ref float result);




        /// Return Type: int
        ///picture: WebPPicture*
        ///left: int
        ///top: int
        ///width: int
        ///height: int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPPictureCrop")]
        public static extern int WebPPictureCrop(ref WebPPicture picture, int left, int top, int width, int height);


        /// Return Type: int
        ///src: WebPPicture*
        ///left: int
        ///top: int
        ///width: int
        ///height: int
        ///dst: WebPPicture*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPPictureView")]
        public static extern int WebPPictureView(ref WebPPicture src, int left, int top, int width, int height, ref WebPPicture dst);


        /// Return Type: int
        ///picture: WebPPicture*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPPictureIsView")]
        public static extern int WebPPictureIsView(ref WebPPicture picture);


        /// Return Type: int
        ///pic: WebPPicture*
        ///width: int
        ///height: int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPPictureRescale")]
        public static extern int WebPPictureRescale(ref WebPPicture pic, int width, int height);


        /// Return Type: int
        ///picture: WebPPicture*
        ///rgb: uint8_t*
        ///rgb_stride: int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPPictureImportRGB")]
        public static extern int WebPPictureImportRGB(ref WebPPicture picture, [InAttribute()] IntPtr rgb, int rgb_stride);


        /// Return Type: int
        ///picture: WebPPicture*
        ///rgba: uint8_t*
        ///rgba_stride: int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPPictureImportRGBA")]
        public static extern int WebPPictureImportRGBA(ref WebPPicture picture, [InAttribute()] IntPtr rgba, int rgba_stride);


        /// Return Type: int
        ///picture: WebPPicture*
        ///rgbx: uint8_t*
        ///rgbx_stride: int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPPictureImportRGBX")]
        public static extern int WebPPictureImportRGBX(ref WebPPicture picture, [InAttribute()] IntPtr rgbx, int rgbx_stride);


        /// Return Type: int
        ///picture: WebPPicture*
        ///bgr: uint8_t*
        ///bgr_stride: int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPPictureImportBGR")]
        public static extern int WebPPictureImportBGR(ref WebPPicture picture, [InAttribute()] IntPtr bgr, int bgr_stride);


        /// Return Type: int
        ///picture: WebPPicture*
        ///bgra: uint8_t*
        ///bgra_stride: int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPPictureImportBGRA")]
        public static extern int WebPPictureImportBGRA(ref WebPPicture picture, [InAttribute()] IntPtr bgra, int bgra_stride);


        /// Return Type: int
        ///picture: WebPPicture*
        ///bgrx: uint8_t*
        ///bgrx_stride: int
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPPictureImportBGRX")]
        public static extern int WebPPictureImportBGRX(ref WebPPicture picture, [InAttribute()] IntPtr bgrx, int bgrx_stride);


        /// Return Type: int
        ///picture: WebPPicture*
        ///colorspace: WebPEncCSP->Anonymous_84ce7065_fe91_48b4_93d8_1f0e84319dba
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPPictureARGBToYUVA")]
        public static extern int WebPPictureARGBToYUVA(ref WebPPicture picture, WebPEncCSP colorspace);


        /// Return Type: int
        ///picture: WebPPicture*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPPictureYUVAToARGB")]
        public static extern int WebPPictureYUVAToARGB(ref WebPPicture picture);


        /// Return Type: void
        ///picture: WebPPicture*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPCleanupTransparentArea")]
        public static extern void WebPCleanupTransparentArea(ref WebPPicture picture);


        /// Return Type: int
        ///picture: WebPPicture*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPPictureHasTransparency")]
        public static extern int WebPPictureHasTransparency(ref WebPPicture picture);


        /// Return Type: int
        ///config: WebPConfig*
        ///picture: WebPPicture*
        [DllImportAttribute(DLL_NAME, EntryPoint = "WebPEncode")]
        public static extern int WebPEncode(ref WebPConfig config, ref WebPPicture picture);
        #endregion NATIVE_WRAPPERS
        // Some useful macros:

        /// <summary>
        /// Returns true if the specified mode uses a premultiplied alpha
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static bool WebPIsPremultipliedMode(WEBP_CSP_MODE mode)
        {

            return (mode == WEBP_CSP_MODE.MODE_rgbA || mode == WEBP_CSP_MODE.MODE_bgrA || mode == WEBP_CSP_MODE.MODE_Argb ||
                mode == WEBP_CSP_MODE.MODE_rgbA_4444);

        }

        /// <summary>
        /// Returns true if the given mode is RGB(A)
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static bool WebPIsRGBMode(WEBP_CSP_MODE mode)
        {

            return (mode < WEBP_CSP_MODE.MODE_YUV);

        }


        /// <summary>
        /// Returns true if the given mode has an alpha channel
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static bool WebPIsAlphaMode(WEBP_CSP_MODE mode)
        {

            return (mode == WEBP_CSP_MODE.MODE_RGBA || mode == WEBP_CSP_MODE.MODE_BGRA || mode == WEBP_CSP_MODE.MODE_ARGB ||
                    mode == WEBP_CSP_MODE.MODE_RGBA_4444 || mode == WEBP_CSP_MODE.MODE_YUVA ||
                    WebPIsPremultipliedMode(mode));

        }



        // 

        /// <summary>
        /// Retrieve features from the bitstream. The *features structure is filled
        /// with information gathered from the bitstream.
        /// Returns false in case of error or version mismatch.
        /// In case of error, features->bitstream_status will reflect the error code.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="data_size"></param>
        /// <param name="features"></param>
        /// <returns></returns>
        public static VP8StatusCode WebPGetFeatures(IntPtr data, UIntPtr data_size, ref WebPBitstreamFeatures features)
        {
            return Decode.WebPGetFeaturesInternal(data, data_size, ref features, WEBP_DECODER_ABI_VERSION);

        }
        /// <summary>
        /// Initialize the configuration as empty. This function must always be
        /// called first, unless WebPGetFeatures() is to be called.
        /// Returns false in case of mismatched version.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static int WebPInitDecoderConfig(ref WebPDecoderConfig config)
        {

            return WebPInitDecoderConfigInternal(ref config, WEBP_DECODER_ABI_VERSION);

        }


        /// <summary>
        /// Initialize the structure as empty. Must be called before any other use. Returns false in case of version mismatch
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static int WebPInitDecBuffer(ref WebPDecBuffer buffer)
        {
            return WebPInitDecBufferInternal(ref buffer, WEBP_DECODER_ABI_VERSION);
        }



        //    // Deprecated alpha-less version of WebPIDecGetYUVA(): it will ignore the

        //// alpha information (if present). Kept for backward compatibility.

        //public IntPtr WebPIDecGetYUV(IntPtr decoder, int* last_y, uint8_t** u, uint8_t** v,

        //    int* width, int* height, int* stride, int* uv_stride) {

        //  return WebPIDecGetYUVA(idec, last_y, u, v, NULL, width, height,

        //                         stride, uv_stride, NULL);

        /// <summary>
        /// Should always be called, to initialize a fresh WebPConfig structure before
        /// modification. Returns false in case of version mismatch. WebPConfigInit()
        /// must have succeeded before using the 'config' object.
        /// Note that the default values are lossless=0 and quality=75.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static int WebPConfigInit(ref WebPConfig config)
        {
            return Decode.WebPConfigInitInternal(ref config, WebPPreset.WEBP_PRESET_DEFAULT, 75.0f, WEBP_ENCODER_ABI_VERSION);
        }

        /// <summary>
        /// This function will initialize the configuration according to a predefined
        /// set of parameters (referred to by 'preset') and a given quality factor.
        /// This function can be called as a replacement to WebPConfigInit(). Will return false in case of error.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="preset"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public static int WebPConfigPreset(ref WebPConfig config, WebPPreset preset, float quality)
        {
            return Decode.WebPConfigInitInternal(ref config, preset, quality, WEBP_ENCODER_ABI_VERSION);
        }

        /// <summary>
        /// Should always be called, to initialize the structure. Returns false in case
        /// of version mismatch. WebPPictureInit() must have succeeded before using the
        /// 'picture' object.
        /// Note that, by default, use_argb is false and colorspace is WEBP_YUV420.
        /// </summary>
        /// <param name="picture"></param>
        /// <returns></returns>
        public static int WebPPictureInit(ref WebPPicture picture)
        {

            return Decode.WebPPictureInitInternal(ref picture, WEBP_ENCODER_ABI_VERSION);

        }
    }
}

#pragma warning restore 1591

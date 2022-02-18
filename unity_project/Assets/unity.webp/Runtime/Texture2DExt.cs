using System;
using System.Runtime.InteropServices;
using unity.libwebp;
using unity.libwebp.Interop;
using UnityEngine;

namespace WebP
{
    /// <summary>
    /// 
    /// </summary>
    public static class Texture2DExt
    {
        public delegate void ScalingFunction(ref int width, ref int height);

        /// <summary>
        /// Scaling funtion to scale image to specific width and height.
        /// </summary>
        /// <returns>The to size.</returns>
        /// <param name="widthInPixels">Width in pixels.</param>
        /// <param name="heightInPixels">Height in pixels.</param>
        public static ScalingFunction ScaleToSize(int widthInPixels, int heightInPixels)
        {
            return delegate (ref int width, ref int height)
            {
                width = widthInPixels;
                height = heightInPixels;
            };
        }

        /// <summary>
        /// Scaling function scale both height and width by a specific amount.
        /// </summary>
        /// <returns>The by.</returns>
        /// <param name="scale">Scale.</param>
        public static ScalingFunction ScaleBy(float scale)
        {
            return delegate (ref int width, ref int height)
            {
                width = (int)(width * scale);
                height = (int)(height * scale);
            };
        }

        /// <summary>
        /// Scaling function to scale to a specifc width while retaining aspect.
        /// </summary>
        /// <returns>The to width.</returns>
        /// <param name="widthInPixels">Width in pixels.</param>
        public static ScalingFunction ScaleToWidth(int widthInPixels)
        {
            return delegate (ref int width, ref int height)
            {
                height = (int)((height / (float)width) * widthInPixels);
                width = widthInPixels;
            };
        }

        /// <summary>
        /// Scaling function to scale to a specific height while retaining aspect.
        /// </summary>
        /// <returns>The to height.</returns>
        /// <param name="heightInPixels">Height in pixels.</param>
        public static ScalingFunction ScaleToHeight(int heightInPixels)
        {
            return delegate (ref int width, ref int height)
            {
                width = (int)((width / (float)height) * heightInPixels);
                height = heightInPixels;
            };
        }

        /// <summary>
        /// Gets dimensions from a webp format block of data.
        /// </summary>
        /// <param name="lData">L data.</param>
        /// <param name="lWidth">L width.</param>
        /// <param name="lHeight">L height.</param>
        public static unsafe void GetWebPDimensions(byte[] lData, out int lWidth, out int lHeight)
        {
            fixed (byte* lDataPtr = lData)
            {
                int w;
                int h;
                lWidth = 0;
                lHeight = 0;
                if (NativeLibwebp.WebPGetInfo(lDataPtr, (UIntPtr)lData.Length, &w, &h) == 0)
                {
                    throw new Exception("Invalid WebP header detected");
                }
                lWidth = w;
                lHeight = h;
            }
        }

        /// <summary>
        /// Loads an image from webp into a byte array in RGBA format.
        /// </summary>
        /// <returns>The RGBA from web p.</returns>
        /// <param name="lData">L data.</param>
        /// <param name="lWidth">L width.</param>
        /// <param name="lHeight">L height.</param>
        /// <param name="lMipmaps">If set to <c>true</c> l mipmaps.</param>
        /// <param name="lError">L error.</param>
        /// <param name="scalingFunction">Scaling function.</param>
        public static unsafe byte[] LoadRGBAFromWebP(byte[] lData, ref int lWidth, ref int lHeight, bool lMipmaps, out Error lError, ScalingFunction scalingFunction = null)
        {
            lError = 0;
            byte[] lRawData = null;
            int lLength = lData.Length;

            fixed (byte* lDataPtr = lData)
            {
                // If we've been supplied a function to alter the width and height, use that now.
                scalingFunction?.Invoke(ref lWidth, ref lHeight);

                // If mipmaps are requested we need to create 1/3 more memory for the mipmaps to be generated in.
                int numBytesRequired = lWidth * lHeight * 4;
                if (lMipmaps)
                {
                    numBytesRequired = Mathf.CeilToInt((numBytesRequired * 4.0f) / 3.0f);
                }

                lRawData = new byte[numBytesRequired];
                fixed (byte* lRawDataPtr = lRawData)
                {
                    int lStride = 4 * lWidth;

                    // As we have to reverse the y order of the data, we pass through a negative stride and 
                    // pass through a pointer to the last line of the data.
                    byte* lTmpDataPtr = lRawDataPtr + (lHeight - 1) * lStride;

                    WebPDecoderConfig config = new WebPDecoderConfig();

                    if (NativeLibwebp.WebPInitDecoderConfig(&config) == 0)
                    {
                        throw new Exception("WebPInitDecoderConfig failed. Wrong version?");
                    }

                    // Set up decode options
                    config.options.use_threads = 1;
                    if (scalingFunction != null)
                    {
                        config.options.use_scaling = 1;
                    }
                    config.options.scaled_width = lWidth;
                    config.options.scaled_height = lHeight;

                    // read the .webp input file information
                    VP8StatusCode result = NativeLibwebp.WebPGetFeatures(lDataPtr, (UIntPtr)lLength, &config.input);
                    if (result != VP8StatusCode.VP8_STATUS_OK)
                    {
                        throw new Exception(string.Format("Failed WebPGetFeatures with error {0}.", result.ToString()));
                    }

                    // specify the output format
                    config.output.colorspace = WEBP_CSP_MODE.MODE_RGBA;
                    config.output.u.RGBA.rgba = lTmpDataPtr;
                    config.output.u.RGBA.stride = -lStride;
                    config.output.u.RGBA.size = (UIntPtr)(lHeight * lStride);
                    config.output.height = lHeight;
                    config.output.width = lWidth;
                    config.output.is_external_memory = 1;

                    // Decode
                    result = NativeLibwebp.WebPDecode(lDataPtr, (UIntPtr)lLength, &config);
                    if (result != VP8StatusCode.VP8_STATUS_OK)
                    {
                        throw new Exception(string.Format("Failed WebPDecode with error {0}.", result.ToString()));
                    }
                }
                lError = Error.Success;
            }
            return lRawData;
        }
        public static Texture2D CreateWebpTexture2D(int width, int height, bool isUseMipmap, bool isLinear)
        {
            if (isUseMipmap)
            {
                UnityEngine.Experimental.Rendering.GraphicsFormat graphicsFormat = UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetGraphicsFormat(TextureFormat.RGBA32, false);
                uint mipmapSize = UnityEngine.Experimental.Rendering.GraphicsFormatUtility.ComputeMipmapSize(width, height, graphicsFormat);
                int mipmapCount = (int)mipmapSize / (width * height);
                return new Texture2D(width, height, TextureFormat.RGBA32, mipCount: mipmapCount, isLinear);
            }
            else
            {
                return new Texture2D(width, height, TextureFormat.RGBA32, mipChain: false, isLinear);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lData"></param>
        /// <param name="lError"></param>
        /// <returns></returns>
		public static unsafe Texture2D CreateTexture2DFromWebP(byte[] lData, bool lMipmaps, bool lLinear, out Error lError, ScalingFunction scalingFunction = null, bool makeNoLongerReadable = true)
        {
            Texture2D lTexture2D = null;
            int lWidth;
            int lHeight;
            GetWebPDimensions(lData, out lWidth, out lHeight);

            byte[] lRawData = LoadRGBAFromWebP(lData, ref lWidth, ref lHeight, lMipmaps, out lError, scalingFunction);

            if (lError == Error.Success)
            {
                lTexture2D = CreateWebpTexture2D(lWidth, lHeight, lMipmaps, lLinear);
                lTexture2D.LoadRawTextureData(lRawData);
                lTexture2D.Apply(lMipmaps, makeNoLongerReadable);
            }

            return lTexture2D;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lTexture2D"></param>
        /// <param name="lData"></param>
        /// <param name="lError"></param>
        public static unsafe void LoadWebP(this Texture2D lTexture2D, byte[] lData, out Error lError, ScalingFunction scalingFunction = null, bool makeNoLongerReadable = true)
        {
            lError = 0;
            bool lMipmaps = lTexture2D.mipmapCount != 1;

            int lWidth = 0, lHeight = 0;

            GetWebPDimensions(lData, out lWidth, out lHeight);

            byte[] lRawData = LoadRGBAFromWebP(lData, ref lWidth, ref lHeight, lMipmaps, out lError, scalingFunction);

            if (lError == Error.Success)
            {
                lTexture2D.LoadRawTextureData(lRawData);
                lTexture2D.Apply(lMipmaps, makeNoLongerReadable);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lTexture2D"></param>
        /// <param name="lError"></param>
        /// <returns></returns>
        public static unsafe byte[] EncodeToWebP(this Texture2D lTexture2D, float lQuality, out Error lError)
        {
            lError = 0;

            if (lQuality < -1)
            {
                lQuality = -1;
            }
            if (lQuality > 100)
            {
                lQuality = 100;
            }

            Color32[] lRawColorData = lTexture2D.GetPixels32();
            int lWidth = lTexture2D.width;
            int lHeight = lTexture2D.height;

            IntPtr lResult = new IntPtr();
            byte** pResult = (byte**)&lResult;
            GCHandle lPinnedArray = GCHandle.Alloc(lRawColorData, GCHandleType.Pinned);
            IntPtr lRawDataPtr = lPinnedArray.AddrOfPinnedObject();

            byte[] lOutputBuffer = null;

            try
            {
                int lLength;

                if (lQuality == -1)
                {
                    lLength = (int)NativeLibwebp.WebPEncodeLosslessRGBA((byte*)lRawDataPtr, lWidth, lHeight, 4 * lWidth, pResult);
                }
                else
                {
                    lLength = (int)NativeLibwebp.WebPEncodeRGBA((byte*)lRawDataPtr, lWidth, lHeight, 4 * lWidth, lQuality, pResult);
                }

                if (lLength == 0)
                {
                    throw new Exception("WebP encode failed!");
                }

                lOutputBuffer = new byte[lLength];
                Marshal.Copy(lResult, lOutputBuffer, 0, lLength);
            }
            finally
            {
                NativeLibwebp.WebPSafeFree(*pResult);
            }

            lPinnedArray.Free();

            return lOutputBuffer;
        }

        public static unsafe void LoadTexture2DFromWebP(byte[] webpBytes, Texture2D texture, bool lMipmaps, bool lLinear, byte[] bytePool, int numBytesRequired, ScalingFunction scalingFunction = null)
        {
            Debug.Assert(bytePool.Length >= numBytesRequired);
            Array.Clear(bytePool, 0, numBytesRequired);

            int width = texture.width;
            int height = texture.height;

            fixed (byte* lDataPtr = webpBytes)
            {
                fixed (byte* lRawDataPtr = bytePool)
                {
                    int lStride = 4 * width;

                    // As we have to reverse the y order of the data, we pass through a negative stride and 
                    // pass through a pointer to the last line of the data.
                    byte* lTmpDataPtr = lRawDataPtr + (height - 1) * lStride;

                    WebPDecoderConfig config = new WebPDecoderConfig();

                    if (NativeLibwebp.WebPInitDecoderConfig(&config) == 0)
                    {
                        throw new Exception("WebPInitDecoderConfig failed. Wrong version?");
                    }

                    // Set up decode options
                    config.options.use_threads = 1;
                    if (scalingFunction != null)
                    {
                        config.options.use_scaling = 1;
                    }
                    config.options.scaled_width = width;
                    config.options.scaled_height = height;

                    // read the .webp input file information
                    VP8StatusCode result = NativeLibwebp.WebPGetFeatures(lDataPtr, (UIntPtr)webpBytes.Length, &config.input);
                    if (result != VP8StatusCode.VP8_STATUS_OK)
                    {
                        throw new Exception(string.Format("Failed WebPGetFeatures with error {0}.", result.ToString()));
                    }

                    // specify the output format
                    config.output.colorspace = WEBP_CSP_MODE.MODE_RGBA;
                    config.output.u.RGBA.rgba = lTmpDataPtr;
                    config.output.u.RGBA.stride = -lStride;
                    config.output.u.RGBA.size = (UIntPtr)(height * lStride);
                    config.output.height = height;
                    config.output.width = width;
                    config.output.is_external_memory = 1;

                    // Decode
                    result = NativeLibwebp.WebPDecode(lDataPtr, (UIntPtr)webpBytes.Length, &config);
                    if (result != VP8StatusCode.VP8_STATUS_OK)
                    {
                        throw new Exception(string.Format("Failed WebPDecode with error {0}.", result.ToString()));
                    }

                    texture.LoadRawTextureData((IntPtr)lRawDataPtr, numBytesRequired);
                    texture.Apply(lMipmaps, true);
                }
            }
        }

        public static int GetRequireByteSize(byte[] webpBytes, bool isUseMipmap = false)
        {
            GetWebPDimensions(webpBytes, out int width, out int height);
            return GetRequireByteSize(width, height, isUseMipmap);
        }

        public static int GetRequireByteSize(int width, int height, bool isUseMipmap = false)
        {
            int numBytesRequired = width * height * 4;
            if (isUseMipmap)
            {
                numBytesRequired = Mathf.CeilToInt((numBytesRequired * 4.0f) / 3.0f);
            }
            return numBytesRequired;
        }
    }
}

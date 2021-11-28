using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using unity.libwebp;
using unity.libwebp.Interop;
using UnityEngine;

namespace WebP.Experiment.Animation
{

    /// <summary>
    /// The decod logic before and after decoding.
    /// </summary>
    public static class WebPDecoderWrapper
    {

        public static async Task<List<(Texture2D, int)>> Decode(byte[] bytes)
        {
            // create the decoder
            if (!CreateDecoder(bytes, out var decoder, out var info)) return null;
            
            Debug.Log($"[WebPDecoderWrapper] Loaded animation: {info.frame_count}, {info.canvas_width}/{info.canvas_height}");
            // decode every frame of the WebP file with threads
            var decodedBytes = await WebPDecodeJob.StartJob(decoder, (int)info.frame_count);
            
            Debug.Log($"[WebPDecoderWrapper] Raw bytes decode complete");

            var textures = CreateTexturesFromBytes(decodedBytes, (int)info.canvas_width, (int)info.canvas_height);

            // release the decoder
            ReleaseDecode(decoder);

            return textures;
        }

        private static unsafe void ReleaseDecode(IntPtr decoder)
        {
            NativeLibwebpdemux.WebPAnimDecoderReset((WebPAnimDecoder*)decoder);
            NativeLibwebpdemux.WebPAnimDecoderDelete((WebPAnimDecoder*)decoder);
        }

        private static List<(Texture2D, int)> CreateTexturesFromBytes(List<(byte[], int)> decodedBytes, int width, int height)
        {
            
            var textures = new List<(Texture2D, int)>();
            // turn bytes into texture
            for (var i = 0; i < decodedBytes.Count; i++)
            {
                var (rawBytes, timestamp) = decodedBytes[i];

                if (ConvertBytesToTexture(rawBytes, width, height, out var texture))
                {
                    textures.Add((texture, timestamp));
                }
                else
                {
                    textures.Add(i == 0
                        ? (new Texture2D(width, height), timestamp)
                        : (textures[i - 1].Item1, timestamp));
                }
            }

            return textures;
        }

        private static bool ConvertBytesToTexture(byte[] bytes, int width, int height, out Texture2D texture)
        {
            if (bytes == null || bytes.Length <= 0)
            {
                texture = null;
                return false;
            }

            texture = new Texture2D(width, height, TextureFormat.RGBA32, mipChain: false, linear: false);
            try
            {
                texture.LoadRawTextureData(bytes);
                texture.Apply(false, true);
            }
            catch (Exception e)
            {
                Debug.Log($"[WebPDecoderWrapper] Bytes to texture error: {e.Message}");
                texture = null;
            }
            
            return texture != null;
        }
        
        public static unsafe bool CreateDecoder(byte[] bytes, out IntPtr decoder, out WebPAnimInfo info)
        {
            decoder = IntPtr.Zero;
            info = new WebPAnimInfo();
            if (bytes == null || bytes.Length <= 0)
            {
                return false;
            }

            fixed (byte* p = bytes)
            {
                WebPData webpData = new WebPData
                {
                    bytes = p,
                    size = (UIntPtr) bytes.Length
                };
                WebPAnimDecoderOptions options = new WebPAnimDecoderOptions
                {
                    //flip = 1, 
                    use_threads = 1,
                    color_mode = WEBP_CSP_MODE.MODE_RGBA
                };

                WebPAnimDecoder* decoderP = NativeLibwebpdemux.WebPAnimDecoderNew(&webpData, &options);

                WebPAnimInfo webpAnimInfo = new WebPAnimInfo();
                var success = NativeLibwebpdemux.WebPAnimDecoderGetInfo(decoderP, &webpAnimInfo);
                info = webpAnimInfo;
                decoder = (IntPtr)decoderP;
                if (success == 0)
                {
                    Debug.LogError($"[WebPDecoderWrapper] Get file info failed");
                    return false;
                }

                return true;
            }
        }

    }
}
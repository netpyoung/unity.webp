using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using unity.libwebp;
using unity.libwebp.Interop;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace WebP.Example
{
    public class WebpAnimation : MonoBehaviour
    {
        public RawImage _img_SoftwareFlip;
        public RawImage _img_ShaderFlip;

        private void Start()
        {
            _ = Example_ShowSoftwareFlipAsync(_img_SoftwareFlip);
            _ = Example_ShowShaderFlipAsync(_img_ShaderFlip);
        }

        private async Task Example_ShowSoftwareFlipAsync(RawImage image)
        {
            if (image == null)
            {
                return;
            }

            List<(Texture2D, int)> lst = LoadAnimation("cat", isUsingSoftwareFlip: true);

            int prevTimestamp = 0;
            for (int i = 0; i < lst.Count; ++i)
            {
                (Texture2D texture, int timestamp) = lst[i];

                image.texture = texture;
                int delay = timestamp - prevTimestamp;
                prevTimestamp = timestamp;

                if (delay < 0)
                {
                    delay = 0;
                }

                await Task.Delay(delay);
                if (i == lst.Count - 1)
                {
                    i = -1;
                }
            }
        }

        private async Task Example_ShowShaderFlipAsync(RawImage image)
        {
            if (image == null)
            {
                return;
            }

            List<(Texture2D, int)> lst = LoadAnimation("cat", isUsingSoftwareFlip: false);

            int prevTimestamp = 0;
            for (int i = 0; i < lst.Count; ++i)
            {
                (Texture2D texture, int timestamp) = lst[i];
                image.texture = texture;
                int delay = timestamp - prevTimestamp;
                prevTimestamp = timestamp;

                if (delay < 0)
                {
                    delay = 0;
                }

                await Task.Delay(delay);
                if (i == lst.Count - 1)
                {
                    i = -1;
                }
            }
        }

        private unsafe List<(Texture2D, int)> LoadAnimation(string loadPath, bool isUsingSoftwareFlip = false)
        {
            List<(Texture2D, int)> ret = new List<(Texture2D, int)>();
            TextAsset textasset = Resources.Load<TextAsset>(loadPath);
            byte[] bytes = textasset.bytes;
            WebPAnimDecoderOptions option = new WebPAnimDecoderOptions
            {
                use_threads = 1,
                color_mode = WEBP_CSP_MODE.MODE_RGBA,
            };
            option.padding[5] = 1;

            NativeLibwebpdemux.WebPAnimDecoderOptionsInit(&option);
            fixed (byte* p = bytes)
            {
                WebPData webpdata = new WebPData
                {
                    bytes = p,
                    size = new UIntPtr((uint)bytes.Length)
                };
                WebPAnimDecoder* dec = NativeLibwebpdemux.WebPAnimDecoderNew(&webpdata, &option);
                //dec->config_.options.flip = 1;

                WebPAnimInfo anim_info = new WebPAnimInfo();

                NativeLibwebpdemux.WebPAnimDecoderGetInfo(dec, &anim_info);

                Debug.LogWarning($"{anim_info.frame_count} {anim_info.canvas_width}/{anim_info.canvas_height}");

                uint size = anim_info.canvas_width * 4 * anim_info.canvas_height;

                int timestamp = 0;

                IntPtr pp = new IntPtr();
                byte** unmanagedPointer = (byte**)&pp;
                for (int i = 0; i < anim_info.frame_count; ++i)
                {
                    int result = NativeLibwebpdemux.WebPAnimDecoderGetNext(dec, unmanagedPointer, &timestamp);
                    Assert.AreEqual(1, result);

                    int lWidth = (int)anim_info.canvas_width;
                    int lHeight = (int)anim_info.canvas_height;
                    bool lMipmaps = false;
                    bool lLinear = false;

                    Texture2D texture = Texture2DExt.CreateWebpTexture2D(lWidth, lHeight, lMipmaps, lLinear);
                    texture.LoadRawTextureData(pp, (int)size);

                    if (isUsingSoftwareFlip)
                    {// Flip updown.
                     // ref: https://github.com/netpyoung/unity.webp/issues/25
                     // ref: https://github.com/netpyoung/unity.webp/issues/21
                     // ref: https://github.com/webmproject/libwebp/blob/master/src/demux/anim_decode.c#L309
                        Color[] pixels = texture.GetPixels();
                        Color[] pixelsFlipped = new Color[pixels.Length];
                        for (int y = 0; y < anim_info.canvas_height; y++)
                        {
                            Array.Copy(pixels, y * anim_info.canvas_width, pixelsFlipped, (anim_info.canvas_height - y - 1) * anim_info.canvas_width, anim_info.canvas_width);
                        }
                        texture.SetPixels(pixelsFlipped);
                    }

                    texture.Apply();
                    ret.Add((texture, timestamp));
                }
                NativeLibwebpdemux.WebPAnimDecoderReset(dec);
                NativeLibwebpdemux.WebPAnimDecoderDelete(dec);
            }
            return ret;
        }
    }
}
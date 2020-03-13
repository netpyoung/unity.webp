using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using WebP.Extern;

public class WebpAnimation : MonoBehaviour
{
    public RawImage image2;

    async void Start()
    {
        List<(Texture2D, int)> lst = LoadAnimation("butterfly_small");

        int prevTimestamp = 0;
        for (int i = 0; i < lst.Count; ++i)
        {
            (Texture2D texture, int timestamp) = lst[i];
            if (image2 == null)
            {
                return;
            }
            image2.texture = texture;
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

    unsafe List<(Texture2D, int)> LoadAnimation(string loadPath)
    {
        List<ValueTuple<Texture2D, int>> ret = new List<ValueTuple<Texture2D, int>>();
        TextAsset textasset = Resources.Load<TextAsset>(loadPath);
        byte[] bytes = textasset.bytes;
        WebPAnimDecoderOptions option = new WebPAnimDecoderOptions
        {
            use_threads = 1,
            color_mode = WEBP_CSP_MODE.MODE_RGBA
        };
        libwebpdemux.WebPAnimDecoderOptionsInit(ref option);
        fixed (byte* p = bytes)
        {
            IntPtr ptr = (IntPtr)p;
            WebPData webpdata = new WebPData
            {
                bytes = ptr,
                size = new UIntPtr((uint)bytes.Length)
            };
            IntPtr dec = libwebpdemux.WebPAnimDecoderNew(ref webpdata, ref option);
            WebPAnimInfo anim_info = new WebPAnimInfo();

            libwebpdemux.WebPAnimDecoderGetInfo(dec, ref anim_info);

            Debug.LogWarning($"{anim_info.frame_count} {anim_info.canvas_width}/{anim_info.canvas_height}");

            int size = anim_info.canvas_width * 4 * anim_info.canvas_height;

            IntPtr unmanagedPointer = new IntPtr();
            int timestamp = 0;
            for (int i = 0; i < anim_info.frame_count; ++i)
            {
                int result = libwebpdemux.WebPAnimDecoderGetNext(dec, ref unmanagedPointer, ref timestamp);
                int lWidth = anim_info.canvas_width;
                int lHeight = anim_info.canvas_height;
                bool lMipmaps = false;
                bool lLinear = false;

                Texture2D texture = new Texture2D(lWidth, lHeight, TextureFormat.RGBA32, lMipmaps, lLinear);
                texture.LoadRawTextureData(unmanagedPointer, size);

                {// Flip updown.
                    // ref: https://github.com/netpyoung/unity.webp/issues/18
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
            libwebpdemux.WebPAnimDecoderReset(dec);
            libwebpdemux.WebPAnimDecoderDelete(dec);
        }
        return ret;
    }
}

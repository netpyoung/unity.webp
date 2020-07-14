using Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using WebP;
using WebP.Extern;

public class WebpAnimationFix : MonoBehaviour
{
    public RawImage image2;

    async void Start()
    {
        //await LoadAnimation2("butterfly_small");
        //List<(Texture2D, int)> lst = LoadAnimation("cat");
        List<(Texture2D, int)> lst = LoadAnimation("cat");
        //image2.texture = lst.First().Item1;
        //Debug.Log(lst.Count);

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

    private unsafe List<(Texture2D, int)> LoadAnimation(string loadPath)
    {
        List<ValueTuple<Texture2D, int>> ret = new List<ValueTuple<Texture2D, int>>();

        TextAsset textasset = Resources.Load<TextAsset>(loadPath);
        byte[] bytes = textasset.bytes;

        var config = new WebPDecoderConfig();
        if (NativeBindings.WebPInitDecoderConfig(ref config) == 0)
        {
            throw new Exception("WebPInitDecoderConfig failed. Wrong version?");
        }

        var iter = new WebPIterator();
        IntPtr webpDataPtr = Marshal.AllocHGlobal(sizeof(WebP.WebPData));
        IntPtr configPtr = Marshal.AllocHGlobal(Marshal.SizeOf(config));
        IntPtr iterPtr = Marshal.AllocHGlobal(Marshal.SizeOf(iter));

        try
        {

            fixed (byte* p = bytes)
            {
                IntPtr ptr = (IntPtr)p;
                WebP.WebPData webpdata = new WebP.WebPData
                {
                    bytes = ptr,
                    size = new UIntPtr((uint)bytes.Length)
                };
                Marshal.StructureToPtr(webpdata, webpDataPtr, false);
                Marshal.StructureToPtr(config, configPtr, false);
                Marshal.StructureToPtr(iter, iterPtr, false);

                IntPtr webPDemuxer = libwebpdemux.WebPDemuxInternal(webpDataPtr, 0, (IntPtr)0, libwebpdemux.WEBP_DEMUX_ABI_VERSION);

                VP8StatusCode result = NativeBindings.WebPGetFeatures(webpdata.bytes, webpdata.size, ref config.input);
                if (result != VP8StatusCode.VP8_STATUS_OK)
                {
                    throw new Exception(string.Format("Failed WebPGetFeatures with error {0}.", result.ToString()));
                }

                var height = config.input.height;
                var width = config.input.height;

                config.options.bypass_filtering = 0;
                config.options.use_threads = 1;
                config.options.no_fancy_upsampling = 0;
                config.options.use_cropping = 0;
                config.options.use_scaling = 1;
                config.options.scaled_width = width;
                config.options.scaled_height = height;
                config.options.flip = 1;
                config.options.dithering_strength = 0;
                config.output.colorspace = WEBP_CSP_MODE.MODE_RGBA;
                config.output.width = width;
                config.output.height = height;
                //byte[] bbb = new byte[width * height];
                //fixed (byte* ppp = bbb)
                //{
                //    config.output.u.RGBA.rgba = (IntPtr)ppp;
                //}
                //config.output.u.RGBA.stride = width * 4;
                //config.output.u.RGBA.size = (UIntPtr)(width * height);
                //config.output.is_external_memory = 1;
                //config.output.is_external_memory = 1;

                int success = libwebpdemux.WebPDemuxGetFrame(webPDemuxer, 1, ref iter);
                if (success != 1)
                {
                    return ret;
                }

                int timestamp = 0;
                int size = width * height * 4;
                do
                {
                    WebP.WebPData frame = iter.fragment;
                    VP8StatusCode status = NativeBindings.WebPDecode(frame.bytes, frame.size, ref config);
                    if (status != VP8StatusCode.VP8_STATUS_OK)
                    {
                        Debug.LogError(status);
                        break;
                    }

                    var texture = new Texture2D(width, height, TextureFormat.RGBA32, mipChain: false, linear: false);
                    texture.LoadRawTextureData(config.output.u.RGBA.rgba, size);
                    texture.Apply();
                    timestamp += iter.duration;
                    ret.Add((texture, timestamp));
                }
                while (libwebpdemux.WebPDemuxNextFrame(ref iter) == 1);

                libwebpdemux.WebPDemuxDelete(webPDemuxer);
                libwebpdemux.WebPDemuxReleaseIterator(ref iter);
            }
        }
        finally
        {
            Marshal.FreeHGlobal(webpDataPtr);
            Marshal.FreeHGlobal(configPtr);
            Marshal.FreeHGlobal(iterPtr);
        }

        return ret;
    }
}

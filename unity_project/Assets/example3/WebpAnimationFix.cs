using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using WebP.NativeWrapper.Dec;
using WebP.NativeWrapper.Demux;

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
        Demux.WebPAnimDecoderOptionsInit(ref option);
        fixed (byte* p = bytes)
        {
            IntPtr ptr = (IntPtr)p;
            WebPData webpdata = new WebPData
            {
                bytes = ptr,
                size = new UIntPtr((uint)bytes.Length)
            };
            IntPtr dec = Demux.WebPAnimDecoderNew(ref webpdata, ref option);
            WebPAnimInfo anim_info = new WebPAnimInfo();

            Demux.WebPAnimDecoderGetInfo(dec, ref anim_info);

            Debug.LogWarning($"{anim_info.frame_count} {anim_info.canvas_width}/{anim_info.canvas_height}");

            int size = anim_info.canvas_width * 4 * anim_info.canvas_height;

            WebPAnimDecoder decoder = (WebPAnimDecoder)Marshal.PtrToStructure(dec, typeof(WebPAnimDecoder));
            decoder.config_.options.flip = 1;
            decoder.config_.options.no_fancy_upsampling = 1;
            Marshal.StructureToPtr(decoder, dec, true);


            IntPtr unmanagedPointer = new IntPtr();
            int timestamp = 0;
            for (int i = 0; i < anim_info.frame_count; ++i)
            {
                int result = Demux.WebPAnimDecoderGetNext(dec, ref unmanagedPointer, ref timestamp);
                if (result != 1)
                {
                    Debug.LogError("WTF");
                }
                int lWidth = anim_info.canvas_width;
                int lHeight = anim_info.canvas_height;
                bool lMipmaps = false;
                bool lLinear = false;

                Texture2D texture = new Texture2D(lWidth, lHeight, TextureFormat.RGBA32, lMipmaps, lLinear);
                texture.LoadRawTextureData(unmanagedPointer, size);

                //{// Flip updown.
                //    // ref: https://github.com/netpyoung/unity.webp/issues/18
                //    // ref: https://github.com/webmproject/libwebp/blob/master/src/demux/anim_decode.c#L309
                //    Color[] pixels = texture.GetPixels();
                //    Color[] pixelsFlipped = new Color[pixels.Length];
                //    for (int y = 0; y < anim_info.canvas_height; y++)
                //    {
                //        Array.Copy(pixels, y * anim_info.canvas_width, pixelsFlipped, (anim_info.canvas_height - y - 1) * anim_info.canvas_width, anim_info.canvas_width);
                //    }
                //    texture.SetPixels(pixelsFlipped);
                //}

                texture.Apply(updateMipmaps: false, makeNoLongerReadable: true);
                ret.Add((texture, timestamp));
            }
            Demux.WebPAnimDecoderReset(dec);
            Demux.WebPAnimDecoderDelete(dec);
        }
        return ret;
    }
    unsafe List<(Texture2D, int)> LoadAnimation3(string loadPath)
    {
        List<ValueTuple<Texture2D, int>> ret = new List<ValueTuple<Texture2D, int>>();
        TextAsset textasset = Resources.Load<TextAsset>(loadPath);
        byte[] bytes = textasset.bytes;
        WebPAnimDecoderOptions option = new WebPAnimDecoderOptions
        {
            use_threads = 1,
            color_mode = WEBP_CSP_MODE.MODE_RGBA
        };

        var config = new WebPDecoderConfig();
        if (Decode.WebPInitDecoderConfig(ref config) == 0)
        {
            throw new Exception("WebPInitDecoderConfig failed. Wrong version?");
        }
        Demux.WebPAnimDecoderOptionsInit(ref option);
        fixed (byte* p = bytes)
        {
            IntPtr ptr = (IntPtr)p;
            var webpdata = new WebPData
            {
                bytes = ptr,
                size = new UIntPtr((uint)bytes.Length)
            };

            WebPAnimDecoderOptions opt = new WebPAnimDecoderOptions();
            Demux.WebPAnimDecoderOptionsInit(ref opt);

            IntPtr webPAnimDecoderPtr = Demux.WebPAnimDecoderNewInternal(ref webpdata, ref opt, Demux.WEBP_DEMUX_ABI_VERSION);
            Debug.Log($"webPAnimDecoderPtr = {webPAnimDecoderPtr}");
            WebPAnimDecoder decoder = (WebPAnimDecoder)Marshal.PtrToStructure(webPAnimDecoderPtr, typeof(WebPAnimDecoder));

            int width = 400;
            int height = 400;
            {
                //config.input.has_alpha = 1;
                //config.options.bypass_filtering = 1;
                //config.options.no_fancy_upsampling = 1;
                config.options.use_threads = 1;
                //config.options.no_fancy_upsampling = 0;
                //config.options.use_cropping = 0;
                //config.options.use_scaling = 1;
                //config.options.scaled_width = width;
                //config.options.scaled_height = height;
                config.options.flip = 1;
                //config.options.dithering_strength = 100;
                config.output.colorspace = WEBP_CSP_MODE.MODE_RGBA;
                //config.output.is_external_memory = 1;
                //config.output.width = width;
                //config.output.height = height;
            }
            decoder.config_ = config;
            Marshal.StructureToPtr(decoder, webPAnimDecoderPtr, true);
            //IntPtr dec = libwebpdemux.WebPAnimDecoderNew(ref webpdata, ref option);
            IntPtr dec = webPAnimDecoderPtr;
            WebPAnimInfo anim_info = new WebPAnimInfo();

            Demux.WebPAnimDecoderGetInfo(dec, ref anim_info);

            Debug.LogWarning($"{anim_info.frame_count} {anim_info.canvas_width}/{anim_info.canvas_height}");

            int size = anim_info.canvas_width * 4 * anim_info.canvas_height;

            IntPtr unmanagedPointer = new IntPtr();
            int timestamp = 0;
            for (int i = 0; i < anim_info.frame_count; ++i)
            {
                int result = Demux.WebPAnimDecoderGetNext(dec, ref unmanagedPointer, ref timestamp);
                int lWidth = anim_info.canvas_width;
                int lHeight = anim_info.canvas_height;
                bool lMipmaps = false;
                bool lLinear = false;

                Texture2D texture = new Texture2D(lWidth, lHeight, TextureFormat.RGBA32, lMipmaps, lLinear);
                //byte[] bb = new byte[lWidth * lHeight * 4];
                //Marshal.Copy(unmanagedPointer, bb, 0, lWidth * lHeight * 4);
                //texture.LoadRawTextureData(bb);
                texture.LoadRawTextureData(unmanagedPointer, size);


                //{// Flip updown.
                //    // ref: https://github.com/netpyoung/unity.webp/issues/18
                //    // ref: https://github.com/webmproject/libwebp/blob/master/src/demux/anim_decode.c#L309
                //    Color[] pixels = texture.GetPixels();
                //    Color[] pixelsFlipped = new Color[pixels.Length];
                //    for (int y = 0; y < anim_info.canvas_height; y++)
                //    {
                //        Array.Copy(pixels, y * anim_info.canvas_width, pixelsFlipped, (anim_info.canvas_height - y - 1) * anim_info.canvas_width, anim_info.canvas_width);
                //    }
                //    texture.SetPixels(pixelsFlipped);
                //}

                texture.Apply(updateMipmaps: false, makeNoLongerReadable: true);
                ret.Add((texture, timestamp));
            }
            Demux.WebPAnimDecoderReset(dec);
            Demux.WebPAnimDecoderDelete(dec);
        }
        return ret;
    }
    private unsafe List<(Texture2D, int)> LoadAnimation2(string loadPath)
    {
        List<ValueTuple<Texture2D, int>> ret = new List<ValueTuple<Texture2D, int>>();

        TextAsset textasset = Resources.Load<TextAsset>(loadPath);
        byte[] bytes = textasset.bytes;

        var config = new WebPDecoderConfig();
        if (Decode.WebPInitDecoderConfig(ref config) == 0)
        {
            throw new Exception("WebPInitDecoderConfig failed. Wrong version?");
        }

        var iter = new WebPIterator();
        IntPtr webpDataPtr = Marshal.AllocHGlobal(sizeof(WebPData));
        IntPtr configPtr = Marshal.AllocHGlobal(Marshal.SizeOf(config));
        IntPtr iterPtr = Marshal.AllocHGlobal(Marshal.SizeOf(iter));

        try
        {

            fixed (byte* p = bytes)
            {
                IntPtr ptr = (IntPtr)p;
                WebPData webpdata = new WebPData
                {
                    bytes = ptr,
                    size = new UIntPtr((uint)bytes.Length)
                };
                Marshal.StructureToPtr(webpdata, webpDataPtr, false);
                Marshal.StructureToPtr(config, configPtr, false);
                Marshal.StructureToPtr(iter, iterPtr, false);
                IntPtr webPDemuxer = Demux.WebPDemuxInternal(webpDataPtr, 0, (IntPtr)0, Demux.WEBP_DEMUX_ABI_VERSION);

                VP8StatusCode result = Decode.WebPGetFeatures(webpdata.bytes, webpdata.size, ref config.input);
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

                int success = Demux.WebPDemuxGetFrame(webPDemuxer, 1, ref iter);
                if (success != 1)
                {
                    return ret;
                }

                int timestamp = 0;
                int size = width * height * 4;
                do
                {
                    WebPData frame = iter.fragment;
                    VP8StatusCode status = Decode.WebPDecode(frame.bytes, frame.size, ref config);
                    if (status != VP8StatusCode.VP8_STATUS_OK)
                    {
                        Debug.LogError(status);
                        break;
                    }

                    var texture = new Texture2D(width, height, TextureFormat.RGBA32, mipChain: false, linear: false);
                    texture.LoadRawTextureData(config.output.u.RGBA.rgba, size);
                    texture.Apply(updateMipmaps: false, makeNoLongerReadable: true);
                    timestamp += iter.duration;
                    ret.Add((texture, timestamp));
                }
                while (Demux.WebPDemuxNextFrame(ref iter) == 1);

                Demux.WebPDemuxDelete(webPDemuxer);
                Demux.WebPDemuxReleaseIterator(ref iter);
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

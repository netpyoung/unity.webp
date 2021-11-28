using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using unity.libwebp;
using unity.libwebp.Interop;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class WebpAnimationFix : MonoBehaviour
{
    public RawImage image2;

    private async void Start()
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
        List<(Texture2D, int)> ret = new List<(Texture2D, int)>();
        TextAsset textasset = Resources.Load<TextAsset>(loadPath);
        byte[] bytes = textasset.bytes;
        WebPAnimDecoderOptions option = new WebPAnimDecoderOptions
        {
            use_threads = 1,
            color_mode = (unity.libwebp.Interop.WEBP_CSP_MODE)WEBP_CSP_MODE.MODE_RGBA
        };
        NativeLibwebpdemux.WebPAnimDecoderOptionsInit(&option);
        fixed (byte* p = bytes)
        {
            WebPData webpdata = new WebPData
            {
                bytes = p,
                size = new UIntPtr((uint)bytes.Length)
            };
            WebPAnimDecoder* dec = NativeLibwebpdemux.WebPAnimDecoderNew(&webpdata, &option);
            WebPAnimInfo anim_info = new WebPAnimInfo();

            NativeLibwebpdemux.WebPAnimDecoderGetInfo(dec, &anim_info);

            Debug.LogWarning($"{anim_info.frame_count} {anim_info.canvas_width}/{anim_info.canvas_height}");

            int size = (int)anim_info.canvas_width * 4 * (int)anim_info.canvas_height;
            dec->config_.options.flip = 1;
            dec->config_.options.no_fancy_upsampling = 1;

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

                Texture2D texture = new Texture2D(lWidth, lHeight, TextureFormat.RGBA32, lMipmaps, lLinear);
                texture.LoadRawTextureData(pp, size);
                texture.Apply(updateMipmaps: false, makeNoLongerReadable: true);
                ret.Add((texture, timestamp));
            }
            NativeLibwebpdemux.WebPAnimDecoderReset(dec);
            NativeLibwebpdemux.WebPAnimDecoderDelete(dec);
        }
        return ret;
    }

    private unsafe List<(Texture2D, int)> LoadAnimation3(string loadPath)
    {
        List<ValueTuple<Texture2D, int>> ret = new List<ValueTuple<Texture2D, int>>();
        TextAsset textasset = Resources.Load<TextAsset>(loadPath);
        byte[] bytes = textasset.bytes;
        WebPAnimDecoderOptions option = new WebPAnimDecoderOptions
        {
            use_threads = 1,
            color_mode = (unity.libwebp.Interop.WEBP_CSP_MODE)WEBP_CSP_MODE.MODE_RGBA
        };

        unity.libwebp.Interop.WebPDecoderConfig config = new unity.libwebp.Interop.WebPDecoderConfig();
        //if (Decode.WebPInitDecoderConfig(ref config) == 0)
        //{
        //    throw new Exception("WebPInitDecoderConfig failed. Wrong version?");
        //}
        NativeLibwebpdemux.WebPAnimDecoderOptionsInit(&option);

        fixed (byte* p = bytes)
        {
            WebPData webpdata = new WebPData
            {
                bytes = p,
                size = new UIntPtr((uint)bytes.Length)
            };

            WebPAnimDecoderOptions opt = new WebPAnimDecoderOptions();
            NativeLibwebpdemux.WebPAnimDecoderOptionsInit(&opt);

            WebPAnimDecoder* webPAnimDecoderPtr = NativeLibwebpdemux.WebPAnimDecoderNewInternal(&webpdata, &opt, NativeLibwebpdemux.WEBP_DEMUX_ABI_VERSION);

            //int width = 400;
            //int height = 400;
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
                config.output.colorspace = (unity.libwebp.Interop.WEBP_CSP_MODE)WEBP_CSP_MODE.MODE_RGBA;
                //config.output.is_external_memory = 1;
                //config.output.width = width;
                //config.output.height = height;
            }
            webPAnimDecoderPtr->config_ = config;
            WebPAnimInfo anim_info = new WebPAnimInfo();

            NativeLibwebpdemux.WebPAnimDecoderGetInfo(webPAnimDecoderPtr, &anim_info);

            Debug.LogWarning($"{anim_info.frame_count} {anim_info.canvas_width}/{anim_info.canvas_height}");

            int size = (int)anim_info.canvas_width * 4 * (int)anim_info.canvas_height;

            int timestamp = 0;
            IntPtr pp = new IntPtr();
            byte** unmanagedPointer = (byte**)&pp;
            for (int i = 0; i < anim_info.frame_count; ++i)
            {
                int result = NativeLibwebpdemux.WebPAnimDecoderGetNext(webPAnimDecoderPtr, unmanagedPointer, &timestamp);
                int lWidth = (int)anim_info.canvas_width;
                int lHeight = (int)anim_info.canvas_height;
                bool lMipmaps = false;
                bool lLinear = false;

                Texture2D texture = new Texture2D(lWidth, lHeight, TextureFormat.RGBA32, lMipmaps, lLinear);
                texture.LoadRawTextureData((IntPtr)unmanagedPointer, size);
                texture.Apply(updateMipmaps: false, makeNoLongerReadable: true);
                ret.Add((texture, timestamp));
            }

            NativeLibwebpdemux.WebPAnimDecoderReset(webPAnimDecoderPtr);
            NativeLibwebpdemux.WebPAnimDecoderDelete(webPAnimDecoderPtr);
        }
        return ret;
    }

    private unsafe List<(Texture2D, int)> LoadAnimation2(string loadPath)
    {
        List<ValueTuple<Texture2D, int>> ret = new List<ValueTuple<Texture2D, int>>();

        TextAsset textasset = Resources.Load<TextAsset>(loadPath);
        byte[] bytes = textasset.bytes;

        WebPDecoderConfig config = new WebPDecoderConfig();
        if (NativeLibwebp.WebPInitDecoderConfig(&config) == 0)
        {
            throw new Exception("WebPInitDecoderConfig failed. Wrong version?");
        }

        WebPIterator iter = new WebPIterator();
        fixed (byte* p = bytes)
        {
            WebPData webpdata = new WebPData
            {
                bytes = p,
                size = new UIntPtr((uint)bytes.Length)
            };
            WebPDemuxer* webPDemuxer = NativeLibwebpdemux.WebPDemuxInternal(&webpdata, 0, (WebPDemuxState*)IntPtr.Zero, NativeLibwebpdemux.WEBP_DEMUX_ABI_VERSION);

            VP8StatusCode result = NativeLibwebp.WebPGetFeatures(webpdata.bytes, webpdata.size, &config.input);
            if (result != VP8StatusCode.VP8_STATUS_OK)
            {
                throw new Exception(string.Format("Failed WebPGetFeatures with error {0}.", result.ToString()));
            }

            int height = config.input.height;
            int width = config.input.height;

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

            int success = NativeLibwebpdemux.WebPDemuxGetFrame(webPDemuxer, 1, &iter);
            if (success != 1)
            {
                return ret;
            }

            int timestamp = 0;
            int size = width * height * 4;
            do
            {
                WebPData frame = iter.fragment;
                VP8StatusCode status = NativeLibwebp.WebPDecode(frame.bytes, frame.size, &config);
                if (status != VP8StatusCode.VP8_STATUS_OK)
                {
                    Debug.LogError(status);
                    break;
                }

                Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, mipChain: false, linear: false);
                texture.LoadRawTextureData((IntPtr)config.output.u.RGBA.rgba, size);
                texture.Apply(updateMipmaps: false, makeNoLongerReadable: true);
                timestamp += iter.duration;
                ret.Add((texture, timestamp));
            }
            while (NativeLibwebpdemux.WebPDemuxNextFrame(&iter) == 1);

            NativeLibwebpdemux.WebPDemuxDelete(webPDemuxer);
            NativeLibwebpdemux.WebPDemuxReleaseIterator(&iter);
        }

        return ret;
    }
}

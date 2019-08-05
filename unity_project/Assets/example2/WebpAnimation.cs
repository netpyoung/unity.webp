using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using WebP;
using WebP.Extern;

// %comspec% /k "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\VC\Auxiliary\Build\vcvars64.bat"
public class WebpAnimation : MonoBehaviour {
	public RawImage image2;

	void Start()
	{
		LoadWebp (image2);
	}

    unsafe void LoadWebp (RawImage image)
    {
		var textasset = Resources.Load<TextAsset> ("butterfly_small");
		var bytes = textasset.bytes;
        WebPAnimDecoderOptions option = new WebPAnimDecoderOptions
        {
            use_threads = 1,
            color_mode = WEBP_CSP_MODE.MODE_RGBA
        };
        libwebpdemux.WebPAnimDecoderOptionsInit(ref option);
        fixed (byte* p = bytes)
        {
            IntPtr ptr = (IntPtr)p;
            var webpdata = new WebPData
            {
                bytes = ptr,
                size = new UIntPtr((uint)bytes.Length)
            };
            IntPtr dec = libwebpdemux.WebPAnimDecoderNew(ref webpdata, ref option);
            WebPAnimInfo anim_info = new WebPAnimInfo();
            libwebpdemux.WebPAnimDecoderGetInfo(dec, ref anim_info);
            
            Debug.LogWarning($"{anim_info.frame_count} {anim_info.canvas_width}/{anim_info.canvas_height}");
            //for (int i = 0; i < anim_info.frame_count; ++i)
            //{

            {
                byte[] arr = new byte[anim_info.canvas_width * 4 * anim_info.canvas_height];
                //byte[] clear = new byte[anim_info.canvas_width * 4 * anim_info.canvas_height];

                IntPtr unmanagedPointer = new IntPtr(); //Marshal.AllocHGlobal(arr.Length);

                int timestamp = 0;

                //while (libwebpdemux.WebPAnimDecoderHasMoreFrames(dec))
                //{
                    var result = libwebpdemux.WebPAnimDecoderGetNext(dec, ref unmanagedPointer, ref timestamp);
                Debug.LogWarning($"result: {result}");
                //Marshal.Copy(unmanagedPointer, arr, 0, arr.Length);
                //Marshal.FreeHGlobal(unmanagedPointer);

                Debug.Log($"{timestamp} - {arr.Length}");
                {
                    int lWidth = anim_info.canvas_width;
                    int lHeight = anim_info.canvas_height;
                    Texture2D lTexture2D;
                    bool lMipmaps = false;
                    bool lLinear = false;
                    lTexture2D = new Texture2D(lWidth, lHeight, TextureFormat.RGBA32, lMipmaps, lLinear);
                    //lTexture2D.LoadRawTextureData(arr);
                    lTexture2D.LoadRawTextureData(unmanagedPointer, arr.Length);
                    lTexture2D.Apply();// .Apply(lMipmaps, true);
                    image2.texture = lTexture2D;
                }
            }
            //}

            libwebpdemux.WebPAnimDecoderReset(dec);
            //}
            //IntPtr demuxer = libwebpdemux.WebPAnimDecoderGetDemuxer(dec);
            libwebpdemux.WebPAnimDecoderDelete(dec);


        }
    }
}

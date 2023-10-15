using UnityEngine;
using UnityEngine.UI;

namespace WebP.Example
{
    public class Example : MonoBehaviour
    {
        public RawImage _img_Origin;
        public RawImage _img_Webp;
        public RawImage _img_WebpUsingPool;

        void Start()
        {
            LoadOrigin(_img_Origin);
            LoadWebp(_img_Webp);
            LoadWebpUsingPool(_img_WebpUsingPool);
        }

        void LoadWebpUsingPool(RawImage image)
        {
            byte[] bytePool = new byte[1024 * 1024 * 10];

            TextAsset textasset = Resources.Load<TextAsset>("webp");
            byte[] webpBytes = textasset.bytes;

            Texture2DExt.GetWebPDimensions(webpBytes, out int width, out int height);

            Texture2D texture = Texture2DExt.CreateWebpTexture2D(width, height, isUseMipmap: true, isLinear: false);
            image.texture = texture;

            int numBytesRequired = Texture2DExt.GetRequireByteSize(width, height, isUseMipmap: true);

            Debug.Assert(bytePool.Length >= numBytesRequired);

            Texture2DExt.LoadTexture2DFromWebP(webpBytes, texture, lMipmaps: true, lLinear: true, bytePool, numBytesRequired);
        }

        void LoadWebp(RawImage image)
        {
            TextAsset textasset = Resources.Load<TextAsset>("webp");
            byte[] bytes = textasset.bytes;

            Texture2D texture = Texture2DExt.CreateTexture2DFromWebP(bytes, lMipmaps: true, lLinear: false, lError: out Error lError);

            if (lError == Error.Success)
            {
                image.texture = texture;
            }
            else
            {
                Debug.LogError("Webp Load Error : " + lError.ToString());
            }
        }

        void LoadOrigin(RawImage image)
        {
            TextAsset textasset = Resources.Load<TextAsset>("origin");
            byte[] bytes = textasset.bytes;
            Texture2D texture = new Texture2D(512, 512, TextureFormat.RGBA32, mipChain: false, linear: false);
            texture.LoadImage(bytes);
            image.texture = texture;
        }
    }
}
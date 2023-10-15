using UnityEngine;
using UnityEngine.UI;
using WebP;

public class Example : MonoBehaviour
{
    public RawImage image1;
    public RawImage image2;
    public RawImage image3;

    void Start()
    {
        LoadOrigin(image1);
        LoadWebp(image2);
        LoadWebpUsingPool(image3);
    }

    void LoadWebpUsingPool(RawImage image)
    {
        byte[] bytePool = new byte[1024 * 1024 * 10];

        var textasset = Resources.Load<TextAsset>("webp");
        var webpBytes = textasset.bytes;

        Texture2DExt.GetWebPDimensions(webpBytes, out int width, out int height);
        
        Texture2D texture = Texture2DExt.CreateWebpTexture2D(width, height, isUseMipmap: true, isLinear: false);
        image.texture = texture;

        int numBytesRequired = Texture2DExt.GetRequireByteSize(width, height, isUseMipmap: true);
        
        Debug.Assert(bytePool.Length >= numBytesRequired);
        
        Texture2DExt.LoadTexture2DFromWebP(webpBytes, texture, lMipmaps: true, lLinear: true, bytePool, numBytesRequired);
    }

    void LoadWebp(RawImage image)
    {
        var textasset = Resources.Load<TextAsset>("webp");
        var bytes = textasset.bytes;

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
        var textasset = Resources.Load<TextAsset>("origin");
        var bytes = textasset.bytes;
        var texture = new Texture2D(512, 512, TextureFormat.RGBA32, mipChain: false, linear: false);
        texture.LoadImage(bytes);
        image.texture = texture;
    }
}

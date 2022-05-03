using System.IO;
using System.Net.WebSockets;
using UnityEditor;
using UnityEngine;
#if UNITY_2020_2_OR_NEWER
using UnityEditor.AssetImporters;
#else
using UnityEditor.Experimental.AssetImporters;
#endif

namespace WebP
{
    [ScriptedImporter(1, "webp")]
    public class WebPImporter : ScriptedImporter
    {
        const TextureFormat WEBP_LOAD_DEFAULT_TEXTURE_FORMAT = TextureFormat.RGBA32;

        [Tooltip("Is Generate Mipmap")]
        public bool Mipmap = false;

        [Tooltip("Is Linear ColorSpace")]
        public bool Linear = false;

        [Tooltip("Should it generate a sprite, alongside the base texture?")]
        public bool ShouldGenerateSprite = false;
        
        [Tooltip("Converted Texture Format")]
        public TextureFormat TextureFormat = WEBP_LOAD_DEFAULT_TEXTURE_FORMAT;

        public override void OnImportAsset(AssetImportContext ctx)
        {
            byte[] bytes = File.ReadAllBytes(ctx.assetPath);
            Texture2D webpTexture = Texture2DExt.CreateTexture2DFromWebP(bytes, lMipmaps: false, lLinear: Linear, lError: out Error lError, makeNoLongerReadable: false);
            if (lError != Error.Success)
            {
                Debug.LogError("Error decoding WebP texture: " + lError);
                Texture2D tex = new Texture2D(1, 1);
                tex.SetPixel(1, 1, Color.red);
                ctx.AddObjectToAsset("main", tex);
                ctx.SetMainObject(tex);
                return;
            }

            Texture2D targetTexture;
            if (Mipmap)
            {
                int width = webpTexture.width;
                int height = webpTexture.height;
                UnityEngine.Experimental.Rendering.GraphicsFormat graphicsFormat = UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetGraphicsFormat(TextureFormat.RGBA32, false);
                uint mipmapSize = UnityEngine.Experimental.Rendering.GraphicsFormatUtility.ComputeMipmapSize(width, height, graphicsFormat);
                int mipmapCount = (int)mipmapSize / (width * height);
                targetTexture = new Texture2D(webpTexture.width, webpTexture.height, TextureFormat, mipCount: mipmapCount, linear: Linear);
            }
            else
            {
                targetTexture = new Texture2D(webpTexture.width, webpTexture.height, TextureFormat, mipCount: -1, linear: Linear);
            }


            bool isSuccess = targetTexture.LoadImage(webpTexture.EncodeToPNG());
            if (!isSuccess)
            {
                Debug.LogError($"Error convert target texture format : {WEBP_LOAD_DEFAULT_TEXTURE_FORMAT} => {TextureFormat}");
                Texture2D tex = new Texture2D(1, 1);
                tex.SetPixel(1, 1, Color.red);
                ctx.AddObjectToAsset("main", tex);
                ctx.SetMainObject(tex);
                return;
            }

            EditorUtility.CompressTexture(targetTexture, TextureFormat, TextureCompressionQuality.Normal);
            ctx.AddObjectToAsset("main", targetTexture);
            ctx.SetMainObject(targetTexture);
            
            if (ShouldGenerateSprite) {
                Rect spriteRect = new Rect(Vector2.zero, new Vector2(webpTexture.width, webpTexture.height));
                Sprite sprite = Sprite.Create(targetTexture, spriteRect, Vector2.zero);
                sprite.name = Path.GetFileNameWithoutExtension(ctx.assetPath);
                ctx.AddObjectToAsset("sprite", sprite);
            }
        }
    }
}
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
        public bool mipMaps = true;
        public bool linear = false;
        public TextureFormat textureFormat = TextureFormat.DXT5;

        public override void OnImportAsset(AssetImportContext ctx)
        {
            var bytes = File.ReadAllBytes(ctx.assetPath);
            var webpTexture = Texture2DExt.CreateTexture2DFromWebP(bytes, lMipmaps: mipMaps, lLinear: linear, lError: out Error lError, makeNoLongerReadable: false);

            if (lError != Error.Success)
            {
                Debug.LogError("Error decoding WebP texture: " + lError);
                var tex = new Texture2D(1, 1);
                tex.SetPixel(1, 1, Color.red);
                ctx.AddObjectToAsset("main", tex);
                ctx.SetMainObject(tex);
                return;
            }

            ctx.AddObjectToAsset("main", webpTexture);
            ctx.SetMainObject(webpTexture);
            webpTexture.alphaIsTransparency = true;
            EditorUtility.CompressTexture(webpTexture, textureFormat, TextureCompressionQuality.Normal);
        }
    }
}
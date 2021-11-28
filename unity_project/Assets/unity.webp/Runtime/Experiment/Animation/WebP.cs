using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace WebP.Experiment.Animation
{
    /// <summary>
    /// Entry of all the loading logic.
    /// </summary>
    public class WebP
    {

#if USE_FAIRYGUI
        /// <summary>
        /// Async loading webp files, and return WebPRender for render.
        /// This function return NTextures which would work with fairygui: https://en.fairygui.com/
        /// </summary>
        /// <param name="url">Remote urls to download or project related absolute path(based on platform)</param>
        /// <returns>WebPRederer to provide NTexture for rendering</returns>
        public static async Task<WebPRendererWrapper<NTexture>> LoadNTexturesAsync(string url)
        {
            var bytes = await WebPLoader.Load(url);
            if (bytes == null || bytes.Length <= 0) return null;

            var sw = new Stopwatch();
            var textures = await WebPDecoderWrapper.Decode(bytes);
            var nTextures = textures.ConvertAll(ret =>
            {
                var (texture, timestamp) = ret;

                return (new NTexture(texture), timestamp);
            });
            var renderer = new WebPRendererWrapper<NTexture>(nTextures);
            Debug.Log($"[WebP] Decode webp into textures in: {sw.ElapsedMilliseconds}");
            sw.Stop();
            return renderer;
        }
#endif

        /// <summary>
        /// Async loading webp files, and return WebPRender for render.
        /// </summary>
        /// <param name="url">Remote urls to download or project related absolute path(based on platform)</param>
        /// <returns>WebPRederer to provide Texture for rendering</returns>
        public static async Task<WebPRendererWrapper<Texture2D>> LoadTexturesAsync(string url)
        {
            byte[] bytes = await WebPLoader.Load(url);
            if (bytes == null || bytes.Length <= 0)
            {
                return null;
            }

            List<(Texture2D, int)> textures = await WebPDecoderWrapper.Decode(bytes);
            WebPRendererWrapper<Texture2D> renderer = new WebPRendererWrapper<Texture2D>(textures);
            return renderer;
        }

        public static async Task<WebPRendererWrapper<Texture2D>> LoadTexturesAsync(byte[] bytes)
        {
            Assert.IsNotNull(bytes);

            List<(Texture2D, int)> textures = await WebPDecoderWrapper.Decode(bytes);
            WebPRendererWrapper<Texture2D> renderer = new WebPRendererWrapper<Texture2D>(textures);
            return renderer;
        }
    }
}
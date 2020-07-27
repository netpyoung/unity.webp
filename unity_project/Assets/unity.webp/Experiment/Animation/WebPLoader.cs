using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace WebP.Experiment.Animation
{

    /// <summary>
    /// WebP loader for loading assets, should be override to suit your own needs.
    /// </summary>
    public class WebPLoader
    {

        /// <summary>
        /// The actual function to load file from remote location or project related absolute path
        /// </summary>
        public static async Task<byte[]> Load(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                Debug.LogError("[WebP] Loading path can not be empty");
                return null;
            }
            Debug.Log($"[WebP] Try loading WebP file: {url}");

            byte[] bytes = null;

            if (url.Contains("//") || url.Contains("///"))
            {
                var www = await LoadAsync(url);
                bytes = www.downloadHandler.data;
            }
            else
            {
                try
                {
                    bytes = File.ReadAllBytes(url);
                }
                catch (Exception e)
                {
                    Debug.LogError($"[WebP] load error: {e.Message}");
                }
            }

            return bytes;
        }

        /// <summary>
        /// Example for async UnityWebRequest
        /// 
        /// This function won't work, just example!!!
        /// You should implement your own loading logic here!
        /// </summary>
        private static async Task<UnityWebRequest> LoadAsync(string path)
        {
            var www = new UnityWebRequest(path);
            var op = www.SendWebRequest();
            while (!op.isDone)
            {
                await Task.Delay(1000 / 60);
            }
            return www;
        }

    }
}
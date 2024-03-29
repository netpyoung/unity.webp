﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace WebP.Example
{
    public class EncodeToWebP : MonoBehaviour
    {
        public RawImage _img_From;
        public RawImage _img_To;

        void Start()
        {
            byte[] webpBytes = Resources.Load<TextAsset>("webp").bytes;
            LoadWebp(_img_From, webpBytes, isLinear: false);
            TestEncodeToWebP(_img_From, _img_To);
        }

        void LoadWebp(RawImage image, byte[] bytes, bool isLinear)
        {
            Error lError;
            Texture2D texture = Texture2DExt.CreateTexture2DFromWebP(bytes, lMipmaps: true, lLinear: isLinear, lError: out lError);
            if (lError != Error.Success)
            {
                Debug.LogError("Webp Load Error : " + lError.ToString());
                return;
            }

            image.texture = texture;
        }

        void TestEncodeToWebP(RawImage fromImage, RawImage toImage)
        {
            Texture texture = fromImage.texture;
            RenderTexture tmp = RenderTexture.GetTemporary(
                                texture.width,
                                texture.height,
                                0,
                                RenderTextureFormat.Default,
                                RenderTextureReadWrite.Linear);

            // Blit the pixels on texture to the RenderTexture
            Graphics.Blit(texture, tmp);
            // Backup the currently set RenderTexture
            RenderTexture previous = RenderTexture.active;
            // Set the current RenderTexture to the temporary one we created
            RenderTexture.active = tmp;

            // Create a new readable Texture2D to copy the pixels to it
            Texture2D myTexture2D = new Texture2D(texture.width, texture.height);

            // Copy the pixels from the RenderTexture to the new Texture
            myTexture2D.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
            myTexture2D.Apply();

            // Reset the active RenderTexture
            RenderTexture.active = previous;

            // Release the temporary RenderTexture
            RenderTexture.ReleaseTemporary(tmp);

            {// Flip updown.
             // ref: https://github.com/netpyoung/unity.webp/issues/25
                Color[] pixels = myTexture2D.GetPixels();
                Color[] pixelsFlipped = new Color[pixels.Length];
                int w = myTexture2D.width;
                int h = myTexture2D.height;
                for (int y = 0; y < h; y++)
                {
                    Array.Copy(pixels, y * h, pixelsFlipped, (h - y - 1) * w, w);
                }
                myTexture2D.SetPixels(pixelsFlipped);
            }

            byte[] bytes = myTexture2D.EncodeToWebP(25, out Error lError);
            if (lError != Error.Success)
            {
                Debug.LogError("Webp EncodeToWebP Error : " + lError.ToString());
                return;
            }
            LoadWebp(toImage, bytes, isLinear: true);
        }
    }
}
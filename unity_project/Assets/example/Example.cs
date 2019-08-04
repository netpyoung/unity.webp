using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebP;

public class Example : MonoBehaviour {
	public RawImage image1;
	public RawImage image2;

	void Start()
	{
		LoadOrigin (image1);
		LoadWebp (image2);
	}

	void LoadWebp (RawImage image) {
		var textasset = Resources.Load<TextAsset> ("webp");
		var bytes = textasset.bytes;

		Error lError;
		Texture2D texture = Texture2DExt.CreateTexture2DFromWebP(bytes, lMipmaps: true, lLinear: true, lError: out lError);

		if (lError == Error.Success) {
			image2.texture = texture;
		} else {
			Debug.LogError("Webp Load Error : " + lError.ToString());
		}
	}

	void LoadOrigin(RawImage image) {
		var textasset = Resources.Load<TextAsset> ("origin");
		var bytes = textasset.bytes;
		var texture = new Texture2D(512, 512, TextureFormat.RGBA32, mipChain: false, linear: false);
		texture.LoadImage (bytes);
		image.texture = texture;
	}
}

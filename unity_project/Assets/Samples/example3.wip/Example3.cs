using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Example3 : MonoBehaviour
{
    public RawImage RawImage;
    async void Start()
    {
        //// remote file loading
        //await LoadWebP("https://www.example.com/path/to/webp.webp");

        //// project related absolute path loading(can be found by File.ReadAllBytes)
        //await LoadWebP(Path.Combine(Application.streamingAssetsPath, "webp.webp"));

        await LoadWebP(Resources.Load<TextAsset>("cat").bytes);
    }

    WebP.Experiment.Animation.WebPRendererWrapper<Texture2D> mRenderer;

    private async Task LoadWebP(string url)
    {
        var renderer = await WebP.Experiment.Animation.WebP.LoadTexturesAsync(url);
        if (renderer != null)
        {
            renderer.OnRender += texture => OnWebPRender(texture, url);
        }
        mRenderer = renderer;
    }

    private async Task LoadWebP(byte[] bytes)
    {
        var renderer = await WebP.Experiment.Animation.WebP.LoadTexturesAsync(bytes);
        if (renderer != null)
        {
            renderer.OnRender += texture => OnWebPRender(texture, null);
        }
        mRenderer = renderer;
    }

    private void OnWebPRender(Texture texture, string url)
    {
        // handle the texture here, give the texture to Image for example
        RawImage.texture = texture;
    }

    private void OnApplicationQuit()
    {
        mRenderer?.Stop();
    }
}
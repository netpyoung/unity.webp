using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class Example3 : MonoBehaviour
{
    async void Start()
    {
        // remote file loading
        await LoadWebP("https://www.example.com/path/to/webp.webp");

        // project related absolute path loading(can be found by File.ReadAllBytes)
        await LoadWebP(Path.Combine(Application.streamingAssetsPath, "webp.webp"));
    }

    private async Task LoadWebP(string url)
    {
        var renderer = await WebP.Experiment.Animation.WebP.LoadTexturesAsync(url);
        if (renderer != null)
        {
            renderer.OnRender += texture => OnWebPRender(texture, url);
        }
    }

    private void OnWebPRender(Texture texture, string url)
    {
        // handle the texture here, give the texture to Image for example
    }
}
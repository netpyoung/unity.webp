# unity.webp

## What's this?

 This project was based in [octo-code/webp-unity3d](https://github.com/octo-code/webp-unity3d).

 I decided that there should be a simpler way and I created **unity.webp**, a plugin that helps you to use [webp](https://developers.google.com/speed/webp/) in your Unity3d projects in a clear and easy way and works in **iOS, Android, Windows, Linux** projects.

## prebuilt library

- prebuilt library are maintained by [prebuilt-libwebp](https://github.com/netpyoung/prebuilt-libwebp)
  - libwebp version v1.3.2

## installation

Choose your preference:

### Using OpenUPM installer

Download and install via the [Package Installer](http://package-installer.glitch.me/v1/installer/OpenUPM/com.netpyoung.webp?registry=https://package.openupm.com). No manual registry setup is needed. 

### Using OpenUPM manually

``` json
{
  "dependencies": {
    "com.netpyoung.webp": "0.3.12"
  },
  "scopedRegistries": [
    {
      "name": "OpenUPM",
      "url": "https://package.openupm.com",
      "scopes": [
        "com.netpyoung.webp",
        "org.nuget.system.runtime.compilerservices.unsafe"
      ]
    }
  ]
}
```

### Using UPM from GitHub and Unity NuGet

``` json
{
  "dependencies": {
    "com.netpyoung.webp": "https://github.com/netpyoung/unity.webp.git?path=unity_project/Assets/unity.webp#0.3.12"
  },
  "scopedRegistries": [
    {
      "name": "Unity NuGet",
      "url": "https://unitynuget-registry.azurewebsites.net",
      "scopes": [
        "org.nuget"
      ]
    }
  ]
}
```

## Example

- check [Samples/ directory](https://github.com/netpyoung/unity.webp/tree/master/unity_project/Assets/Samples)

## Demo

![animation.webp](./animation.webp)

## WebGL

- WebGL's [System.Threading.Tasks](https://docs.microsoft.com/dotnet/api/system.threading.tasks.task?view=net-6.0) async based logic is not stable. If you want to use async based logic for WebGL build, try to use [Cysharp/UniTask](https://github.com/Cysharp/UniTask).
  - ref: <https://forum.unity.com/threads/async-await-and-webgl-builds.472994/>
- For WebGL build, this repo copied [webmproject/libwebp](https://github.com/webmproject/libwebp) directly. It will be more cleanable if it can be support prebuilt. But It needs more investigate.


## stop to support .unitypackage

- This library depends on System.Runtime.CompilerServices.Unsafe. But with `package manager` and `.unitypackage` it is hard to maintain both. so I deprecated to support .unitypackage.

## LICENCE

### webp

- BSD

  ![webp](webplogo.png)

## Ref

- [octo-code/webp-unity3d](https://github.com/octo-code/webp-unity3d) - License: Apache License

# unity.webp

## What's this?

 This project was based in [octo-code/webp-unity3d](https://github.com/octo-code/webp-unity3d).

 I decided that there should be a simpler way and I created **unity.webp**, a plugin that helps you to use [webp](https://developers.google.com/speed/webp/) in your Unity3d projects in a clear and easy way and works in **iOS, Android, Windows, Linux** projects.

## prebuilt library

- prebuilt library are maintained by [prebuilt-libwebp](https://github.com/netpyoung/prebuilt-libwebp)
  - libwebp version v1.2.2

## installation

choose your preference

### using .unitypackage

- [Download this .unitypackage from Release Page](https://github.com/netpyoung/unity.webp/releases)

### using OpenUPM

As a shared dependency, this uses the openUPM package `com.system-community.systemruntimecompilerservicesunsafe`, you will need to add it to OpenUPM's scopes, and it will be pulled in by this package.

```json
{
  "dependencies": {
    "com.netpyoung.webp": "0.3.7"
  },
  "scopedRegistries": [
    {
      "name": "package.openupm.com",
      "url": "https://package.openupm.com",
      "scopes": [
        "com.netpyoung.webp",
        "com.system-community.systemruntimecompilerservicesunsafe"
      ]
    }
  ]
}
```

### using UPM

using `#{version}` for versioning.

ex)

``` json
"com.netpyoung.webp": "https://github.com/netpyoung/unity.webp.git?path=unity_project/Assets/unity.webp#0.3.7",
"com.system-community.systemruntimecompilerservicesunsafe": "https://github.com/system-community/SystemRuntimeCompilerServicesUnsafe.git?path=Assets/_Root#6.0.0",

```

## Example

- check [Samples/ directory](https://github.com/netpyoung/unity.webp/tree/master/unity_project/Assets/Samples)

## Demo

![animation.webp](./animation.webp)

## WebGL

- WebGL's [System.Threading.Tasks](https://docs.microsoft.com/dotnet/api/system.threading.tasks.task?view=net-6.0) async based logic is not stable. If you want to use async based logic for WebGL build, try to use [Cysharp/UniTask](https://github.com/Cysharp/UniTask).
  - ref: <https://forum.unity.com/threads/async-await-and-webgl-builds.472994/>
- For WebGL build, this repo copied [webmproject/libwebp](https://github.com/webmproject/libwebp) directly. It will be more cleanable if it can be support prebuilt. But It needs more investigate.

## LICENCE

### webp

- BSD

  ![webp](webplogo.png)

## Ref

- [octo-code/webp-unity3d](https://github.com/octo-code/webp-unity3d) - License: Apache License

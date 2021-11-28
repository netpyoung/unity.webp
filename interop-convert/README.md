# interop convert

- Instead of manually write interop code, there are some interop generator like [java's panama](https://openjdk.java.net/projects/panama/).
  - <https://github.com/microsoft/ClangSharp>
  - <https://github.com/mono/CppSharp>

``` cmd
// Build Tools Command Prompt

> dotnet tool install --global ClangSharpPInvokeGenerator --version 12.0.0-beta2
> cp ClangShap_libwebp.rsp      webp/ClangShap_libwebp.rsp
> cp ClangShap_libwebpdemux.rsp webp/ClangShap_libwebpdemux.rsp

> ClangSharpPInvokeGenerator @ClangShap_libwebp.rsp
> ClangSharpPInvokeGenerator @ClangShap_libwebpdemux.rsp
```

## Do Change

- Generator itself is not perfect so It needs to manually modify some codes.
- added: System.Runtime.CompilerServices.Unsafe.dll - 6.0.0
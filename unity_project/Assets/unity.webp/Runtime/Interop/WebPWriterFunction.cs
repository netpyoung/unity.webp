using System;
using System.Runtime.InteropServices;

namespace unity.libwebp.Interop
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int WebPWriterFunction([NativeTypeName("const uint8_t *")] byte* data, [NativeTypeName("size_t")] UIntPtr data_size, [NativeTypeName("const WebPPicture *")] WebPPicture* picture);
}

using System.Runtime.InteropServices;

namespace unity.libwebp.Interop
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int WebPProgressHook(int percent, [NativeTypeName("const WebPPicture *")] WebPPicture* picture);
}

using System.Runtime.InteropServices;

namespace unity.libwebp.Interop
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void BlendRowFunc([NativeTypeName("uint32_t *const")] uint* param0, [NativeTypeName("const uint32_t *const")] uint* param1, int param2);
}

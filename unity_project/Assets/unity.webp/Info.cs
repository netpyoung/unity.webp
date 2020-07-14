
using System;
using System.Text;
using System.Collections.Generic;
using WebP.Extern;

namespace WebP
{
    public class Info
    {
        public static string GetDecoderVersion()
        {
            
            uint v = (uint)NativeBindings.WebPGetDecoderVersion();
            var revision = v % 256;
            var minor = (v >> 8) % 256;
            var major = (v >> 16) % 256;
            return major + "." + minor + "." + revision;
        }

        public static string GetEncoderVersion()
        {
            uint v = (uint)NativeBindings.WebPGetEncoderVersion();
            var revision = v % 256;
            var minor = (v >> 8) % 256;
            var major = (v >> 16) % 256;
            return major + "." + minor + "." + revision;
        }
    }
}

using System.Runtime.InteropServices;

#pragma warning disable 1591

namespace WebP.NativeWrapper.Dec
{
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct WebPConfig
    {

        /// int
        public int lossless;

        /// float
        public float quality;

        /// int
        public int method;

        /// WebPImageHint->Anonymous_838f22f5_6f57_48a0_9ecb_8eec917009f9
        public WebPImageHint image_hint;

        /// int
        public int target_size;

        /// float
        public float target_PSNR;

        /// int
        public int segments;

        /// int
        public int sns_strength;

        /// int
        public int filter_strength;

        /// int
        public int filter_sharpness;

        /// int
        public int filter_type;

        /// int
        public int autofilter;

        /// int
        public int alpha_compression;

        /// int
        public int alpha_filtering;

        /// int
        public int alpha_quality;

        /// int
        public int pass;

        /// int
        public int show_compressed;

        /// int
        public int preprocessing;

        /// int
        public int partitions;

        /// int
        public int partition_limit;

        public int emulate_jpeg_size;

        public int thread_level;

        public int low_memory;

        /// uint32_t[5]
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.U4)]
        public uint[] pad;
    }
}

#pragma warning restore 1591

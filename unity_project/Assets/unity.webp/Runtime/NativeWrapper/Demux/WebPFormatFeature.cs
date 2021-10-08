#pragma warning disable 1591

namespace WebP.NativeWrapper.Demux
{
    public enum WebPFormatFeature
    {
        WEBP_FF_FORMAT_FLAGS,      // bit-wise combination of WebPFeatureFlags
                                   // corresponding to the 'VP8X' chunk (if present).
        WEBP_FF_CANVAS_WIDTH,
        WEBP_FF_CANVAS_HEIGHT,
        WEBP_FF_LOOP_COUNT,        // only relevant for animated file
        WEBP_FF_BACKGROUND_COLOR,  // idem.
        WEBP_FF_FRAME_COUNT        // Number of frames present in the demux object.
                                   // In case of a partial demux, this is the number
                                   // of frames seen so far, with the last frame
                                   // possibly being partial.
    };
}
namespace WebP.NativeWrapper.Demux
{
    // Dispose method (animation only). Indicates how the area used by the current
    // frame is to be treated before rendering the next frame on the canvas.
    public enum WebPMuxAnimDispose
    {
        WEBP_MUX_DISPOSE_NONE, // Do not dispose.
        WEBP_MUX_DISPOSE_BACKGROUND // Dispose to background color.
    }

}
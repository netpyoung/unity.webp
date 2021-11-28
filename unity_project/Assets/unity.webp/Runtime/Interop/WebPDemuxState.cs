namespace unity.libwebp.Interop
{
    public enum WebPDemuxState
    {
        WEBP_DEMUX_PARSE_ERROR = -1,
        WEBP_DEMUX_PARSING_HEADER = 0,
        WEBP_DEMUX_PARSED_HEADER = 1,
        WEBP_DEMUX_DONE = 2,
    }
}

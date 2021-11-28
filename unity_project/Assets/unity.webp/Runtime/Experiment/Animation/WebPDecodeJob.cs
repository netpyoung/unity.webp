using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Unity.Jobs;
using UnityEngine;
using unity.libwebp.Interop;
using unity.libwebp;

namespace WebP.Experiment.Animation
{

    /// <summary>
    /// WebP decoding process which optimized by using threads.
    /// </summary>
    public static class WebPDecodeJob
    {

        public static Task<List<(byte[], int)>> StartJob(IntPtr decoder, int count)
        {
            Debug.Log($"[WebPDecodeJob] Starting multi-threading webp decode");
            var managedBytes = new ConcurrentDictionary<int, (byte[], int)>();
            var task = new WebPDecodeTask(decoder, managedBytes);
            var taskRef = GCHandle.Alloc(task);
            var job = new WebPDecoderJob { Task = taskRef };
            var handler = job.Schedule(count, 1);
            var handled = false;

            while (!handled)
            {
                handled = handler.IsCompleted;
            }

            handler.Complete();
            taskRef.Free();

            if (managedBytes.Count <= 0 || managedBytes.Values.Count <= 0) return Task.FromResult(new List<(byte[], int)>());

            var sorted = managedBytes.OrderBy(kvp => kvp.Key).ToList().ConvertAll(kvp => kvp.Value);
            return Task.FromResult(sorted);
        }
    }

    internal interface IWebPDecodeTask
    {
        void Excute(int index);
    }

    internal unsafe class WebPDecodeTask : IWebPDecodeTask
    {
        // have to use Concurrent structures to prevent concurrent race exceptions
        private ConcurrentDictionary<int, (byte[], int)> _managedBytes;
        private WebPAnimDecoder* _decoder;

        public WebPDecodeTask(IntPtr decoder, ConcurrentDictionary<int, (byte[], int)> managedBytes)
        {
            _managedBytes = managedBytes;
            _decoder = (WebPAnimDecoder * )decoder;
        }

        /// <summary>
        /// The actual decode process which make the magic happen
        /// </summary>
        public unsafe void Excute(int index)
        {
            // get the demuxer (which contains almost all the information about the WebP file)
            var demuxer = NativeLibwebpdemux.WebPAnimDecoderGetDemuxer(_decoder);
            WebPIterator iter = new WebPIterator();
            // use the demuxer and WebPIterator to extract one frame from the WebP data
            var success = NativeLibwebpdemux.WebPDemuxGetFrame(demuxer, index + 1, &iter);
            if (success == 0)
            {
                Debug.LogError($"[WebPDecodeTask] Decode frame data {index} failed");
                return;
            }

            // use native memory marshal to minimize the momory consumption from native memory to csharp managed memory
            var size = (int)iter.fragment.size;
            var bytes = new byte[size];
            Marshal.Copy((IntPtr)iter.fragment.bytes, bytes, 0, size);

            // parse the memory data (structured bitmap data) to texture bytes, which can be used to parse into Texture
            var loadedBytes = Texture2DExt.LoadRGBAFromWebP(bytes, ref iter.width, ref iter.height, false, out var error, null);
            if (error != Error.Success)
            {
                Debug.LogError($"[WebPDecodeTask] Decode texture bytes {index} failed");
                return;
            }
            // sequential added to the concurrent dictionary
            // if not using concurrent dict, exceptions may happen 
            _managedBytes?.TryAdd(index, (loadedBytes, iter.duration));
            // release the iterator pointer
            NativeLibwebpdemux.WebPDemuxReleaseIterator(&iter);
        }
    }

    /// <summary>
    /// Convinience for task usage
    /// </summary>
    internal struct WebPDecoderJob : IJobParallelFor
    {

        public GCHandle Task;

        public void Execute(int index)
        {
            var task = (IWebPDecodeTask)Task.Target;
            task?.Excute(index);
        }
    }
}
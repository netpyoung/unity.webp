using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebP.Experiment.Animation
{

    /// <summary>
    /// WebP render logic based on parsed WebP animation list.
    /// </summary>
    public class WebPRendererWrapper<T>
    {
        
        public Action<T> OnRender;

        private List<(T, int)> _animation;

        private CancellationTokenSource _cancellationTokenSource;
        
        private bool _playing;
        private bool _stopped;
        private int _frame;
        private int _loop;

        public WebPRendererWrapper(List<(T, int)> animation)
        {
            _animation = animation;
            Reset();
            Start();
        }
        
        /// <summary>
        /// total frames
        /// </summary>
        public int totalFrame
        {
            get => this._animation.Count;

        }

        /// <summary>
        /// current frame
        /// </summary>
        public int frame
        {
            get => _frame;
            set
            {
                if (_frame == value) return;
                
                _frame = value;
                Stop();
                Start();
            }
        }

        /// <summary>
        /// loop times, defaults to -1, which means infinite loops
        /// </summary>
        public int loop
        {
            get => _loop;
            set => _loop = value;
        }

        public void Start()
        {
            if (_playing) return;
            _playing = true;
            _stopped = false;
            _cancellationTokenSource = new CancellationTokenSource();
            
            RenderAnimation();
        }
        
        private async void RenderAnimation()
        {
            if (_animation == null || _animation.Count == 0) return;
            
            for (; _animation != null && _frame < _animation.Count; ++_frame)
            {
                // if stopped, breaks
                if (_stopped) break;
                
                // actual render
                var (texture, timestamp) = _animation[_frame];
                OnRender?.Invoke(texture);
                
                // allows for canceling play at any time, by catch exceptions between loops
                try
                {
                    await Task.Delay(timestamp, _cancellationTokenSource.Token);
                }
                catch (TaskCanceledException)
                {
                    break;
                }

                // if not last frame, go on; otherwise keep looping
                if (_animation == null || _animation.Count <= 0 || _frame != _animation.Count - 1) continue;
                
                // dec loop times
                if (_loop > 0)
                    _loop--;

                // if loop time equals 0, stop
                if (_loop == 0) break;
                
                // restart from the first frame
                _frame = 0;
            }
        }

        public void Stop()
        {
            _stopped = true;
            _playing = false;
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }

        public void Reset()
        {
            _frame = 0;
            _loop = -1;
            _playing = false;
            _stopped = false;
        }

        public void Dispose()
        {
            Reset();
            _animation = null;
            OnRender = null;
            // just in case 
            if (_cancellationTokenSource == null) return;
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }
    }
}
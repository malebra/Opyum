using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio;

namespace Opyum.StandardPlayback
{
    public partial class StandardPlayer
    {
        private WaveOut waveOut;
        private BufferedWaveProvider bufferedWaveProvider;

        private delegate void StreamDisposer();
        StreamDisposer sdd;

        MemoryAudioCache sourceStream;

        enum StreamState
        {
            Playing = 1,
            Paused = 2,
            Stopped = 4
        }

        private StreamState _streamState { get ; set; }

        private void _InitializeTimer()
        {
            
        }

        private WaveOut CreateWaveOut() => new WaveOut();


        private static IMp3FrameDecompressor CreateFrameDecompressor(Mp3Frame frame)
        {
            WaveFormat waveFormat = new Mp3WaveFormat(frame.SampleRate, frame.ChannelMode == ChannelMode.Mono ? 1 : 2,
                frame.FrameLength, frame.BitRate);
            return new AcmMp3FrameDecompressor(waveFormat);
        }

        //private void StreamMp3(object state)
        //{
        //    //fullyDownloaded = false;
        //    var url = (string)state;
        //    //location = url;

        //    var buffer = new byte[16384 * 4]; // needs to be big enough to hold a decompressed frame

        //    long counter = 0;

        //    IMp3FrameDecompressor decompressor = null;
        //    try
        //    {
        //        //opens a stream from the file
        //        using (MemoryCache responseStream = MemoryCache.Create(url))
        //        {
        //            if (sdd == null)
        //            {
        //                sdd += responseStream.Dispose;
        //            }
        //            //var readFullyStream = new ReadAheadStream(responseStream);
        //            do
        //            {
        //                //while (responseStream.BufferEmpty)
        //                //{

        //                //}
        //                Mp3Frame frame;
        //                try
        //                {
        //                    frame = Mp3Frame.LoadFromStream(responseStream);
        //                }
        //                catch (EndOfStreamException)
        //                {
        //                    //fullyDownloaded = true;
        //                    //reached the end of the MP3 file / stream
        //                    break;
        //                }
        //                if (frame == null)
        //                {
        //                    break;
        //                }
        //                if (decompressor == null)
        //                {
        //                    //don't think these details matter too much - just help ACM select the right codec
        //                    //however, the buffered provider doesn't know what sample rate it is working at
        //                    //until we have a frame
        //                    decompressor = CreateFrameDecompressor(frame);
        //                    bufferedWaveProvider = new BufferedWaveProvider(decompressor.OutputFormat);
        //                    bufferedWaveProvider.BufferDuration = TimeSpan.FromSeconds(20); // allow us to get well ahead of ourselves

        //                }
        //                int decompressed = decompressor.DecompressFrame(frame, buffer, 0);
        //                counter += decompressed;
        //                Debug.WriteLine(String.Format("Decompressed a frame {0}", decompressed));
        //                try
        //                {
        //                    bufferedWaveProvider.AddSamples(buffer, 0, decompressed);
        //                }
        //                catch (Exception)
        //                {
                            
        //                }
        //                if (waveOut == null & decompressed > 0)
        //                {
        //                    StartPlaying(bufferedWaveProvider);
        //                }


        //            } while (true);
        //            Debug.WriteLine("Exiting");
        //            //was doing this in a finally block, but for some reason
        //            //we are hanging on response stream .Dispose so never get there
        //            decompressor.Dispose();
        //        }
        //    }
        //    finally
        //    {
        //        if (decompressor != null)
        //        {
        //            decompressor.Dispose();
        //        }
        //    }
        //}


        private void StreamMp3(object state)
        {
            IWaveProvider _waveProvider;
            sourceStream = MemoryAudioCache.Create((string)state);
            //_waveProvider = new RawSourceWaveStream(sourceStream, sourceStream.GetWaveFormat());
            
            StartPlaying(new RawSourceWaveStream(sourceStream, sourceStream.GetWaveFormat()));
        }

        /// <summary>
        /// Creates a new <see cref="WaveOut"></see> device and starts playing from the provided <see cref="IWaveProvider"/>  
        /// </summary>
        /// <param name="waveProvider"></param>
        private void StartPlaying(IWaveProvider waveProvider)
        {
            if (waveOut != null)
            {
                waveOut.Dispose();
            }
            waveOut = CreateWaveOut();
            waveOut.Init(waveProvider);
            waveOut.Play();
            _streamState = StreamState.Playing;
        }


        public void StartStream(string url)
        {
            ThreadPool.QueueUserWorkItem(StreamMp3, url);
        }

        public void PlayPauseStream()
        {
            if (waveOut != null)
            {
                if (_streamState == StreamState.Playing)
                {
                    waveOut?.Pause();
                    _streamState = StreamState.Paused;
                }
                else if (_streamState == StreamState.Paused)
                {
                    waveOut?.Play();
                    _streamState = StreamState.Playing;
                }
            }
        }
    }

        
}

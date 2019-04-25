//using System;
//using System.IO;
//using System.Threading;
//using System.Diagnostics;
//using NAudio.Wave;

//namespace Opyum.StandardPlayback
//{
//    public partial class StandardPlayer
//    {
//        enum StreamingPlaybackState
//        {
//            Stopped,
//            Playing,
//            Buffering,
//            Paused
//        }

//        Object thread_baton = new Object();

//        //the timer for
//        private Timer timer1;
//        //check if timer is enabled
//        private bool timer1Enabled;
//        //points out the amount of buffered space
//        private int progressBarBufferValue = 0;

//        private BufferedWaveProvider bufferedWaveProvider;
//        //player
//        private IWavePlayer waveOut;
//        private volatile StreamingPlaybackState playbackState;
//        private volatile bool fullyDownloaded;
//        private VolumeWaveProvider16 volumeProvider;

//        private string location = String.Empty;

//        delegate void ShowErrorDelegate(string message);
//        private Timer volumeTimer;


//        private void _InitializeTimer()
//        {
//            timer1 = new Timer((TimerCallback)timer1_Tick, state: null, dueTime: 0, period: 250);
//        }




//        public void PlayStream(string file)
//        {
//            if (playbackState == StreamingPlaybackState.Stopped)
//            {
//                playbackState = StreamingPlaybackState.Buffering;
//                bufferedWaveProvider = null;
//                ThreadPool.QueueUserWorkItem(StreamMp3, file);
//                timer1Enabled = true;
//            }
//            else if (playbackState == StreamingPlaybackState.Paused)
//            {
//                playbackState = StreamingPlaybackState.Buffering;
//            }
//        }




//        private void StreamMp3(object state)
//        {
//            fullyDownloaded = false;
//            var url = (string)state;
//            location = url;

//            var buffer = new byte[16384 * 4]; // needs to be big enough to hold a decompressed frame

//            IMp3FrameDecompressor decompressor = null;
//            try
//            {
//                //opens a stream from the file
//                //using (MemoryCache responseStream = MemoryCache.Create(url))
//                using (var responseStream = new FileStream(url, FileMode.Open))
//                {
//                    var readFullyStream = new ReadAheadStream(responseStream);
//                    do
//                    {
//                        if (IsBufferNearlyFull)
//                        {
//                            Debug.WriteLine("Buffer getting full, taking a break");
//                            Thread.Sleep(500);
//                        }
//                        else
//                        {
//                            Mp3Frame frame;
//                            try
//                            {
//                                frame = Mp3Frame.LoadFromStream(readFullyStream);
//                            }
//                            catch (EndOfStreamException)
//                            {
//                                fullyDownloaded = true;
//                                // reached the end of the MP3 file / stream
//                                break;
//                            }
//                            if (frame == null) break;
//                            if (decompressor == null)
//                            {
//                                // don't think these details matter too much - just help ACM select the right codec
//                                // however, the buffered provider doesn't know what sample rate it is working at
//                                // until we have a frame
//                                decompressor = CreateFrameDecompressor(frame);
//                                bufferedWaveProvider = new BufferedWaveProvider(decompressor.OutputFormat);
//                                bufferedWaveProvider.BufferDuration =
//                                    TimeSpan.FromSeconds(20); // allow us to get well ahead of ourselves
//                                //this.bufferedWaveProvider.BufferedDuration = 250;
//                            }
//                            int decompressed = decompressor.DecompressFrame(frame, buffer, 0);
//                            //Debug.WriteLine(String.Format("Decompressed a frame {0}", decompressed));
//                            bufferedWaveProvider.AddSamples(buffer, 0, decompressed);
//                        }

//                    } while (playbackState != StreamingPlaybackState.Stopped);
//                    Debug.WriteLine("Exiting");
//                    // was doing this in a finally block, but for some reason
//                    // we are hanging on response stream .Dispose so never get there
//                    decompressor.Dispose();
//                }
//            }
//            finally
//            {
//                if (decompressor != null)
//                {
//                    decompressor.Dispose();
//                }
//            }
//        }




//        private static IMp3FrameDecompressor CreateFrameDecompressor(Mp3Frame frame)
//        {
//            WaveFormat waveFormat = new Mp3WaveFormat(frame.SampleRate, frame.ChannelMode == ChannelMode.Mono ? 1 : 2,
//                frame.FrameLength, frame.BitRate);
//            return new AcmMp3FrameDecompressor(waveFormat);
//        }




//        private bool IsBufferNearlyFull
//        {
//            get
//            {
//                return bufferedWaveProvider != null &&
//                       bufferedWaveProvider.BufferLength - bufferedWaveProvider.BufferedBytes
//                       < bufferedWaveProvider.WaveFormat.AverageBytesPerSecond / 4;
//            }
//        }




//        private void buttonPlay_Click(object sender, EventArgs e)
//        {
//            if (playbackState == StreamingPlaybackState.Stopped)
//            {
//                playbackState = StreamingPlaybackState.Buffering;
//                bufferedWaveProvider = null;
//                ThreadPool.QueueUserWorkItem(StreamMp3, location);
//                timer1Enabled = true;
//            }
//            else if (playbackState == StreamingPlaybackState.Paused)
//            {
//                playbackState = StreamingPlaybackState.Buffering;
//            }
//        }




//        private void StopPlayback()
//        {
//            if (playbackState != StreamingPlaybackState.Stopped)
//            {

//                playbackState = StreamingPlaybackState.Stopped;
//                if (waveOut != null)
//                {
//                    waveOut.Stop();
//                    waveOut.Dispose();
//                    waveOut = null;
//                }
//                timer1Enabled = false;
//                // n.b. streaming thread may not yet have exited
//                Thread.Sleep(500);
//                ShowBufferState(0);
//            }
//        }




//        private void ShowBufferState(double totalSeconds)
//        {
//            progressBarBufferValue = (int)(totalSeconds * 1000);
//        }




//        private void timer1_Tick(object sender)
//        {
//            if (timer1Enabled)
//            {
//                if (playbackState != StreamingPlaybackState.Stopped)
//                {
//                    lock (thread_baton)
//                    {
//                        if (waveOut == null && bufferedWaveProvider != null)
//                        {
//                            Debug.WriteLine("Creating WaveOut Device");
//                            waveOut = CreateWaveOut();
//                            waveOut.PlaybackStopped += OnPlaybackStopped;
//                            volumeProvider = new VolumeWaveProvider16(bufferedWaveProvider);
//                            volumeProvider.Volume = 1.0f;
//                            waveOut.Init(bufferedWaveProvider);
//                        }
//                        else if (bufferedWaveProvider != null)
//                        {
//                            var bufferedSeconds = bufferedWaveProvider.BufferedDuration.TotalSeconds;
//                            ShowBufferState(bufferedSeconds);
//                            // make it stutter less if we buffer up a decent amount before playing
//                            if (bufferedSeconds < 0.5 && playbackState == StreamingPlaybackState.Playing && !fullyDownloaded)
//                            {
//                                PauseStream();
//                            }
//                            else if (bufferedSeconds > 4 && playbackState == StreamingPlaybackState.Buffering)
//                            {
//                                Play();
//                            }
//                            else if (fullyDownloaded && bufferedSeconds == 0)
//                            {
//                                Debug.WriteLine("Reached end of stream");
//                                StopPlayback();
//                            }
//                        }
//                    }

//                }
//            }
//        }


//        private void Play()
//        {
//            waveOut.Play();
//            Debug.WriteLine(String.Format("Started playing, waveOut.PlaybackState={0}", waveOut.PlaybackState));
//            playbackState = StreamingPlaybackState.Playing;
//        }




//        private void PauseStream()
//        {
//            playbackState = StreamingPlaybackState.Paused;
//            waveOut.Pause();
//            Debug.WriteLine(String.Format("Paused to buffer, waveOut.PlaybackState={0}", waveOut.PlaybackState));
//        }




//        private IWavePlayer CreateWaveOut()
//        {
//            return new WaveOut();
//        }




//        private void MP3StreamingPanel_Disposing(object sender, EventArgs e)
//        {
//            StopPlayback();
//        }




//        private void buttonPause_Click(object sender, EventArgs e)
//        {
//            if (playbackState == StreamingPlaybackState.Playing || playbackState == StreamingPlaybackState.Buffering)
//            {
//                waveOut.Pause();
//                Debug.WriteLine(String.Format("User requested Pause, waveOut.PlaybackState={0}", waveOut.PlaybackState));
//                playbackState = StreamingPlaybackState.Paused;
//            }
//        }




//        private void buttonStop_Click(object sender, EventArgs e)
//        {
//            StopPlayback();
//        }




//        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
//        {
//            Debug.WriteLine("Playback Stopped");
//            if (e.Exception != null)
//            {
//                throw e.Exception;//MessageBox.Show(String.Format("Playback Error {0}", e.Exception.Message));
//            }
//        }

//        void OnVolumeChange(object sender, EventArgs e)
//        {
//            if (volumeProvider != null)
//            {
//                volumeProvider.Volume = Volume;
//            }
//        }

//    }


//}


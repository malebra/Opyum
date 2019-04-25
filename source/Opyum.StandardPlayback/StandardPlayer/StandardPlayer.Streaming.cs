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
        


        private void _InitializeTimer()
        {

        }

        private void CreateWaveOut() => new WaveOut();


        private static IMp3FrameDecompressor CreateFrameDecompressor(Mp3Frame frame)
        {
            WaveFormat waveFormat = new Mp3WaveFormat(frame.SampleRate, frame.ChannelMode == ChannelMode.Mono ? 1 : 2,
                frame.FrameLength, frame.BitRate);
            return new AcmMp3FrameDecompressor(waveFormat);
        }

        private void StreamMp3(object state)
        {
            //fullyDownloaded = false;
            var url = (string)state;
            //location = url;

            var buffer = new byte[16384 * 4]; // needs to be big enough to hold a decompressed frame

            IMp3FrameDecompressor decompressor = null;
            try
            {
                //opens a stream from the file
                using (MemoryCache responseStream = MemoryCache.Create(url))
                {
                    var readFullyStream = new ReadAheadStream(responseStream);
                    do
                    {
                        Mp3Frame frame;
                        try
                        {
                            frame = Mp3Frame.LoadFromStream(readFullyStream);
                        }
                        catch (EndOfStreamException)
                        {
                            //fullyDownloaded = true;
                            //reached the end of the MP3 file / stream
                            break;
                        }
                        if (frame == null) break;
                        if (decompressor == null)
                        {
                            //don't think these details matter too much - just help ACM select the right codec
                            //however, the buffered provider doesn't know what sample rate it is working at
                            //until we have a frame
                            decompressor = CreateFrameDecompressor(frame);
                            bufferedWaveProvider = new BufferedWaveProvider(decompressor.OutputFormat) { BufferDuration = 250 };
                            bufferedWaveProvider.BufferDuration = TimeSpan.FromSeconds(20); // allow us to get well ahead of ourselves

                        }
                        int decompressed = decompressor.DecompressFrame(frame, buffer, 0);
                        Debug.WriteLine(String.Format("Decompressed a frame {0}", decompressed));
                        bufferedWaveProvider.AddSamples(buffer, 0, decompressed);

                    } while (true);
                    Debug.WriteLine("Exiting");
                    //was doing this in a finally block, but for some reason
                    //we are hanging on response stream .Dispose so never get there
                    decompressor.Dispose();
                }
            }
            finally
            {
                if (decompressor != null)
                {
                    decompressor.Dispose();
                }
            }
        }
    }

        
}

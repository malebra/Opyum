using System;
using System.IO;
using System.Security;
using System.Text;
using System.Threading;
using NAudio.Wave;

namespace Opyum.StandardPlayback
{
    /// <summary>
    /// Used for caching files into memory
    /// </summary>
    public class AudioFileMemoryStream : Stream, IDisposable
    {
        protected long _position = 0;
        protected byte[] memoryBuffer = new byte[0];
        protected int tempBufferSize = 1024 * 4;

        public BufferingState BufferingStatus { get; private set; } = 0;
        public AudioFileType FileType { get; private set; } = 0;
        public string FilePath { get; private set; } = String.Empty;

        Object load_lock = new Object();
        Mp3Frame frame = null;

        public enum BufferingState
        {
            Empty = 0,
            Buffering = 1,
            Done = 2
        }

        public enum AudioFileType
        {
            Unknown = 0,
            Mp3 = 1,
            Wave = 2
        }


        /// <summary>
        /// Returns boolen true if stream can be read
        /// </summary>
        public override bool CanRead => true;
        /// <summary>
        /// Returns boolen true if stream can seek
        /// </summary>
        public override bool CanSeek => true;
        /// <summary>
        /// Returns boolen true if stream can be written
        /// </summary>
        public override bool CanWrite => false;
        /// <summary>
        /// Returns the number of bytes buffered in the memory
        /// </summary>
        public override long Length => memoryBuffer == null ? 0 : memoryBuffer.Length;
        /// <summary>
        /// Gets or sets the current position (the current byte) in the memory buffer
        /// </summary>
        public override long Position { get => _position; set => _position = value; }
        /// <summary>
        /// Gets or sets percentage of the current position in the buffer
        /// </summary>
        public double Percentage
        {
            get => Length == 0 ? 0 : (double)Position / (double)Length;
            set
            {
                if (memoryBuffer != null && Length != 0)
                {
                    _position = (long)(value * (double)Length);
                }

            }
        }


        //public bool BufferEmpty => memoryBuffer == null ? true : memoryBuffer.Length > 1024*64 ? false : true; 

        protected AudioFileMemoryStream()
        {

        }

        /// <summary>Returns the internal buffer.</summary>
        public virtual byte[] GetBuffer() => memoryBuffer;


        /// <summary>Sets the current position of the stream.
        /// <para>Positive and negative offsets are acceptable in differet contexts.</para>
        /// <para>If the position overshoots, it will be corrected to the stream's end.</para>
        /// <para>If the position undershoots, it will be corrected to the stream's start.</para>
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            long temp = 0;
            if (origin == SeekOrigin.Begin)
            {
                _position = offset < 0 ? 0 : offset > Length ? Length : offset;
            }
            else if (origin == SeekOrigin.End)
            {
                temp = Length - offset;
                _position = temp > Length ? Length : temp < 0 ? 0 : temp;
            }
            else
            {
                temp = _position + offset;
                _position = temp > Length ? Length : temp < 0 ? 0 : temp;
            }

            return _position;
        }



        /// <summary>
        /// Reads the cached data from the memory into the buffer.
        /// </summary>
        /// <param name="buffer">The buffer the data is being coppied to.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin storing the data read from the current stream.</param>
        /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (memoryBuffer == null || buffer == null)
            {
                throw new ArgumentNullException();
            }

            int toCopy = memoryBuffer.Length - _position < (long)count ? (int)(memoryBuffer.Length - _position) : count;
            Array.Copy(memoryBuffer, _position, buffer, offset, toCopy);


            _position += toCopy;

            return toCopy;
        }


        /// <summary>
        /// Loads the file into the memory buffer.
        /// </summary>
        /// <param name="file">the file path.</param>
        /// <exception cref="Exception">Unhandled exceptions</exception>
        /// <exception cref="ArgumentNullException"></exception>
        protected virtual void Load(string file)
        {
            FileType = System.IO.Path.GetExtension(file) == ".mp3" ? AudioFileType.Mp3 :
                System.IO.Path.GetExtension(file) == ".wav" ? AudioFileType.Wave : AudioFileType.Unknown;
            FilePath = file;


            if (file == null)
            {
                throw new ArgumentNullException();
            }
            lock (load_lock)
            {
                int point = 0;
                byte[] tempBuffer = new byte[tempBufferSize];
                int readBytes = 0;

                memoryBuffer = new byte[(new FileInfo(FilePath)).Length];


                BufferingStatus = BufferingState.Buffering;

                #region Dynamic_Loading_Into_Memory

                MemoryStream ms = new MemoryStream(memoryBuffer);
                ms.Flush();
                using (Stream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    BufferingStatus = BufferingState.Buffering;
                    do
                    {
                        readBytes = fs.Read(tempBuffer, 0, tempBufferSize);
                        if (readBytes != 0)
                        {
                            ms.Write(tempBuffer, 0, readBytes);
                            point += readBytes;
                        }
                        if (frame == null)
                        {
                            frame = Mp3Frame.LoadFromStream(this);
                        }
                    } while (readBytes > 0);
                    tempBuffer = null;
                }
                BufferingStatus = BufferingState.Done;

                ms.Dispose();
                GC.Collect();

                #endregion

            }
        }

        /// <summary>
        /// Returns a new <see cref="WaveFormat"/> depending on the file in the memory.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual WaveFormat GetWaveFormat()
        {


            WaveFormat format;
            if (FileType == AudioFileType.Mp3)
            {
                int counter = 0;
                while (frame == null)
                {
                    frame = Mp3Frame.LoadFromStream(this);
                    Thread.Sleep(50);
                    counter++;
                    if (counter > 100)
                    {
                        throw new ArgumentNullException();
                    }
                }

                format = new Mp3WaveFormat(frame.SampleRate, frame.ChannelMode == ChannelMode.Mono ? 1 : 2,
                    frame.FrameLength, frame.BitRate);
            }
            else//if (_fileType == AudioFileType.Wave)
            {
                format = (new WaveFileReader(FilePath)).WaveFormat;
            }
            return format;
        }



        #region Garbage_COllectiong

        /// <summary>
        /// Will be called when the <see cref="Dispose()"/> function is called.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                memoryBuffer = null;
            }
        }

        #endregion

        #region NOT_IMPLEMENTED

        /// <summary>
        /// This function is not implemented
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="InvalidOperationException">Will always throw an exception</exception>
        public override void SetLength(long value)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// This function is not implemented
        /// </summary>
        /// <exception cref="InvalidOperationException">Will always throw this exception</exception>
        public override void Flush()
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// This function is not implemented
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <exception cref="InvalidOperationException">Will always throm invalid operation exception</exception>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new InvalidOperationException();
        }

        #endregion
 


        /// <summary>
        /// Creates a new <see cref="AudioFileMemoryStream"/> from a given filepath.
        /// </summary>
        /// <param name="file">The audio file. It's not checked for being and actual audio file. That's on you</param>
        /// <returns><see cref="AudioFileMemoryStream"/></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="SecurityException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="IOException"></exception>
        public static AudioFileMemoryStream Create(string file)
        {
            if (file == null)
            {
                throw new ArgumentNullException();
            }
            if (!File.Exists(file))
            {
                throw new FileNotFoundException();
            }
            var info = new FileInfo(file);
            if (info.Length == 0)
            {
                throw new IOException(message: "File is empty");
            }

            var temp = new AudioFileMemoryStream();
            ThreadPool.QueueUserWorkItem((i) => temp.Load(file));
            Thread.Sleep(100);
            //temp.Load(file);
            return temp;
        }
    }
}

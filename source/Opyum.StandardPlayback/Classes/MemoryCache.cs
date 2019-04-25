using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security;
using System.Threading;

namespace Opyum.StandardPlayback
{
    /// <summary>
    /// Used for caching files into memory
    /// </summary>
    public class MemoryCache : Stream, IDisposable
    {
        private long _position = 0;
        private byte[] memoryBuffer = new byte[0];
        private BufferStatus bufferStatus = 0;

        Object load_lock = new Object();

        public enum BufferStatus
        {
            Empty = 0,
            Buffering = 1,
            Done = 2
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


        private MemoryCache()
        {

        }


        

        /// <summary>
        /// Sets the current position of the stream.
        /// Positive and negative offsets are acceptable in differet contexts.
        /// If the position overshoots, it will be corrected to the stream's end.
        /// If the position undershoots, it will be corrected to the stream's start.
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
        /// <param name="file"></param>
        /// <exception cref="Exception">Unhandled exceptions</exception>
        /// <exception cref="ArgumentNullException"></exception>
        private void Load(string file)
        {
            if (file == null)
            {
                throw new ArgumentNullException();
            }
            lock (load_lock)
            {
                int point = 0;
                byte[] tempBuffer = new byte[4096];
                List<byte> buildList = new List<byte>();
                int readBytes = 0;

                bufferStatus = BufferStatus.Buffering;
                using (Stream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    do
                    {
                        readBytes = fs.Read(tempBuffer, 0, 4096);
                        if (readBytes != 0)
                        {
                            Array.Resize<byte>(ref memoryBuffer, tempBuffer.Length + memoryBuffer.Length);
                            Buffer.BlockCopy(tempBuffer, 0, memoryBuffer, point, readBytes);
                            point += readBytes;
                        }
                    } while (readBytes > 0);
                }

                buildList = null; 
            }
        }

        void IDisposable.Dispose()
        {
            memoryBuffer = null;
        }



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
        /// Creates a new <see cref="MemoryCache"/> from a given filepath.
        /// </summary>
        /// <param name="file"></param>
        /// <returns><see cref="MemoryCache"/></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="SecurityException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="IOException"></exception>
        public static MemoryCache Create(string file)
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

            var temp = new MemoryCache();
            ThreadPool.QueueUserWorkItem((i) => temp.Load(file));
            Thread.Sleep(100);
            return temp;
        }
    }
}

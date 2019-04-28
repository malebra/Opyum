﻿using System;
using System.IO;
using System.Security;
using System.Threading;

namespace Opyum.StandardPlayback
{
    /// <summary>
    /// Used for caching files into memory
    /// </summary>
    public class FileMemoryStream : Stream, IDisposable
    {
        protected long _position = 0;
        protected byte[] memoryBuffer = new byte[0];
        protected int tempBufferSize = 1024 * 4;

        public BufferingState BufferingStatus { get; private set; } = 0;
        public string FilePath { get; private set; } = String.Empty;

        Object load_lock = new Object();

        public enum BufferingState
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


        //public bool BufferEmpty => memoryBuffer == null ? true : memoryBuffer.Length > 1024*64 ? false : true; 

        protected FileMemoryStream()
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
                    } while (readBytes > 0);
                    tempBuffer = null;
                }
                BufferingStatus = BufferingState.Done;

                ms.Dispose();
                GC.Collect();

                #endregion

            }
        }



        #region Garbage_COllectiong

        /// <summary>
        /// Disposes of resources.
        /// </summary>
        public new void Dispose()
        {
            Dispose(true);
            GC.Collect();
        }

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
        /// Creates a new <see cref="FileMemoryStream"/> from a given filepath.
        /// </summary>
        /// <param name="file">The audio file. It's not checked for being and actual audio file. That's on you</param>
        /// <returns><see cref="FileMemoryStream"/></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="SecurityException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="IOException"></exception>
        public static FileMemoryStream Create(string file)
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

            var temp = new FileMemoryStream();
            ThreadPool.QueueUserWorkItem((i) => temp.Load(file));
            Thread.Sleep(100);
            //temp.Load(file);
            return temp;
        }
    }
}

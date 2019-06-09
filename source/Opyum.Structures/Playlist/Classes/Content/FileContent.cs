using System;
using System.IO;
using Opyum.Structures;
using NAudio.Wave;

namespace Opyum.Playlist
{
    [Opyum.Structures.Attributes.Content(ContentType.File, "Audio file stream loaded into the memory.")]
    public class FileContent : Content, IContent
    {
        /// <summary>
        /// The <see cref="IFileFromMemoryStream"/> that contains the full file loaded into the memory.
        /// </summary>
        public IFileFromMemoryStream FileMemoryStream { get; protected set; }

        /// <summary>
        /// Returns the <see cref="Stream"/> form of the <see cref="IFileFromMemoryStream"/>.
        /// </summary>
        public new Stream DataStream { get => (Stream)FileMemoryStream; }

        /// <summary>
        /// The <see cref="PlaylistItem"/> type.
        /// </summary>
        public ContentType ItemType { get => ContentType.File; }

        /// <summary>
        /// The file path of the audio from <see cref="IFileFromMemoryStream"/> loaded into the memory.
        /// </summary>
        public new string Source => FileMemoryStream?.FilePath;

        /// <summary>
        /// Returns the <see cref="WaveFormat"/> from the file stream.   
        /// </summary>
        public new WaveFormat Format { get { if(_format == null) { _format = new WaveFormat(new BinaryReader(DataStream)); } return _format;} private set => _format = value; }

        /// <summary>
        /// Used to monitor canges in the file.
        /// </summary>
        virtual public IWatcher Watcher { get; private set; }

        /// <summary>
        /// The object tasked with turning the raw audio data into audio samples.
        /// </summary>
        public new object AudioSampleConverter { get; protected set; }



        /// <summary>
        /// private format.
        /// </summary>
        private WaveFormat _format;

        protected FileContent()
        {

        }

        ~FileContent()
        {
            Dispose();
        }


        /// <summary>
        /// Creates a returns a new <see cref="FileContent"/> 
        /// </summary>
        /// <param name="file">The full path</param>
        /// <returns></returns>
        public static FileContent Create(string file) => new FileContent() { FileMemoryStream = FileFromMemoryStream.Create(file) };
        

        public override void AddSampleConverter(object converter) => AudioSampleConverter = converter;



        #region Garbage_Collection

        public override void Dispose()
        {
            this.State = ContentStatus.Disposing;
            Dispose(true);
            GC.Collect();
            this.State = ContentStatus.Disposed;
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _format = null;
                FileMemoryStream = null;

            }
        }

        #endregion
    }
}

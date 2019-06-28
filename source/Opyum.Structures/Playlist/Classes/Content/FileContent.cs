using System;
using System.IO;
using Opyum.Structures;
using NAudio.Wave;

namespace Opyum.Playlist
{
    [Opyum.Structures.Attributes.Content(ContentType.File, "Audio file stream loaded into the memory.")]
    public class FileContent : Content, IContent
    {
        #region Excessive Data

        /// <summary>
        /// The file path of the audio from <see cref="IFileFromMemoryStream"/> loaded into the memory.
        /// </summary>
        //public string SourcePath { get; internal protected set; }

        /// <summary>
        /// The file path of the audio from <see cref="IFileFromMemoryStream"/> loaded into the memory.
        /// </summary>
        //public string OriginalSourcePath { get; internal protected set; }

        /// <summary>
        /// The duration of the item.
        /// <para>(for a <see cref="System.IO.Stream"/> it's set to 0.)</para>
        /// </summary>
        //public TimeSpan Duration { get; internal protected set; }



        /// <summary>
        /// The <see cref="PlaylistItem"/> type.
        /// </summary>
        //public ContentType ContentType { get => ContentType.File; }

        /// <summary>
        /// The state of the content.
        /// </summary>
        //public ContentStatus State
        //{
        //    get
        //    {
        //        return _state;
        //    }
        //    private set
        //    {
        //        _state = value;
        //        StateChanged?.Invoke(this, new EventArgs());
        //    }
        //}
        //private ContentStatus _state = 0;

        /// <summary>
        /// Audio file information
        /// </summary>
        //public AudioInfo AudioInfo { get; internal protected set; }

        /// <summary>
        /// The object tasked with turning the raw audio data into audio samples & additional functionality.
        /// </summary>
        //public ContentResolver ContentResolver { get; protected set; }



        /// <summary>
        /// Event raised by state change.
        /// </summary>
        //public event EventHandler StateChanged;


        #endregion



        /// <summary>
        /// The <see cref="IFileFromMemoryStream"/> that contains the full file loaded into the memory.
        /// </summary>
        public IFileFromMemoryStream FileMemoryStream { get; internal protected set; }

        /// <summary>
        /// Returns the <see cref="Stream"/> form of the <see cref="IFileFromMemoryStream"/>.
        /// </summary>
        public override Stream DataStream { get => (State & ContentStatus.LoadingDataToMemory) == ContentStatus.LoadingDataToMemory ? new FileStream(SourcePath, FileMode.Open) : FileMemoryStream.GetStream(); }  //needs change



        /// <summary>
        /// Used to monitor canges in the file.
        /// </summary>
        virtual public IWatcher Watcher { get; internal protected set; }




        /// <summary>
        /// Returns the <see cref="WaveFormat"/> from the file stream.   
        /// </summary>
        public override WaveFormat Format { get { if(_format == null) { _format = new WaveFormat(new BinaryReader(DataStream)); } return _format;} internal protected set => _format = value; }




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
        /// Adds a sample converter to the instance.
        /// </summary>
        /// <param name="converter"></param>
        public override void AddContentResolver(ContentResolver resolver) => ContentResolver = resolver;

        /// <summary>
        /// Loads the file into the memory from the current SourcePath.
        /// </summary>
        protected virtual void LoadToMemory()
        {
            try
            {
                FileMemoryStream = FileFromMemoryStream.Create(SourcePath);
            }
            catch
            {

            }
        }


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


        /// <summary>
        /// Creates and returns a new <see cref="FileContent"/> 
        /// </summary>
        /// <param name="file">The full path</param>
        /// <returns></returns>
        public static IContent Create(string file) => new FileContent();
    }
}
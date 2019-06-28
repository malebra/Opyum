using NAudio.Wave;
using System;
using System.IO;

namespace Opyum.Playlist
{
    public abstract class Content : IContent
    {
        /// <summary>
        /// The duration of the item.
        /// <para>(for a <see cref="System.IO.Stream"/> it's set to 0.)</para>
        /// </summary>
        public TimeSpan Duration { get; internal protected set; }

        /// <summary>
        /// The object tasked with turning the raw audio data into audio samples & additional funcitionality.
        /// </summary>
        public ContentResolver ContentResolver { get; internal protected set; }

        /// <summary>
        /// Audio file information
        /// </summary>
        public AudioInfo AudioInfo { get; internal protected set; }

        /// <summary>
        /// The <see cref="PlaylistItem"/> content type
        /// </summary>
        public ContentType ContentType { get; internal protected set; }

        /// <summary>
        /// The state of the content.
        /// </summary>
        public ContentStatus State
        {
            get
            {
                return _state;
            }
            internal protected set
            {
                _state = value;
                StateChanged?.Invoke(this, new EventArgs());
            }
        }
        protected ContentStatus _state = 0;

        /// <summary>
        /// The raw data stream of the audio.
        /// </summary>
        public virtual Stream DataStream { get; }

        /// <summary>
        /// File audio format.
        /// </summary>
        public virtual WaveFormat Format { get; internal protected set; }

        /// <summary>
        /// The location of the audio (current).
        /// </summary>
        public string SourcePath { get; internal protected set; }

        /// <summary>
        /// The location of the audio (original).
        /// </summary>
        public string OriginalSourcePath { get; internal protected set; }

        /// <summary>
        /// Adds a sample converter to the Content.
        /// </summary>
        /// <param name="converter"></param>
        public abstract void AddContentResolver(ContentResolver resolver);

        /// <summary>
        /// Event raised by state change.
        /// </summary>
        public event EventHandler StateChanged;



        #region Garbage collection

        public abstract void Dispose();


        #endregion
    }
}

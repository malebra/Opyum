using NAudio.Wave;
using System;
using System.IO;

namespace Opyum.Structures.Playlist
{
    public class Content : IContent
    {
        internal Content()
        {

        }


        /// <summary>
        /// The point in the song where the audio starts.
        /// <para>This should be ignored if the <see cref="IContent"/> is a <see cref="System.IO.Stream"/>.</para>
        /// </summary>
        public virtual TimeSpan Begining { get; set; } = TimeSpan.Zero; //ignore if Content is stream

        ///////////////////////////////////////////////////////////////////////////////////    LINKED TO VOLUMECURVE    ///////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The duration of the item.
        /// <para>(or to be more precise, for a <see cref="System.IO.Stream"/>, after what <see cref="TimeSpan"/> of playing should the item stop.)</para>
        /// </summary>
        public virtual TimeSpan Duration { get; set; }
        ///////////////////////////////////////////////////////////////////////////////////    LINKED TO VOLUMECURVE    ///////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// The type of item duration (SET or DYNAMIC)
        /// </summary>
        public virtual DurationType DurationType { get; protected internal set; }

        /// <summary>
        /// The curve determening the volume of the audio while playing.
        /// </summary>
        virtual public VolumeCurve VolumeCurve { get; set; }

        /// <summary>
        /// Audio file information
        /// </summary>
        public virtual AudioInfo AudioInfo { get; internal protected set; }

        /// <summary>
        /// The <see cref="PlaylistItem"/> content type
        /// </summary>
        public virtual SourceType SourceType { get; internal protected set; }

        /// <summary>
        /// The state of the content.
        /// </summary>
        public ContentStatus Status => ContentInterpreter == null ? _status : ContentInterpreter.Status;
        protected ContentStatus _status = 0;

        /// <summary>
        /// The raw data stream of the audio.
        /// </summary>
        public virtual Stream Stream { get; protected internal set; }

        /// <summary>
        /// The location of the audio (current).
        /// </summary>
        public virtual string Source { get; protected internal set; }



        /// <summary>
        /// Adds a sample converter to the Content.
        /// </summary>
        /// <param name="converter"></param>
        //public abstract void AddContentInterpreter(ContentInterpreter interpreter);

        /// <summary>
        /// The object tasked with turning the raw audio data into audio samples & additional funcitionality.
        /// </summary>
        public virtual ContentInterpreter ContentInterpreter { get; internal protected set; }



        /// <summary>
        /// Event raised by state change.
        /// </summary>
        public virtual event EventHandler StateChanged;

        /// <summary>
        /// Triggered on content change
        /// </summary>
        public virtual event EventHandler ContentChanged;



        #region Garbage collection

        public virtual void Dispose()
        {
            Dispose(true);
            GC.Collect();
        }

        private void Dispose(bool v)
        {
            if (v)
            {
                this.ContentInterpreter = null;
                _status = ContentStatus.Disposing;
                this.AudioInfo = null;
                this.ContentChanged = null;
                this.Source = null;
                this.StateChanged = null;
                this.Stream = null;
                this.VolumeCurve = null;
                this._status = ContentStatus.Disposed;
            }
        }


        #endregion

        /// <summary>
        /// Triggeres the ContentChanged event.
        /// </summary>
        //protected void OnContentChange()
        //{
        //    ContentChanged?.Invoke(this, new EventArgs());
        //}
        //protected void OnContentChange(ContentChangeEventArgs e)
        //{
        //    ContentChanged?.Invoke(this, e);
        //}
    }
}

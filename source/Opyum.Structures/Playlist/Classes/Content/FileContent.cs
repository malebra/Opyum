using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opyum.Structures.Playlist
{
    [Opyum.Structures.Attributes.Content(SourceType.File, "Audio file stream loaded into the memory.")]
    public class FileContent : Content, IContent, IDisposable
    {



        private FileContent()
        {

        }

        /// <summary>
        /// The duration of the item.
        /// <para>(for a <see cref="System.IO.Stream"/> it's set to 0.)</para>
        /// </summary>
        public override TimeSpan Duration { get; set; }

        /// <summary>
        /// The type of item duration (SET or DYNAMIC)
        /// </summary>
        public override DurationType DurationType { get; set; }

        /// <summary>
        /// The point in the song where the audio starts.
        /// <para>This should be ignored if the <see cref="IContent"/> is a <see cref="System.IO.Stream"/>.</para>
        /// </summary>
        public override TimeSpan Begining { get; set; }

        /// <summary>
        /// The curve determening the volume of the audio while playing.
        /// </summary>
        public override VolumeCurve VolumeCurve { get; set; }


        /// <summary>
        /// The object tasked with turning the raw audio data into audio samples & additional funcitionality.
        /// </summary>
        public override ContentInterpreter ContentInterpreter { get; protected internal set; }
        
        /// <summary>
        /// Audio file information
        /// </summary>
        public override AudioInfo AudioInfo { get; protected internal set; }

        /// <summary>
        /// The <see cref="PlaylistItem"/> content type
        /// </summary>
        public override SourceType SourceType { get; protected internal set; }

        /// <summary>
        /// The state of the content.
        /// </summary>
        public override ContentStatus Status => ContentInterpreter.Status; 

        /// <summary>
        /// The raw data stream of the audio file.
        /// </summary>
        public override Stream Stream { get; }


        /// <summary>
        /// The location of the audio (current).
        /// </summary>
        public override string Source { get; protected internal set; }

        /// <summary>
        /// The location of the audio (original).
        /// </summary>
        //string OriginalSourcePath { get; }

        /// <summary>
        /// Adds a sample converter to the Content.
        /// </summary>
        /// <param name="converter"></param>
        public override void AddContentInterpreter(ContentInterpreter interpreter)
        {
            ContentInterpreter = interpreter;
        }

        /// <summary>
        /// Event raised by state change.
        /// </summary>
        event EventHandler StateChanged;

        /// <summary>
        /// Triggered on content change
        /// </summary>
        event EventHandler ContentChanged;

        private void CallStateChangedEvent(object sender, EventArgs e)
        {
            StateChanged?.Invoke(this, new EventArgs());
        }

        private void CallContentChangedEvent(object sender, EventArgs e)
        {
            ContentChanged?.Invoke(this, new EventArgs());
        }

        #region Garbage Collection

        public override void Dispose()
        {
            Dispose(true);
            GC.Collect();
        }

        private void Dispose(bool v)
        {
            if (v)
            {
                //dispose of unwanted shit
            }
        }

        #endregion
    }
}

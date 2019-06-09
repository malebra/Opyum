using NAudio.Wave;
using System.IO;

namespace Opyum.Playlist
{
    public abstract class Content : IContent
    {
        /// <summary>
        /// The location of the audio.
        /// </summary>
        public string Source { get; protected set; }

        /// <summary>
        /// The stream of the audio samples.
        /// </summary>
        public Stream AudioStream { get; protected set; }

        /// <summary>
        /// The raw data stream of the audio.
        /// </summary>
        public Stream DataStream { get; protected set; }

        /// <summary>
        /// The <see cref="PlaylistItem"/> content type
        /// </summary>
        public ContentType ContentType { get; protected set; }

        /// <summary>
        /// The state of the content.
        /// </summary>
        public ContentStatus State { get; protected set; }

        /// <summary>
        /// File audio format.
        /// </summary>
        public WaveFormat Format { get; protected set; }

        /// <summary>
        /// Audio file information
        /// </summary>
        public AudioInfo AudioInfo { get; protected set; }


        /// <summary>
        /// The object tasked with turning the raw audio data into audio samples.
        /// </summary>
        public object AudioSampleConverter { get; protected set; }

        public abstract void Dispose();

        /// <summary>
        /// Adds a sample converter to the Content.
        /// </summary>
        /// <param name="converter"></param>
        public abstract void AddSampleConverter(object converter);
    }
}

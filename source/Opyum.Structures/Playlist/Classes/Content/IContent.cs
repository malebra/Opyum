using System;
using System.IO;
using NAudio.Wave;

namespace Opyum.Playlist
{
    public interface IContent : IDisposable
    {
        /// <summary>
        /// The duration of the item.
        /// <para>(for a <see cref="System.IO.Stream"/> it's set to 0.)</para>
        /// </summary>
        TimeSpan Duration { get; }

        /// <summary>
        /// The object tasked with turning the raw audio data into audio samples & additional funcitionality.
        /// </summary>
        ContentResolver ContentResolver { get; }

        /// <summary>
        /// Audio file information
        /// </summary>
        AudioInfo AudioInfo { get; }

        /// <summary>
        /// The <see cref="PlaylistItem"/> content type
        /// </summary>
        ContentType ContentType { get; }

        /// <summary>
        /// The state of the content.
        /// </summary>
        ContentStatus State { get; }

        /// <summary>
        /// The raw data stream of the audio.
        /// </summary>
        Stream DataStream { get; }

        /// <summary>
        /// File audio format.
        /// </summary>
        WaveFormat Format { get; }

        /// <summary>
        /// The location of the audio (current).
        /// </summary>
        string SourcePath { get; }

        /// <summary>
        /// The location of the audio (original).
        /// </summary>
        string OriginalSourcePath { get; }

        /// <summary>
        /// Adds a sample converter to the Content.
        /// </summary>
        /// <param name="converter"></param>
        void AddContentResolver(ContentResolver resolver);

        /// <summary>
        /// Event raised by state change.
        /// </summary>
        event EventHandler StateChanged;
    }
}
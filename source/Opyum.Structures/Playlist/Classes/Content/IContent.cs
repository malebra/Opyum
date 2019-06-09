using System;
using System.IO;
using NAudio.Wave;

namespace Opyum.Playlist
{
    public interface IContent : IDisposable
    {
        /// <summary>
        /// The object tasked with turning the raw audio data into audio samples.
        /// </summary>
        object AudioSampleConverter { get; }

        /// <summary>
        /// Audio file information
        /// </summary>
        AudioInfo AudioInfo { get; }

        /// <summary>
        /// The stream of the audio samples.
        /// </summary>
        Stream AudioStream { get; }

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
        /// The location of the audio.
        /// </summary>
        string Source { get; }

        /// <summary>
        /// Adds a sample converter to the Content.
        /// </summary>
        /// <param name="converter"></param>
        void AddSampleConverter(object converter);

        

    }
}
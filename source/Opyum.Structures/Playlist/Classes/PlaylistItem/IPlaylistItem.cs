using System;

namespace Opyum.Structures.Playlist
{
    public interface IPlaylistItem : IDisposable
    {
        /// <summary>
        /// The content to play from.
        /// </summary>
        IContent Content { get; }



        /// <summary>
        /// The item history...
        /// <para>When the item is changed, this tracks the changes.</para>
        /// </summary>
        ItemHistory History { get; }

        /// <summary>
        /// The audio information like name, artists, album, year etc.
        /// </summary>
        ItemInfo ItemInformation { get; set; }

        /// <summary>
        /// The type of the item.
        /// <para>Impropper assignment will throw an <see cref="InvalidOperationException"/></para>
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        PlaylistItemType ItemType { get; }

        /// <summary>
        /// Additional options editor to change or specity the item capabilities.
        /// </summary>
        IItemOptions Options { get; }

        /// <summary>
        /// The time the audio is going to be played to start playing.
        /// </summary>
        DateTime PlayTime { get; }

        /// <summary>
        /// The time the audio is supposed to start playing.
        /// </summary>
        DateTime SetTime { get; }

        /// <summary>
        /// The standard settings for each item.
        /// </summary>
        ItemSettings Settings { get; set; }

        /// <summary>
        /// The status of the item (wheather it can be used, if it's playing...).
        /// </summary>
        ItemStatus Status { get; }

        /// <summary>
        /// Temporary tags that can be set in the running list.
        /// <para>These tags are applicable to only one item and are deleted the moment the item stops playling.</para>
        /// </summary>
        ITags Tags { get; set; }

        /// <summary>
        /// The type of item start time duration (SET or DYNAMIC)
        /// </summary>
        TimeType TimeType { get; }

        event PlaylistItemChangedEventHandler Changed;
        event PlaylistItemActionCallEventHandler ItemActionCall;
        event PlaylistItemChangedEventHandler ItemLengthChanged;
        event PlaylistItemChangedEventHandler NextItemUpdateRequest;
    }
}
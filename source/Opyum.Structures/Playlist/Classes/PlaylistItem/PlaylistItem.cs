﻿using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;

namespace Opyum.Structures.Playlist
{
    [Opyum.Structures.Attributes.PlaylistItem]
    [Serializable]
    public class PlaylistItem : IDisposable
    {
        /// <summary>
        /// The content to play from.
        /// </summary>
        public IContent Content { get; set; }



        /// <summary>
        /// The point in the song where the audio starts.
        /// <para>This should be ignored if the <see cref="IContent"/> is a <see cref="System.IO.Stream"/>.</para>
        /// </summary>
        public TimeSpan Begining { get; internal protected set; } = TimeSpan.Zero; //ignore if Content is stream

        ///////////////////////////////////////////////////////////////////////////////////    LINKED TO VOLUMECURVE    ///////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The duration of the item.
        /// <para>(or to be more precise, for a <see cref="System.IO.Stream"/>, after what <see cref="TimeSpan"/> of playing should the item stop.)</para>
        /// </summary>
        public TimeSpan Duration { get => VolumeCurve.Duration; set => VolumeCurve.Duration = value; }
        ///////////////////////////////////////////////////////////////////////////////////    LINKED TO VOLUMECURVE    ///////////////////////////////////////////////////////////////////////////////////



        /// <summary>
        /// The time the audio is supposed to start playing.
        /// </summary>
        public DateTime PresetTime { get; internal protected set; }
        /// <summary>
        /// The time the audio is going to be played to start playing.
        /// </summary>
        public DateTime PlayTime { get; internal protected set; }



        /// <summary>
        /// The type of item duration (SET or DYNAMIC)
        /// </summary>
        public DurationType DurationType { get; internal protected set; } = 0;

        /// <summary>
        /// The type of item start time duration (SET or DYNAMIC)
        /// </summary>
        public TimeType TimeType { get; internal protected set; } = 0;
        


        /// <summary>
        /// Temporary tags that can be set in the running list.
        /// <para>These tags are applicable to only one item and are deleted the moment the item stops playling.</para>
        /// </summary>
        public ITags Tags { get; set; }

        /// <summary>
        /// The state of the item (wheather it can be used, if it's playing...).
        /// </summary>
        virtual public ItemStatus State { get; internal protected set; }

        /// <summary>
        /// The audio information like name, artists, album, year etc.
        /// </summary>
        virtual public ItemInfo ItemInformation { get; internal protected set; }/*get from file (filepath) and database [file type, audio type, waveformat, duration, artist, cover image, etc. ]*/

        /// <summary>
        /// The curve determening the volume of the audio while playing.
        /// </summary>
        virtual public VolumeCurve VolumeCurve { get; set; }

        /// <summary>
        /// The standard settings for each item.
        /// </summary>
        virtual public ItemSettings Settings { get; internal protected set; }

        /// <summary>
        /// The item history...
        /// <para>When the item is changed, this tracks the changes.</para>
        /// </summary>
        virtual public ItemHistory History { get; internal protected set; }

        /// <summary>
        /// Additional options editor to change or specity the item capabilities.
        /// </summary>
        virtual public IItemOptions Options { get; set; }


        /// <summary>
        /// The type of the item.
        /// <para>Impropper assignment will throw an <see cref="InvalidOperationException"/></para>
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        virtual public PlaylistItemType ItemType
        {
            get
            {
                return _itemType;
            }
            protected set
            {
                if (typeof(PlaylistSlot) == this.GetType() && value != PlaylistItemType.Slot)
                {
                    throw new InvalidOperationException($"Impropper value assignment! {value.ToString()} cannot be assigned to a {this.GetType().ToString()} item type.");
                }
                if (typeof(PlaylistZone) == this.GetType() && (value != PlaylistItemType.ZoneStart && value != PlaylistItemType.ZoneEnd))
                {
                    throw new InvalidOperationException($"Impropper value assignment! {value.ToString()} cannot be assigned to a {this.GetType().ToString()} item type.");
                }
                _itemType = value;
            }
        } 
        protected PlaylistItemType _itemType = PlaylistItemType.None;

        internal protected Playlist CurrentPlaylist { get; set; }


        #region Change Events

        public event PlaylistItemChangedEventHandler Changed;
        public event PlaylistItemChangedEventHandler NextItemUpdateRequest;
        public event PlaylistItemChangedEventHandler ItemLengthChanged;

        protected virtual void OnItemChange()
        {
            OnItemChange(new PlaylistItemChangedEventArgs() { Changes = ItemChanges.NotDefined });
        }

        protected virtual void OnItemChange(PlaylistItemChangedEventArgs changes)
        {

            Changed?.Invoke(this, changes);
        }

        #endregion

        internal protected PlaylistItem()
        {
            
        }

        ~PlaylistItem()
        {
            Dispose();
        }


        #region Garbage Collection

        public void Dispose()
        {
            Dispose(true);
            GC.Collect();
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Content?.Dispose();
                this.Content = null;
                this.Tags = null;
                this.Options = null;
                this.VolumeCurve = null;
            }
        }

        #endregion

        #region >>>>>>>>>>>>>>>>>>>> UNFINISHED <<<<<<<<<<<<<<<<<<<<<<<<
        
        /// <summary>
        /// UNFINISHED
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static PlaylistItem Create(string location)
        {
            var temp = new PlaylistItem();

            return temp;
        }

        /// <summary>
        /// Initialises item from a given content.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static PlaylistItem CreateFromContent(IContent content)
        {
            var temp = new PlaylistItem();
            temp.Content = content;

            return temp;
        }


        #endregion
    }
}

using System;
using System.IO;

namespace Opyum.Playlist
{
    [Opyum.Structures.Attributes.PlaylistItem]
    public class PlaylistItem
    {
        /// <summary>
        /// The content to play from.
        /// </summary>
        protected IContent Content { get; set; }


        /// <summary>
        /// The point in the song where the audio starts.
        /// <para>This should be ignored if the <see cref="IContent"/> is a <see cref="System.IO.Stream"/>.</para>
        /// </summary>
        public TimeSpan Begining { get; private set; } //ignore if Content is stream

        /// <summary>
        /// The duration of the item.
        /// <para>(or to be more precise, for a <see cref="System.IO.Stream"/>, after what <see cref="TimeSpan"/> of playing should the item stop.)</para>
        /// </summary>
        public TimeSpan Duration { get; private set; }

        /// <summary>
        /// The time the audio is supposed to start playing.
        /// </summary>
        public DateTime PresetTime { get; private set; }
        /// <summary>
        /// The time the audio is going to be played to start playing.
        /// </summary>
        public DateTime PlayTime { get; private set; }


        /// <summary>
        /// The type of item duration (SET or DYNAMIC)
        /// </summary>
        public DurationType DurationType { get; private set; } = 0;

        /// <summary>
        /// The type of item start time duration (SET or DYNAMIC)
        /// </summary>
        public TimeType TimeType { get; private set; } = 0;
        


        /// <summary>
        /// Tags that are pulled from the database or imported list.
        /// <para>These tags cannot be altered or deleted unles done so from inside the database or from the editor or admin.</para>
        /// </summary>
        public ITags AudioTags { get; private set; }

        /// <summary>
        /// Temporary tags that can be set in the running list.
        /// <para>These tags are applicable to only one item and are deleted the moment the item stops playling.</para>
        /// </summary>
        public ITags ItemTags { get; set; }

        /// <summary>
        /// The state of the item (wheather it can be used, if it's playing...).
        /// </summary>
        virtual public ItemStatus State { get; private set; }

        /// <summary>
        /// The audio information like name, artists, album, year etc.
        /// </summary>
        virtual public AudioInfo AudioInformation { get; private set; }/*get from file (filepath) and database [file type, audio type, waveformat, duration, artist, cover image, etc. ]*/

        /// <summary>
        /// The curve determening the volume of the audio while playing.
        /// </summary>
        virtual public VolumeCurve VolumeCurve { get; set; }

        /// <summary>
        /// Additional options editor to change or specity the item capabilities.
        /// </summary>
        virtual public IOptions Options { get; set; }

        virtual public ItemSettings Settings { get; set; }


        protected PlaylistItemType _itemType = PlaylistItemType.None;
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
                    throw new InvalidOperationException($"Impropper value assignment! A {value.ToString()} cannot be assigned to a {this.GetType().ToString()} item type.");
                }
                if (typeof(PlaylistZone) == this.GetType() && (value != PlaylistItemType.ZoneStart || value != PlaylistItemType.ZoneEnd))
                {
                    throw new InvalidOperationException($"Impropper value assignment! A {value.ToString()} cannot be assigned to a {this.GetType().ToString()} item type.");
                }
                _itemType = value;
            }
        } 

        public PlaylistItem NextItem { get; protected internal set; }
        public PlaylistItem PreviousItem { get; protected internal set; }
        internal protected Playlist CurrentPlaylist { get; set; }


        #region Change Events

        public event PlaylistItemChangedEventHandler Changed;

        protected virtual void OnItemChange()
        {
            OnItemChange(new PlaylistItemChangedEventArgs() { Changes = ItemChanges.None });
        }

        protected virtual void OnItemChange(PlaylistItemChangedEventArgs changes)
        {

            Changed?.Invoke(this, changes);
        }

        #endregion


        protected PlaylistItem()
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
                this.AudioInformation = null;
                this.AudioTags = null;
                this.ItemTags = null;
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

        #endregion
    }
}

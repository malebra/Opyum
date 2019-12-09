using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;

namespace Opyum.Structures.Playlist
{
    [Opyum.Structures.Attributes.PlaylistItem]
    [Serializable]
    public class PlaylistItem : IDisposable, IPlaylistItem
    {
        /// <summary>
        /// The content to play from.
        /// </summary>
        public IContent Content { get => _content; protected internal set { _content = value; } }
        protected IContent _content;




        /// <summary>
        /// The time the audio is supposed to start playing.
        /// </summary>
        public DateTime SetTime { get; internal protected set; }
        /// <summary>
        /// The time the audio is going to be played to start playing.
        /// </summary>
        public DateTime PlayTime { get; internal protected set; }
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
        /// The status of the item (wheather it can be used, if it's playing...).
        /// </summary>
        virtual public ItemStatus Status { get; internal protected set; }

        /// <summary>
        /// The audio information like name, artists, album, year etc.
        /// </summary>
        virtual public ItemInfo ItemInformation { get; set; }/*get from file (filepath) and database [file type, audio type, waveformat, duration, artist, cover image, etc. ]*/

        /// <summary>
        /// The standard settings for each item.
        /// </summary>
        virtual public ItemSettings Settings { get; set; }

        /// <summary>
        /// The item history...
        /// <para>When the item is changed, this tracks the changes.</para>
        /// </summary>
        virtual public ItemHistory History { get; internal protected set; }

        /// <summary>
        /// Additional options editor to change or specity the item capabilities.
        /// </summary>
        virtual public IItemOptions Options { get; protected internal set; }//     set; }


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
                if (typeof(PlaylistZone) == this.GetType() && (value != PlaylistItemType.Zone))//Start && value != PlaylistItemType.ZoneEnd))
                {
                    throw new InvalidOperationException($"Impropper value assignment! {value.ToString()} cannot be assigned to a {this.GetType().ToString()} item type.");
                }
                _itemType = value;
            }
        }
        protected PlaylistItemType _itemType = PlaylistItemType.None;

        internal protected Playlist CurrentPlaylist { get; set; }


        internal protected PlaylistItem()
        {
            History = new ItemHistory();

        }

        ~PlaylistItem()
        {
            Dispose();
        }

        #region Change Events

        public event PlaylistItemChangedEventHandler Changed;
        public event PlaylistItemChangedEventHandler NextItemUpdateRequest;
        public event PlaylistItemChangedEventHandler ItemLengthChanged;
        public event PlaylistItemActionCallEventHandler ItemActionCall;

        protected virtual void OnItemChange()
        {
            OnItemChange(new PlaylistItemChangedEventArgs() { Changes = ItemChanges.NotDefined });
        }

        protected virtual void OnItemChange(PlaylistItemChangedEventArgs changes)
        {

            Changed?.Invoke(this, changes);
        }

        protected virtual void OnActionCall(Action a)
        {
            ItemActionCall?.Invoke(this, a);
        }

        #endregion

        #region Methods

        protected virtual void UpdateSetup()
        {
            Content.ContentChanged += UpdateItem;
            Content.StateChanged += UpdateItem;
        }

        protected virtual void UpdateItem(object sender, EventArgs e)
        {

        }

        #endregion

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
                this.Content.VolumeCurve = null;
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

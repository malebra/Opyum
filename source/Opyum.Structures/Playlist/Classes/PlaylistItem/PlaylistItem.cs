

namespace Opyum.Playlist
{
    [Opyum.Structures.Attributes.PlaylistItem]
    public class PlaylistItem
    {
        public IContent Content { get; protected set; }

        public DurationType DurationType { get; private set; }

        public TimeType TimeType { get; private set; }
        /// <summary>
        /// Tags that are pulled from the database or imported list.
        /// </summary>
        public ITags Tags { get; private set; }
        /// <summary>
        /// Temporary tags that can be set in the running list.
        /// </summary>
        public ITags TempTags { get; set; }
        /// <summary>
        /// The state of the item (wheather it can be used, if it's playing...).
        /// </summary>
        virtual public ItemStatus State { get; private set; }
        /// <summary>
        /// The audio information like name, artists, album, year etc.
        /// </summary>
        virtual public AudioInfo AudioInformation { get; private set; }
        /// <summary>
        /// The curve determening the volume of the audio while playing.
        /// </summary>
        virtual public VolumeCurve VolumeCurve { get; set; }

        public IOptions Options { get; set; }

        //Fade in

        //Fade out

        //StartCue

        //EndCue

        //VoxStart

        //Volume

        #region Change_Events

        public event PlaylistItemChangedEventHandler ItemChanged;

        protected virtual void OnItemChange()
        {
            ItemChanged?.Invoke(this, new PlaylistItemChangedEventArgs() { Changes = ItemChanges.None });
        }


        #endregion

    }


}



namespace Opyum.Playlist
{
    [Opyum.Structures.Attributes.PlaylistItem]
    public class PlaylistItem
    {
        /// <summary>
        /// The content to play from.
        /// </summary>
        public IContent Content { get; protected set; }

        public DurationType DurationType { get; private set; } = 0;

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
        virtual public AudioInfo AudioInformation { get; private set; }
        /// <summary>
        /// The curve determening the volume of the audio while playing.
        /// </summary>
        virtual public VolumeCurve VolumeCurve { get; set; }
        /// <summary>
        /// Additional options to change or specity the item capabilities.
        /// </summary>
        virtual public IOptions Options { get; set; }
        /// <summary>
        /// The type of the item.
        /// </summary>
        virtual public PlaylistItemType ItemType { get; private set; } = PlaylistItemType.None;

        //Fade in

        //Fade out

        //StartCue

        //EndCue

        //VoxStart

        //Volume

        #region Change_Events

        public event PlaylistItemChangedEventHandler Changed;

        protected virtual void OnItemChange()
        {
            Changed?.Invoke(this, new PlaylistItemChangedEventArgs() { Changes = ItemChanges.None });
        }


        #endregion

    }
}

using System;
using Opyum.Structures.Playlist;

namespace Opyum.Structures.PlaylistSupport
{
    /// <summary>
    /// Unfinished
    /// <para>Unfinished</para>
    /// <para>Unfinished</para>
    /// <para>Unfinished</para>
    /// <para>Unfinished</para>
    /// <para>Unfinished</para>
    /// <para>Unfinished</para>
    /// <para>Unfinished</para>
    /// <para>Unfinished</para>
    /// <para>Unfinished</para>
    /// <para>Unfinished</para>
    /// <para>Unfinished</para>
    /// 
    /// </summary>
    public class ItemSupporter
    {
        protected string Path { get; set; }

        protected Content Content { get; set; }

        


        public ItemSupporter(string path)
        {
            Path = path;
            Content = (Content)InstantiateContentFromContentType(DeterminContentTypeFromPath());
        }

        public void InitializeItem()
        {
            //Check if the file exists
            if (!System.IO.File.Exists(Path))
            {
                Content.State = ContentStatus.UnsuccessfulPathResolution;
                return;
            }

            //needs immeditate change
            var contentResolver = new ContentResolver(Path);
            if (contentResolver == null)
            {
                Content.State = ContentStatus.UnsupportedFormat;
            }

            //Add content resolver so the audio can be played
            Content.AddContentResolver(contentResolver);

            //Update status
            Content.State = ContentStatus.Preloading;

            //send caching request 

            SetProperties();

            PlaylistItem item = PlaylistItem.CreateFromContent(Content);

            ItemSetup(item);
            
        }

        /// <summary>
        /// Unfinished
        /// <para>Unfinished</para>
        /// <para>Unfinished</para>
        /// <para>Unfinished</para>
        /// <para>Unfinished</para>
        /// <para>Unfinished</para>
        /// <para>Unfinished</para>
        /// <para>Unfinished</para>
        /// <para>Unfinished</para>
        /// <para>Unfinished</para>
        /// <para>Unfinished</para>
        /// <para>Unfinished</para>
        /// 
        /// </summary>
        protected void ItemSetup(PlaylistItem item)
        {
            item.Begining = TimeSpan.Zero;
            item.DurationType = 0;
            item.TimeType = 0;
            //item.Tags = 
            item.ItemInformation = ItemInfo.CrearteDefault();
            item.VolumeCurve = VolumeCurve.CreateDefault(item.Content.Duration);
            //item.Settings =
            //item.History = 
            //item.ItemType =

        }


        /// <summary>
        /// Unfinished
        /// <para>Unfinished</para>
        /// <para>Unfinished</para>
        /// <para>Unfinished</para>
        /// <para>Unfinished</para>
        /// <para>Unfinished</para>
        /// <para>Unfinished</para>
        /// <para>Unfinished</para>
        /// <para>Unfinished</para>
        /// <para>Unfinished</para>
        /// <para>Unfinished</para>
        /// <para>Unfinished</para>
        /// 
        /// </summary>
        protected void SetProperties()
        {
            
        }

        /// <summary>
        /// Unfinished.
        /// </summary>
        /// <returns></returns>
        protected ContentType DeterminContentTypeFromPath()
        {
            return ContentType.None;
        }

        protected IContent InstantiateContentFromContentType(ContentType type) => FileContent.Create(Path);// <-------------------    CHANGE ASAP    -------------------> //
    }
}

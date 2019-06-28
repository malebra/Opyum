using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opyum.Playlist.PlaylistSupport
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
    /// </summary>89
    public class ItemSupporter
    {
        protected string Path { get; set; }

        protected Content Content { get; set; }




        public ItemSupporter(string path)
        {
            Path = path;
            Content = (Content)InstantioateContentFromContentType(DeterminContentTypeFromPath());
        }

        public void InitializeItem()
        {
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

            Content.AddContentResolver(contentResolver);

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

        protected ContentType DeterminContentTypeFromPath()
        {
            return ContentType.None;
        }

        protected IContent InstantioateContentFromContentType(ContentType type) => FileContent.Create(Path);// <-------------------    CHANGE ASAP    -------------------> //
    }
}

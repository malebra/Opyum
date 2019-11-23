namespace Opyum.Structures.Playlist
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
    public class ItemInfo
    {
        public string Name { get; internal protected set; }

        public string Album { get; internal protected set; }

        public string Author { get; internal protected set; }

        public string Lyrics { get; internal protected set; } //could be a class with lyrics that follow along the time progression

        public string AlbumYear { get; internal protected set;
        }
        public AudioImage Image { get; internal protected set; }

        //public ContentType Type => ContentType.None;

        public string AditionalInfo { get; internal protected set; }

        public string Notes { get; internal protected set; }




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
        public static ItemInfo CrearteDefault()
        {
            var temp = new ItemInfo();

            temp.Name = "";
            temp.Album = "";
            temp.Author = "";
            temp.Lyrics = "";
            temp.AlbumYear = "";
            temp.AditionalInfo = "";
            temp.Notes = "";

            return temp;
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
        public static ItemInfo CreateFromPath(string path)
        {
            return ItemInfo.CrearteDefault();
        }
    }
}

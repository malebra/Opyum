using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opyum.Playlist
{
    /// <summary>
    /// A <see cref="PlaylistZone"/> is a playlistitem that determines the start or end of a Zone inside the playlist.
    /// <para>A zone can be used to limit access to that part of the list, to determine what type of files can or can't be inserted there</para>
    /// </summary>
    [Opyum.Structures.Attributes.PlaylistItem]
    public class PlaylistZone : PlaylistItem
    {
        
    }
}

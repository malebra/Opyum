using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opyum.Playlist
{
    /// <summary>
    /// After setup, the slot waits for the proper file or source to load and plays it at the correct point in the playlist.
    /// </summary>
    [Opyum.Structures.Attributes.PlaylistItem]
    public class PlaylistSlot : PlaylistItem
    {
        public override PlaylistItemType ItemType => PlaylistItemType.Slot;
    }
}
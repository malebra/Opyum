using System;
using System.Collections;
using System.Collections.Generic;

namespace Opyum.Structures.Playlist
{
    public class Playlist : IEnumerable<PlaylistItem>
    {
        public IEnumerator<PlaylistItem> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

    }
}

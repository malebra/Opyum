using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opyum.AudioBase
{
    public class PlaylistItem : IPlaylistItem
    {
        public IDuration Duration { get; }

        public ITime Time { get;  }

        public ITags Tags { get; }

        public IWatcher Watcher { get; }


        public IContent Conntent { get; }

        public IOptions Options { get; }



    }
}

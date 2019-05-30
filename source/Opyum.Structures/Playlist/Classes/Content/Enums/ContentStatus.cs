using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opyum.Playlist
{
    public enum ContentStatus
    {
        Preloaded = 0,
        LoadingData = 1,
        Ready = 2,
        Playling = 4,
        Stopped = 8,
        Disposing = 16,
        Disposed = 32
    }
}

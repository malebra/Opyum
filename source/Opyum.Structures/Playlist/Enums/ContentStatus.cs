using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opyum.Playlist
{
    public enum ContentStatus
    {
        Empty = 0,
        Preloading = 1,
        Preloaded = 2,
        LoadingDataToMemory = 4,
        Ready = 8,
        Playling = 16,
        Stopped = 32,
        Disposing = 64,
        Disposed = 128,
        UnsuccessfulPathResolution = 256,
        UnsupportedFormat = 512
    }
}

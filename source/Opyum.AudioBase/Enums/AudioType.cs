using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opyum.AudioBase
{
    public enum AudioType
    {
        None = 0,
        File = 1,
        LocalStream = 2,
        InternetStream = 4,
        InternetAudio = 8 //something like a YouTube or SoundCloud video
    }
}

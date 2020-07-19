﻿namespace Opyum.AudioBase
{
    public enum ItemType
    {
        None = 0,
        File = 1,
        LocalStream = 2,
        InternetStream = 4,
        InternetAudio = 8, //something like a YouTube or SoundCloud video
        Slot = 16
    }
}

namespace Opyum.Playlist
{
    public enum ItemStatus
    {
        None = 0,
        InUse = 1,
        Playing = 2,
        Stopped = 4,
        Paused = 8,
        Updating = 16,
        Loading = 32,
        PrePlay = 64
    }
}

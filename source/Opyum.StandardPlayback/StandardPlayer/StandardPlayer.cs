using System;

namespace Opyum.StandardPlayback
{
    [Opyum.StandardPlayback.Attributes.AudioPlayer("Standard Player", Version = "1.0.0", SupportedFormats = "*.mp3; *.wav")]
    public partial class StandardPlayer : IDisposable
    {
        public StandardPlayer()
        {
            InitializePlayer();
            //_InitializeTimer();
        }        
    }
}

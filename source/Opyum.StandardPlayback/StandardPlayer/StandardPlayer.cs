using System;
using NAudio.Wave;

namespace Opyum.StandardPlayback
{
    [Opyum.StandardPlayback.Attributes.AudioPlayer("Standard Player", Version = "1.0.0", SupportedFormats = "*.mp3; *.wav")]
    public partial class StandardPlayer : IDisposable
    {
        IWavePlayer _wavePlayer;

        public StandardPlayer()
        {
            InitializePlayer();
            //_InitializeTimer();
        }

        public StandardPlayer(IWavePlayer wavePlayer)
        {
            _wavePlayer = wavePlayer;
        }
    }
}

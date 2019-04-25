using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

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

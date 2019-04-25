using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using NAudio.MediaFoundation;

namespace Opyum.StandardPlayback
{
    public partial class StandardPlayer
    {
        private void InitializePlayer()
        {
            output = new DirectSoundOut();
        }
        

        private void DisposeOf()
        {
            if (pcm != null)
            {
                pcm.Dispose();
                pcm = null;
            }
            if (blockAlignStream != null)
            {
                blockAlignStream.Dispose();
                blockAlignStream = null;
            }
            if (wavereader != null)
            {
                wavereader.Dispose();
                wavereader = null;
            }
            if (audioreader != null)
            {
                audioreader.Dispose();
                audioreader = null;
            }
            if (aiffreader != null)
            {
                aiffreader.Dispose();
                aiffreader = null;
            }
        }

        public void Dispose()
        {
            DisposeOf();
            //StopPlayback();
            if (output != null)
            {
                output.Dispose();
                output = null;
            }
        }

        //On the change of volume

        
    }


}

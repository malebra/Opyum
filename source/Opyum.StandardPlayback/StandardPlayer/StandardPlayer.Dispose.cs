using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opyum.StandardPlayback
{
    public partial class StandardPlayer
    {
        private void Dispose(bool disposing)
        {
            if (disposing)
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
                if (output != null)
                {
                    output.Dispose();
                    output = null;
                }
                sourceStream?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.Collect();
        }
    }
}

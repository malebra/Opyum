using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Opyum.AudioBase.Attributes;

namespace Opyum.AudioBase
{
    [Opyum.AudioBase.Attributes.PlaylistItem]
    public class PlaylistItem
    {
        public DurationType DurationType { get; private set; }

        public TimeType TimeType { get; private set; }

        public ITags Tags { get; private set; }

        public ITags TempTags { get; set; }

        virtual public IWatcher Watcher { get; }

        
        //public IContent Content { get; }

        public IOptions Options { get; }

        //Fade in

        //Fade out

        //StartCue

        //EndCue

        //VoxStart

        //Volume

    }
}

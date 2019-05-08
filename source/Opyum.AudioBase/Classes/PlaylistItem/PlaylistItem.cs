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
        public IContent Content { get; protected set; }

        public DurationType DurationType { get; private set; }

        public TimeType TimeType { get; private set; }
        /// <summary>
        /// Tags that are pulled from the database or imported list.
        /// </summary>
        public ITags Tags { get; private set; }
        /// <summary>
        /// Temporary tags that can be set in the running list.
        /// </summary>
        public ITags TempTags { get; set; }
        /// <summary>
        /// Used to monitor canges in the file.
        /// </summary>
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

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opyum.AudioBase
{
    public class PlaylistItemChangedEventArgs
    {
        public PlaylistItemChangedEventArgs()
        {

        }

        public ItemChanges Changes { get; set; }

    }

    
}
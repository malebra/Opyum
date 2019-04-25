using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opyum.StandardPlayback.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AudioPlayerAttribute : Attribute
    {
        public AudioPlayerAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; } = String.Empty;

        public string Version { get; set; } = String.Empty;

        public Opyum.AudioBase.AudioType Type { get; set; } = Opyum.AudioBase.AudioType.None;

        public string SupportedFormats { get; set; } = String.Empty;
    }
}

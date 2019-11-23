using System;

namespace Opyum.Structures.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class PlaylistItemAttribute : Attribute
    {
        public string Version { get; set; }
    }
}

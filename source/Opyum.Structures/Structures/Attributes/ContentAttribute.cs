using System;
using Opyum.Playlist;

namespace Opyum.Structures.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class ContentAttribute : Attribute
    {
        public ContentAttribute(ContentType type, string name)
        {
            _type = type;
            Name = name;
        }

        private ContentType _type { get; set; }
        public ContentType Type { get => _type; }

        //private string _name = String.Empty;
        //public string Name { get => _name; set { _name = value; } }

        public string Name { get; private set; } = String.Empty;

        public string Descriptopn { get; set; } = String.Empty;
    }
}

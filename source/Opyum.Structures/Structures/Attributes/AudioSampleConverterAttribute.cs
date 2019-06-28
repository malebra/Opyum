using System;

namespace Opyum.Structures.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AudioSampleConverterAttribute : Attribute
    {
        public AudioSampleConverterAttribute()
        {

        }

        public string Extention { get; set; }


    }
}
using System;

namespace Opyum.Structures.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ApplicationPlatformAttribute : Attribute
    {
        public Enums.ApplicationPlatform Platform { get; private set; }

        public ApplicationPlatformAttribute(Enums.ApplicationPlatform platform)
        {
            Platform = platform;
        }
    }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opyum.WindowsPlatform
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ShortcutMethodAttribute : Attribute
    {
        public string Command { get; set; }

        public ShortcutMethodAttribute(string command)
        {
            Command = command;
        }
    }
}

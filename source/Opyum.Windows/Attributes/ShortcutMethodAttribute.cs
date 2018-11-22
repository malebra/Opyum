using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opyum.Windows.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    class ShortcutMethodAttribute : Attribute
    {
        public string Command { get; set; }

        public ShortcutMethodAttribute(string command)
        {
            Command = command;
        }
    }
}

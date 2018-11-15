using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opyum.Structures
{
    
    public class OpyumAttributes
    {
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
        public class UIModule : Attribute
        {
            public string Parent { get; set; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opyum.Windows.Class.Colors
{
    public interface IPlugin
    {
        String Name { get; }

        String Author { get; }

        String Company { get; }

        String Copyright { get; }

        void Load();

        void Load(IInteractions i);
    }
}

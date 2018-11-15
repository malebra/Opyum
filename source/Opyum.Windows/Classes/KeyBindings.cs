using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Opyum.Windows
{
    public class KeyBindings
    {
        public static KeyBinginArgument FullScreen { get; private set; } = new KeyBinginArgument() { Keys = new Keys[] { Keys.F11, Keys.Control}.ToList() };
    }
}

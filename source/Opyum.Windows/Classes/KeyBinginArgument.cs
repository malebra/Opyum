using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Opyum.Windows
{
    public class KeyBinginArgument
    {
        public List<Keys> Keys = new List<Keys>();

        public bool Match(KeyEventArgs e)
        {
            foreach (Keys key in Keys)
            {
                if (key == System.Windows.Forms.Keys.Control)
                {
                    if (Control.ModifierKeys.HasFlag(System.Windows.Forms.Keys.Control) == false)
                    {
                        return false; 
                    }
                }
                else if (key == System.Windows.Forms.Keys.Alt && Control.ModifierKeys.HasFlag(System.Windows.Forms.Keys.Alt) == false)
                {
                    if (Control.ModifierKeys.HasFlag(System.Windows.Forms.Keys.Alt) == false)
                    {
                        return false;
                    }
                }
                else if (key == System.Windows.Forms.Keys.Shift && Control.ModifierKeys.HasFlag(System.Windows.Forms.Keys.Shift) == false)
                {
                    if (Control.ModifierKeys.HasFlag(System.Windows.Forms.Keys.Shift) == false)
                    {
                        return false;
                    }
                }
                else if (e.KeyCode != key)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

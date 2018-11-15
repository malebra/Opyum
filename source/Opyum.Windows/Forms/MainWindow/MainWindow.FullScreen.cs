using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Opyum.Windows
{
    public partial class MainWindow
    {
		public void FullScreen_Check(KeyEventArgs e)
        {
            if (KeyBindings.FullScreen.Match(e))
            {
                if (IsFullScreen)
                {
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    this.Size = sizePriorToFullScreen;
                    this.Location = locationPriorToFullScreen;
                    IsFullScreen = false;
                    UnhideMenuBar();
                }
                else
                {
                    sizePriorToFullScreen = this.Size;
                    locationPriorToFullScreen = this.Location;
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Normal;
                    this.Bounds = Screen.PrimaryScreen.Bounds;
                    IsFullScreen = true;
                    HideMenuBar();
                }
            }
        }
    }
}

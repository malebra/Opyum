using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Opyum.Windows.Attributes;

namespace Opyum.Windows
{
    public partial class MainWindow
    {
        [ShortcutMethod("full_screen_mode_switch")]
		public void GoFullScreen()
        {
            if (IsFullScreen)
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.Size = sizePriorToFullScreen;
                this.Location = locationPriorToFullScreen;
                IsFullScreen = false;
                UnhideMenuBar();
                OnNormalScreen();
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
                OnFullScreen();
            }
        }

        public void OnFullScreen()
        {
            MenuStrip.Hide();
        }

        public void OnNormalScreen()
        {
            MenuStrip.Show();
        }
    }
}

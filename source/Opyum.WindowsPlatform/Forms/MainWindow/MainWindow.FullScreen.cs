using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Opyum.Structures.Attributes;

namespace Opyum.WindowsPlatform
{
    public partial class MainWindow
    {
        public enum ScreenMode {None = 0,  Normal = 1, FullScreen = 2 , Previous = 99};

        private ScreenMode _previousScreenViewMode;
        private ScreenMode _screenViewMode;

        public ScreenMode ScreenViewMode
        {
            get
            {
                return _screenViewMode;
            }
            set
            {
                if (value == ScreenMode.Previous)
                {
                    var temp = _previousScreenViewMode;
                    _screenViewMode = _previousScreenViewMode;
                    _previousScreenViewMode = temp;
                }
                else
                {
                    _previousScreenViewMode = _screenViewMode;
                    _screenViewMode = value;
                }
                ScreenModeChanged?.Invoke(this, new EventArgs());
            }
        }


        private Size sizePriorToFullScreen = new Size(50, 50);
        private Point locationPriorToFullScreen;


        public delegate void ScreenModeEvent(object sender, EventArgs e);
        public event ScreenModeEvent ScreenModeChanged;

        [ShortcutMethod("full_screen_mode_switch")]
		public void FullScreenModeChange()
        {
            if (ScreenViewMode == ScreenMode.FullScreen)
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.Size = sizePriorToFullScreen;
                this.Location = locationPriorToFullScreen;
                //IsFullScreen = false;
                UnhideMenuBar();
                OnNormalScreen();
                ScreenViewMode = ScreenMode.Previous;
            }
            else
            {
                sizePriorToFullScreen = this.Size;
                locationPriorToFullScreen = this.Location;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Normal;
                this.Bounds = Screen.PrimaryScreen.Bounds;
                //IsFullScreen = true;
                HideMenuBar();
                OnFullScreen();
                ScreenViewMode = ScreenMode.FullScreen;
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

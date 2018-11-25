using Opyum.Structures.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Opyum.WindowsPlatform
{
    public partial class MainWindow
    {
        private void MenuStripSetup()
        {

        }

        [ShortcutMethod("show_menu_strip_for_touch")]
        public void MenuStripOnCall()//object sender, EventArgs e)
        {
            MenuStrip.Height = MenuStrip.Height < 20 ? MenuStrip.Height * 4 : 18;
            foreach (ToolStripMenuItem item in MenuStrip.Items) item.Width = MenuStrip.Height < 20 ? (int)(item.Width / 4) : (int)(item.Width * 4);
            
            //ResizeMenuStripItems(MenuStrip.Items, MenuStrip.Height < 20 ? 1 / 4 : 4);

            MenuStrip.Show();
        }

        private void ResizeMenuStripItems(System.Windows.Forms.ToolStripItemCollection ctrl, double multiplyer)
        {
            if (ctrl.Count > 0)
            {
                foreach (ToolStripMenuItem control in ctrl)
                {
                    control.AutoSize = false;
                    if (control.DropDownItems.Count > 0)
                    {
                        ResizeMenuStripItems(control.DropDownItems, multiplyer);
                    }
                    control.Height = (int)(control.Height * multiplyer);
                }
            }
        }

        
    }
}

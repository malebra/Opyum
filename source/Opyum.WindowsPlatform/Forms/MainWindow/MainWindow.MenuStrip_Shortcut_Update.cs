using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Opyum.WindowsPlatform
{
    public partial class MainWindow : Form
    {
        /// <summary>
        /// Starts updating the shortcuta written in the menustrip bar
        /// </summary>
        private void MenuStrip_Shortcut_Update()
        {
            Dictionary<string, Keys> Shortcuts_Dict = new Dictionary<string, Keys>();

            Update_Shortcut_For_Item(MenuStrip.Items, Shortcuts_Dict);

            Update_MenueStrip_Items(MenuStrip.Items, Shortcuts_Dict);
        }

        /// <summary>
        /// Method finds items in the menustrip and updates the shortcuts assigned to them
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="shorts"></param>
        void Update_Shortcut_For_Item(ToolStripItemCollection collection, Dictionary<string, Keys> shorts)
        {
            foreach (var item in collection)
            {
                if (item.GetType() != typeof(ToolStripMenuItem))
                {
                    continue;
                }
                if (((ToolStripMenuItem)item).Tag != null)
                {
                    if (!shorts.Keys.Contains(((ToolStripMenuItem)item).Tag.ToString()))
                    {
                        IKeyBindingArgument arg = KeyBindingArgument.All.Where((x) => x.Command == ((ToolStripMenuItem)item).Tag.ToString()).FirstOrDefault();
                        if (arg != null)
                        {
                            shorts.Add(arg.Command, arg.Shortcut); 
                        }
                    } 
                }
                if (((ToolStripMenuItem)item).HasDropDownItems)
                {
                    Update_Shortcut_For_Item(((ToolStripMenuItem)item).DropDownItems, shorts);
                }
            }
        }

        /// <summary>
        /// Method updates the written shortucts for every item in the menustrip
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="shorts"></param>
        void Update_MenueStrip_Items(ToolStripItemCollection collection, Dictionary<string, Keys> shorts)
        {
            foreach (var item in collection)
            {
                if (item.GetType() != typeof(ToolStripMenuItem))
                {
                    continue;
                }
                if (((ToolStripMenuItem)item).Tag != null)
                {
                    if (shorts.Keys.Contains(((ToolStripMenuItem)item).Tag.ToString()))
                    {
                        ((ToolStripMenuItem)item).ShortcutKeys = shorts[((ToolStripMenuItem)item).Tag.ToString()];
                        ((ToolStripMenuItem)item).ShowShortcutKeys = true;
                    } 
                }
                if (((ToolStripMenuItem)item).HasDropDownItems)
                {
                    Update_MenueStrip_Items(((ToolStripMenuItem)item).DropDownItems, shorts);
                }
            }
        }
    }
}
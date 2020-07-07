using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Opyum.Structures.Attributes;
using Opyum.Structures.Playlist;

namespace Opyum.WindowsPlatform.Forms.Settings
{
    [OpyumSettingsPanelElement(typeof(List<ShortcutKeyBinding>))]
    public partial class ShortcutPanelElement : UserControl, ISettingsPanel<List<ShortcutKeyBinding>>, ISettingsPanel
    {
        int counter;
        public ShortcutPanelElement()
        {
            InitializeComponent();
            ListViewSetup();
            this.Show();
            //this.Resize += ResizeColumns;
        }

        private void ListViewSetup()
        {
            //Description.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            //ResizeColumns(this, new EventArgs());

            listViewshortcuts.ItemSelectionChanged += ItemSelected;
        }

        private void ItemSelected(object sender, EventArgs e)
        {
            if (listViewshortcuts.SelectedItems != null && listViewshortcuts.SelectedItems.Count > 0)
            {
                textBoxShortcut.Text = listViewshortcuts.SelectedItems[0].SubItems[2].Text; 
            }
        }

        public object LoadElements(List<ShortcutKeyBinding> data)
        {
            counter = 1;
            listViewshortcuts.Items.AddRange(data.Select(s => GenerateItem(s)).ToArray());
            return this;
        }

        public object LoadElements(object data)
        {
            return LoadElements((List<ShortcutKeyBinding>)data);
        }


        protected ListViewItem GenerateItem(ShortcutKeyBinding shortcut)
        {
            return new ListViewItem(new[] { counter++.ToString(), shortcut.Action, string.Join(", ", shortcut.Shortcut.ToArray()), shortcut.Description }) { Tag = shortcut };
        }


        //private void ResizeColumns(object sender, EventArgs e)
        //{
        //    int s = 0;
        //    try
        //    {
        //        foreach (var item in ShortcutListView.Columns)
        //        {
        //            s += ((ColumnHeader)item).Width;
        //        }
        //    }
        //    catch 
        //    {

        //        throw;
        //    }
        //    Description.Width = ShortcutListView.Width - s;
        //}
    }
}

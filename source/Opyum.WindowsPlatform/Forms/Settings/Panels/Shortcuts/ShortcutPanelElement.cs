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
using System.Configuration;
using Opyum.WindowsPlatform.Settings;
using System.Reflection;
using Opyum.WindowsPlatform.Attributes;
using Opyum.WindowsPlatform.Settings;
using System.IO;
using static System.Windows.Forms.ListView;
using System.Collections;

namespace Opyum.WindowsPlatform.Forms.Settings
{
    [OpyumSettingsPanelElement(typeof(List<ShortcutKeyBinding>))]
    public partial class ShortcutPanelElement : UserControl, ISettingsPanel, IUndoRedoable
    {
        List<ListViewItem> Data { get; set; } = null;
        UndoRedoStack UnredoStack { get; set; } = new UndoRedoStack();
        

        public ShortcutPanelElement()
        {
            InitializeComponent();
            this.KeyDown += KeyPressResolve;
            this.listViewshortcuts.ItemSelectionChanged += ItemSelected;
            this.Show();
            
        }

        private void ItemSelected(object sender, EventArgs e)
        {
            if (listViewshortcuts.SelectedItems != null && listViewshortcuts.SelectedItems.Count > 0)
            {
                textBoxShortcut.Text = string.Join(", ", ((IShortcutKeyBinding)listViewshortcuts.SelectedItems[0].Tag).Shortcut);
                isGlobalCheckBox.Checked = ((IShortcutKeyBinding)listViewshortcuts.SelectedItems[0].Tag).Global;
                isDisabledCheckBox.Checked = ((IShortcutKeyBinding)listViewshortcuts.SelectedItems[0].Tag).IsDisabled;
            }
        }


        public object LoadElements()
        {
            //var directory = Path.GetDirectoryName(Uri.UnescapeDataString((new UriBuilder(Assembly.GetExecutingAssembly().CodeBase)).Path));
            //var files = Directory.CreateDirectory(directory).GetFiles(searchPattern: "*.dll", searchOption: SearchOption.AllDirectories).Where(a => a.FullName.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))?.Select(u => u.FullName);
            //List<OpyumShortcutMethodAttribute> sclist = new List<OpyumShortcutMethodAttribute>();

            //foreach (var item in files)
            //{
            //    try
            //    {
            //        sclist.AddRange(Assembly.LoadFile(item)?.GetTypes()?.SelectMany(t => t.GetMethods()?.Select(m => m.GetCustomAttribute<OpyumShortcutMethodAttribute>()))?.Where(h => h != null));
            //    }
            //    catch (Exception)
            //    {
            //        continue;
            //    }
            //}
            var p = Assembly.GetExecutingAssembly().GetTypes()?.SelectMany(g => g.GetMethods().Select(u => u.GetCustomAttribute<OpyumShortcutMethodAttribute>()))?.Where(f => f != null);

            //Data = new List<ListViewItem>(sclist.Select(l => GenerateItem(l)));
            Data = p?.Select(g => GenerateItem(g)).ToList();
            listViewshortcuts.Items.AddRange(Data.ToArray());
            return this;

        }


        protected ListViewItem GenerateItem(IShortcutKeyBinding shortcut)
        {
            return new ListViewItem(new[] { shortcut.Action, string.Join(", ", shortcut.Shortcut.ToArray()), shortcut.Global ? "Yes" : "No", shortcut.IsDisabled ? "Yes" : "No", shortcut.Description }) { Tag = shortcut };
        }

        protected ListViewItem GenerateItem(ShortcutKeyBinding shortcut)
        {
            return GenerateItem((IShortcutKeyBinding)shortcut);
        }

        protected ListViewItem GenerateItem(OpyumShortcutMethodAttribute shortcut)
        {
            var keybinding = SettingsEditor.Settings.NewSettings.Shortcuts.Where(d => d.Command == shortcut.Command)?.FirstOrDefault();
            if (keybinding != null)
            {
                return GenerateItem(keybinding);
            }
            else
            {
                return new ListViewItem(new[] { shortcut.Action, "", "", "", shortcut.Description });
            }
            
        }

        //updates list viwe when something is typed in the search text box
        private void textBoxSearch_TextChange(object sender, EventArgs e)
        {
            if (textBoxSearch.Text != string.Empty && textBoxSearch != null)
            {
                listViewshortcuts.Items.Clear();
                listViewshortcuts.Items.AddRange(Data.Where(a => textBoxSearch?.Text?.ToLower()?.Split(' ')?.Select(b => (bool)a.SubItems[0].Text.ToLower()?.Contains(b) && b != string.Empty)?.Where(b => (bool)b)?.FirstOrDefault() == true ? true : false)?.ToArray());//listViewshortcuts.Items.AddRange((new List<ListViewItem>(lst))?.Where(a => textBoxSearch?.Text?.ToLower()?.Split(' ')?.Select(b => (bool)a.SubItems[0].Text.ToLower()?.Contains(b) && b != string.Empty)?.Where(b => (bool)b)?.FirstOrDefault() == true ? true : false)?.ToArray());
            }
        }

        //clear the search
        private void buttonClearSearch_Click(object sender, EventArgs e)
        {
            listViewshortcuts?.Items.Clear();
            textBoxSearch.Text = string.Empty;
            listViewshortcuts.Items.AddRange(Data.ToArray());
        }


        private void getShortcut(object sender, KeyEventArgs e)
        {
            textBoxShortcut.Clear();
            textBoxShortcut.Text = ShortcutResolver.GetShortcutString(sender, e);
            textBoxShortcut.Text = textBoxShortcut.Text == "Back" ? "" : textBoxShortcut.Text;
        }

        private void buttonSaveShortcut_Click(object sender, EventArgs e)
        {
            
            if (listViewshortcuts.SelectedItems.Count == 1 && textBoxShortcut.Text != string.Empty)
            {
                var existing = Data.Where(a => a.SubItems[1].Text == textBoxShortcut.Text);
                if (existing.Count() > 0 && existing.FirstOrDefault() != listViewshortcuts.SelectedItems[0])
                {
                    if (MessageBox.Show($"This string is already in use by \"{existing?.FirstOrDefault()?.SubItems[0].Text}\".\nDo you want to owerwrite it?\n\nWARNING: the old shortcut will be deleted!", "nWARNING: shortcut already in use!", MessageBoxButtons.YesNo, icon: MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        List<UndoRedoMethodCapsule> lst = new List<UndoRedoMethodCapsule>();
                        var cone = ((IShortcutKeyBinding)existing?.FirstOrDefault().Tag).Clone();
                        (cone.Global, cone.IsDisabled, cone.Shortcut) = (isGlobalCheckBox.Checked, isDisabledCheckBox.Checked, new List<string>());
                        lst.Add(new UndoRedoMethodCapsule(ChangeShortcut, existing?.FirstOrDefault(), cone, this));

                        cone = ((IShortcutKeyBinding)listViewshortcuts.SelectedItems[0].Tag).Clone();
                        (cone.Global, cone.IsDisabled, cone.Shortcut) = (isGlobalCheckBox.Checked, isDisabledCheckBox.Checked, new List<string>(textBoxShortcut.Text.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)));
                        lst.Add(new UndoRedoMethodCapsule(ChangeShortcut, listViewshortcuts.SelectedItems[0], cone, this));

                        UnredoStack.DoMany(lst);
                        return;
                    }
                }
                var clone = ((ShortcutKeyBinding)listViewshortcuts.SelectedItems[0].Tag).Clone();
                clone.Shortcut = new List<string>(textBoxShortcut.Text.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries));
                (clone.Global, clone.IsDisabled) = (isGlobalCheckBox.Checked, isDisabledCheckBox.Checked);

                UnredoStack.Do(ChangeShortcut, listViewshortcuts.SelectedItems[0],  clone, this);
            }
        }

        public object ChangeShortcut(object o, object srt)
        {
            if (o is ListViewItem && srt is IShortcutKeyBinding)
            {
                var old = ((IShortcutKeyBinding)((ListViewItem)o).Tag).Clone();
                ((IShortcutKeyBinding)((ListViewItem)o).Tag).UpdateDataFromKeybinding((IShortcutKeyBinding)srt);
                //((ListViewItem)o).SubItems[1].Text = string.Join(", ", ((IShortcutKeyBinding)((ListViewItem)o).Tag).Shortcut);
                var f = GenerateItem((IShortcutKeyBinding)((ListViewItem)o).Tag);
                for (int i = 0;  i < ((ListViewItem)o).SubItems.Count;  i++)
                {
                    ((ListViewItem)o).SubItems[i] = f.SubItems[i];
                }
                return old; 
            }
            return new List<string>();
        }

        public void KeyPressResolve(object sender, KeyEventArgs e)
        {
            if (!textBoxShortcut.Focused)
            {
                if (e.KeyData == (Keys.Control | Keys.Z))
                {
                    UnredoStack.Undo();
                }
                if (e.KeyData == (Keys.Control | Keys.Shift | Keys.Z))
                {
                    UnredoStack.Redo();
                } 
            }
        }

        public void Undo()
        {
            UnredoStack?.Undo();
        }

        public void Redo()
        {
            UnredoStack?.Redo();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Opyum.Structures.Attributes;
using Opyum.WindowsPlatform.Settings;
using System.Reflection;
using Opyum.Engine.Attributes;
using System.IO;
using static System.Windows.Forms.ListView;
using System.Collections;
using Opyum.Structures.Global;
using Opyum.Engine;
using Opyum.Engine.Settings;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Opyum.WindowsPlatform.Forms.Settings
{
    [OpyumSettingsPanelElement(typeof(List<ShortcutKeyBinding>))]
    public partial class ShortcutPanelElement : UserControl, ISettingsPanel, IUndoRedoable
    {
        List<ListViewItem> Data { get; set; } = null;
        

        public ShortcutPanelElement()
        {
            InitializeComponent();
            this.KeyDown += KeyPressResolve;
            this.listviewshortcuts.ItemSelectionChanged += ItemSelected;
            this.isGlobalCheckBox.CheckedChanged += globalChecked;
            this.isDisabledCheckBox.CheckedChanged += disabledChecked;
            this.Show();
        }

        private void ItemSelected(object sender, EventArgs e)
        {
            if (listviewshortcuts.SelectedItems != null && listviewshortcuts.SelectedItems.Count > 0)
            {
                textBoxShortcut.Text = string.Join(", ", ((IShortcutKeyBinding)listviewshortcuts.SelectedItems[0].Tag).Shortcut);
                isGlobalCheckBox.Enabled = true;
                isGlobalCheckBox.Checked = ((IShortcutKeyBinding)listviewshortcuts.SelectedItems[0].Tag).Global;
                isDisabledCheckBox.Enabled = true;
                isDisabledCheckBox.Checked = ((IShortcutKeyBinding)listviewshortcuts.SelectedItems[0].Tag).IsDisabled;
            }
            else
            {
                isGlobalCheckBox.Checked = false;
                isDisabledCheckBox.Checked = false;
                isGlobalCheckBox.Enabled = false;
                isDisabledCheckBox.Enabled = false;
                textBoxShortcut.Text = string.Empty;
            }
        }

        private void globalChecked(object sender, EventArgs e)
        {
            if (listviewshortcuts.SelectedItems.Count > 0)
            {
                //do something
            }
        }

        private void disabledChecked(object sender, EventArgs e)
        {
            if (listviewshortcuts.SelectedItems.Count > 0)
            {
                //do something
            }
        }


        public async Task<Control> LoadElements()
        {
            //var p = SettingsEditor.Settings?.NewSettings?.Shortcuts;
            //Data = p?.Select(g => GenerateItem(g)).ToList();
            //listviewshortcuts.Items.AddRange(Data.ToArray());
            //return this;



            Data = new List<ListViewItem>();
            foreach (var item in SettingsEditor.Settings?.NewSettings?.Shortcuts)
            {
                var tempItem = await Task.Run(() => GenerateItem(item));
                listviewshortcuts.Items.Add(tempItem);
                Data.Add(tempItem);

            }
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
            //if something is written in the textbox
            if (textBoxSearch.Text != string.Empty && textBoxSearch != null)
            {
                listviewshortcuts.BeginUpdate();
                listviewshortcuts.Items.Clear();
                //listviewshortcuts.Items.AddRange(Data.Where(a => textBoxSearch?.Text?.ToLower()?.Split(' ')?.Select(b => (bool)a.SubItems[0].Text.ToLower()?.Contains(b) && b != string.Empty)?.Where(b => (bool)b)?.FirstOrDefault() == true ? true : false)?.ToArray());//listViewshortcuts.Items.AddRange((new List<ListViewItem>(lst))?.Where(a => textBoxSearch?.Text?.ToLower()?.Split(' ')?.Select(b => (bool)a.SubItems[0].Text.ToLower()?.Contains(b) && b != string.Empty)?.Where(b => (bool)b)?.FirstOrDefault() == true ? true : false)?.ToArray());
                
                //compare each word in the textbox and find if all are present in either the name or the description of the shortcut, update the listview
                listviewshortcuts.Items.AddRange(Data.Where(a => (bool)textBoxSearch?.Text?.ToLower().Split(' ')?.Where(u => u != String.Empty)?.All(p => a.SubItems[0].Text.ToLower().Contains(p) || a.SubItems[4].Text.ToLower().Contains(p))).ToArray());
                listviewshortcuts.EndUpdate();
                return;
            }
            //if the textbox was cleare empty, put all data back in
            listviewshortcuts?.Items.Clear();
            listviewshortcuts.Items.AddRange(Data.ToArray());
        }

        //clear the search
        private void buttonClearSearch_Click(object sender, EventArgs e)
        {
            listviewshortcuts?.Items.Clear();
            textBoxSearch.Text = string.Empty;
            listviewshortcuts.Items.AddRange(Data.ToArray());
        }

        private void buttonClearShortcut_Click(object sender, EventArgs e)
        {
            if (listviewshortcuts.SelectedItems.Count > 0 && listviewshortcuts.SelectedItems[0] != null)
            {
                try
                {
                    var arg = ((IShortcutKeyBinding)listviewshortcuts.SelectedItems[0].Tag).Clone();
                    arg.Clear();
                    SettingsEditor.Settings?.UndoRedo?.Do(new ChangeShortcutWorker((IShortcutKeyBinding)listviewshortcuts.SelectedItems[0].Tag, arg, listviewshortcuts.SelectedItems[0]));
                }
                catch (Exception err)
                {
                    throw err;
                }
            }
        }

        private void buttonSaveShortcut_Click(object sender, EventArgs e)
        {
            
            if (listviewshortcuts.SelectedItems.Count == 1 && textBoxShortcut.Text != string.Empty)
            {
                //check if the shortcut is already in use
                var existing = Data.Where(a => a.SubItems[1].Text == textBoxShortcut.Text);
                if (existing.Count() > 0 && existing.FirstOrDefault() != listviewshortcuts.SelectedItems[0])
                {
                    //asks if the olds shortcut should be deleted
                    if (MessageBox.Show($"This string is already in use by \"{existing?.FirstOrDefault()?.SubItems[0].Text}\".\nDo you want to owerwrite it?\n\nWARNING: the old shortcut will be deleted!", "nWARNING: shortcut already in use!", MessageBoxButtons.YesNo, icon: MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }
                    //if the shortcut should be replaced
                    else
                    {
                        //create a list that will contain the operations that need to be executed
                        List<IUndoRedoWorker> lst = new List<IUndoRedoWorker>();
                        //empty the old shortcut
                        var cone = ((IShortcutKeyBinding)existing?.FirstOrDefault().Tag).Clone();
                        (cone.Global, cone.IsDisabled, cone.Shortcut) = (isGlobalCheckBox.Checked, isDisabledCheckBox.Checked, new List<string>());
                        lst.Add(new ChangeShortcutWorker((IShortcutKeyBinding)existing?.FirstOrDefault().Tag, cone, existing?.FirstOrDefault()));

                        //create the new shortcut
                        cone = ((IShortcutKeyBinding)listviewshortcuts.SelectedItems[0].Tag).Clone();
                        (cone.Global, cone.IsDisabled, cone.Shortcut) = (isGlobalCheckBox.Checked, isDisabledCheckBox.Checked, new List<string>(textBoxShortcut.Text.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)));
                        lst.Add(new ChangeShortcutWorker((IShortcutKeyBinding)listviewshortcuts.SelectedItems[0].Tag, cone, listviewshortcuts.SelectedItems[0]));

                        //requiest to execute the operation
                        SettingsEditor.Settings?.UndoRedo?.DoMany(lst);
                        return;
                    }
                }
                //if the new shortcut is not already in use
                var clone = ((ShortcutKeyBinding)listviewshortcuts.SelectedItems[0].Tag).Clone();
                clone.Shortcut = new List<string>(textBoxShortcut.Text.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries));
                (clone.Global, clone.IsDisabled) = (isGlobalCheckBox.Checked, isDisabledCheckBox.Checked);

                //requiest to execute the operation
                SettingsEditor.Settings?.UndoRedo?.Do(new ChangeShortcutWorker((IShortcutKeyBinding)listviewshortcuts.SelectedItems[0].Tag,  clone, listviewshortcuts.SelectedItems[0]));
            }
        }

        public void KeyPressResolve(object sender, KeyEventArgs e)
        {
            if (!textBoxShortcut.Focused)
            {
                if (e.KeyData == (Keys.Control | Keys.Z))
                {
                    SettingsEditor.Settings?.UndoRedo?.Undo();
                }
                if (e.KeyData == (Keys.Control | Keys.Shift | Keys.Z))
                {
                    SettingsEditor.Settings?.UndoRedo?.Redo();
                } 
            }
        }

        /// <summary>
        /// Get the shortcut when pressed on the textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getShortcut(object sender, KeyEventArgs e)
        {
            textBoxShortcut.Clear();
            var text = ShortcutResolver.GetShortcutString(sender, e);
            textBoxShortcut.Text = text == "Back" ? "" : text;
        }

        public void Undo()
        {
            SettingsEditor.Settings?.UndoRedo?.Undo();
        }

        public void Redo()
        {
            SettingsEditor.Settings?.UndoRedo?.Redo();
        }
    }
}
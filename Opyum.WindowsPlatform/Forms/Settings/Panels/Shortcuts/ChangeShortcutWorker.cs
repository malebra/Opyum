using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Opyum.Engine;
using Opyum.Structures.Global;

namespace Opyum.WindowsPlatform.Forms.Settings
{
    public class ChangeShortcutWorker : UndoRedoWorker<IShortcutKeyBinding>
    {

        protected ListViewItem _listViewItem { get; set; }
        protected ChangeShortcutWorker() { }
        public ChangeShortcutWorker(IShortcutKeyBinding item, IShortcutKeyBinding newValue, ListViewItem listViewItem) : base(item, newValue) 
        {
            _listViewItem = listViewItem;
            Do(item, newValue);
            
        }



        public override void Do(IShortcutKeyBinding item, IShortcutKeyBinding newValue)
        {
            //grab the old shortcut
            var old = item.Clone();
            //update the shortcut info
            item.UpdateDataFromKeybinding(newValue);

            Item = item;
            PreviousValue = old;
            updateListViewItem(_listViewItem);
        }

        public override void Redo()
        {
            Do(Item, PreviousValue);
        }

        public override void Undo()
        {
            Do(Item, PreviousValue);
        }






        private static void updateListViewItem(ListViewItem listViewItem)
        {
            for (int i = 0; i < listViewItem.SubItems.Count; i++)
            {
                listViewItem.SubItems[i] = generateItem((IShortcutKeyBinding)listViewItem.Tag).SubItems[i];
            }
        }

        private static ListViewItem generateItem(IShortcutKeyBinding shortcut)
        {
            return new ListViewItem(new[] { shortcut.Action, string.Join(", ", shortcut.Shortcut.ToArray()), shortcut.Global ? "Yes" : "No", shortcut.IsDisabled ? "Yes" : "No", shortcut.Description }) { Tag = shortcut };
        }

        public static void DoWork(IShortcutKeyBinding item, IShortcutKeyBinding newItem, ListViewItem listViewItem, UndoRedoStackV2 stack)
        {
            stack.Do(new ChangeShortcutWorker(item, newItem, listViewItem));
        }
    }
}

using Opyum.Structures.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opyum.WindowsPlatform.Settings
{
    public partial class SettingsEditor
    {
        internal UndoRedoStack UndoRedo = new UndoRedoStack();

        private void UpdareUponUndoRedoStackChange(object sender, EventArgs e)
        {
            applyButton.Enabled = UndoRedo?.UndoCount > 0 ? true : false;
        }
    }
}

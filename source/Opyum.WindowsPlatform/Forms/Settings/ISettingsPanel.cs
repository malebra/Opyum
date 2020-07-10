using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Opyum.WindowsPlatform.Settings;

namespace Opyum.WindowsPlatform.Forms.Settings
{
    public interface ISettingsPanel<T> : IUndoRedoable
    {
        object LoadElements(T data);
    }

    public interface ISettingsPanel : IUndoRedoable
    {
        object LoadElements();

        void KeyPressResolve(object sender, KeyEventArgs e);
    }


}

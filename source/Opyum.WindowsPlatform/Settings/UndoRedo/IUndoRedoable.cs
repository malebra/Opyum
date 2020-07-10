using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Opyum.WindowsPlatform.Settings
{
    public interface IUndoRedoable
    {
        void Undo();
        void Redo();

        //void Undo(object sender, KeyEventArgs e);
        //void Redo(object sender, KeyEventArgs e);


        //void Do(Delegate method, object args);
    }
}

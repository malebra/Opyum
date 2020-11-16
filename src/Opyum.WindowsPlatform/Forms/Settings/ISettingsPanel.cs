using System.Windows.Forms;
using Opyum.WindowsPlatform.Settings;
using Opyum.Structures.Global;
using System.Threading.Tasks;

namespace Opyum.WindowsPlatform.Forms.Settings
{
    public interface ISettingsPanel<T> : IUndoRedoable
    {
        Task<Control> LoadElements(T data);
    }

    public interface ISettingsPanel : IUndoRedoable
    {
        Task<Control> LoadElements();

        void KeyPressResolve(object sender, KeyEventArgs e);
    }


}

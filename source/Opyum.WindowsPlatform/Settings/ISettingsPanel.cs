using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opyum.WindowsPlatform.Forms.Settings
{
    public interface ISettingsPanel<T>
    {
        object LoadElements(T data);
    }

    public interface ISettingsPanel : ISettingsPanel<object>
    {

    }
}

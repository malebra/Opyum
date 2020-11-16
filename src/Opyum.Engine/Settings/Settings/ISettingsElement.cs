using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opyum.Engine.Settings
{
    public interface ISettingsElement<T>
    {
        T Clone();
    }
}

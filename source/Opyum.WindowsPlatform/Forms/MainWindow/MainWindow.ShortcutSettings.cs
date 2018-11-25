using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Opyum.Structures.Attributes;

namespace Opyum.WindowsPlatform
{
    partial class MainWindow
    {
        void ResolveShortcut(object sender, KeyEventArgs e)
        {
            KeyBindingArgument.All.Where(i => i.Shortcut == e.KeyData).FirstOrDefault()?.Run();
        }

        void SetUpShortcuts()
        {
            //KeyBindingArgument.All.Where(i => i.Command == "full_screen_mode_switch").FirstOrDefault().AddFunction(GoFullScreen);
            var info = Assembly.GetExecutingAssembly().GetTypes().SelectMany(i => i.GetMethods().Where(m => m.GetCustomAttributes(typeof(ShortcutMethodAttribute)).Count() > 0)).ToArray();
            
            KeyBindingArgument.All.ForEach(arg =>
            {
                MethodInfo method = info.Where(m => m.GetCustomAttribute<ShortcutMethodAttribute>().Command == arg.Command).FirstOrDefault();
                if (method != null)
                {
                    arg.AddFunction(Delegate.CreateDelegate(typeof(KeyBindingArgument.DELL), this, method));
                }
            });

        }
    }
}

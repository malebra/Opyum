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
        /// <summary>
        /// Runs the method assigned to the shortcut
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ResolveShortcut(object sender, KeyEventArgs e)
        {
            IKeyBindingArgument z = KeyBindingArgument.All.Where(i => i.Shortcut == e.KeyData).FirstOrDefault();
            z?.Run();
        }

        /// <summary>
        /// Binds the shortcuts from the shortcut file to the given method
        /// </summary>
        void SetUpShortcuts()
        {
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

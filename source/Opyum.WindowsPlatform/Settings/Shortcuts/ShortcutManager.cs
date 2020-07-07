using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Opyum.WindowsPlatform.Attributes;

namespace Opyum.WindowsPlatform.Settings
{
    public static class ShortcutManager
    {
        static int _errorCount { get; set; } = 0;
        internal static void SetUpShortcutsOnRequest(object arg, EventArgs e)
        {
            var info = Assembly.GetExecutingAssembly().GetTypes().SelectMany(i => i.GetMethods().Where(m => m.GetCustomAttributes(typeof(OpyumShortcutMethodAttribute)).Count() > 0)).ToArray();

            MethodInfo method = info.Where(m => m.GetCustomAttribute<OpyumShortcutMethodAttribute>().Command == ((IShortcutKeyBinding)arg).Command).FirstOrDefault();
            if (method != null)
            {
                ((IShortcutKeyBinding)arg).AddFunction(Delegate.CreateDelegate(typeof(ShortcutKeyBinding.DELEGATE), null, method));
                ((IShortcutKeyBinding)arg).Description = ((OpyumShortcutMethodAttribute)method.GetCustomAttribute(typeof(OpyumShortcutMethodAttribute))).Description;
                ((IShortcutKeyBinding)arg).Action = ((OpyumShortcutMethodAttribute)method.GetCustomAttribute(typeof(OpyumShortcutMethodAttribute))).Action;
            }

        }

        internal static List<ShortcutKeyBinding> GetUdatedShortcuts(string path)
        {
            string fileText = SettingsInterpreter.GetJsonFormFile(path);
            var temp = new List<ShortcutKeyBinding>(JsonConvert.DeserializeObject<SettingsContainer>(fileText).Shortcuts);

            //clone all the temp ones
            return temp.Select(x => new ShortcutKeyBinding() { Command = x.Command, Shortcut = x.Shortcut }).ToList();
            
        }
    }
}

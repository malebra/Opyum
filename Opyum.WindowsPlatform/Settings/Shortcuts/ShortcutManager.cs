using Newtonsoft.Json;
using Opyum.WindowsPlatform.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime;

namespace Opyum.WindowsPlatform.Settings
{
    public static class ShortcutManager
    {
        private static Dictionary<string, MethodInfo> CommandMethods = null;

        static int _errorCount { get; set; } = 0;
        public static void SetUpShortcutsOnRequest(object arg, EventArgs e)
        {
            //var info = Assembly.GetExecutingAssembly().GetTypes().SelectMany(i => i.GetMethods().Where(m => m.GetCustomAttributes(typeof(OpyumShortcutMethodAttribute)).Count() > 0)).ToArray();
            if (CommandMethods == null)
            {
                GetCommandMethods();
            }

            MethodInfo method = CommandMethods?.Where(m => m.Key == ((IShortcutKeyBinding)arg).Command)?.FirstOrDefault().Value;
            if (method != null)
            {
                var attr = method.GetCustomAttribute<OpyumShortcutMethodAttribute>();
                ((IShortcutKeyBinding)arg).AddFunction(Delegate.CreateDelegate(typeof(ShortcutKeyBinding.DELEGATE), null, method));
                ((IShortcutKeyBinding)arg).Description = attr.Description;
                ((IShortcutKeyBinding)arg).Action = attr.Action;
            }
        }

        public static List<ShortcutKeyBinding> GetAllUnbindedShortcuts()
        {
            if (CommandMethods == null)
            {
                GetCommandMethods();
            }


            List<ShortcutKeyBinding> list = new List<ShortcutKeyBinding>();
            try
            {
                list.AddRange(CommandMethods?.Where(s => !(bool)SettingsManager.GlobalSettings?.Shortcuts?.Exists(g => g.Command == s.Key))?.Select(k => GetUnbindedKeybindingFromMethodInfo(k.Value)));
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }

        private static void GetShortcutToItsMethod(IShortcutKeyBinding shortcut)
        {
            
        }

        private static void GetCommandMethods()
        {
            if (CommandMethods == null)
            {
                CommandMethods = new Dictionary<string, MethodInfo>();
            }

            var directory = Path.GetDirectoryName(Uri.UnescapeDataString((new UriBuilder(Assembly.GetExecutingAssembly().CodeBase)).Path));
            var files = Directory//.CreateDirectory(directory)
                        .EnumerateFiles(directory)
                        //?.GetFiles(searchPattern: "*.dll|*.exe", searchOption: SearchOption.AllDirectories)
                        ?.Where(a => a.ToLower().EndsWith(".dll", StringComparison.OrdinalIgnoreCase) || a.ToLower().EndsWith(".exe", StringComparison.OrdinalIgnoreCase));
                        //?.Select(u => u.FullName);

            List<KeyValuePair<string, MethodInfo>> add = new List<KeyValuePair<string, MethodInfo>>();
            foreach (var file in files)
            {
                try
                {
                    add.AddRange(Assembly.LoadFile(file)
                        ?.GetTypes()
                        ?.SelectMany(t => t?.GetMethods()
                            ?.Where(m => m?.GetCustomAttribute<OpyumShortcutMethodAttribute>() != null))
                        ?.Distinct()
                        ?.Select(x => new KeyValuePair<string, MethodInfo>(x.GetCustomAttribute<OpyumShortcutMethodAttribute>().Command, x)));
                }
                catch (Exception e)
                {
                    if (e is InvalidOperationException || e is ReflectionTypeLoadException || e is BadImageFormatException || e is FileLoadException || e is FileNotFoundException)
                    {
                        continue; 
                    }
                    throw e;
                }
            }

            foreach (var item in add?.Distinct())
            {
                CommandMethods.Add(item.Key, item.Value);
            }
        }

        public static List<ShortcutKeyBinding> GetUdatedShortcuts(string path)
        {
            string fileText = SettingsInterpreter.GetJsonFormFile(path);
            var temp = new List<ShortcutKeyBinding>(JsonConvert.DeserializeObject<SettingsContainer>(fileText).Shortcuts);

            //clone all the temp ones
            return temp.Select(x => new ShortcutKeyBinding() { Command = x.Command, Shortcut = x.Shortcut }).ToList();
            
        }

        private static ShortcutKeyBinding GetKeybindingFromMethodInfo(MethodInfo method)
        {
            try
            {
                var atr = method.GetCustomAttribute<OpyumShortcutMethodAttribute>();
                if (atr == null)
                {
                    return null;
                }
                else
                {
                    return ShortcutKeyBinding.GenerateKeyBinding(atr.Command, atr.DefaultShortcut, atr.Action, atr.Description);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static ShortcutKeyBinding GetUnbindedKeybindingFromMethodInfo(MethodInfo method)
        {
            try
            {
                var atr = method.GetCustomAttribute<OpyumShortcutMethodAttribute>();
                if (atr == null)
                {
                    return null;
                }
                else
                {
                    return ShortcutKeyBinding.GenerateKeyBinding(atr.Command, new List<string>(), atr.Action, atr.Description);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

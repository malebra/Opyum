using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using Newtonsoft.Json;

namespace Opyum.Windows
{
    public class SettingsInterpreter
    {
        public static XmlDocument SettingsXML { get; private set; }

        public static void Load()
        {
            Load(Paths.BaseSettingsPath);
            
        }

        public static void Load(string file)
        {
            Dictionary<string, string> files = new Dictionary<string, string>();
            foreach (string f in System.IO.Directory.GetFiles(Paths.SettingsDirectoryPath))
            {
                files.Add(f, Path.GetFileNameWithoutExtension(f));
            }

            string BaseSettings;  

            using (var location = File.Open(file, FileMode.Open))
            using (StreamReader sr = new StreamReader(location, Encoding.Default))
            {
                BaseSettings = sr.ReadToEnd();
            }
            
            foreach (KeyValuePair<string, string> key in files)
            {
                string Short = string.Empty;

                using (var location = File.Open(key.Key, FileMode.Open))
                using (StreamReader sr = new StreamReader(location, Encoding.Default))
                {
                    Short = sr.ReadToEnd();
                }

                BaseSettings = JSON_Swapper(BaseSettings, $"\"{key.Value}\"", Paths.ShortcutJSONPath);
            }

            SettingsXML = JsonConvert.DeserializeXmlNode(BaseSettings);
        }

        private static string JSON_Swapper(string OriginalJSON, string replaceing, string replacement_file)
        {
            string replacement;
            while (!FileWork.IsFileReady(replacement_file)) { }
            using (var file = File.Open(replacement_file, FileMode.Open))
            using (StreamReader sr = new StreamReader(file, Encoding.Default))
            {
                replacement = sr.ReadToEnd();
            }

            return OriginalJSON.Replace($"{replaceing}: null", $"{replaceing}: {replacement}");

        }
    }
}

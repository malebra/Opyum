using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Opyum.WindowsPlatform.Settings
{
    public static class SettingsManager 
    {
        public static SettingsContainer GlobalSettings { get; set; }





        public static string GetSettingsDirectoryPath()
        {
            return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"..\Settings"));
        }

        public static void LoadSettings()
        {
            GlobalSettings = SettingsInterpreter.Load();
        }

        public static void UpdateSettingsFromFile(string path)
        {
            //check if the file exists and is in the poper folder
            if (!File.Exists(path) || Path.GetDirectoryName(path) != GetSettingsDirectoryPath())
            {
                return;
            }

            //update the settings by reading the Json from the file
            UpdateSettings(JsonConvert.DeserializeObject<SettingsContainer>($"{{{SettingsInterpreter.GetJsonFormFile(path)}}}"));
        }

        public static void UpdateSettings(SettingsContainer settings)
        {
            //get all properties from the type of settings and make sure they are public and instantiated
            var props = new List<PropertyInfo>(settings.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public));
            
            //remo the "Item" property representing the SettingsContainer's indexer property <this[string property] {get; set;}>
            props.RemoveAll(t => t.Name == "Item");

            //update each non-null property in settings to GlobalSettings
            foreach (var prop in props)
            {
                if (settings[prop.Name] != null)
                {
                    //if the property exists, put it in the GlobalSettings
                    //then remove it from the settings obejct so it can be disposed
                    GlobalSettings[prop.Name] = settings[prop.Name];
                    settings[prop.Name] = null;
                }
            }
            //clear the garbage
            settings.Dispose();
        }
    }
}

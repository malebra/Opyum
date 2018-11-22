using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Opyum.Windows
{
    public partial class MainWindow
    {
        private static string _settingsPath = $"{Path.GetDirectoryName(Path.GetDirectoryName((System.Reflection.Assembly.GetEntryAssembly().Location)))}\\Settings\\Settings.json";

        public static string SettingsPath
        {
            get { return _settingsPath; }
            set { _settingsPath = value; }
        }


    }
}

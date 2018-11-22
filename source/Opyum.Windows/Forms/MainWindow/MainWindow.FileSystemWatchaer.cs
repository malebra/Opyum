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

        FileSystemWatcher watcher = new FileSystemWatcher();

        private void FileSystemWatcherSetup()
        {
            watcher.Path = Paths.SettingsDirectoryPath;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "*.json";
            watcher.EnableRaisingEvents = true;
            watcher.Changed += (a, b) =>
            {
                SettingsInterpreter.Load();
            };
        }
    }
}

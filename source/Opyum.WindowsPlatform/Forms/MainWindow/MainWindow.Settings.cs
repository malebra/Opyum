using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Opyum.WindowsPlatform
{
    public partial class MainWindow
    {
        private void OpenSettings_OnClick(object sender, EventArgs e)
        {
            var ss = new Settings.Settings();
            ss.Show();
        }
    }
}

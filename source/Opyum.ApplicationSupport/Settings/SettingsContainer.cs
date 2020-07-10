using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

namespace Opyum.ApplicationSupport.Settings
{
    public class SettingsContainer
    {
        public void LoadSettings()
        {

            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);

        }
    }
}

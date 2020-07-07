using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Opyum.WindowsPlatform.Attributes;

namespace Opyum.WindowsPlatform.Settings
{
    public partial class SettingsForm : Form
    {
        static SettingsForm Settings { get; set; }
        private SettingsForm()
        {
            InitializeComponent();
            Setup();
        }


        void Closed(object a, FormClosedEventArgs e)
        {
            Settings = null;
            GC.Collect();
        }

        public static void OpenSettings()
        {
            if (Settings == null)
            {
                Settings = new SettingsForm();
                Settings.Show();
            }
            else
            {
                Settings.Focus();
            }
        }
    }
}

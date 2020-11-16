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
using Opyum.Engine.Attributes;

namespace Opyum.WindowsPlatform.Settings
{
    public partial class SettingsEditor : Form
    {
        public bool SettingsEditedFlag { get; set; } = false;
        public static SettingsEditor Settings { get; set; }
        private SettingsEditor()
        {
            InitializeComponent();
            Setup();
            UndoRedo.Changed += UpdareUponUndoRedoStackChange;
            
        }

        void WhenClosed(object a, FormClosedEventArgs e)
        {
            Settings = null;
            GC.Collect();
        }

        public static void OpenSettings()
        {
            if (Settings == null)
            {
                Settings = new SettingsEditor();
                Settings.Show();
            }
            else
            {
                Settings.Focus();
            }
        }
    }
}

using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Opyum.Structures.Attributes;

namespace Opyum.WindowsPlatform
{
    partial class MainWindow
    {
        //Autofac scope
        private static ILifetimeScope _scope;
        static ILifetimeScope Scope { get { return _scope; } }


        void WindowSetup()
        {
            //Autofac setup
            IContainer build = ContainerConfig.Configure();
            _scope = build.BeginLifetimeScope();

            //Load settings from file
            SettingsInterpreter.Load();

            //Bind all shortcuts
            KeyBindingArgument.AllBindingsSetup(SettingsInterpreter.SettingsXML.Clone());
            SetUpShortcuts();


            //Size Settings
            this._oldSize = this.Size;
            //this.SizeChanged += SizeSettings;
            this.SizeChanged += PanelSizeAdaptation;

            //File wather that checks if the settings files have been changed
            FileSystemWatcherSetup();

            //Back
            this.BackColor = Color.AliceBlue;

            //MenuStrip Settings
            this.MenuStripSetup();

            //FullScreenModeChange();
        }

        private void PanelSizeAdaptation(object sender, EventArgs e)
        {
            panel2.Width = (int)((double)this.Width / 4.8);
            panel2.Width = panel2.Width < 300 ? 300 : panel2.Width;

            panel1.Height = (int)((double)this.Height / 7.2);
            panel1.Height = panel1.Height < 120 ? 120 : panel1.Height;
        }

        [ShortcutMethod("bullshit")]
        public void Kill()
        {
            panel1.Visible = !panel1.Visible;
            panel2.Visible = !panel2.Visible;
            //panel2.Visible = !panel2.Visible;
        }
    }
}

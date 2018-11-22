using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Opyum.Windows.Attributes;

namespace Opyum.Windows
{
    partial class MainWindow
    {
        //Autofac scope
        private static ILifetimeScope _scope;
        static ILifetimeScope Scope { get { return _scope; } }// = ContainerConfig.Configure().BeginLifetimeScope();


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

            //File wather that checks if the settings files have been changed
            FileSystemWatcherSetup();

            //Back
            this.BackColor = Color.AliceBlue;
            
        }
    }
}

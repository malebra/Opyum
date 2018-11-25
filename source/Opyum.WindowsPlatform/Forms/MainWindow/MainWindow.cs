using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using Opyum.Structures;

namespace Opyum.WindowsPlatform
{
    [Opyum.Structures.Attributes.ApplicationPlatform(Structures.Enums.ApplicationPlatform.Windows)]
    public partial class MainWindow : Form
    {
        //public bool IsFullScreen { get; set; } = false;

        public MainWindow()
        {
            InitializeComponent();
            WindowSetup();

            panel2.SizeChanged += (a, b) =>
            {
                button1.Width = panel2.Width / 4;  
                button2.Width = panel2.Width / 4;
                button3.Width = panel2.Width / 4;
            };
        }

        private void MainWindow_MaximizedBoundsChanged(object sender, EventArgs e)
        {

        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            ResolveShortcut(sender, e);
        }
        

        //////////////////////////////////////////    VARS    //////////////////////////////////////////




        ///////////////////////////////////////    STATIC VARS    ///////////////////////////////////////




        ///////////////////////////////////////    CONSTRUCTOR    ///////////////////////////////////////




        ///////////////////////////////////      STATIC METHODS      ///////////////////////////////////




        ///////////////////////////////////////      METHOD      ///////////////////////////////////////




        ///////////////////////////////////////      EVENTS      ///////////////////////////////////////




    }
}

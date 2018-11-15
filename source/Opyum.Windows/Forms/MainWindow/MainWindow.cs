using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Opyum.TestWindow;

namespace Opyum.Windows
{
    public partial class MainWindow : Form
    {
        public bool IsFullScreen { get; set; } = false;
        private Size sizePriorToFullScreen = new Size(50, 50);
        private Point locationPriorToFullScreen;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_MaximizedBoundsChanged(object sender, EventArgs e)
        {

        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            FullScreen_Check(e);
        }



        //////////////////////////////////////////    VARS    //////////////////////////////////////////




        ///////////////////////////////////////    STATIC VARS    ///////////////////////////////////////




        ///////////////////////////////////////    CONSTRUCTOR    ///////////////////////////////////////




        ///////////////////////////////////      STATIC METHODS      ///////////////////////////////////




        ///////////////////////////////////////      METHOD      ///////////////////////////////////////




        ///////////////////////////////////////      EVENTS      ///////////////////////////////////////




    }
}

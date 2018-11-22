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
            WindowSetup();
            
        }

        private void MainWindow_MaximizedBoundsChanged(object sender, EventArgs e)
        {

        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            ResolveShortcut(sender, e);
            //ShortcutInterpreter.ResolveShortcut(sender, e);
        }

        void RemoveThisLater()
        {

            string z;
            string path = @"C:\Users\Borno\AppData\Roaming\Sublime Text 3\Packages\SublimeCodeIntel";

            using (StreamReader sr = new StreamReader(File.Open($"{path}\\k.json", FileMode.Open), Encoding.Default))
            {
                z = sr.ReadToEnd();
            }


            XmlDocument doc2 = JsonConvert.DeserializeXmlNode(z);

            string sss;
            using (StringWriter sw = new StringWriter())
            using (var xML = XmlWriter.Create(sw, new XmlWriterSettings() { OmitXmlDeclaration = false }))
            {
                doc2.WriteTo(xML);
                xML.Flush();
                sss = sw.GetStringBuilder().ToString();
            }

            XmlDocument doc = new XmlDocument();

            XmlNode root = doc.CreateElement("some");
            XmlNode r = doc.CreateElement("1");
            XmlNode t = doc.CreateElement("2");
            XmlNode uz = doc.CreateElement("2.1");
            XmlNode a = doc.CreateElement("2.2");

            t.AppendChild(uz);
            t.AppendChild(a);
            root.AppendChild(r);
            root.AppendChild(t);
            doc.AppendChild(root);

            string p = JsonConvert.SerializeXmlNode(doc);
        }

        //////////////////////////////////////////    VARS    //////////////////////////////////////////




        ///////////////////////////////////////    STATIC VARS    ///////////////////////////////////////




        ///////////////////////////////////////    CONSTRUCTOR    ///////////////////////////////////////




        ///////////////////////////////////      STATIC METHODS      ///////////////////////////////////




        ///////////////////////////////////////      METHOD      ///////////////////////////////////////




        ///////////////////////////////////////      EVENTS      ///////////////////////////////////////




    }
}

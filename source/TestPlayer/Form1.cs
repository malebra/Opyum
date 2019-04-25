using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Opyum.StandardPlayback;

namespace TestPlayer
{
    public partial class Form1 : Form
    {
        private StandardPlayer player = new StandardPlayer();

        public Form1()
        {
            InitializeComponent();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "AudioFile | *.mp3; *.mp2; *.wav; *.aif; *.wma; *.mp4; *.aac";
            if (ofd.ShowDialog() != DialogResult.OK) return;

            player.Dispose();
            //player.PlayStream(ofd.FileName);
            pauseButton.Enabled = true;

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
            {
            player.Dispose();
            player = null;
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (player != null)
            {
                player.PausePlay();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using Opyum.StandardPlayback;
using Opyum.Structures;

namespace TestPlayer
{
    public partial class Form1 : Form
    {
        private StandardPlayer player;

        IWavePlayer pp;

        public Form1()
        {
            InitializeComponent();

            this.MinimumSize = this.Size;

            #region WaveOutEvent

            //for (int i = 0; i < WaveOut.DeviceCount; i++)
            //{
            //    var caps = WaveOut.GetCapabilities(i);
            //    richTextBox1.AppendText($"{caps.ProductName} \t\t {caps.ProductGuid}\n");
            //}

            //pp = new WaveOutEvent() { DeviceNumber = 0 };
            #endregion

            //pp = new AsioOut(AsioOut.GetDriverNames()[0]);
            pp = new WaveOut();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                try
                {
                    player.Dispose();
                    GC.Collect();
                }
                catch (Exception)
                {

                }
            }

            else
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "AudioFile | *.mp3; *.mp2; *.wav; *.aif; *.wma; *.mp4; *.aac";
                if (ofd.ShowDialog() != DialogResult.OK) return;

                if (player == null)
                {
                    if (pp != null)
                    {
                        richTextBox1.AppendText("Instantiated\n");
                        player = new StandardPlayer(pp);
                    }
                    else
                    {
                        player = new StandardPlayer(); 
                    }
                }
                
                player?.StopStream();

                player.Dispose();
                player.StartStream(ofd.FileName);
                pauseButton.Enabled = true; 
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                try
                {
                    player.sourceStream.Position = 0;
                }
                catch (Exception)
                {

                }
            }
            else
            {
                if (player != null)
                {
                    player.PausePlay();
                }
                player.PlayPauseStream(); 
            }
        }
    }
}

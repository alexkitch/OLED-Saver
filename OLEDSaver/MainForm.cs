using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OLEDSaver2
{
    public partial class MainForm : Form
    {
        private bool isFullScreen = false;
        private bool isDisplaying = false;

        private Thread worker;

        public MainForm()
        {
            InitializeComponent();
            UpdateForState();
            BeginPlayback();
            BeginProtection();
        }

        public void BeginPlayback()
        {
            FileInfo video = new FileInfo("video.webm");
            this.videoPlayer.SetMedia(video);
        }

        public void BeginProtection()
        {
            worker = new Thread(() => ProtectionThread.Work(this));
            worker.Start();
        }

        public void SetProtection(bool enabled)
        {
            isDisplaying = enabled;
            UpdateForState();
        }

        private void vlcControl1_VlcLibDirectoryNeeded(object sender, Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs e)
        {
            // VLC to be initialised with the directory containing its binaries
            if (IntPtr.Size == 4)
                e.VlcLibDirectory = new DirectoryInfo(@".\libvlc\win-x86\");
            else
                e.VlcLibDirectory = new DirectoryInfo(@".\libvlc\win-x64\");
        }

        private void ToggleState()
        {
            isFullScreen = !isFullScreen;

            UpdateForState();
        }

        private void UpdateForState()
        {
            this.FormBorderStyle = isFullScreen ? FormBorderStyle.None : FormBorderStyle.FixedSingle;
            this.WindowState = isFullScreen ? FormWindowState.Maximized : FormWindowState.Normal;
            this.videoPlayer.Visible = isFullScreen && isDisplaying;

            if (isFullScreen)
            {
                this.Opacity = isDisplaying ? 1.0f : 0.0f;
            }
            else
            {
                this.Opacity = 1.0f;
            }

            if (this.videoPlayer.Visible && !this.videoPlayer.IsPlaying)
            {
                this.videoPlayer.Play();
            }
            else if (!this.videoPlayer.Visible && this.videoPlayer.IsPlaying)
            {
                this.videoPlayer.Pause();
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            // Dunno if it's just my keyboard but Alt is coming through as 'Menu'. Don't
            // quite understand that one.
            if (e.KeyCode == Keys.Menu)
            {
                ToggleState();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // G'bye!
            worker.Abort();
        }
    }
}

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ClassicWindowsForms
{
    /// <summary>
    /// Demonstrate what happens when an IO operation blocks the UI thread.
    /// </summary>
    /// <remarks>
    /// Windows SDK ISO (Large Download):
    /// https://go.microsoft.com/fwlink/p/?linkid=845299
    /// 
    /// Artificially Slow Down HTTP Requests:
    /// http://deelay.me/5000/http://www.delsink.com
    /// </remarks>
    public partial class DownloaderForm : Form
    {
        public DownloaderForm()
        {
            InitializeComponent();
        }

        private DateTime _startTime = DateTime.MaxValue;

        private void btnDownloadSync_Click(object sender, EventArgs e)
        {
            DisableDownloadButtons();
            StartTimer();
            using (var downloader = new System.Net.WebClient())
            {
                var page = downloader.DownloadString(txtURL.Text);
            }
            StopTimer();
            EnableDownloadButtons();
        }

        private void StartTimer()
        {
            _startTime = DateTime.UtcNow;
            ShowDuration();
            timer.Enabled = true;
        }
        private void ShowDuration()
        {
            lblDuration.Text = "Duration: " + (DateTime.UtcNow - _startTime).TotalMilliseconds.ToString() + "ms";
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            ShowDuration();
        }
        private void StopTimer()
        {
            timer.Enabled = false;
            ShowDuration();
        }
        private void EnableDownloadButtons()
        {
            btnDownloadSync.Enabled = true;
            btnDownloadAsync.Enabled = true;
        }

        private void DisableDownloadButtons()
        {
            btnDownloadSync.Enabled = false;
            btnDownloadAsync.Enabled = false;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // danger: do not modify the UI from this thread!

            // this.Text = "This is a test!";

            //var targetMethod = new Action(UpdateUI);
            //this.Invoke(targetMethod);

            using (var downloader = new System.Net.WebClient())
            {
                var page = downloader.DownloadString(txtURL.Text);
            }
        }

        private void UpdateUI()
        {
            this.Text = "This is a test!";
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StopTimer();
            EnableDownloadButtons();
        }

        private void btnDownloadAsync_Click(object sender, EventArgs e)
        {
            DisableDownloadButtons();
            StartTimer();
            backgroundWorker.RunWorkerAsync();
        }
    }
}

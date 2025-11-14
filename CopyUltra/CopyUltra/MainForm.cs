using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CopyUltra
{
    public partial class MainForm : Form
    {
        private long totalBytesToCopy = 0;
        private long totalBytesCopied = 0;
        private Stopwatch stopwatch = new Stopwatch();
        public MainForm()
        {
            InitializeComponent();
            AllowDrop = true;
            DragEnter += MainForm_DragEnter;
            DragDrop += MainForm_DragDrop;
        }
        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (var f in files)
                listSource.Items.Add(f);
        }
        private void btnBrowseSource_Click(object sender, EventArgs e)
        {
            try
            {
                using var f = new FolderBrowserDialog();
                if (f.ShowDialog() == DialogResult.OK)
                    listSource.Items.Add(f.SelectedPath);
            }
            catch (Exception err)
            {

            }
        }

        private void btnBrowseTarget_Click(object sender, EventArgs e)
        {
            try
            {
                using var f = new FolderBrowserDialog();
                if (f.ShowDialog() == DialogResult.OK)
                    txtTarget.Text = f.SelectedPath;
            }
            catch (Exception err)
            {

            }
        }

        private async Task btnStart_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                if (listSource.Items.Count == 0)
                {
                    MessageBox.Show("Please add at least one source folder/file!");
                    return;
                }
                if (!Directory.Exists(txtTarget.Text))
                {
                    MessageBox.Show("Target folder not valid!");
                    return;
                }

                btnStart.Enabled = false;
                progressBar.Value = 0;

                totalBytesToCopy = CalculateTotalBytes();
                totalBytesCopied = 0;

                stopwatch.Restart();

                foreach (var item in listSource.Items)
                {
                    string source = item.ToString();
                    await CopyWithRobocopyAsync(source, txtTarget.Text);
                }

                stopwatch.Stop();
                MessageBox.Show("Copy Completed!");
                btnStart.Enabled = true;
            }
            catch (Exception err)
            {

            }
        }
        private long CalculateTotalBytes()
        {
            long total = 0;
            foreach (var item in listSource.Items)
            {
                var path = item.ToString();
                if (File.Exists(path))
                    total += new FileInfo(path).Length;
                else if (Directory.Exists(path))
                    total += Directory.GetFiles(path, "*", SearchOption.AllDirectories)
                                      .Sum(f => new FileInfo(f).Length);
            }
            return total;
        }

        private async Task CopyWithRobocopyAsync(string source, string target)
        {
            string targetFolder = Path.Combine(target, Path.GetFileName(source));

            var process = new Process();
            process.StartInfo.FileName = "robocopy";
            process.StartInfo.Arguments = $"\"{source}\" \"{targetFolder}\" /E /Z /R:1 /W:1 /MT:8 /NP /LOG+:copy.log";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;

            process.OutputDataReceived += (s, e) =>
            {
                if (e.Data == null) return;
                ParseRobocopyProgress(e.Data);
            };

            process.Start();
            process.BeginOutputReadLine();
            await process.WaitForExitAsync();
        }

        private void ParseRobocopyProgress(string line)
        {
            if (line.Contains("100%") || line.Contains("   "))
                return;

            // Format robocopy usually: "      New File          12345        filename.ext"
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length >= 2 && long.TryParse(parts[1], out long bytes))
            {
                totalBytesCopied += bytes;
                UpdateProgressUI();
            }
        }

        private void UpdateProgressUI()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateProgressUI));
                return;
            }

            double percent = (double)totalBytesCopied / totalBytesToCopy * 100.0;
            progressBar.Value = Math.Min(100, (int)percent);

            lblPercent.Text = $"{percent:F2}%";

            double seconds = stopwatch.Elapsed.TotalSeconds;
            double speed = totalBytesCopied / seconds / 1024 / 1024;

            lblSpeed.Text = $"{speed:F2} MB/s";

            double etaSeconds = (totalBytesToCopy - totalBytesCopied) / (speed * 1024 * 1024);
            lblETA.Text = TimeSpan.FromSeconds(etaSeconds).ToString(@"hh\:mm\:ss");
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (listSource.Items.Count == 0)
                {
                    MessageBox.Show("Please add at least one source folder/file!");
                    return;
                }
                if (!Directory.Exists(txtTarget.Text))
                {
                    MessageBox.Show("Target folder not valid!");
                    return;
                }

                btnStart.Enabled = false;
                progressBar.Value = 0;

                totalBytesToCopy = CalculateTotalBytes();
                totalBytesCopied = 0;

                stopwatch.Restart();

                foreach (var item in listSource.Items)
                {
                    string source = item.ToString();
                    await CopyWithRobocopyAsync(source, txtTarget.Text);
                }

                stopwatch.Stop();
                MessageBox.Show("Copy Completed!");
                btnStart.Enabled = true;
            }
            catch (Exception err)
            {

            }
        }
    }
}

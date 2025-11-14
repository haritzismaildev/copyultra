namespace CopyUltra
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnBrowseSource = new Button();
            btnBrowseTarget = new Button();
            btnStart = new Button();
            listSource = new ListBox();
            txtTarget = new TextBox();
            label1 = new Label();
            label2 = new Label();
            progressBar = new ProgressBar();
            lblPercent = new Label();
            lblSpeed = new Label();
            lblETA = new Label();
            logBox = new TextBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // btnBrowseSource
            // 
            btnBrowseSource.Location = new Point(25, 25);
            btnBrowseSource.Name = "btnBrowseSource";
            btnBrowseSource.Size = new Size(140, 35);
            btnBrowseSource.TabIndex = 0;
            btnBrowseSource.Text = "Add Source Folder";
            btnBrowseSource.UseVisualStyleBackColor = true;
            btnBrowseSource.Click += btnBrowseSource_Click;
            // 
            // btnBrowseTarget
            // 
            btnBrowseTarget.Location = new Point(25, 280);
            btnBrowseTarget.Name = "btnBrowseTarget";
            btnBrowseTarget.Size = new Size(120, 30);
            btnBrowseTarget.TabIndex = 1;
            btnBrowseTarget.Text = "Target Folder";
            btnBrowseTarget.UseVisualStyleBackColor = true;
            btnBrowseTarget.Click += btnBrowseTarget_Click;
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.LightGreen;
            btnStart.Location = new Point(25, 410);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(140, 40);
            btnStart.TabIndex = 2;
            btnStart.Text = "Start Copy";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnStart_Click;
            // 
            // listSource
            // 
            listSource.AllowDrop = true;
            listSource.FormattingEnabled = true;
            listSource.ItemHeight = 25;
            listSource.Location = new Point(25, 80);
            listSource.Name = "listSource";
            listSource.Size = new Size(600, 179);
            listSource.TabIndex = 3;
            // 
            // txtTarget
            // 
            txtTarget.Location = new Point(160, 280);
            txtTarget.Name = "txtTarget";
            txtTarget.Size = new Size(465, 31);
            txtTarget.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 60);
            label1.Name = "label1";
            label1.Size = new Size(234, 25);
            label1.TabIndex = 11;
            label1.Text = "Source Folders (Drag & Drop)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(25, 255);
            label2.Name = "label2";
            label2.Size = new Size(119, 25);
            label2.TabIndex = 10;
            label2.Text = "Target Folder:";
            // 
            // progressBar
            // 
            progressBar.Location = new Point(25, 350);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(600, 30);
            progressBar.TabIndex = 5;
            // 
            // lblPercent
            // 
            lblPercent.AutoSize = true;
            lblPercent.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPercent.Location = new Point(25, 320);
            lblPercent.Name = "lblPercent";
            lblPercent.Size = new Size(70, 28);
            lblPercent.TabIndex = 6;
            lblPercent.Text = "0.00%";
            // 
            // lblSpeed
            // 
            lblSpeed.AutoSize = true;
            lblSpeed.Location = new Point(160, 320);
            lblSpeed.Name = "lblSpeed";
            lblSpeed.Size = new Size(127, 25);
            lblSpeed.TabIndex = 7;
            lblSpeed.Text = "Speed: 0 MB/s";
            // 
            // lblETA
            // 
            lblETA.AutoSize = true;
            lblETA.Location = new Point(350, 320);
            lblETA.Name = "lblETA";
            lblETA.Size = new Size(118, 25);
            lblETA.TabIndex = 8;
            lblETA.Text = "ETA: 00:00:00";
            // 
            // logBox
            // 
            logBox.Location = new Point(25, 470);
            logBox.Multiline = true;
            logBox.Name = "logBox";
            logBox.ScrollBars = ScrollBars.Vertical;
            logBox.Size = new Size(600, 140);
            logBox.TabIndex = 9;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(25, 445);
            label3.Name = "label3";
            label3.Size = new Size(93, 25);
            label3.TabIndex = 0;
            label3.Text = "Copy Log:";
            // 
            // MainForm
            // 
            ClientSize = new Size(650, 630);
            Controls.Add(label3);
            Controls.Add(logBox);
            Controls.Add(lblETA);
            Controls.Add(lblSpeed);
            Controls.Add(lblPercent);
            Controls.Add(progressBar);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtTarget);
            Controls.Add(listSource);
            Controls.Add(btnStart);
            Controls.Add(btnBrowseTarget);
            Controls.Add(btnBrowseSource);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FastCopy Pro - Ultra Speed Copy Tool";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnBrowseSource;
        private System.Windows.Forms.Button btnBrowseTarget;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListBox listSource;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label lblETA;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.Label label3;
    }
}
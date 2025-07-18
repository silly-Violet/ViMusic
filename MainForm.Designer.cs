﻿namespace ViMusic
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            pausePlayButton = new Button();
            timeLabel = new Label();
            progressBar = new PictureBox();
            openFile = new Button();
            openFolder = new Button();
            fileDialog = new OpenFileDialog();
            songName = new Label();
            albumName = new Label();
            artistName = new Label();
            folderDialog = new FolderBrowserDialog();
            hoverLabel = new Label();
            playlistListBox = new ListBox();
            toolTipHandler = new ToolTip(components);
            stopButton = new Button();
            muteButton = new Button();
            volumeDown = new Button();
            volumeUp = new Button();
            volumeDisplay = new PictureBox();
            playlistBack = new Button();
            playlistForward = new Button();
            aboutButton = new Button();
            playlistCounter = new Label();
            notifyIcon = new NotifyIcon(components);
            contextMenuStrip = new ContextMenuStrip(components);
            hideMainWindowToolStripMenuItem = new ToolStripMenuItem();
            playToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)progressBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)volumeDisplay).BeginInit();
            contextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // pausePlayButton
            // 
            pausePlayButton.BackColor = Color.FromArgb(11, 17, 47);
            pausePlayButton.FlatStyle = FlatStyle.Popup;
            pausePlayButton.Location = new Point(12, 187);
            pausePlayButton.Name = "pausePlayButton";
            pausePlayButton.Size = new Size(32, 32);
            pausePlayButton.TabIndex = 0;
            toolTipHandler.SetToolTip(pausePlayButton, "Pause/Play");
            pausePlayButton.UseVisualStyleBackColor = false;
            pausePlayButton.Click += PausePlayButton_Click;
            // 
            // timeLabel
            // 
            timeLabel.AutoSize = true;
            timeLabel.BackColor = Color.Transparent;
            timeLabel.Location = new Point(50, 181);
            timeLabel.Name = "timeLabel";
            timeLabel.Size = new Size(72, 15);
            timeLabel.TabIndex = 99;
            timeLabel.Text = "00:00 / 00:00";
            // 
            // progressBar
            // 
            progressBar.BackColor = Color.FromArgb(38, 65, 111);
            progressBar.Location = new Point(154, 199);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(368, 20);
            progressBar.TabIndex = 3;
            progressBar.TabStop = false;
            toolTipHandler.SetToolTip(progressBar, "Click to seek");
            progressBar.Click += ProgressBar_Click;
            progressBar.MouseMove += ProgressBar_MouseMove;
            // 
            // openFile
            // 
            openFile.BackColor = Color.FromArgb(11, 17, 47);
            openFile.FlatStyle = FlatStyle.Popup;
            openFile.Image = (Image)resources.GetObject("openFile.Image");
            openFile.Location = new Point(12, 12);
            openFile.Name = "openFile";
            openFile.Size = new Size(32, 32);
            openFile.TabIndex = 7;
            toolTipHandler.SetToolTip(openFile, "Open a file");
            openFile.UseVisualStyleBackColor = false;
            openFile.Click += OpenFile_Click;
            // 
            // openFolder
            // 
            openFolder.BackColor = Color.FromArgb(11, 17, 47);
            openFolder.FlatStyle = FlatStyle.Popup;
            openFolder.Image = (Image)resources.GetObject("openFolder.Image");
            openFolder.Location = new Point(50, 12);
            openFolder.Name = "openFolder";
            openFolder.Size = new Size(32, 32);
            openFolder.TabIndex = 8;
            toolTipHandler.SetToolTip(openFolder, "Open a folder as playlist");
            openFolder.UseVisualStyleBackColor = false;
            openFolder.Click += OpenFolder_Click;
            // 
            // fileDialog
            // 
            fileDialog.Filter = "Audio files (.mp3,.wav,.flac,.ogg)|*.mp3;*.wav;*.flac;*.ogg";
            // 
            // songName
            // 
            songName.AutoSize = true;
            songName.BackColor = Color.Transparent;
            songName.Location = new Point(298, 50);
            songName.Name = "songName";
            songName.Size = new Size(67, 15);
            songName.TabIndex = 99;
            songName.Text = "Name: N/A";
            // 
            // albumName
            // 
            albumName.AutoSize = true;
            albumName.BackColor = Color.Transparent;
            albumName.Location = new Point(298, 118);
            albumName.Name = "albumName";
            albumName.Size = new Size(71, 15);
            albumName.TabIndex = 99;
            albumName.Text = "Album: N/A";
            // 
            // artistName
            // 
            artistName.AutoSize = true;
            artistName.BackColor = Color.Transparent;
            artistName.Location = new Point(298, 84);
            artistName.Name = "artistName";
            artistName.Size = new Size(63, 15);
            artistName.TabIndex = 99;
            artistName.Text = "Artist: N/A";
            // 
            // hoverLabel
            // 
            hoverLabel.AutoSize = true;
            hoverLabel.BackColor = Color.Transparent;
            hoverLabel.Location = new Point(206, 118);
            hoverLabel.Name = "hoverLabel";
            hoverLabel.Size = new Size(0, 15);
            hoverLabel.TabIndex = 9;
            hoverLabel.Tag = "hidden";
            hoverLabel.Visible = false;
            // 
            // playlistListBox
            // 
            playlistListBox.BackColor = Color.FromArgb(11, 17, 47);
            playlistListBox.BorderStyle = BorderStyle.FixedSingle;
            playlistListBox.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            playlistListBox.ForeColor = Color.White;
            playlistListBox.ItemHeight = 17;
            playlistListBox.Location = new Point(12, 50);
            playlistListBox.Name = "playlistListBox";
            playlistListBox.Size = new Size(280, 121);
            playlistListBox.TabIndex = 99;
            playlistListBox.TabStop = false;
            playlistListBox.SelectedIndexChanged += PlaylistListBox_SelectedIndexChanged;
            // 
            // stopButton
            // 
            stopButton.BackColor = Color.FromArgb(11, 17, 47);
            stopButton.FlatStyle = FlatStyle.Popup;
            stopButton.Image = (Image)resources.GetObject("stopButton.Image");
            stopButton.Location = new Point(50, 199);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(20, 20);
            stopButton.TabIndex = 1;
            toolTipHandler.SetToolTip(stopButton, "Stop");
            stopButton.UseVisualStyleBackColor = false;
            stopButton.Click += StopButton_Click;
            // 
            // muteButton
            // 
            muteButton.BackColor = Color.FromArgb(11, 17, 47);
            muteButton.FlatStyle = FlatStyle.Popup;
            muteButton.Image = (Image)resources.GetObject("muteButton.Image");
            muteButton.Location = new Point(76, 199);
            muteButton.Name = "muteButton";
            muteButton.Size = new Size(20, 20);
            muteButton.TabIndex = 2;
            toolTipHandler.SetToolTip(muteButton, "Mute");
            muteButton.UseVisualStyleBackColor = false;
            muteButton.Click += MuteButton_Click;
            // 
            // volumeDown
            // 
            volumeDown.BackColor = Color.FromArgb(11, 17, 47);
            volumeDown.FlatStyle = FlatStyle.Popup;
            volumeDown.Image = (Image)resources.GetObject("volumeDown.Image");
            volumeDown.Location = new Point(102, 199);
            volumeDown.Name = "volumeDown";
            volumeDown.Size = new Size(20, 20);
            volumeDown.TabIndex = 3;
            toolTipHandler.SetToolTip(volumeDown, "Volume Down");
            volumeDown.UseVisualStyleBackColor = false;
            volumeDown.Click += VolumeDown_Click;
            // 
            // volumeUp
            // 
            volumeUp.BackColor = Color.FromArgb(11, 17, 47);
            volumeUp.FlatStyle = FlatStyle.Popup;
            volumeUp.Image = (Image)resources.GetObject("volumeUp.Image");
            volumeUp.Location = new Point(128, 199);
            volumeUp.Name = "volumeUp";
            volumeUp.Size = new Size(20, 20);
            volumeUp.TabIndex = 4;
            toolTipHandler.SetToolTip(volumeUp, "Volume Up");
            volumeUp.UseVisualStyleBackColor = false;
            volumeUp.Click += VolumeUp_Click;
            // 
            // volumeDisplay
            // 
            volumeDisplay.BackColor = Color.FromArgb(38, 65, 111);
            volumeDisplay.Location = new Point(102, 225);
            volumeDisplay.Name = "volumeDisplay";
            volumeDisplay.Size = new Size(46, 10);
            volumeDisplay.TabIndex = 16;
            volumeDisplay.TabStop = false;
            toolTipHandler.SetToolTip(volumeDisplay, "Volume Meter");
            // 
            // playlistBack
            // 
            playlistBack.BackColor = Color.FromArgb(11, 17, 47);
            playlistBack.FlatStyle = FlatStyle.Popup;
            playlistBack.Image = (Image)resources.GetObject("playlistBack.Image");
            playlistBack.Location = new Point(298, 143);
            playlistBack.Name = "playlistBack";
            playlistBack.Size = new Size(28, 28);
            playlistBack.TabIndex = 5;
            toolTipHandler.SetToolTip(playlistBack, "Previous Song");
            playlistBack.UseVisualStyleBackColor = false;
            playlistBack.Click += PlaylistBack_Click;
            // 
            // playlistForward
            // 
            playlistForward.BackColor = Color.FromArgb(11, 17, 47);
            playlistForward.FlatStyle = FlatStyle.Popup;
            playlistForward.Image = (Image)resources.GetObject("playlistForward.Image");
            playlistForward.Location = new Point(332, 143);
            playlistForward.Name = "playlistForward";
            playlistForward.Size = new Size(28, 28);
            playlistForward.TabIndex = 6;
            toolTipHandler.SetToolTip(playlistForward, "Next Song");
            playlistForward.UseVisualStyleBackColor = false;
            playlistForward.Click += PlaylistForward_Click;
            // 
            // aboutButton
            // 
            aboutButton.BackColor = Color.FromArgb(11, 17, 47);
            aboutButton.FlatStyle = FlatStyle.Popup;
            aboutButton.Image = (Image)resources.GetObject("aboutButton.Image");
            aboutButton.Location = new Point(490, 12);
            aboutButton.Name = "aboutButton";
            aboutButton.Size = new Size(32, 32);
            aboutButton.TabIndex = 9;
            toolTipHandler.SetToolTip(aboutButton, "About");
            aboutButton.UseVisualStyleBackColor = false;
            aboutButton.Click += AboutButton_Click;
            // 
            // playlistCounter
            // 
            playlistCounter.AutoSize = true;
            playlistCounter.BackColor = Color.Transparent;
            playlistCounter.Location = new Point(366, 150);
            playlistCounter.Name = "playlistCounter";
            playlistCounter.Size = new Size(30, 15);
            playlistCounter.TabIndex = 99;
            playlistCounter.Text = "0 / 0";
            toolTipHandler.SetToolTip(playlistCounter, "Current / Amount in playlist");
            // 
            // notifyIcon
            // 
            notifyIcon.Icon = (Icon)resources.GetObject("notifyIcon.Icon");
            notifyIcon.Text = "Vi Music";
            notifyIcon.Visible = true;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.BackColor = Color.FromArgb(41, 41, 41);
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { hideMainWindowToolStripMenuItem, playToolStripMenuItem, exitToolStripMenuItem });
            contextMenuStrip.Name = "contextMenuStrip1";
            contextMenuStrip.RenderMode = ToolStripRenderMode.Professional;
            contextMenuStrip.Size = new Size(175, 70);
            // 
            // hideMainWindowToolStripMenuItem
            // 
            hideMainWindowToolStripMenuItem.BackColor = Color.FromArgb(31, 31, 31);
            hideMainWindowToolStripMenuItem.ForeColor = Color.White;
            hideMainWindowToolStripMenuItem.Name = "hideMainWindowToolStripMenuItem";
            hideMainWindowToolStripMenuItem.Size = new Size(174, 22);
            hideMainWindowToolStripMenuItem.Text = "Hide main window";
            hideMainWindowToolStripMenuItem.Click += HideMainWindowToolStripMenuItem_Click;
            // 
            // playToolStripMenuItem
            // 
            playToolStripMenuItem.BackColor = Color.FromArgb(31, 31, 31);
            playToolStripMenuItem.ForeColor = Color.White;
            playToolStripMenuItem.Name = "playToolStripMenuItem";
            playToolStripMenuItem.Size = new Size(174, 22);
            playToolStripMenuItem.Text = "Play";
            playToolStripMenuItem.Click += PlayToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.BackColor = Color.FromArgb(31, 31, 31);
            exitToolStripMenuItem.ForeColor = Color.White;
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(174, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 32, 78);
            ClientSize = new Size(534, 241);
            Controls.Add(playlistCounter);
            Controls.Add(aboutButton);
            Controls.Add(playlistForward);
            Controls.Add(playlistBack);
            Controls.Add(volumeDisplay);
            Controls.Add(volumeUp);
            Controls.Add(volumeDown);
            Controls.Add(muteButton);
            Controls.Add(stopButton);
            Controls.Add(playlistListBox);
            Controls.Add(hoverLabel);
            Controls.Add(artistName);
            Controls.Add(albumName);
            Controls.Add(songName);
            Controls.Add(openFolder);
            Controls.Add(openFile);
            Controls.Add(progressBar);
            Controls.Add(timeLabel);
            Controls.Add(pausePlayButton);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Vi Music";
            DragDrop += FileDrop;
            DragOver += FileHover;
            ((System.ComponentModel.ISupportInitialize)progressBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)volumeDisplay).EndInit();
            contextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button pausePlayButton;
        private Label timeLabel;
        private PictureBox progressBar;
        private Button openFile;
        private Button openFolder;
        private OpenFileDialog fileDialog;
        private Label songName;
        private Label albumName;
        private Label artistName;
        private FolderBrowserDialog folderDialog;
        private Label hoverLabel;
        private ListBox playlistListBox;
        private ToolTip toolTipHandler;
        private Button stopButton;
        private Button muteButton;
        private Button volumeDown;
        private Button volumeUp;
        private PictureBox volumeDisplay;
        private Button playlistBack;
        private Button playlistForward;
        private Button aboutButton;
        private Label playlistCounter;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem hideMainWindowToolStripMenuItem;
        private ToolStripMenuItem playToolStripMenuItem;
    }
}

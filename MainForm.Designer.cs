namespace ViMusic
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
            ((System.ComponentModel.ISupportInitialize)progressBar).BeginInit();
            SuspendLayout();
            // 
            // pausePlayButton
            // 
            pausePlayButton.BackColor = Color.FromArgb(41, 41, 41);
            pausePlayButton.FlatStyle = FlatStyle.Popup;
            pausePlayButton.Location = new Point(12, 177);
            pausePlayButton.Name = "pausePlayButton";
            pausePlayButton.Size = new Size(32, 32);
            pausePlayButton.TabIndex = 1;
            pausePlayButton.UseVisualStyleBackColor = false;
            pausePlayButton.Click += PausePlayButton_Click;
            // 
            // timeLabel
            // 
            timeLabel.AutoSize = true;
            timeLabel.BackColor = Color.Transparent;
            timeLabel.Location = new Point(50, 200);
            timeLabel.Name = "timeLabel";
            timeLabel.Size = new Size(72, 15);
            timeLabel.TabIndex = 2;
            timeLabel.Text = "00:00 / 00:00";
            // 
            // progressBar
            // 
            progressBar.BackColor = Color.FromArgb(30, 43, 66);
            progressBar.Location = new Point(50, 177);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(472, 20);
            progressBar.TabIndex = 3;
            progressBar.TabStop = false;
            progressBar.Click += ProgressBar_Click;
            progressBar.MouseMove += ProgressBar_MouseMove;
            // 
            // openFile
            // 
            openFile.BackColor = Color.FromArgb(41, 41, 41);
            openFile.FlatStyle = FlatStyle.Popup;
            openFile.Image = (Image)resources.GetObject("openFile.Image");
            openFile.Location = new Point(12, 12);
            openFile.Name = "openFile";
            openFile.Size = new Size(32, 32);
            openFile.TabIndex = 4;
            openFile.UseVisualStyleBackColor = false;
            openFile.Click += OpenFile_Click;
            // 
            // openFolder
            // 
            openFolder.BackColor = Color.FromArgb(41, 41, 41);
            openFolder.FlatStyle = FlatStyle.Popup;
            openFolder.Image = (Image)resources.GetObject("openFolder.Image");
            openFolder.Location = new Point(50, 12);
            openFolder.Name = "openFolder";
            openFolder.Size = new Size(32, 32);
            openFolder.TabIndex = 5;
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
            songName.TabIndex = 6;
            songName.Text = "Name: N/A";
            // 
            // albumName
            // 
            albumName.AutoSize = true;
            albumName.BackColor = Color.Transparent;
            albumName.Location = new Point(298, 144);
            albumName.Name = "albumName";
            albumName.Size = new Size(71, 15);
            albumName.TabIndex = 7;
            albumName.Text = "Album: N/A";
            // 
            // artistName
            // 
            artistName.AutoSize = true;
            artistName.BackColor = Color.Transparent;
            artistName.Location = new Point(298, 98);
            artistName.Name = "artistName";
            artistName.Size = new Size(63, 15);
            artistName.TabIndex = 8;
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
            playlistListBox.BackColor = Color.Black;
            playlistListBox.BorderStyle = BorderStyle.FixedSingle;
            playlistListBox.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            playlistListBox.ForeColor = Color.White;
            playlistListBox.ItemHeight = 17;
            playlistListBox.Location = new Point(12, 50);
            playlistListBox.Name = "playlistListBox";
            playlistListBox.Size = new Size(280, 104);
            playlistListBox.Sorted = true;
            playlistListBox.TabIndex = 10;
            playlistListBox.SelectedIndexChanged += PlaylistListBox_SelectedIndexChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(31, 31, 31);
            ClientSize = new Size(534, 226);
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
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Vi Music";
            ((System.ComponentModel.ISupportInitialize)progressBar).EndInit();
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
    }
}

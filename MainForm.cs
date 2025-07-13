using System.Drawing.Drawing2D;
using Microsoft.WindowsAPICodePack.Shell;

namespace ViMusic
{
    public partial class MainForm : Form
    {
        private readonly MusicPlayer musicPlayer = new();

        private ShellFile? currentSong;
        private List<string> currentPlaylist = new();

        private int playlistIndex = -1; //negative indicates not in playlist mode

        private int hoverCounter = 0;
        private readonly Graphics progressBarGraphics;

        public MainForm()
        {
            InitializeComponent();

            progressBarGraphics = progressBar.CreateGraphics();
            playlistListBox.BackColor = this.BackColor;

            ResetRender();

            //musicPlayer.LoadAudioFromFile("D:\\Music\\Main\\Misc\\Notion.mp3");

            Task.Run(() => BackgroundTask());

        }

        private async void PausePlayButton_Click(object sender, EventArgs e)
        {
            if (!musicPlayer.IsPlaying)
                await musicPlayer.Play();
            else
                musicPlayer.Pause();
        }

        private void UpdateEverything()
        {
            // UpdateTimer
            {
                var minutes = musicPlayer.CurrentTime.Minutes;
                var seconds = musicPlayer.CurrentTime.Seconds;

                var totalMinutes = musicPlayer.Duration.Minutes;
                var totalSeconds = musicPlayer.Duration.Seconds;

                timeLabel.Text = $"{(minutes < 10 ? $"0{minutes}" : minutes)}:{(seconds < 10 ? $"0{seconds}" : seconds)} / {(totalMinutes < 10 ? $"0{totalMinutes}" : totalMinutes)}:{(totalSeconds < 10 ? $"0{totalSeconds}" : totalSeconds)}";
            }

            // UpdateProgress
            {
                /* 
                    get the amount of pixels that should be filled in
                    first need the percentage ((amount / total) * 100)
                    then multiply that by the length to find how many pixels
                    then fill the pixels
                */

                progressBarGraphics.FillRectangle(
                    new SolidBrush(Color.FromArgb(255, 127, 167, 237)), // colour choice
                    0, 0, // position of top left corner
                    (float)(progressBar.Width * (musicPlayer.CurrentTime.TotalSeconds / musicPlayer.Duration.TotalSeconds)), // width of rectangle
                    progressBar.Height); // height of rectangle
            }

            // UpdatePlayIcon
            {
                var ready = musicPlayer.IsReady;

                pausePlayButton.Enabled = ready;

                if (!ready)
                    pausePlayButton.Image = Image.FromStream(new MemoryStream(Icons.invalid));
                else if (musicPlayer.IsPlaying)
                    pausePlayButton.Image = Image.FromStream(new MemoryStream(Icons.pause));
                else
                    pausePlayButton.Image = Image.FromStream(new MemoryStream(Icons.play));
            }

            // UpdateSongStats
            {
                if (currentSong != null)
                {
                    var title = currentSong.Properties.System.Title.Value;

                    songName.Text = "Name: " + (string.IsNullOrWhiteSpace(title) ? currentSong.Name.Substring(0, currentSong.Name.IndexOf('.')) : title);

                    var album = currentSong.Properties.System.Music.AlbumTitle.Value;

                    albumName.Text = "Album: " + (string.IsNullOrWhiteSpace(album) ? "N/A" : album);

                    var artistArr = currentSong.Properties.System.Music.Artist.Value;
                    string? artist = null;

                    if (artistArr != null) artist = string.Join(", ", artistArr);

                    artistName.Text = "Artist: " + (string.IsNullOrEmpty(artist) ? "N/A" : artist);
                }
            }
            // UpdateHoverLabel
            {
                int hoverCooldown = 5; //change to modify how long it takes to hide hover

                if (hoverLabel.Tag.ToString() == "modified")
                {
                    hoverLabel.Tag = "counting";

                    hoverCounter = 0;
                }
                else if (hoverLabel.Tag.ToString() == "counting")
                {
                    if (hoverCounter >= hoverCooldown)
                    {
                        hoverLabel.Hide();
                        hoverLabel.Tag = "hidden";
                    }
                    else
                        hoverCounter++;
                }
            }

        }

        private void ResetRender()
        {
            timeLabel.Text = "00:00 / 00:00";
            progressBarGraphics.Clear(Color.FromArgb(255, 30, 43, 66));
            songName.Text = "Name: N/A";
            albumName.Text = "Album: N/A";
            artistName.Text = "Artist: N/A";
            UpdateEverything();
        }

        private void BackgroundTask()
        {
            while (true)
            {
                //manage playlist
                if (playlistIndex >= 0)
                {
                    if (musicPlayer.IsStopped)
                    {
                        if (playlistIndex < currentPlaylist.Count)
                        {
                            this.Invoke(new Action<string>(LoadSong), [currentPlaylist[playlistIndex]]);
                            playlistIndex++;
                        }

                    }
                }

                // rendering

                //if (!musicPlayer.IsStopped)

                try
                {
                    if (!this.IsHandleCreated) continue; // make sure invoke isn't called when it's not allowed
                    Invoke(UpdateEverything);
                }
                catch (ObjectDisposedException) { return; } // crashed on closing without this


                Thread.Sleep(100);
            }
        }

        private void LoadSong(string filename)
        {
            currentSong = ShellFile.FromFilePath(filename);
            musicPlayer.LoadAudioFromFile(currentSong.ParsingName);
            ResetRender();
            musicPlayer.Play();
        }

        private void LoadSong(ShellFile file)
        {
            currentSong = file;
            musicPlayer.LoadAudioFromFile(currentSong.ParsingName);
            ResetRender();
            musicPlayer.Play();
        }

        private void LoadPlaylist(string filepath)
        {
            currentPlaylist = new();

            string[] validExtensions = [".mp3", ".wav", ".ogg", ".flac"];

            foreach (var file in new DirectoryInfo(filepath).GetFiles())
            {
                if (validExtensions.Contains(file.Extension))
                {
                    currentPlaylist.Add(file.FullName);


                    var title = ShellFile.FromFilePath(file.FullName).Properties.System.Title.Value;

                    playlistListBox.Items.Add(string.IsNullOrWhiteSpace(title) ? file.Name.Substring(0, file.Name.IndexOf('.')) : title);
                }

            }

            

            if (!musicPlayer.IsStopped) musicPlayer.Stop();
            LoadSong(currentPlaylist[0]);
            playlistIndex = 1;
        }

        private void ProgressBar_Click(object sender, EventArgs e)
        {
            //if (musicPlayer.IsStopped) return;

            if (!musicPlayer.IsReady) return;

            //https://stackoverflow.com/questions/18040945/read-picture-box-mouse-coordinates-on-click

            var clickCoords = ((MouseEventArgs)e).Location;

            musicPlayer.Seek(TimeSpan.FromSeconds(musicPlayer.Duration.TotalSeconds * ((double)clickCoords.X / (double)progressBar.Width))); // seek to correct time from the location clicked
            ResetRender();
        }

        private void ProgressBar_MouseMove(object sender, EventArgs e)
        {
            var mousePos = progressBar.PointToClient(Cursor.Position);

            var hoverTime = TimeSpan.FromSeconds(musicPlayer.Duration.TotalSeconds * ((double)mousePos.X / (double)progressBar.Width));
            var minutes = hoverTime.Minutes;
            var seconds = hoverTime.Seconds;

            hoverLabel.Show();

            hoverLabel.Text = $"{(minutes < 10 ? $"0{minutes}" : minutes)}:{(seconds < 10 ? $"0{seconds}" : seconds)}";
            hoverLabel.Top = progressBar.Top - 18;
            hoverLabel.Left = mousePos.X + 33;

            hoverLabel.Tag = "modified";
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadSong(fileDialog.FileName);
            }
        }

        private void OpenFolder_Click(object sender, EventArgs e)
        {
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                LoadPlaylist(folderDialog.SelectedPath);
            }
        }

        private void PlaylistListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadSong(currentPlaylist[playlistListBox.SelectedIndex]);
            playlistIndex = playlistListBox.SelectedIndex;
            musicPlayer.Stop();
        }


        /* gradient drawing code
        public void GradientBackground(object sender, PaintEventArgs e)
        {
            //https://learn.microsoft.com/en-us/dotnet/desktop/winforms/advanced/how-to-create-a-linear-gradient

             var gradBrush = new LinearGradientBrush(
               new Point(0, 0),
               new Point(this.Width, this.Height),
               Color.FromArgb(255, 73, 65, 158),   
               Color.FromArgb(255, 31, 30, 65));  

            e.Graphics.FillRectangle(gradBrush, 0, 0, this.Width, this.Height);
        }
        */
    }
}

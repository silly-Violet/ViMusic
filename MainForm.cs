using Microsoft.WindowsAPICodePack.Shell;
using System.Reflection;
using System.Text.Json;

namespace ViMusic
{
    public partial class MainForm : Form
    {
        private readonly MusicPlayer musicPlayer;

        private ShellFile? currentSong;
        private List<string> currentPlaylist = new();

        private int playlistIndex = -1; //negative indicates not in playlist mode

        private int hoverCounter = 0;
        private readonly Graphics progressBarGraphics;
        private readonly Graphics volumeDisplayGraphics;
        private bool muted = false;
        private float prevVolume = 1f;

        //modifiable by settings:
        public float volumeStep = 0.1f;
        public Color barFillColour = Color.FromArgb(255, 127, 167, 237);
        public Color muteActive = Color.FromArgb(255, 191, 116, 255);

        public MainForm()
        {
            InitializeComponent();

            //code yoinked from my own library I didn't want to import all of
            var programDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            ConfirmFileExists("volume.json", 1f);

            musicPlayer = new(ReadFromJson<float>("volume.json"));
            progressBarGraphics = progressBar.CreateGraphics();
            volumeDisplayGraphics = volumeDisplay.CreateGraphics();
            playlistListBox.BackColor = this.BackColor;

            ResetRender();

            //musicPlayer.LoadAudioFromFile("D:\\Music\\Main\\Misc\\Notion.mp3");

            Task.Run(() => BackgroundTask());

        }

        //also joinked
        public static bool ConfirmFileExists<T>(string filename, T data)
        {
            if (!File.Exists(filename)) { SaveToJson(filename, data); return false; }
            return true;
        }

        //these two are yoinked as well
        /// <summary>
        /// Saves data to a file
        /// </summary>
        /// <typeparam name="T">The type of the data</typeparam>
        /// <param name="filename">The filename/path to store the file</param>
        /// <param name="data">The data to store</param>
        public static void SaveToJson<T>(string filename, T data)
        {
            using FileStream fileStream = File.Create(filename);
            JsonSerializer.Serialize(fileStream, data);
        }

        /// <summary>
        /// Returns the data located in the specified file path
        /// </summary>
        /// <typeparam name="T?">The type of the data</typeparam>
        /// <param name="filename">The filename/path of the file</param>
        /// <returns>The data in the file (May return null if no data is present)</returns>
        public static T? ReadFromJson<T>(string filename)
        {
            using FileStream fileStream = File.OpenRead(filename);
            return JsonSerializer.Deserialize<T>(fileStream);
        }

        private async void PausePlayButton_Click(object sender, EventArgs e)
        {
            if (musicPlayer.IsStopped)
            {
                musicPlayer.Seek(TimeSpan.Zero);
                ResetRender();
                musicPlayer.Play();
            }
            else if (!musicPlayer.IsPlaying)
                await musicPlayer.Play();
            else
                musicPlayer.Pause();
        }

        private void UpdateEverything()
        {
            //Debug
            {

            }


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
                    new SolidBrush(barFillColour), // colour choice
                    0, 0, // position of top left corner
                    (float)(progressBar.Width * (musicPlayer.CurrentTime.TotalSeconds / musicPlayer.Duration.TotalSeconds)), // width of rectangle
                    progressBar.Height); // height of rectangle
            }

            // UpdateVolumeDisplay
            {
                volumeDisplayGraphics.FillRectangle(
                    new SolidBrush(barFillColour),
                    0, 0,
                    (float)(volumeDisplay.Width * (musicPlayer.Volume / 1)),
                    volumeDisplay.Height);

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

            // UpdatePlaylistCounter
            {
                playlistCounter.Text = (playlistIndex < 0 ? "0" : playlistIndex) + " / " + currentPlaylist.Count;
                if (playlistCounter.Text == "0 / 0") playlistCounter.Text = ""; //make it invisible if there is no playlist loaded
            }

        }

        private void ResetRender()
        {
            timeLabel.Text = "00:00 / 00:00";
            progressBarGraphics.Clear(progressBar.BackColor);
            songName.Text = "Name: N/A";
            albumName.Text = "Album: N/A";
            artistName.Text = "Artist: N/A";
            playlistCounter.Text = "0 / 0";
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
            playlistListBox.Items.Clear();

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

        private void LoadPlaylist(string[] paths)
        {
            currentPlaylist = new();
            playlistListBox.Items.Clear();

            string[] validExtensions = [".mp3", ".wav", ".ogg", ".flac"];

            foreach (var file in paths)
            {
                var shell = ShellFile.FromFilePath(file);
                if (validExtensions.Contains(shell.Properties.System.FileExtension.Value))
                {
                    currentPlaylist.Add(file);


                    var title = shell.Properties.System.Title.Value;
                    var fileName = shell.Properties.System.FileName.Value;

                    playlistListBox.Items.Add(string.IsNullOrWhiteSpace(title) ? fileName.Substring(0, fileName.IndexOf('.')) : title);
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
            hoverLabel.Left = mousePos.X + progressBar.Left - 18;

            hoverLabel.Tag = "modified";
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                currentPlaylist = new();
                playlistListBox.Items.Clear();
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

        private void StopButton_Click(object sender, EventArgs e)
        {
            if (!musicPlayer.IsReady) return;

            musicPlayer.Stop();
            musicPlayer.Play();     // <- this is here so it doesn't go to the next song when in a playlist
            musicPlayer.Pause();    // <-
            ResetRender();

        }

        private void VolumeDown_Click(object sender, EventArgs e)
        {
            musicPlayer.Volume += -volumeStep;

            var volumeAmount = (float)(volumeDisplay.Width * (musicPlayer.Volume / 1));

            volumeDisplayGraphics.FillRectangle(
                    new SolidBrush(volumeDisplay.BackColor),
                    volumeAmount, 0,
                    volumeDisplay.Width - volumeAmount,
                    volumeDisplay.Height);

            SaveToJson("volume.json", musicPlayer.Volume);
        }

        private void VolumeUp_Click(object sender, EventArgs e)
        {
            musicPlayer.Volume += volumeStep;
            SaveToJson("volume.json", musicPlayer.Volume);
        }

        private void PlaylistBack_Click(object sender, EventArgs e)
        {
            if (playlistIndex > 1 && musicPlayer.IsReady)
            {
                playlistIndex -= 2;
                musicPlayer.Stop();
            }
        }

        private void PlaylistForward_Click(object sender, EventArgs e)
        {
            if (playlistIndex < currentPlaylist.Count && musicPlayer.IsReady)
            {
                musicPlayer.Stop();
            }
        }

        private void FileHover(object sender, DragEventArgs e)
        {
            //https://learn.microsoft.com/en-us/dotnet/desktop/winforms/advanced/walkthrough-performing-a-drag-and-drop-operation-in-windows-forms

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void FileDrop(object sender, DragEventArgs e)
        {
            //https://stackoverflow.com/questions/68598/how-do-i-drag-and-drop-files-into-an-application            

            string[] files = (from file in (string[])e.Data.GetData(DataFormats.FileDrop) where (file.ToLower().EndsWith(".mp3") || file.ToLower().EndsWith(".ogg") || file.ToLower().EndsWith(".wav") || file.ToLower().EndsWith(".flac")) select file).ToArray();

            if (files.Length == 1)
            {
                currentPlaylist = new();
                playlistListBox.Items.Clear();
                LoadSong(files[0]);
            }
            else if (files.Length != 0)
            {
                LoadPlaylist(files);
            }
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            var aboutForm = new About();
            aboutForm.ShowDialog();
        }

        private void MuteButton_Click(object sender, EventArgs e)
        {
            if (muted)
            {
                muted = false;
                musicPlayer.Volume = prevVolume;
                muteButton.BackColor = pausePlayButton.BackColor;
                muteButton.Image = Image.FromStream(new MemoryStream(Icons.playing));
                volumeDown.Enabled = true;
                volumeUp.Enabled = true;
                volumeDown.Image = Image.FromStream(new MemoryStream(Icons.volumedown));
                volumeUp.Image = Image.FromStream(new MemoryStream(Icons.volumeup));
            }
            else
            {
                muted = true;
                prevVolume = musicPlayer.Volume;
                musicPlayer.Volume = 0f;
                muteButton.BackColor = muteActive;
                muteButton.Image = Image.FromStream(new MemoryStream(Icons.muted));
                volumeDown.Enabled = false;
                volumeUp.Enabled = false;
                volumeDown.Image = Image.FromStream(new MemoryStream(Icons.volumedowndisabled));
                volumeUp.Image = Image.FromStream(new MemoryStream(Icons.volumeupdisabled));
            }

        }

        /*
        public void OnLoop(object sender, LoopEventArgs e)
        {
            this.Invoke(ResetRender);
        }*/
    }
}

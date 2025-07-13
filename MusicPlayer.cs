using Microsoft.VisualBasic.Devices;
using NAudio.Vorbis;
using NAudio.Wave;
using System.Runtime.CompilerServices;

namespace ViMusic
{
    internal class MusicPlayer
    {
        //https://github.com/naudio/NAudio/blob/master/Docs/PlayAudioFileConsoleApp.md

        //private variables
        private readonly WaveOutEvent outputDevice;
        private AudioFileReader? audioReader;
        private VorbisWaveReader? oggReader;
        private bool isOgg;

        //public fields
        public TimeSpan CurrentTime
        {
            get
            {
                if (audioReader == null && oggReader == null) return new TimeSpan(0, 0, 0);
                return isOgg ? oggReader.CurrentTime : audioReader.CurrentTime;
            }
            set
            {
                if (isOgg) oggReader.CurrentTime = value;
                else audioReader.CurrentTime = value;
            }
        }

        public TimeSpan Duration
        {
            get
            {
                if (audioReader == null && oggReader == null) return new TimeSpan(0, 0, 0);
                return isOgg ? oggReader.TotalTime : audioReader.TotalTime;
            }
        }

        public float Volume
        {
            get
            {
                return outputDevice.Volume;
            }
            set
            {
                if (value < 0)
                    outputDevice.Volume = 0;
                else if (value > 1)
                    outputDevice.Volume = 1;
                else
                    outputDevice.Volume = MathF.Round(value, 1);
            }
        }

        public bool IsPlaying
        {
            get
            {
                return outputDevice.PlaybackState == PlaybackState.Playing;
            }
        }

        public bool IsStopped
        {
            get
            {
                return outputDevice.PlaybackState == PlaybackState.Stopped;
            }
        }

        public bool IsReady
        {
            get
            {
                return !(audioReader == null && oggReader == null);
            }
        }

        public MusicPlayer()
        {
            outputDevice = new WaveOutEvent();
            outputDevice.Volume = 1.0f;
        }

        public void LoadAudioFromFile(string filename)
        {
            isOgg = filename.ToLower().EndsWith(".ogg");

            audioReader = isOgg ? null : new AudioFileReader(filename); // these two lines are very strange but they stop exceptions
            oggReader = isOgg ? new VorbisWaveReader(filename) : null;  // probably could have just used try catch but this does work so

            if (outputDevice.PlaybackState == PlaybackState.Playing) outputDevice.Stop();

            outputDevice.Init(isOgg ? oggReader : audioReader);
        }

        public void Seek(TimeSpan time)
        {
            
            if (time > Duration) CurrentTime = Duration;
            else if (time.TotalSeconds < 0) CurrentTime = new TimeSpan(0);
            else CurrentTime = time;
        }

        /// <summary>
        /// Starts the loaded audio file in a background thread
        /// </summary>
        public async Task Play()
        {

            await Task.Run(() =>
            {
                outputDevice.Play();

                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(500);
                }
            });
        }

        public void Play(string filename)
        {
            LoadAudioFromFile(filename);
            Play();
        }

        /// <summary>
        /// Stops the currently playing audio file
        /// </summary>
        public void Stop()
        {
            Seek(-CurrentTime);

            outputDevice.Stop();
        }

        /// <summary>
        /// Pauses the currently playing audio file
        /// </summary>
        public void Pause()
        {
            outputDevice.Pause();
        }

    }
}

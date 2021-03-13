using System;
using System.Diagnostics;
using System.IO;
using Plugin.AudioRecorder;
using Xamarin.Essentials;

namespace DL
{
    public class SoundPlayer : ISoundPlayer
    {

        public string _filePath = Path.Combine(FileSystem.AppDataDirectory, "Recording.wav");
        private AudioPlayer player;

        public SoundPlayer()
        {
            player = new AudioPlayer();
        }
        public void PlayRecording()
        {
            Debug.WriteLine("Optagelsen forsøges afspillles og optagelsen er færdig" + DateTime.Now.ToString());
            try
            {
                player.Play(_filePath);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Optagelsen forsøges afspillles og optagelsen er færdig, men kunne ikke afspille" + DateTime.Now.ToString());
            }
        }

        public void PlayRecording(Stream sound)
        {
            SaveFileStream(_filePath, sound);
            PlayRecording();
        }

        private void SaveFileStream(string path, Stream stream)
        {
            var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            stream.CopyTo(fileStream);
            fileStream.Dispose();
        }
    }
}
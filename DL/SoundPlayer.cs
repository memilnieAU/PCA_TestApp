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

        /// <summary>
        /// Her bliver der afspillede lyd fra default filen i AppDataDir
        /// </summary>
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


        /// <summary>
        /// Her bliver der afspillede en bestemt stream som skal injectieres.
        /// Denne Stream vil blive gemt i AppDataDir,
        /// hvorefter den bliver afspillet med vores default PlayRecording().
        /// 
        /// Dette er smart når vi skal til at hente tidligere målinger,
        /// da disse som udgangspunkt IKKE er gemt i AppDataDir fra start af.
        /// </summary>
        /// <param name="sound">Den specefikke lyd der ønskes afspillet</param>
        public void PlayRecording(Stream sound)
        {
            //Todo denne skal fjernes når vi skal kunne loade fra vores database
            //SaveFileStream(_filePath, sound);
            PlayRecording();
        }


        /// <summary>
        /// Kan gemme et Stream objekt ned i en fil, hvis sti medtages som parameter
        /// </summary>
        /// <param name="path">Sti hvor lyden er gemt</param>
        /// <param name="stream">Lyden der skal gemmes</param>
        private void SaveFileStream(string path, Stream stream)
        {
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
                fileStream.Dispose();
                //TODO Skal vi lukke vores FileStream Sikkert ned????
                //fileStream.Close();
            }
        }
    }
}
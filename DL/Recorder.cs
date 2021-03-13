using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using DL.DTO;
using Plugin.AudioRecorder;
using Xamarin.Essentials;

namespace DL
{
    public class Recorder : IRecorder
    {
        private AudioRecorderService recorder;
        private AudioPlayer player;
        public string _filePath = Path.Combine(FileSystem.AppDataDirectory, "Recording.wav");
        private Measurement measureDTO;

        public Recorder()
        {
            recorder = new AudioRecorderService();
            player = new AudioPlayer();

            recorder.StopRecordingOnSilence = false;
            recorder.StopRecordingAfterTimeout = true;
            recorder.TotalAudioTimeout = TimeSpan.FromSeconds(LengthOfRecording);
            recorder.AudioInputReceived += HandleRecordIsFinished;
        }

        private string _recorderFilePath = @"";

        public string RecorderFilePath
        {
            get { return _recorderFilePath; }
            set { _recorderFilePath = value; }
        }
        private double _lengthOfRecording = 10;

        public double LengthOfRecording
        {
            get { return _lengthOfRecording; }
            set { _lengthOfRecording = value; }
        }

        public void PlayRecording()
        {
            //using (var stream = recorder.GetAudioFileStream())
            //{
            //    measureDTO.SoundStream = stream;
            //    SaveFileStream(_filePath, stream); //Appdatadirectory sti
            //}

            if (!recorder.IsRecording)
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
            else
            {
                Debug.WriteLine("Optagelsen forsøges afspillles, men optagelsen er stadig igang" + DateTime.Now.ToString());
            }
        }

        public void SaveFileStream(string path, Stream stream)
        {
            var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            stream.CopyTo(fileStream);
            fileStream.Dispose();
        }
        Stopwatch stopWatch = new Stopwatch();


        private void HandleRecordIsFinished(object sender, string e)
        {
            stopWatch.Stop();
            using (var stream = recorder.GetAudioFileStream())
            {
                measureDTO.SoundStream = stream;
                SaveFileStream(_filePath, stream); //Appdatadirectory sti
            }
            Debug.WriteLine("Vi har modtaget en event som vi lige har ageret på" + DateTime.Now.ToString());
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Debug.WriteLine("RunTime " + elapsedTime);
            stopWatch = new Stopwatch();
        }

        public async Task RecordTask()
        {
            try
            {
                stopWatch.Start();
                System.Diagnostics.Debug.WriteLine("Nu burde vi starte med at optage" + DateTime.Now.ToString());
                await recorder.StartRecording();
                System.Diagnostics.Debug.WriteLine("Nu burde vi være færdig med at optage" + DateTime.Now.ToString());
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Measurement> RecordAudio()
        {

            if (!recorder.IsRecording)
            {
                measureDTO = new Measurement(DateTime.Now);
                await RecordTask();
                //using (var stream = recorder.GetAudioFileStream())
                //{
                //    measureDTO.SoundStream = stream;
                //    SaveFileStream(_filePath, stream); //Appdatadirectory sti
                //}
                RecorderFilePath = recorder.FilePath; //Midlertidige cachefil. Henter filstien til lydfil og gemmer i vores property til øvrige metoder.
                return measureDTO;
            }
            else
            {
                measureDTO = new Measurement();
                return measureDTO;
            }

        }
    }
}

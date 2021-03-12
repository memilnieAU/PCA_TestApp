using System;
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
            recorder.StopRecordingAfterTimeout = true;
            recorder.TotalAudioTimeout = TimeSpan.FromSeconds(LengthOfRecording);
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
            try
            {
                player.Play(_filePath);
            }
            catch (Exception e)
            {
                //Console.WriteLine("Der var sku ikke nogen fil at afspille");
            }
        }

        public void SaveFileStream(string path, Stream stream)
        {
            var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            stream.CopyTo(fileStream);
            fileStream.Dispose();
        }

        public async Task RecordTask()
        {
            try
            {
                await recorder.StartRecording();

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
                using (var stream = recorder.GetAudioFileStream())
                {
                    measureDTO.SoundStream = stream;
                    SaveFileStream(_filePath, stream); //Appdatadirectory sti
                }
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

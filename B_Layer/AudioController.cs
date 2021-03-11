using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.AudioRecorder;
using Xamarin.Essentials;
using D_Layer.DTO;

namespace B_Layer
{
    public class AudioController : IAudioController
    {
        private AudioRecorderService recorder;
        private AudioPlayer player;
        public string _filePath = Path.Combine(FileSystem.AppDataDirectory, "Recording.wav");
        private Measurement measureDTO;

        private string _pageText = "Du har nu åbnet Plugin Audio Recorder skærmen";

        public string PageText
        {
            get { return _pageText;}
            set { _pageText = value; }
        }

        private string _sampleRate;

        public string SampleRate
        {
            get { return _sampleRate;}
            set { _sampleRate = value; }
        }
        private string _recorderFilePath = @"";

        public string RecorderFilePath
        {
            get { return _recorderFilePath;}
            set { _recorderFilePath = value;}
        }

        public AudioController()
        {
            recorder = new AudioRecorderService();
            player = new AudioPlayer();
        }
        public void PlayRecording()
        {
            try
            {
                player.Play(_filePath);
                PageText="Dato for optagelse: " + measureDTO.StartTime.ToString(); //Dette er kun for test og ikke en reel feature.
            }
            catch (Exception e)
            {
                Console.WriteLine("Der var sku ikke nogen fil at afspille");
            }
        }

        public void SaveFileStream(string path, Stream stream)
        {
            var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            stream.CopyTo(fileStream);
            fileStream.Dispose();
        }


        public async Task RecordAudio()
        {
            try
            {
                if (recorder.IsRecording)
                {
                    await recorder.StopRecording();
                    PageText = "Bliver der optaget lige nu: " + recorder.IsRecording.ToString();

                    RecorderFilePath = recorder.FilePath; //Midlertidige cachefil. Henter filstien til lydfil og gemmer i vores property til øvrige metoder.

                    using (var stream = recorder.GetAudioFileStream())
                    {
                        measureDTO.SoundStream = stream;
                        SaveFileStream(_filePath, stream);
                    }

                }
                else
                {
                    if (!recorder.IsRecording)
                    {
                        await recorder.StartRecording();
                        PageText = "Bliver der optaget lige nu: " + recorder.IsRecording.ToString();
                        measureDTO = new Measurement(DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Java.Nio.FileNio;
using Plugin.AudioRecorder;
using MvvmHelpers.Commands;
using P_Layer.DTO;
using P_Layer.XML;
using Xamarin.Essentials;
using FileSystem = Xamarin.Essentials.FileSystem;

namespace P_Layer.ViewModels
{
    public class PluginAudioRecorderViewModel : ViewModelBase
    {
        private AudioRecorderService recorder;
        private AudioPlayer player;
        public string _filePath = Path.Combine(FileSystem.AppDataDirectory, "Recording.wav");
        private Measurement measureDTO;

        public PluginAudioRecorderViewModel()
        {
            recorder = new AudioRecorderService();
            player = new AudioPlayer();
            RecordAudioCommand = new Command(StartRecordTask);
            PlayAudioCommand = new Command(PlayRecordingTask);
        }
        private string sampleRate;

        public string SampleRate
        {
            get => sampleRate;
            set => SetProperty(ref sampleRate, value);
        }

        private string _pageText = "Du har nu åbnet Plugin Audio Recorder skærmen";

        public string PageText
        {
            get => _pageText;
            set => SetProperty(ref _pageText, value);
        }
        private string _recorderFilePathText = @"";

        public string RecorderFilePath
        {
            get => _recorderFilePathText;
            set => SetProperty(ref _recorderFilePathText, value);
        }

        public ICommand RecordAudioCommand { get; }
        public ICommand PlayAudioCommand { get; }
        private async void StartRecordTask()
        {
            PageText = "Bliver der optaget lige nu: " +recorder.IsRecording.ToString();
            await RecordAudio();
        }

        async Task RecordAudio()
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
                        SaveFileStream(_filePath,stream);
                    }

                }
                else
                {
                    if (!recorder.IsRecording)
                    {
                        await recorder.StartRecording();
                        PageText = "Bliver der optaget lige nu: "+ recorder.IsRecording.ToString();
                        measureDTO = new Measurement(DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void PlayRecordingTask()
        {
            //player.Play(FilePath);
            try
            {
                player.Play(_filePath);
                PageText = "Dato for optagelse: " + measureDTO.StartTime.ToString(); //Dette er kun for test og ikke en reel feature.
            }
            catch (Exception e)
            {
                Console.WriteLine("Der var sku ikke nogen fil at afspille");
            }
        }
        private void SaveFileStream(String path, Stream stream)
        {
            var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            stream.CopyTo(fileStream);
            fileStream.Dispose();
        }
    }
}

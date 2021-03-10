using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Java.Nio.FileNio;
using Plugin.AudioRecorder;
using MvvmHelpers.Commands;
using P_Layer.XML;

namespace P_Layer.ViewModels
{
    public class PluginAudioRecorderViewModel : ViewModelBase
    {
        private AudioRecorderService recorder;
        private AudioPlayer player;
        private string testfilnavn = "Testfil";


        public PluginAudioRecorderViewModel()
        {
            recorder = new AudioRecorderService();
            //recorder.FilePath = @""; Denne skal vi sætte til vores egen sti, så snart vi ved hvor!
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
        private string _filePathText = @"";

        public string FilePath
        {
            get => _filePathText;
            set => SetProperty(ref _filePathText, value);
        }

        public ICommand RecordAudioCommand { get; }
        public ICommand PlayAudioCommand { get; }
        private async void StartRecordTask()
        {
            PageText = "Bliver der optaget lige nu: " +recorder.IsRecording.ToString();
            await RecordAudio();
        }

        string audioFile;
        async Task RecordAudio()
        {
            try
            {
                if (recorder.IsRecording)
                {
                    await recorder.StopRecording();
                    PageText = "Bliver der optaget lige nu: " + recorder.IsRecording.ToString();

                    FilePath = recorder.FilePath; //Henter filstien til lydfil og gemmer i vores prop til øvrige metoder.

                    //PageText = (File.ReadAllText(filePath)); virker ikke at læse datapunkter ind fra lydfilen.

                }
                else if (!recorder.IsRecording)
                {
                    var audioRecordTask = await recorder.StartRecording();
                    PageText = "Bliver der optaget lige nu: "+ recorder.IsRecording.ToString();
                    //audioFile = await audioRecordTask;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void PlayRecordingTask()
        {
            player.Play(FilePath);
        }

    }
}

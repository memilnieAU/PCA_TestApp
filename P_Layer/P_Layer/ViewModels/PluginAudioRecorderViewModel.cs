using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Java.Nio.FileNio;
using Plugin.AudioRecorder;
using Xamarin.Essentials;
using BL;
using DTOs;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace P_Layer.ViewModels
{
    public class PluginAudioRecorderViewModel : ViewModelBase
    {
        private IRecorderController ctrl;
        public PluginAudioRecorderViewModel()
        {
            ctrl = new RecorderController(HandleAnalyzeFinishedEvent);

            RecordAudioCommand = new Command(StartRecordTask);
            PlayAudioCommand = new Command(PlayRecordingTask);
        }

        private string _pageText = "Du har nu åbnet Plugin Audio Recorder skærmen";

        public string PageText
        {
            get => _pageText;
            set => SetProperty(ref _pageText, value);
        }
     
        private string _recorderFilePath = @"";

        public string RecorderFilePath
        {
            get => _recorderFilePath;
            set => SetProperty(ref _recorderFilePath, value);
        }

        private string _sampleRate;

        public string SampleRate
        {
            get { return _sampleRate; }
            set { _sampleRate = value; }
        }


        public ICommand RecordAudioCommand { get; }
        public ICommand PlayAudioCommand { get; }

        private async void StartRecordTask()
        {
            PageText = ctrl.PageText;
            await ctrl.RecordAudio();
        }
        private void PlayRecordingTask()
        {
            ctrl.PlayRecording();
            PageText = ctrl.PageText;
        }

        public Measurement MeasureDTO { get; set; }

         private void HandleAnalyzeFinishedEvent(object sender,AnalyzeFinishedEventArgs e)
         {
             MeasureDTO = e.DTO;
             AnalyzeText = $"Tiden for måling: {MeasureDTO.StartTime}" +
                           $"\n Change for patologisk hjertelyd er: {MeasureDTO.ProbabilityProcent}%";
         }

        private string _analyzeText;

        public string AnalyzeText
        {
            get => _analyzeText;
            set => SetProperty(ref _analyzeText, value);
        }



    }
}

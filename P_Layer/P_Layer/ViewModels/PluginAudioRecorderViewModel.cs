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
using Xamarin.Essentials;
using BL;

namespace P_Layer.ViewModels
{
    public class PluginAudioRecorderViewModel : ViewModelBase
    {
        private RecorderController ctrl;
        public PluginAudioRecorderViewModel()
        {
            ctrl = new RecorderController();
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



    }
}

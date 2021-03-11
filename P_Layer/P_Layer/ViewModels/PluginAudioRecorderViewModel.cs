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
using FileSystem = Xamarin.Essentials.FileSystem;
using B_layer;

namespace P_Layer.ViewModels
{
    public class PluginAudioRecorderViewModel : ViewModelBase
    {
        private string _recorderFilePath
        private AudioController ctrl;
        public PluginAudioRecorderViewModel()
        {
            RecordAudioCommand = new Command(StartRecordTask);
            PlayAudioCommand = new Command(PlayRecordingTask);
        }
      

        private string _pageText = "Du har nu åbnet Plugin Audio Recorder skærmen";

     
        private string _recorderFilePathText = @"";

     

        public ICommand RecordAudioCommand { get; }
        public ICommand PlayAudioCommand { get; }
        private async void StartRecordTask()
        {
            PageText = "Bliver der optaget lige nu: " +recorder.IsRecording.ToString();
            await RecordAudio();
        }
        private async void PlayRecordingTask()
        {
            
            await RecordAudio();
        }



    }
}

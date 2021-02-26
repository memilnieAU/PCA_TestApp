using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Android;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.V4.Content;

using MvvmHelpers.Commands;
using PCLStorage;
using Plugin.AudioRecorder;
using Xamarin.Essentials;
using FileSystem = PCLStorage.FileSystem;

namespace P_Layer.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private AudioRecorderService recorder;
        AudioPlayer player;
        public HomePageViewModel()
        {
            Title = "Home Page new";
            IncreaseCount = new Command(OnIncrease);
            recorder = new AudioRecorderService
            {
                TotalAudioTimeout = TimeSpan.FromSeconds(15),
                StopRecordingOnSilence = true
            };
            player = new AudioPlayer();
            HomepageText = recorder.IsRecording.ToString();

        }

        private string _HomepageText = "Du har nu åbnet Homepage";

        public string HomepageText
        {
            get => _HomepageText;
            set => SetProperty(ref _HomepageText, value);
        }

        public ICommand IncreaseCount { get; }
        public ICommand AddUser { get; }
        async private void OnIncrease()
        {
            HomepageText = recorder.IsRecording.ToString();
            await RecordAudio();
        }

        async Task RecordAudio()
        {
            try
            {
                if (recorder.IsRecording)
                {
                    await recorder.StopRecording();
                    HomepageText = recorder.IsRecording.ToString();

                    var filePath = recorder.GetAudioFilePath();
                    //HomepageText = recorder.FilePath;
                    HomepageText += "\n&&&&&&&&&&&&&&\n" + recorder.GetAudioFileStream();
                    HomepageText += "\n&&&&&&&&&&&&&&\n" + recorder.GetAudioFilePath();

                    player.Play(filePath);
                    PCLStorageSample();

                }
                else if (!recorder.IsRecording)
                {
                    var audioRecordTask = await recorder.StartRecording();

                    HomepageText = recorder.IsRecording.ToString();
                    audioFile = await audioRecordTask;
                }

                HomepageText += "\n%%%%%%%%%%%%%%\n" + audioFile;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        string audioFile;
        public async Task PCLStorageSample()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync("MySubFolder",
                CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync("answer.txt",
                CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(recorder.PreferredSampleRate.ToString());
            HomepageText += "\n@@@@@@@@@@@@@@\n" + recorder.AudioStreamDetails;
            HomepageText += "\n@@@@@@@@@@@@@@\n" + file.Path;
            var cacheDir = Xamarin.Essentials.FileSystem.CacheDirectory;
            Vibration.Vibrate();
            using (var stream = await Xamarin.Essentials.FileSystem.OpenAppPackageFileAsync(recorder.GetAudioFilePath()))
            {
                using (var reader = new StreamReader(stream))
                {
                    var fileContents = await reader.ReadToEndAsync();
                }
            }
        }


    }
}


using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using MvvmHelpers.Commands;
using Xamarin.Essentials;

namespace P_Layer.ViewModels
{
    class FileHelpersViewModel : ViewModelBase
    {

        //https://docs.microsoft.com/en-gb/xamarin/essentials/file-system-helpers?tabs=android

        public FileHelpersViewModel()
        {
            Title = "File Page new";
            LoadFromAssets = new Command(LoadFromAssets_Method);
            LoadFromExternalStorage = new Command(LoadFromExternalStorage_Method);
            SaveToExternalStorage = new Command(SaveToExternalStorage_Method);
            ResetLoadText = new Command(ResetLoadText_Method);
            var mainDor = FileSystem.AppDataDirectory;
            localPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ExternalFileName);
            //localPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ExternalFileName);
        }

        private string localPath;
        private const string ExternalFileName = "MyExternalFile.txt";
        private const string templateFileName = "MyTempFile.txt";

        private string filePath;

        public string ShowFilePath
        {
            get => filePath;
            set => SetProperty(ref filePath, value);
        }

        private string _LoadText = "Du har nu åbnet FileHelpers Page";

        public string LoadText
        {
            get => _LoadText;
            set => SetProperty(ref _LoadText, value);
        }
        public ICommand LoadFromAssets { get; }
        public ICommand LoadFromExternalStorage { get; }
        public ICommand SaveToExternalStorage { get; }
        public ICommand ResetLoadText { get; }

        async private void LoadFromAssets_Method()
        {
            using (var stream = await FileSystem.OpenAppPackageFileAsync(templateFileName))
            {
                using (var reader = new StreamReader(stream))
                {
                    LoadText = await reader.ReadToEndAsync();
                }
            }
            ShowFilePath = templateFileName;
        }




        private void LoadFromExternalStorage_Method()
        {
            try
            {
                LoadText = File.ReadAllText(localPath);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                LoadText = "Der var ingen fil her!!!";
            }
            ShowFilePath = localPath;
        }

        private void SaveToExternalStorage_Method()
        {
            File.WriteAllText(localPath, LoadText);
        }




        private void ResetLoadText_Method()
        {
            LoadText = "Reset";
        }
    }
}

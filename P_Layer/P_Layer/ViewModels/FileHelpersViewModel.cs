using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Essentials;

namespace P_Layer.ViewModels
{
    class FileHelpersViewModel : ViewModelBase
    {
       
        //https://docs.microsoft.com/en-gb/xamarin/essentials/file-system-helpers?tabs=android

            string mainDor;
        public FileHelpersViewModel()
        {
            Title = "File Page new";
            LoadJson = new Command(LoadJson_Method);
            SaveJson = new Command(SaveJson_Method);
            LoadFromAssets = new Command(LoadFromAssets_Method);
            LoadFromExternalStorage = new Command(LoadFromExternalStorage_Method);
            SaveToExternalStorage = new Command(SaveToExternalStorage_Method);
            ResetLoadText = new Command(ResetLoadText_Method);
            mainDor = FileSystem.AppDataDirectory;
            localPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ExternalFileName);
            //localPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ExternalFileName);
        }

        private void LoadJson_Method()
        {
            string filePath = Path.Combine(mainDor, "employee.json");
           
            // read JSON directly from a file
            using (StreamReader file = File.OpenText(filePath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);
                JsonSerializer jsonSerializer = new JsonSerializer();

            }

            using FileStream openStream = File.OpenRead(filePath);
            //  weatherForecast = await JsonSerializer.DeserializeAsync<WeatherForecast>(openStream);
        }

        private void SaveJson_Method()
        {
            testcalse t = new testcalse();
            string json = JsonConvert.SerializeObject(t);

            string filePath = Path.Combine(mainDor, "employee.json");
            using (var file = File.Open(filePath, FileMode.Create, FileAccess.Write))
            using (var strm = new StreamWriter(file))
            {
                strm.Write(json);
            }
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

        public ICommand LoadJson { get; }
        public ICommand SaveJson { get; }

        

        async private void LoadFromAssets_Method()
        {
            using (var stream = await FileSystem.OpenAppPackageFileAsync(templateFileName))
            {
                using (var reader = new StreamReader(stream))
                {
                    LoadText = await reader.ReadToEndAsync();
                }
                ShowFilePath = templateFileName;

               
            }
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

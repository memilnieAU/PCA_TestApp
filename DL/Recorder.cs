using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using DTOs;
using EventArgss;
using Plugin.AudioRecorder;
using Xamarin.Essentials;

namespace DL
{
    /// <summary>
    /// Denne gør at vi kan afkoble AudioRecorderService under test
    /// </summary>
    public interface IAudioRecorderService
    {
        /// <summary>
        /// Retunerer true, hvis optagelsen er i gang
        /// </summary>
        bool IsRecording { get; }
        /// <summary>
        /// Den filsti som der gemmes en stream på, kan ændres
        /// </summary>
        string FilePath { get; set; }
        /// <summary>
        /// Get AudioRecorderService.TotalAudioTimeout som double
        /// Set AudioRecorderService.TotalAudioTimeout som double, mothoden konverter selv til TimeSpan
        /// </summary>
        double AudioTimeout { get; set; }
        event EventHandler<string> AudioInputReceived;
        /// <summary>
        /// Starter en async optagelse
        /// </summary>
        /// <returns></returns>
        Task<Task<string>> StartRecording();
        Stream GetAudioFileStream();
    }

    /// <summary>
    /// Denne klasse arver fra AudioRecorderService og implamentere IAudioRecorderService
    /// Det gør at man kan teste på dette datalag
    /// </summary>
    public class ExtendedAudioRecorderService : AudioRecorderService, IAudioRecorderService
    {
        /// <summary>
        /// Sætter som default optagelsen til at stoppe efter 10 sek, og uden at den stopper OnSilence
        /// </summary>
        /// <param name="handleRecordIsFinished">Notificeres når optagelsen er færdig</param>
        public ExtendedAudioRecorderService(EventHandler<string> handleRecordIsFinished)
        {
            StopRecordingOnSilence = false;
            StopRecordingAfterTimeout = true;
            AudioTimeout = 10;

            AudioInputReceived += handleRecordIsFinished;
        }


        public double AudioTimeout

        {
            get { return Convert.ToDouble(TotalAudioTimeout); }
            set { TotalAudioTimeout = TimeSpan.FromSeconds(value); }
        }
    }


    /// <summary>
    /// Bruges til at afkoble systemet så det kan testes
    /// </summary>
    public interface ISaveToMobile
    {
        /// <summary>
        /// Kan gemme en stream
        /// </summary>
        /// <param name="SaveToFilePath">Stien på der der ønskes at gemmes</param>
        /// <param name="ElementToSave">Det stream objekt der ønskes at gemmes</param>
        void Save(string SaveToFilePath, Stream ElementToSave);
    }

    public class SaveToMobile : ISaveToMobile
    {
        public void Save(string saveToFilePath, Stream elementToSave)
        {
            var fileStream = new FileStream(saveToFilePath, FileMode.Create, FileAccess.Write);
            elementToSave.CopyTo(fileStream);
            fileStream.Dispose();
        }
    }

    /// <summary>
    /// Bruges til at få DateTime.Now og som StopUr
    /// Men dette gør recorder testbar
    /// </summary>
    public interface ITimeProvider : IStopWatch
    {
        DateTime GetDateTime();
    }

    /// <summary>
    /// Bruges til stopur
    /// Gør at recorder er testbar
    /// </summary>
    public interface IStopWatch
    {

        void Start();
        void Stop();
        void Reset();

        /// <summary>
        /// Printer total elapsed time measured by the current instance i Debug.WriteLine
        /// </summary>
        void PrintElapsed();
        /// <summary>
        /// Denne metode starter uret på ny og udskriver at den er started i Debug.WriteLine
        /// </summary>
        void StartTimer();
        /// <summary>
        /// Dette er en kombi, da den både kan stoppe, Printe i Debug.WriteLine og Resette stopuret
        /// </summary>
        /// <param name="print">True, hvis det skal printes til Debug.WriteLine</param>
        /// <param name="reset">True, hvis uret ska resettes til 0</param>
        void StopTimer(bool print, bool reset);
    }

    public class RealTimeProvicer : Stopwatch, ITimeProvider
    {
        public DateTime GetDateTime()
        {
            return DateTime.Now;
        }

        public void PrintElapsed()
        {
            TimeSpan ts = Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Debug.WriteLine("RunTime " + elapsedTime);
        }

        public void StopTimer(bool print, bool reset)
        {
            Stop();
            if (print)
            {
                PrintElapsed();
            }
            if (reset)
            {
                Reset();

            }
        }

        public void StartTimer()
        {
            Start();
            Debug.WriteLine("Måling er startet");
        }
    }

    public interface IFileAccess
    {
        string CombinePath(string FileName);
    }

    public class MyFileSystem : IFileAccess
    {
        
        public string CombinePath(string fileName)
        {
           return Path.Combine(FileSystem.AppDataDirectory, fileName);
        }
    }
    public class Recorder : IRecorder
    {
        #region Dependencies

        public IAudioRecorderService _recorder;
        public ISaveToMobile _localStorage;
        public ITimeProvider _timeProvider;
        public IFileAccess _fileAccess;
        #endregion
        #region Event

        public event EventHandler<RecordFinishedEventArgs> RecordFinishedEvent;
        protected virtual void OnRecordingFinished(RecordFinishedEventArgs e)
        {
            RecordFinishedEvent?.Invoke(this, e);
        }

        #endregion
        #region Props

        private string _filePathToLocalStorage;

        //TODO Til MM Fra MEN => Vi bruger ikke denne og kan ikke lige huske hvorfor?
        private string _recorderFilePath;

        public string RecorderFilePath
        {
            get { return _recorderFilePath; }
            set
            {
                if (_recorderFilePath == null)
                {
                    _recorderFilePath = _recorder.FilePath; //Midlertidige cachefil. Henter filstien til lydfil og gemmer i vores property til øvrige metoder.
                }
                _recorderFilePath = value;
            }
        }

        #endregion
        
        public Recorder(EventHandler<RecordFinishedEventArgs> recordFinishedEventHandler, IAudioRecorderService audioRecorderService, ISaveToMobile saveToMobile,
            ITimeProvider timeProvider, IFileAccess fileAccess)
        {
            RecordFinishedEvent += recordFinishedEventHandler;

            _recorder = audioRecorderService ?? new ExtendedAudioRecorderService(HandleRecorderIsFinished);
            _localStorage = saveToMobile ?? new SaveToMobile();
            _timeProvider = timeProvider ?? new RealTimeProvicer();
            _fileAccess = fileAccess ?? new MyFileSystem();
        }

        public Recorder(EventHandler<RecordFinishedEventArgs> recordFinishedEventHandler)
        {
            RecordFinishedEvent += recordFinishedEventHandler;

            _recorder = new ExtendedAudioRecorderService(HandleRecorderIsFinished);
            _localStorage = new SaveToMobile();
            _timeProvider = new RealTimeProvicer();
            _fileAccess = new MyFileSystem();

        }


        private void HandleRecorderIsFinished(object sender, string e)
        {
            _timeProvider.StopTimer(true, true);

            Measurement tempMeasureDTO = new Measurement(_timeProvider.GetDateTime());

            using (var stream = _recorder.GetAudioFileStream())
            {
                tempMeasureDTO.SoundStream = stream;
                _localStorage.Save(_fileAccess.CombinePath("Recording.wav"), stream);
            }

            OnRecordingFinished(new RecordFinishedEventArgs
            {
                measureDTO = tempMeasureDTO
            });

        }

        public void RecordAudio()
        {
            if (!_recorder.IsRecording)
            {
                _timeProvider.StartTimer();
                _recorder.StartRecording();
            }
        }
    }
}

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
    public interface IAudioRecorderService
    {
        bool IsRecording { get; }
        string FilePath { get; set; }
        double AudioTimeout { get; set; }
        event EventHandler<string> AudioInputReceived;
        Task<Task<string>> StartRecording();
        Stream GetAudioFileStream();
    }
    public class MyAudioRecorderService : AudioRecorderService, IAudioRecorderService
    {
        public event EventHandler<RecordFinishedEventArgs> RecordFinishedEvent;

        public MyAudioRecorderService(EventHandler<string> handleRecordIsFinished)
        {
            StopRecordingOnSilence = false;
            StopRecordingAfterTimeout = true;
            TotalAudioTimeout = TimeSpan.FromSeconds(10);

            AudioInputReceived += handleRecordIsFinished;
        }


        public double AudioTimeout
        {
            get { return Convert.ToDouble(TotalAudioTimeout); }
            set { TotalAudioTimeout = TimeSpan.FromSeconds(value); }
        }


    }

    public interface ISaveToMobile
    {
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

    public interface ITimeProvider
    {
        DateTime GetDateTime();
        void Stop();
        void Start();
        void Reset();
        void PrintElapsed();
        void StopTimer(bool print, bool reset);
        void StartTimer();
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

    public class Recorder : IRecorder
    {
        private IAudioRecorderService _recorder;
        private ISaveToMobile _localStorage;
        private ITimeProvider _timeProvider;

        #region Event

        public event EventHandler<RecordFinishedEventArgs> RecordFinishedEvent;
        protected virtual void OnRecordingFinished(RecordFinishedEventArgs e)
        {
            RecordFinishedEvent?.Invoke(this, e);
        }

        #endregion
        #region Props


        public string _filePath = Path.Combine(FileSystem.AppDataDirectory, "Recording.wav");
        Stopwatch stopWatch = new Stopwatch();

        private string _recorderFilePath = @"";

        public string RecorderFilePath
        {
            get { return _recorderFilePath; }
            set { _recorderFilePath = value; }
        }


        #endregion

        public Recorder()
        {
            _recorder = new MyAudioRecorderService(HandleRecordIsFinished);

            _localStorage = new SaveToMobile();
            _timeProvider = new RealTimeProvicer();
        }


        private void HandleRecordIsFinished(object sender, string e)
        {

            _timeProvider.StopTimer(true, true);

            Measurement tempMeasureDTO = new Measurement(_timeProvider.GetDateTime());

            using (var stream = _recorder.GetAudioFileStream())
            {
                tempMeasureDTO.SoundStream = stream;
                _localStorage.Save(_filePath, stream);
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
                RecorderFilePath = _recorder.FilePath; //Midlertidige cachefil. Henter filstien til lydfil og gemmer i vores property til øvrige metoder.

            }
        }
    }
}

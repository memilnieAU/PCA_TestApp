using System;
using System.Threading.Tasks;
using DTOs;
using EventArgss;

namespace DL
{
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

        public Recorder(EventHandler<RecordFinishedEventArgs> recordFinishedEventHandler, IAudioRecorderService audioRecorderService, ISaveToMobile saveToMobile,
            ITimeProvider timeProvider, IFileAccess fileAccess)
        {
            RecordFinishedEvent += recordFinishedEventHandler;

            _recorder = audioRecorderService ?? new ExtendedAudioRecorderService(HandleRecorderIsFinished);
            _localStorage = saveToMobile ?? new SaveToMobile();
            _timeProvider = timeProvider ?? new RealTimeProvider();
            _fileAccess = fileAccess ?? new FileSystemAccess();
        }

        public Recorder(EventHandler<RecordFinishedEventArgs> recordFinishedEventHandler)
        {
            RecordFinishedEvent += recordFinishedEventHandler;

            _recorder = new ExtendedAudioRecorderService(HandleRecorderIsFinished);
            _localStorage = new SaveToMobile();
            _timeProvider = new RealTimeProvider();
            _fileAccess = new FileSystemAccess();
        }

        public void RecordAudio()
        {
            _timeProvider.StartTimer();
            _recorder.StartRecording();
        }

        private void HandleRecorderIsFinished(object sender, string e)
        {
            _timeProvider.StopTimer(true, true);

            Measurement tempMeasureDTO = new Measurement(_timeProvider.GetDateTime());

            using (var stream = _recorder.GetAudioFileStream())
            {
                tempMeasureDTO.SoundStream = stream;
                _localStorage.Save(_fileAccess.GetCombinePath("Recording.wav"), stream);
            }

            OnRecordingFinished(new RecordFinishedEventArgs
            {
                measureDTO = tempMeasureDTO
            });
        }
    }
}

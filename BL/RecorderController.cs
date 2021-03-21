﻿using System;
using System.IO;
using System.Threading.Tasks;
using Plugin.AudioRecorder;
using Xamarin.Essentials;
using DL;
using DTOs;
using EventArgss;


namespace BL
{
    public class RecorderController : IRecorderController
    {
        private Measurement measureDTO;

        #region Props

        private string _pageText = "Du har nu åbnet Plugin Audio Recorder skærmen";

        public string PageText
        {
            get { return _pageText; }
            set { _pageText = value; }
        }
        //RecorderController skal sørge for at udskrive vigtige meddelelser ifbm. recording!!!!!!!!!!!
        private string _sampleRate;

        public string SampleRate
        {
            get { return _sampleRate; }
            set { _sampleRate = value; }
        }
        private bool _isRecording; //Todo Få knapper på UI til at være inaktive når der optages (Fx.)

        public bool IsRecording
        {
            get { return _isRecording; }
            private set { _isRecording = value; }
        }

        #endregion

        #region Dependencies

        private IRecorder _recorderLogic;
        private ISoundModifyLogic _soundModifyLogic;
        private IAnalyzeLogic _analyse;
        private ISaveData _dataStorage;

        #endregion

        public RecorderController(EventHandler<AnalyzeFinishedEventArgs> handleAnalyzeFinishedEvent)
        {
            _recorderLogic = new Recorder(HandleRecordingFinishedEvent);
            _soundModifyLogic = new SoundModifyLogic(null);
            _analyse = new AnalyzeLogic(handleAnalyzeFinishedEvent);
            _dataStorage = new FakeStorage(); //ligger som internal class

            IsRecording = false;
        }


        public void PlayRecording()
        {
            _soundModifyLogic.PlayRecording(MeasureDTO.SoundStream);
        }

        public void RecordAudio()
        {
            if (IsRecording == false)
            {
                _recorderLogic.RecordAudio();
                IsRecording = true;
            }
        }
        private Measurement _measureDTO;

        public Measurement MeasureDTO
        {
            get { return _measureDTO; }
            set { _measureDTO = value; }
        }

        private void HandleRecordingFinishedEvent(object sender, RecordFinishedEventArgs e)
        {
            IsRecording = false;
            MeasureDTO = e.measureDTO;
            MeasureDTO = _analyse.Analyze(MeasureDTO);
            _dataStorage.SaveToStorage(MeasureDTO);
        }
    }

    internal class FakeStorage: ISaveData
    {
        public void SaveToStorage(Measurement elementToStorage)
        {
            System.Diagnostics.Debug.WriteLine("Dine data er nu gemt");
        }
    }
}

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


        private IRecorderLogic _recorderLogic;
        private ISoundModifyLogic _soundModifyLogic;
        private IAnalyzeLogic _analyse;
        private ISaveData _dataStoreage;

        public RecorderController(EventHandler<AnalyzeFinishedEventArgs> handleAnalyzeFinishedEvent)
        {
            _recorderLogic = new RecorderLogic(HandleRecordingFinishedEvent);
            _soundModifyLogic = new SoundModifyLogic();

            _analyse = new AnalyzeLogic();
            _analyse.AnalyzeFinishedEvent += handleAnalyzeFinishedEvent;
            
            
            _dataStoreage = new FakeStorage();
        }


        public void PlayRecording()
        {
            _soundModifyLogic.PlayRecording(MeasureDTO.SoundStream);
        }

        public async Task RecordAudio()
        {
            await _recorderLogic.RecordAudio();
        }
        private Measurement _measureDTO;

        public Measurement MeasureDTO
        {
            get { return _measureDTO; }
            set { _measureDTO = value; }
        }

        private void HandleRecordingFinishedEvent(object sender, RecordFinishedEventArgs e)
        {
            MeasureDTO = e.measureDTO;
            MeasureDTO = _analyse.Analyze(MeasureDTO);
            _dataStoreage.SaveToStorage(_measureDTO);
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

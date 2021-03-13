using System;
using System.IO;
using System.Threading.Tasks;
using Plugin.AudioRecorder;
using DL.DTO;
using Xamarin.Essentials;
using DL;


namespace BL
{
    public class RecorderController : IRecorderController
    {
        private Measurement measureDTO;

        private string _pageText = "Du har nu åbnet Plugin Audio Recorder skærmen";

        public string PageText
        {
            get { return _pageText;}
            set { _pageText = value; }
        }
        //RecorderController skal sørge for at udskrive vigtige meddelelser ifbm. recording!!!!!!!!!!!
        private string _sampleRate;

        public string SampleRate
        {
            get { return _sampleRate;}
            set { _sampleRate = value; }
        }

        
        private IRecorderLogic _recorder;
   

        public RecorderController()
        {
            _recorder = new RecorderLogic();
        }


        public void PlayRecording()
        {
            _recorder.PlayRecording();
        }


        public void SaveFileStream()
        {
            
        }


        public async Task RecordAudio()
        {
            await _recorder.RecordAudio();
        }
    }
}

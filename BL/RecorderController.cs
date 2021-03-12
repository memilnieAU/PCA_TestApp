using System;
using System.IO;
using System.Threading.Tasks;
using Plugin.AudioRecorder;
using DL.DTO;
using Xamarin.Essentials;


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
   

        public RecorderController()
        {
            recorder = new AudioRecorderService();
            player = new AudioPlayer();
        }
        public void PlayRecording()
        {
            
        }

        public void SaveFileStream(string path, Stream stream)
        {
            
        }


        public async Task RecordAudio()
        {
            
        }
    }
}

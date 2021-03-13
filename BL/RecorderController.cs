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

        public RecorderController()
        {
            _recorderLogic = new RecorderLogic();
            _soundModifyLogic = new SoundModifyLogic();
        }


        public void PlayRecording()
        {
            _soundModifyLogic.PlayRecording();
        }



        public async Task RecordAudio()
        {
            await _recorderLogic.RecordAudio();
        }
    }
}

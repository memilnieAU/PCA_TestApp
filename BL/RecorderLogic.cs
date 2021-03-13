using System;
using System.IO;
using System.Threading.Tasks;
using DL;
using EventArgss;

namespace BL
{
    public class RecorderLogic : IRecorderLogic
    {
        private IRecorder _recorder;
        public RecorderLogic(EventHandler<RecordFinishedEventArgs> eventHandler)
        {
            _recorder = new Recorder();
            _recorder.RecordFinishedEvent += eventHandler;
        }
        public async Task RecordAudio()
        {
            await _recorder.RecordAudio();
        }


    }
}

using System;
using System.IO;
using System.Threading.Tasks;
using DL;

namespace BL
{
    public class RecorderLogic : IRecorderLogic
    {
        private IRecorder _recorder;
        public RecorderLogic()
        {
            _recorder = new Recorder();
        }
        

        public async Task RecordAudio()
        {
            await _recorder.RecordAudio();
        }
    }
}

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
        public void PlayRecording()
        {
            _recorder.PlayRecording();
        }

        public void SaveFileStream()
        {
            throw new NotImplementedException();
        }

        public async Task RecordAudio()
        {
            await _recorder.RecordAudio();
        }
    }
}

using System;
using System.IO;
using System.Threading.Tasks;
using DL;
using EventArgss;

namespace BL
{
    public class RecorderLogic : IRecorderLogic
    {
        public IRecorder _recorder;
        public RecorderLogic(EventHandler<RecordFinishedEventArgs> eventHandler, IRecorder recorder)
        {
            _recorder = recorder;
            _recorder.RecordFinishedEvent += eventHandler;
        }
        public void RecordAudio()
        {
            _recorder.RecordAudio();
        }


    }
}

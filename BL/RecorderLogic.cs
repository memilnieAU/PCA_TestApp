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
            _recorder = (recorder==null? new Recorder(eventHandler,null,null,null,null):recorder);
        }
        
        public void RecordAudio()
        {
            _recorder.RecordAudio();
        }


    }
}

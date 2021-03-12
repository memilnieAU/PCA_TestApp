using System;
using System.IO;
using System.Threading.Tasks;

namespace BL
{
    public class RecorderLogic : IRecorderLogic
    {
        public void PlayRecording();
        public void SaveFileStream();
        public Task RecordAudio();
    }
}

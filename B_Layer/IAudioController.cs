using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace B_Layer
{
    public interface IAudioController
    {
        void PlayRecordingTask();
        void SaveFileStream(String path, Stream stream);
        Task RecordAudio();


    }
}

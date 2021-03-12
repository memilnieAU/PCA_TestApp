using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DL.DTO;

namespace DL
{
    public interface IRecorder
    {
        void PlayRecording();
        void SaveFileStream(String path, Stream stream);
        Task<Measurement> RecordAudio();
    }
}

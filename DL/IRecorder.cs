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
        Task<Measurement> RecordAudio();
    }
}

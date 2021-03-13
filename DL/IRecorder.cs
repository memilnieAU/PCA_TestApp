using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DL.DTO;
using DL.EventArgs;

namespace DL
{
    public interface IRecorder
    {
        event EventHandler<RecordFinishedEventArgs> RecordFinishedEvent;
        Task<Measurement> RecordAudio();
    }
}

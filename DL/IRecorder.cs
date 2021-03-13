using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using EventArgss;

namespace DL
{
    public interface IRecorder
    {
        event EventHandler<RecordFinishedEventArgs> RecordFinishedEvent;
        Task<Measurement> RecordAudio();
    }
}

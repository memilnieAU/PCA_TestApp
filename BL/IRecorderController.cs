using System;
using System.IO;
using System.Threading.Tasks;

namespace BL
{
    public interface IRecorderController
    {
        void PlayRecording();
        Task RecordAudio();
        bool IsRecording { get; }
        string PageText { get; set; }
    }
}

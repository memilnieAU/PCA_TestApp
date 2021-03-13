using System;
using System.IO;
using System.Threading.Tasks;

namespace BL
{
    public interface IRecorderLogic
    {
        Task RecordAudio();
    }
}
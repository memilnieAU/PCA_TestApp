using System.IO;

namespace BL
{
    public interface ISoundModifyLogic
    {
        void PlayRecording();
        void PlayRecording(Stream sound);
    }
}
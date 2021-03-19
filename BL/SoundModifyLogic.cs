using System;
using System.IO;
using DL;

namespace BL
{
    public class SoundModifyLogic : ISoundModifyLogic
    {
        private ISoundPlayer _soundPlayer;

        public SoundModifyLogic(ISoundPlayer soundPlayer)
        {
            _soundPlayer = (soundPlayer == null ? new SoundPlayer() : soundPlayer);
        }
        
        public void PlayRecording()
        {
            _soundPlayer.PlayRecording();
        }
        public void PlayRecording(Stream sound)
        {
            _soundPlayer.PlayRecording(sound);
        }
    }
}
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
            _soundPlayer = soundPlayer ?? new SoundPlayer();
        }
        
        public void PlayRecording(Stream sound)
        {
            _soundPlayer.PlayRecording(sound);
        }
    }
}
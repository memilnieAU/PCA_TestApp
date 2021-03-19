using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DL;
using DTOs;
using EventArgss;
using NSubstitute;
using NUnit.Framework;
using Xamarin.Essentials;

namespace BL.TESTS.UNIT
{
    public class RecorderLogic_Test_Unit
    {
        private RecorderLogic UUT;
        private EventHandler<RecordFinishedEventArgs> eventHandler;
        IRecorder sub;

        [SetUp]
        public void Setup()
        {
            sub = Substitute.For<IRecorder>();
            UUT = new RecorderLogic(eventHandler, sub);
        }

        #region RecordAudioTests: ZOMxIxS (ZOMBIES)

        [Test]
        public void RecordAudio_DontCallRecordAudioInSubUponConstruction_IsCalledZeroTimes()
        {
            //ACT
            sub.DidNotReceive().RecordAudio();
        }
        [Test]
        public void RecordAudio_CallRecordAudioMethodInSub_IsCalledOneTime()
        {
            
            //ACT
            UUT.RecordAudio();

            sub.Received(1).RecordAudio();
        }

        [Test]
        public void RecordAudio_CallRecordAudioMethodInSub_IsCalledTwoTimes()
        {
           
            //ACT
            UUT.RecordAudio();
            UUT.RecordAudio();

            sub.Received(2).RecordAudio();
        }

        #endregion

    }
}

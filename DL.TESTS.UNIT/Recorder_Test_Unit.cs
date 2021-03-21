using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using EventArgss;
using NSubstitute;
using NUnit.Framework;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DL.TESTS.UNIT
{
    public class Recorder_Test_Unit
    {
        private Recorder UUT;
        private EventHandler<RecordFinishedEventArgs> RecordFinishedEventHandler;
        private IAudioRecorderService sub_Recoder;
        private ITimeProvider sub_TimeProvider;
        private IFileAccess sub_FileAccess;
        [SetUp]
        public void Setup()
        {
            sub_Recoder = Substitute.For<IAudioRecorderService>();
            sub_TimeProvider = Substitute.For<ITimeProvider>();
            sub_FileAccess = Substitute.For<IFileAccess>();
            UUT = new Recorder(RecordFinishedEventHandler, sub_Recoder, sub_TimeProvider, sub_FileAccess);
        }

        //Disse metoder er testet efter ZOMBIE-princip 

        //Z = Zero
        //O = One
        //M = Multipel
        //B = Boundries
        //I = Interfaces
        //E = Exceptional Behavior


        [Test]
        public void RecordAudio_ZeroCall_ReveivedCallIsZero()
        {
            //Assert
            sub_Recoder.Received(0).StartRecording();
            //Assert.That(DTO.StartTime, Is.Not.Null);
        }
        [Test]
        public void RecordAudio_OneCall_ReveivedCallIsOne()
        {
            //Arrange
            //Measurement DTO;
            //ACT
            UUT.RecordAudio();
            //Assert
            sub_Recoder.Received(1).StartRecording();
            //Assert.That(DTO.StartTime, Is.Not.Null);
        }
        [Test]
        public void RecordAudio_TwoCalls_ReveivedCallIsTwo()
        {
            //ACT
            UUT.RecordAudio();
            UUT.RecordAudio();
            //Assert
            sub_Recoder.Received(2).StartRecording();
            //Assert.That(DTO.StartTime, Is.Not.Null);
        }
    }
}


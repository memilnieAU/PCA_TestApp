﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
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
        private IAudioRecorderService sub_Recorder;
        private ITimeProvider sub_TimeProvider;
        private IFileAccess sub_FileAccess;
        private RecordFinishedEventArgs _recordFinishedEventArgs;
        private DateTime testDateTime;
        [SetUp]
        public void Setup()
        {
            _recordFinishedEventArgs = null;
            sub_Recorder = Substitute.For<IAudioRecorderService>();
            sub_TimeProvider = Substitute.For<ITimeProvider>();
            sub_FileAccess = Substitute.For<IFileAccess>();
            UUT = new Recorder(RecordFinishedEventHandler, sub_Recorder, sub_TimeProvider, sub_FileAccess);
            sub_Recorder.AudioTimeout = 1; //gør tests meget hurtigere
            testDateTime = new DateTime(2012, 12, 31, 16, 00, 0);
            UUT.RecordFinishedEvent += (o, args) =>
            {
                _recordFinishedEventArgs = args;
            };
        }

        //Disse metoder er testet efter ZOMBIE-princip 

        //Z = Zero
        //O = One
        //M = Multipel
        //B = Boundries
        //I = Interfaces
        //E = Exceptional Behavior


        [Test]
        public void RecordAudio_ZeroCall_ReceivedCallIsZero()
        {
            //Assert
            sub_Recorder.Received(0).StartRecording();
            //Assert.That(DTO.StartTime, Is.Not.Null);
        }
        [Test]
        public void RecordAudio_OneCall_ReceivedCallIsOne()
        {
            //Arrange
            //Measurement DTO;
            //ACT
            UUT.RecordAudio();
            //Assert
            sub_Recorder.Received(1).StartRecording();
            //Assert.That(DTO.StartTime, Is.Not.Null);
        }
        [Test]
        public void RecordAudio_TwoCalls_ReceivedCallIsTwoForStartRecordingAndStartTimer()
        {
            //ACT
            UUT.RecordAudio();
            UUT.RecordAudio();
            Assert.Multiple(() =>
            {
                sub_Recorder.Received(2).StartRecording();
                sub_TimeProvider.Received(2).StartTimer();
            });
            //Assert.That(DTO.StartTime, Is.Not.Null);
        }

        [Test] //Tester på at eventet bliver triggered når recording er færdig
        public void HandleRecorderIsFinished_WhenRecordFinished_EventTriggeredAndStopRecordingCalledOnce()
        {
            //ACT
            UUT.HandleRecorderIsFinished(this, "test");
            Assert.Multiple(() =>
                {
                    Assert.That(_recordFinishedEventArgs, Is.Not.Null);
                    sub_TimeProvider.Received(1).StopTimer(true, true);
                });
        }

        [Test] //tester for at der en korrekt DTO med i evntet
        public void HandleRecorderIsFinished_WhenRecordFinished_EventTriggeredWithCorrectDTOStartDateTime()
        {
            //ARRANGE
            sub_TimeProvider.GetDateTime().Returns(testDateTime);

            UUT.HandleRecorderIsFinished(this, "Test");

            Assert.That(_recordFinishedEventArgs.measureDTO.StartTime, Is.EqualTo(testDateTime));
        }
        [Test]//tester for at der er en fake DTO med i event
        public void HandleRecorderIsFinished_WhenRecordFinished_EventTriggeredButWithFalseDTOStartDateTime()
        {
            //ARRANGE
            sub_TimeProvider.GetDateTime().Returns(testDateTime);

            UUT.HandleRecorderIsFinished(this, "Test");
            DateTime falseDateTime = new DateTime(2021, 12, 31, 16, 00, 0);
            Assert.That(_recordFinishedEventArgs.measureDTO.StartTime, Is.Not.EqualTo(falseDateTime));
        }

        [Test] //Tester for at få audiofilestream korrekt
        public void HandleRecorderIsFinished_WhenRecordFinished_EventTriggeredAndGetAudioFileStreamIsCorrect()
        {
            //ARRAGE
            var memorystream = new MemoryStream(100);
            sub_Recorder.GetAudioFileStream().Returns(memorystream);

            UUT.HandleRecorderIsFinished(this, "test");

            Assert.That(_recordFinishedEventArgs.measureDTO.SoundStream, Is.EqualTo(memorystream));

        }
    }
}


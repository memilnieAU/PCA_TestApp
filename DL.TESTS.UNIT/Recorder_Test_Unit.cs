using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using NUnit.Framework;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DL.TESTS.UNIT
{
    public class Recorder_Test_Unit
    {
        private Recorder UUT;
        [SetUp]
        public void Setup()
        {
            UUT = new Recorder();
        }

        //Disse metoder er testet efter ZOMBIE-princip 

        //Z = Zero
        //O = One
        //M = Multipel
        //B = Boundries
        //I = Interfaces
        //E = Exceptional Behavior


        [Test]
        public void RecordAudio_RecordOneMeasurement_DTOReturnedHasStartTime()
        {
            //Arrange
            //Measurement DTO;
            //ACT
            //DTO =  UUT.RecordAudio();
            //Assert
            //Assert.That(DTO.StartTime, Is.Not.Null);
        }
    }
}


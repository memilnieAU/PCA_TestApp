using System;
using System.Collections.Generic;
using System.Text;

namespace P_Layer.DTO
{
    public class SoundSamples
    {
        public List<Sample> SampleList { get; set; }
        public DateTime StartTime { get; set; }
        public int AmountOfSamples { get; set; }
        public int SampleFrekvens { get; set; }

        public SoundSamples(DateTime startTime, int amountOfSamples, int sampleFrekvens)
        {
            AmountOfSamples = amountOfSamples;
            StartTime = startTime;
            SampleList = new List<Sample>();
            SampleFrekvens = sampleFrekvens;
        }

        public SoundSamples()
        {
            SampleList = new List<Sample>();
        }

    }
}

using System;
using System.IO;

namespace DTOs
{
    public class Measurement
    {
        //public List<SoundSamples> SoundSamplesList { get; set; } erstattes af Stream per 10.03.21
        public Stream SoundStream { get; set; }
        public DateTime StartTime { get; set; }
        public int ProbabilityProcent { get; set; }
        public int PatientID { get; set; }
        public int HealthProfessionalID { get; set; }
        public enum PlacementOfDevice { CorDexter, CorSinister, CorInfra}
        private PlacementOfDevice placement;

        public PlacementOfDevice CurrentPlacementOfDevice
        {
            get { return placement;}
            set { placement = value;}
        }

        public Measurement(DateTime start)
        {
            //SoundSamplesList = new List<SoundSamples>();
            StartTime = start;
            //ProbabilityProcent = probProcent;
            //PatientID = patID;
            //HealthProfessionalID = profID;
            //CurrentPlacementOfDevice = placementOfDevice;
        }

        public Measurement()
        {
            //SoundSamplesList = new List<SoundSamples>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace P_Layer.DTO
{
    public class Measurement
    {
        public List<SoundSamples> SoundSamplesList { get; set; }
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

        public Measurement(DateTime start, int probProcent, int patID, int profID)
        {
            SoundSamplesList = new List<SoundSamples>();
            StartTime = start;
            ProbabilityProcent = probProcent;
            PatientID = patID;
            HealthProfessionalID = profID;
            //CurrentPlacementOfDevice = placementOfDevice;
        }

        public Measurement()
        {
            SoundSamplesList = new List<SoundSamples>();
        }
    }
}

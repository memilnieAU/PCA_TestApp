using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_Layer.XML
{
    public class MeasurementXML
    {
        public string name;

        public string weight;

        public string gender;

        public string sensorAmount;

        public MeasurementXML(string _name, string _weight,string _gender, string _sensorAmount)
        {
            name = _name;
            weight = _weight;
            gender = _gender;
            sensorAmount = _sensorAmount;
        }

        public MeasurementXML()
        {
        }
    }
}

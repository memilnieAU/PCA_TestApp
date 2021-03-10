using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace P_Layer.XML
{
    class XMLOperations
    {
        public static void Save(MeasurementXML config, string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);

            XmlSerializer serializer = new XmlSerializer(typeof(MeasurementXML));

            serializer.Serialize(fs, config);

            fs.Close();
        }

        public static MeasurementXML Load(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);

            XmlSerializer serializer = new XmlSerializer(typeof(MeasurementXML));

            MeasurementXML config = (MeasurementXML)serializer.Deserialize(fs);

            fs.Close();

            return config;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using AZ.objectMappings;

namespace AZ
{

    // helper class to ignore namespaces when de-serializing
    public class NamespaceIgnorantXmlTextReader : XmlTextReader
    {
        public NamespaceIgnorantXmlTextReader(StreamReader reader) : base(reader) { }

        public override string NamespaceURI
        {
            get { return ""; }
        }
    }

    public class Reader : IReadable
    {
        private const string Path = "../../xsd/points.xml";
        private XmlSerializer _serializer;
        private StreamReader _reader;

        public void CreateDeserializer()
        {
            _serializer = new XmlSerializer(typeof(Problems));
        }

        public Problems ReadDataFromXml()
        {
            _reader = new StreamReader(Path);
            return (Problems)_serializer.Deserialize(new NamespaceIgnorantXmlTextReader(_reader));
        }

        public void CloseDeSerializer()
        {
            _reader.Close();
        }
    }
}

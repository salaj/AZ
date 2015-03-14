using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using AZ.objectMappings;

namespace AZ
{
    public class Writer : IWriter
    {
        private XmlSerializer _writer;
        private FileStream _file;
        private XmlSerializerNamespaces _serializerNamespaces;
        private const string Path = "../../xsd/result.xml";

        public void CreateSerializer()
        {
            _writer = new XmlSerializer(typeof(Result));
            _file = File.Create(Path);
            _serializerNamespaces = new XmlSerializerNamespaces();
            _serializerNamespaces.Add("", "");
        }

        public void WriteDataToXml(Result result)
        {
            _writer.Serialize(_file, result, _serializerNamespaces);
        }
    }
}

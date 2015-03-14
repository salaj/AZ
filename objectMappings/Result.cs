using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AZ.objectMappings
{
    [Serializable()]
    [System.Xml.Serialization.XmlRoot("Result")]
    public class Result
    {
        [System.Xml.Serialization.XmlElement("Area")]
        public double Area { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AZ.objectMappings
{
    [Serializable()]
    public class Point
    {
        [System.Xml.Serialization.XmlAttribute("x")]
        public float X { get; set; }

        [System.Xml.Serialization.XmlAttribute("y")]
        public float Y { get; set; }

    }
}

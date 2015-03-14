using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AZ.objectMappings
{
    [Serializable()]
    public class Point
    {
        [System.Xml.Serialization.XmlAttribute("x")]
        public int X { get; set; }

        [System.Xml.Serialization.XmlAttribute("y")]
        public int Y { get; set; }

    }
}

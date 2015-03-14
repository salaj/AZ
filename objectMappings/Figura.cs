using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AZ.objectMappings
{
    [Serializable()]
    [System.Xml.Serialization.XmlRoot("Figura")]
    public class Figura
    {
        [XmlArrayItem("Point")]
        public Point[] Points;
    }
}

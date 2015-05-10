using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AZ.objectMappings
{
    [Serializable()]
    [System.Xml.Serialization.XmlRoot("Problems")]
    public class Problems
    {
        [XmlElement("MainProblem")]
        public MainProblem MainProblem { get; set; }

        [XmlElement("AdditionalProblem")]
        public AdditionalProblem AdditionalProblem { get; set; }
    }
}

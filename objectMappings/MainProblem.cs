﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AZ.objectMappings
{
    [Serializable()]
    public class MainProblem
    {
        [XmlArray("FigurePoints")]
        [XmlArrayItem("FigurePoint")]
        public Point[] FigurePoints;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AZ.objectMappings;

namespace AZ
{
    public interface IAreaCalculator
    {
        void SetPolygonPoints(IEnumerable points);
        double CalculatePolygonArea();
    }
}

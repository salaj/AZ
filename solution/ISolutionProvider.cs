using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AZ.objectMappings;

namespace AZ
{
    public interface ISolutionProvider
    {
        void Initialize(Problems problems);
        bool CheckIfPolygonIsSimple();

        bool IsPointInsidePolygon();

        double CalculatePolygonArea();
    }
}

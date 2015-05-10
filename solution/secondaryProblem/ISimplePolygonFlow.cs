using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AZ.objectMappings;

namespace AZ.solution.secondaryProblem
{
    public interface ISimplePolygonFlow
    {
        void SetPolygonPoints(IEnumerable points);
        bool CheckIfPolygonIsSimple();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AZ.solution.secondaryProblem
{
    public interface ISimplePolygonChecker
    {
        bool CheckIfAnyPointsPairOverlap();

        double FindMinimalXPoint();
        double FindMinimalYPoint();

        void TranslateCoordinateSystem(Geometry.Point minimalPoint);

        void SortPointsByNonDecreasingX();

        bool CheckIfAnyTwoEdgesIntersects();

        void SetSegments();
    }
}

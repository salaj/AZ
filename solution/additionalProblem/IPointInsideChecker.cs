using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AZ.objectMappings;

namespace AZ.solution.additionalProblem
{
    public interface IPointInsideChecker
    {
        void SetPolygonPoints(IEnumerable points);

        bool IsPointInsidePolygon(Point point);

        bool IsIntersectionInExactlyOneVertex(Geometry.Segment s1, Geometry.Segment s2);

        bool AreSegmentsOverlapping(Geometry.Segment s1, Geometry.Segment s2);

        Intersection CheckIntersections(Geometry.Segment s1, Geometry.Segment s2);
    }
}

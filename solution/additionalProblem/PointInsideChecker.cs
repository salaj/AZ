using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AZ.objectMappings;

namespace AZ.solution.additionalProblem
{
    public enum Intersection
    {
        InOnePointButNotVertex,
        InOnePointExacltyVertex,
        Overlapping,
        NoIntersection
    }
    public class PointInsideChecker : IPointInsideChecker
    {
        private readonly List<Geometry.Point> PolygonPoint = new List<Geometry.Point>();
        public void SetPolygonPoints(IEnumerable points)
        {
            Geometry.Point previous;
            foreach (Point point in points)
            {
                var p = new Geometry.Point(point.X, point.Y);
                PolygonPoint.Add(p);
            }
        }

        public bool IsPointInsidePolygon(Point point)
        {
            var R = new Geometry.Point(point.X, point.Y);
            int n = PolygonPoint.Count;
            int M = 0;
            //półprosta
            var k = new Geometry.Segment(R, new Geometry.Point(double.MaxValue, R.y));
            //prosta
            var l = new Geometry.Segment(new Geometry.Point(double.MinValue, R.y),
                new Geometry.Point(double.MaxValue, R.y));
            IDictionary<int, Geometry.Point> L = new Dictionary<int, Geometry.Point>();
            for(int i = 0; i < PolygonPoint.Count; i++)
            {
                L.Add(new KeyValuePair<int, Geometry.Point>(i, PolygonPoint[i]));
            }
            L.Add(new KeyValuePair<int, Geometry.Point>(-2, L[n-2]));
            L.Add(new KeyValuePair<int, Geometry.Point>(-1, L[n-1]));
            L.Add(new KeyValuePair<int, Geometry.Point>(n, L[0]));
            L.Add(new KeyValuePair<int, Geometry.Point>(n + 1, L[1]));

            for (int i = 0; i < n; i++)
            {
                var e = new Geometry.Segment(L[i], L[i+1]);
                switch (CheckIntersections(e, k))
                {
                    case Intersection.InOnePointButNotVertex:
                        M++;
                    break;
                    case Intersection.InOnePointExacltyVertex:
                        if (i == 0 || !Geometry.Intersection(new Geometry.Segment(L[i], L[i + 2]), l))
                        {
                            continue;
                        }
                        else
                        {
                            M++;
                            i++;
                        }
                        break;
                    case Intersection.Overlapping:
                        if (!Geometry.Intersection(new Geometry.Segment(L[i - 1], L[i + 2]), l))
                        {
                            continue;
                        }
                        else
                        {
                            M++;
                        }
                        break;
                }
            }
            return M % 2 == 0 ? false : true;
        }

        public bool IsIntersectionInExactlyOneVertex(Geometry.Segment s1, Geometry.Segment s2)
        {
            if (s1.ps == s2.ps || s1.ps == s2.pe || s1.pe == s2.ps || s1.pe == s2.pe)
                return true;
            return false;
        }

        public bool AreSegmentsOverlapping(Geometry.Segment s1, Geometry.Segment s2)
        {
            return Geometry.Overlapping(s1, s2);
        }

        public Intersection CheckIntersections(Geometry.Segment s1, Geometry.Segment s2)
        {
            if (Geometry.Intersection(s1, s2))
            {
                if (AreSegmentsOverlapping(s1, s2))
                    return Intersection.Overlapping;
                if (IsIntersectionInExactlyOneVertex(s1, s2))
                    return Intersection.InOnePointExacltyVertex;
                else
                {
                    return Intersection.InOnePointButNotVertex;
                }
            }
            else
            {
                return Intersection.NoIntersection;
            }
        }
    }
}

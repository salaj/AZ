using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AZ.objectMappings;

namespace AZ.solution.secondaryProblem
{
    public class SimplePolygonChecker : ISimplePolygonChecker, ISimplePolygonFlow
    {
        private readonly List<Geometry.Point> PolygonPoint = new List<Geometry.Point>();
        private readonly  Dictionary<Geometry.Point, Geometry.Segment> Segments = new Dictionary<Geometry.Point, Geometry.Segment>(); 
        public void SetPolygonPoints(IEnumerable points)
        {
            Geometry.Point previous;
            foreach (Point point in points)
            {
                var p = new Geometry.Point(point.X, point.Y);
                PolygonPoint.Add(p);
            }
        }

        public bool CheckIfPolygonIsSimple()
        {
            if (CheckIfAnyPointsPairOverlap())
                return false;
            Geometry.Point minimal = FindMinimalXYPoint();
            TranslateCoordinateSystem(minimal);
            SetSegments();
            SortPointsByNonDecreasingX();
            if (CheckIfAnyTwoEdgesIntersects())
                return false;
            return true;
        }

        public bool CheckIfAnyPointsPairOverlap()
        {
            var points = new Dictionary<Geometry.Point, int>();
            foreach (Geometry.Point point in PolygonPoint)
            {
                if (!points.ContainsKey(point))
                {
                    points.Add(point, 0);
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public Geometry.Point FindMinimalXYPoint()
        {
            var minimal = new Geometry.Point(Double.MaxValue, Double.MaxValue);
            foreach (Geometry.Point point in PolygonPoint)
            {
                if (minimal.x > point.x)
                {
                    minimal = point;
                } else if (minimal.x == point.x && minimal.y > point.y)
                {
                    minimal = point;
                }
            }
            return minimal;
        }

        public void TranslateCoordinateSystem(Geometry.Point minimalPoint)
        {
            for(int i = 0; i < PolygonPoint.Count; i++)
            {
                PolygonPoint[i] = PolygonPoint[i] - minimalPoint;
            }
        }

        public void SortPointsByNonDecreasingX()
        {
            PolygonPoint.Sort((point, point1) =>
            {
                if (point.x < point1.x)
                    return -1;
                return 1;
            });
        }

        public bool CheckIfAnyTwoEdgesIntersects()
        {
            var T = new Tree();
            int n = PolygonPoint.Count;
            for (int i = 0; i < n; i++)
            {
                Geometry.Point p = PolygonPoint[i];
                Geometry.Segment s = Segments[p];
                double minX = Math.Min(s.ps.x, s.pe.x);
                if (minX == p.x)
                {
                    T.Nodes.Add(s, new Node(s));
                    Geometry.Segment s_previous = T.GetPrevious(s);
                    Geometry.Segment s_next = T.GetNext(s);
                    if (s_previous != null && Geometry.Intersection(s_previous, s))
                        return true;
                    if (s_next != null && Geometry.Intersection(s_next, s))
                        return true;
                }
                else
                {
                    Geometry.Segment s_previous = T.GetPrevious(s);
                    Geometry.Segment s_next = T.GetNext(s);
                    if (s_previous != null  && s_next != null && Geometry.Intersection(s_previous, s_next))
                        return true;
                }

            }
            return false;
        }

        public void SetSegments()
        {
            for (int i = 0; i < PolygonPoint.Count; i++)
            {
                var p = new Geometry.Point(PolygonPoint[i].x, PolygonPoint[i].y);
                if (i % 2 == 0)
                {
                    var pNext = new Geometry.Point(PolygonPoint[i + 1].x, PolygonPoint[i + 1].y);
                    var s = new Geometry.Segment(p, pNext);
                    Segments.Add(p, s);
                }
                else
                {
                    var pPrev = new Geometry.Point(PolygonPoint[i - 1].x, PolygonPoint[i - 1].y);
                    Geometry.Segment s = Segments[pPrev];
                    Segments.Add(p, s);
                }
            }
        }
    }
}

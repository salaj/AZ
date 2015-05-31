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
        private readonly Dictionary<Geometry.Point, Geometry.Segment> Segments = new Dictionary<Geometry.Point, Geometry.Segment>();
        private List<Geometry.Segment> SegmentsList = new List<Geometry.Segment>();
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
            SetSegments();
            bool result = true;
            for (int i = 0; i < SegmentsList.Count; i++ )
            {
                for(int j = i + 1 ; j < SegmentsList.Count; j++)
                {
                    if (CheckCrossing(SegmentsList[i], SegmentsList[j]))
                        result = false;
                    if (!result)
                        return result;
                }
            }
            return result;

                if (CheckIfAnyPointsPairOverlap())
                    return false;
            Geometry.Point minimal = new Geometry.Point(FindMinimalXPoint(), FindMinimalYPoint());
            TranslateCoordinateSystem(minimal);
            SetSegments();
            SortPointsByNonDecreasingX();
            if (CheckIfAnyTwoEdgesIntersects())
                return false;
            return true;
        }

        public bool CheckCrossing(Geometry.Segment s1, Geometry.Segment s2)
        {
            if (s1.ps.Equals(s2.ps) || s1.ps.Equals(s2.pe) || s1.pe.Equals(s2.ps) || s1.pe.Equals(s2.pe))
                return false;
            return Geometry.Intersection(s1, s2);
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

        public double FindMinimalXPoint()
        {
            double minimal = Double.MaxValue;
            foreach (Geometry.Point point in PolygonPoint)
            {
                if (minimal > point.x)
                {
                    minimal = point.x;
                }
            }
            return minimal;
        }

        public double FindMinimalYPoint()
        {
            double minimal = Double.MaxValue;
            foreach (Geometry.Point point in PolygonPoint)
            {
                if (minimal > point.y)
                {
                    minimal = point.y;
                }
            }
            return minimal;
        }

        public void TranslateCoordinateSystem(Geometry.Point minimalPoint)
        {
            for (int i = 0; i < PolygonPoint.Count; i++)
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
                    T.Nodes.Add(new Geometry.Segment(s.ps, s.pe), new Node(s));
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
                    if (s_previous != null && s_next != null && Geometry.Intersection(s_previous, s_next))
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
                //if (i % 2 == 0)
                {
                    var pNext = new Geometry.Point(PolygonPoint[(i + 1) % PolygonPoint.Count].x, PolygonPoint[(i + 1) % PolygonPoint.Count].y);
                    var s = new Geometry.Segment(p, pNext);
                    Segments.Add(p, s);
                    SegmentsList.Add(s);
                }
               // else
                //{
                //    if (i > 0)
                //    {
                //        var pPrev = new Geometry.Point(PolygonPoint[i - 1].x, PolygonPoint[i - 1].y);
                //        Geometry.Segment s = Segments[pPrev];
                //        Segments.Add(p, s);
                //    }
                //    //else
                //    //{
                //    //}
                //}
            }
            //var pPrev2 = new Geometry.Point(PolygonPoint[PolygonPoint.Count - 1].x, PolygonPoint[PolygonPoint.Count - 1].y);
            //Geometry.Segment s2 = Segments[pPrev2];
            //Segments.Add(new Geometry.Point(PolygonPoint[0].x, PolygonPoint[0].y), s2);
        }
    }
}

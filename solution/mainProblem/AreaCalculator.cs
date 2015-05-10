using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AZ.objectMappings;
using AZ.solution;

namespace AZ
{
    public class AreaCalculator : IAreaCalculator
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


        public double CalculatePolygonArea()
        {
            int n = PolygonPoint.Count;
            double sum = 0;
            for (int i = 0; i < n; i++)
            {
                Geometry.Point currentPoint = PolygonPoint[i];
                Geometry.Point nextPoint = i == n - 1 ? PolygonPoint[0] :
                    PolygonPoint[i + 1];
                Geometry.Point previousPoint = i == 0 ? PolygonPoint[n - 1]
                    : PolygonPoint[i - 1];

                sum += (currentPoint.x * (nextPoint.y - previousPoint.y));
            }
            sum = Math.Abs(sum);

            sum *= 0.5f;

            return sum;
        }
    }
}

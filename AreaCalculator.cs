using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AZ.objectMappings;

namespace AZ
{
    public class AreaCalculator : IAreaCalculator
    {
        public Result Calculate(Figura figura)
        {
            int n = figura.Points.Length;
            double sum = 0;
            for (int i = 0; i < n; i++)
            {
                Point currentPoint = figura.Points[i];
                Point nextPoint = i == n - 1 ? figura.Points[0] :
                    figura.Points[i + 1];
                Point previousPoint = i == 0 ? figura.Points[n - 1]
                    : figura.Points[i - 1];

                sum += (currentPoint.X * (nextPoint.Y - previousPoint.Y));
            }
            sum = Math.Abs(sum);

            sum *= 0.5f; 

            return new Result { Area = sum };
        }
    }
}

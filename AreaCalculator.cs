using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AZ.objectMappings;

namespace AZ
{
    public class AreaCalculator : IAreaCalculator
    {
        public Result Calculate(MainProblem figura)
        {
            int n = figura.FigurePoints.Length;
            double sum = 0;
            for (int i = 0; i < n; i++)
            {
                Point currentPoint = figura.FigurePoints[i];
                Point nextPoint = i == n - 1 ? figura.FigurePoints[0] :
                    figura.FigurePoints[i + 1];
                Point previousPoint = i == 0 ? figura.FigurePoints[n - 1]
                    : figura.FigurePoints[i - 1];

                sum += (currentPoint.X * (nextPoint.Y - previousPoint.Y));
            }
            sum = Math.Abs(sum);

            sum *= 0.5f; 

            return new Result { Area = sum };
        }
    }
}

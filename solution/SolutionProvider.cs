using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AZ.objectMappings;
using AZ.solution.additionalProblem;
using AZ.solution.secondaryProblem;

namespace AZ
{
    public class SolutionProvider : ISolutionProvider
    {
        private readonly ISimplePolygonFlow simplePolygonChecker = new SimplePolygonChecker();
        private readonly IAreaCalculator areaCalculator = new AreaCalculator();
        private readonly IPointInsideChecker pointInsideChecker = new PointInsideChecker();
        private MainProblem mainProblem;
        private AdditionalProblem additionalProblem;
        public void Initialize(Problems problems)
        {
            mainProblem = problems.MainProblem;
            additionalProblem = problems.AdditionalProblem;
            areaCalculator.SetPolygonPoints(mainProblem.FigurePoints);
            simplePolygonChecker.SetPolygonPoints(mainProblem.FigurePoints);
            pointInsideChecker.SetPolygonPoints(mainProblem.FigurePoints);
        }

        public bool CheckIfPolygonIsSimple()
        {
            return simplePolygonChecker.CheckIfPolygonIsSimple();
        }

        public bool IsPointInsidePolygon()
        {
            return pointInsideChecker.IsPointInsidePolygon(additionalProblem.PointToCheck);
        }

        public double CalculatePolygonArea()
        {
            return areaCalculator.CalculatePolygonArea();
        }
    }
}

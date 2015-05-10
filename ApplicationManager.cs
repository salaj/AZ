using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AZ.objectMappings;
using AZ.solution.additionalProblem;
using AZ.solution.secondaryProblem;

namespace AZ
{
    public class ApplicationManager : IApplicationFlow
    {
        public void onApplicationStarted()
        {
            IReadable readable = new Reader();
            readable.CreateDeserializer();
            onApplicationInitialized(readable);
        }

        public void onApplicationInitialized(IReadable readable)
        {
            Problems problems = readable.ReadDataFromXml();
            var solutionProvider = new SolutionProvider();
            solutionProvider.Initialize(problems);
            bool isPolygonSimple = solutionProvider.CheckIfPolygonIsSimple();
            Result result;
            if (isPolygonSimple)
            {
                result = new Result
                {
                    Area = solutionProvider.CalculatePolygonArea(),
                    IsSimplePolygon = true,
                    IsPointInside = solutionProvider.IsPointInsidePolygon()
                };
            }
            else
            {
                result = new Result
                {
                    Area = -1,
                    IsSimplePolygon = false,
                    IsPointInside = false
                };
            }
            //
            onApplicationFinished(readable, result);
        }

        public void onApplicationFinished(IReadable readable, Result result)
        {
            IWriter writer = new Writer();
            writer.CreateSerializer();
            writer.WriteDataToXml(result);
            readable.CloseDeSerializer();
        }
    }
}

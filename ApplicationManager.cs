using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AZ.objectMappings;

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
            MainProblem mainProblem = problems.MainProblem;
            //HERE ALGORITHM
            var areaCalculator = new AreaCalculator();
            var result = areaCalculator.Calculate(mainProblem);
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

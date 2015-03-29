using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AZ.objectMappings;

namespace AZ
{
    public interface IAreaCalculator
    {
        Result Calculate(MainProblem figura);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AZ.objectMappings;

namespace AZ
{
    public interface IApplicationFlow
    {
        void onApplicationStarted();
        void onApplicationInitialized(IReadable readable);
        void onApplicationFinished(IReadable readable, Result resul);
    }
}

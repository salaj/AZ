using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AZ.objectMappings;

namespace AZ
{
    public interface IReadable
    {
        void CreateDeserializer();
        Figura ReadDataFromXml();
        void CloseDeSerializer();
    }
}

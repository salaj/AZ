using System;
using System.IO;
using System.Xml.Serialization;

namespace AZ
{
    public class Program
    {
        private static void Main(string[] args)
        {
            IApplicationFlow applicationManager = new ApplicationManager();
            applicationManager.onApplicationStarted();
        }
    }
}

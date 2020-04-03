using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Configuration;
using TargetSiteInteractor.Utilities;
using TargetSiteInteractor.Models;
using System.Reflection;

namespace CloseTargetSite
    {
    class CloseTargetSite
        {
        static void Main(string[] args)
        {
            string browserComparisonEnabled = ConfigurationManager.AppSettings["browserComparisonEnabled"];
            string browserNameComparison = ConfigurationManager.AppSettings["browserNameComparison"];
            string propertiesFileName = ConfigurationManager.AppSettings["propertiesFileName"];

            // Get current directory 
            //string currentDirectory = Directory.GetCurrentDirectory();
            string currentDirectory = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            // Read properties from file
            string propertiiesFilePath = Path.Combine(currentDirectory, propertiesFileName);
            ProcessProperties processProperties = FileManager.ReadPropertiesFromFile(propertiiesFilePath);

            // Get process information back and validate it
            Process browserProcess = Process.GetProcessById(processProperties.processId);
            if (browserComparisonEnabled.Equals("true"))
            {
                // Validate by checking if the process name is the same as the one found earlier
                if (browserNameComparison.Equals(processProperties.processName))
                {
                    browserProcess.Kill(true);
                    Console.WriteLine("Killed process: " + processProperties.processName);
                }
            }
            else
            {
                browserProcess.Kill(true);
                Console.WriteLine("Killed process: " + processProperties.processName);
            }


        }
    }
}

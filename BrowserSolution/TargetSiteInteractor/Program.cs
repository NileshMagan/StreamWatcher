using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Configuration;
using TargetSiteInteractor.Utilities;
using TargetSiteInteractor.Models;

namespace TargetSiteInteractor
{
    class TargetSiteInteractor
    {
        static void Main(string[] args)
        {
            // Extract constants from app.config
            string targetUrl = ConfigurationManager.AppSettings["targetUrl"];
            string browserLocation = ConfigurationManager.AppSettings["browserLocation"];
            string browserComparisonEnabled = ConfigurationManager.AppSettings["browserComparisonEnabled"];
            string browserNameComparison = ConfigurationManager.AppSettings["browserNameComparison"];
            string propertiesFileName = ConfigurationManager.AppSettings["propertiesFileName"];

            // Get path to chromium
            string currentDirectory = Directory.GetCurrentDirectory();
            string chromiumDirectory = Path.Combine(currentDirectory, browserLocation);

            // Create file information
            string propertiiesFilePath = Path.Combine(currentDirectory, propertiesFileName);
            FileManager.CreateFile(propertiiesFilePath);

            // Launch target url
            Console.WriteLine("Launching myDahlia's stream");
            Process process = OpenUrl(chromiumDirectory, targetUrl);

            // Store process information
            //var browserProcessId = process.Id;
            //var browserProcessName = process.ProcessName;
            ProcessProperties propertiesToStore = new ProcessProperties {
                processId = process.Id,
                processName = process.ProcessName
            };

            FileManager.WritePropertiesIntoFile(propertiesToStore, propertiiesFilePath);

            while (true)
            {
                ConsoleKeyInfo result = Console.ReadKey(true);
                if (result.Key == ConsoleKey.Escape) break;
                //if (result.KeyChar == 'I' || result.KeyChar == 'i')
                //{
                //    Console.WriteLine("ID number: " + browserProcessId);
                //}
                //if (result.KeyChar == 'K' || result.KeyChar == 'k')
                {
                    // Read properties from file
                    // Create file information
                    string propertiiesFilePath2 = Path.Combine(currentDirectory, propertiesFileName);
                    ProcessProperties processProperties = FileManager.ReadPropertiesFromFile(propertiiesFilePath2);

                    // Get process information back and validate it
                    //Process p = Process.GetProcessById(browserProcessId);
                    Process browserProcess = Process.GetProcessById(processProperties.processId);
                    if (browserComparisonEnabled.Equals("true"))
                    {
                        // Validate by checking if the process name is the same as the one found earlier
                        //if (browserNameComparison.Equals(browserProcessName))
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

        private static Process OpenUrl(string executablePath, string targetUrl)
        {
            try
            {
                return Process.Start(executablePath, targetUrl);
            }
            catch (Exception e)
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    targetUrl = targetUrl.Replace("&", "^&");
                    return Process.Start(new ProcessStartInfo("cmd", $"/c start {targetUrl}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    return Process.Start("xdg-open", targetUrl);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    return Process.Start("open", targetUrl);
                }
                else
                {
                    throw;
                }
            }
            return null;
        }

    }
}

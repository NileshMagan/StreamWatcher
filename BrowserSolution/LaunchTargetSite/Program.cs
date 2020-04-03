using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Configuration;
using TargetSiteInteractor.Utilities;
using TargetSiteInteractor.Models;
using System.Reflection;

namespace LaunchTargetSite
    {
    class Program
        {
        static void Main(string[] args)
        {
            // Extract constants from app.config
            string targetUrl = ConfigurationManager.AppSettings["targetUrl"];
            string browserLocation = ConfigurationManager.AppSettings["browserLocation"];
            string propertiesFileName = ConfigurationManager.AppSettings["propertiesFileName"];

            // Get path to chromium
            //string currentDirectory = Directory.GetCurrentDirectory();
            string currentDirectory = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);


            string chromiumDirectory = Path.Combine(currentDirectory, browserLocation);

            // Create file information
            string propertiiesFilePath = Path.Combine(currentDirectory, propertiesFileName);

            Console.WriteLine("targetUrl :" + targetUrl);
            Console.WriteLine("browserLocation :" + browserLocation);
            Console.WriteLine("propertiesFileName :" + propertiesFileName);
            Console.WriteLine("currentDirectory :" + currentDirectory);
            Console.WriteLine("chromiumDirectory: " + chromiumDirectory);
            Console.WriteLine("propertiiesFilePath: " + propertiiesFilePath);

            FileManager.CreateFile(propertiiesFilePath);

            // Launch target url
            Console.WriteLine("Launching myDahlia's stream");
            Process process = OpenUrl(chromiumDirectory, targetUrl);

            // Store process information
            ProcessProperties propertiesToStore = new ProcessProperties
            {
                processId = process.Id,
                processName = process.ProcessName
            };

            FileManager.WritePropertiesIntoFile(propertiesToStore, propertiiesFilePath);
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

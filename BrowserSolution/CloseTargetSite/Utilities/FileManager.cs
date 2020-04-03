using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TargetSiteInteractor.Models;

namespace TargetSiteInteractor.Utilities
{
    static class FileManager
    {
        static public void CreateFile(string propertiiesFilePath)
        {
            if (File.Exists(propertiiesFilePath)) { File.Delete(propertiiesFilePath); }
                        
            var fileStream = File.Create(propertiiesFilePath);
            fileStream.Close();
        }

        static public void WritePropertiesIntoFile(ProcessProperties properties, string path)
        {
            string content = JsonConvert.SerializeObject(properties);
            File.AppendAllText(path, content);
        }

        static public ProcessProperties ReadPropertiesFromFile(string path)
        {
            string content = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<ProcessProperties>(content);
        }
    }
}

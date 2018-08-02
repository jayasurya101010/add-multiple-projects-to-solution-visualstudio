using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using AddExistingProjectsToSolution.Workers;

namespace AddExistingProjectsToSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleOutputWorker.OutputSolutionFilePath();
            string solutionFilePath = Console.ReadLine();
            ConsoleOutputWorker.OutputProjectPath();
            List<string> projectDirectories = Console.ReadLine().Split(',').ToList();

            if (string.IsNullOrEmpty(solutionFilePath) || projectDirectories.Count == 0)
            {
                ConsoleOutputWorker.OutputPathsEmptyStatements();
                if (Console.ReadLine().ToLower() == "c")
                {
                    Main(args);
                }
                else
                    Environment.Exit(0);
            }           

            bool isAppend = false;
            ConsoleOutputWorker.OutputIsAppendStatements();
            isAppend = Console.ReadLine().ToLower() == "a" ? true : false;
            AddCurrentSolutionDirectory(projectDirectories, solutionFilePath);
            Console.WriteLine("Microsoft Visual Studio Solution File, Format Version 12.00");

            using (var writer = new StreamWriter(solutionFilePath, isAppend, Encoding.UTF8))
            {
                var seenElements = new HashSet<string>();
                foreach (var directoryPath in projectDirectories.Distinct())
                {
                    ConsoleOutputWorker.OutputStatement($"Going through directory - {directoryPath}");
                    
                    DirectoryInfo dirInfo = (new DirectoryInfo(directoryPath));
                    FileInfo[] fileInfo = dirInfo.GetFiles("*.csproj", SearchOption.AllDirectories);
                    foreach (var file in fileInfo)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(file.Name);
                        if (seenElements.Add(fileName))
                        {
                            var guid = ReadGuid(file.FullName);
                            ConsoleOutputWorker.OutputStatement($"Adding project {fileName} to the solution file");
                            writer.WriteLine(string.Format(@"Project(""0"") = ""{0}"", ""{1}"",""{2}""", fileName, file.FullName, guid));
                            writer.WriteLine("EndProject");
                        }
                    }
                }
            }
            ConsoleOutputWorker.OutputCompletedStatements(solutionFilePath);
        }
        static Guid ReadGuid(string fileName)
        {
            using (var file = File.OpenRead(fileName))
            {
                var elements = XElement.Load(XmlReader.Create(file));
                return Guid.Parse(elements.Descendants().First(element => element.Name.LocalName == "ProjectGuid").Value);
            }
        }

        static void AddCurrentSolutionDirectory(List<string> projectDirectories, string fullDirectoryPath)
        {
            string currentSolutionDirectory = GetCurrentSolutionDirectory(fullDirectoryPath, "\\") == string.Empty ? GetCurrentSolutionDirectory(fullDirectoryPath, "/") : GetCurrentSolutionDirectory(fullDirectoryPath, "\\");
            if (!string.IsNullOrEmpty(currentSolutionDirectory) && !projectDirectories.Contains(currentSolutionDirectory))
                projectDirectories.Add(currentSolutionDirectory);
        }

        static string GetCurrentSolutionDirectory(string fullDirectoryPath, string character = "//")
        {
            if (!string.IsNullOrWhiteSpace(fullDirectoryPath))
            {
                int charLocation = fullDirectoryPath.LastIndexOf(character, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return fullDirectoryPath.Substring(0, charLocation);
                }
            }
            return string.Empty;
        }
    }
}

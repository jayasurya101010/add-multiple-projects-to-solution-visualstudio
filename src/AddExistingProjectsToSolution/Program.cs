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
            string[] projectDirectories = Console.ReadLine().Split(',');

            if (string.IsNullOrEmpty(solutionFilePath) || projectDirectories.Count() == 0)
            {
                ConsoleOutputWorker.OutputPathsEmptyStatements();
                if (Console.ReadLine().ToLower() == "c")
                {
                    Main(args);
                }
                else
                    Environment.Exit(0);
            }

            using (var writer = new StreamWriter(solutionFilePath, true, Encoding.UTF8))
            {
                ConsoleOutputWorker.OutputVisualStudioVersionSelection();
                int visualStudioVersion = Convert.ToInt32(Console.ReadLine());
                VisualStudioVersionWorker.WriteVisualStudioVersionInformation(visualStudioVersion, writer);

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
    }
}

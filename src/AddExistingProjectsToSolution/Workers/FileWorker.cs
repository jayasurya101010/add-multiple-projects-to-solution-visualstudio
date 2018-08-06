using AddExistingProjectsToSolution.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using AddExistingProjectsToSolution.Extensions;

namespace AddExistingProjectsToSolution.Workers
{
    public class FileWorker
    {
        /// <summary>
        /// Writes each project information to the solution file
        /// </summary>
        /// <param name="solutionFilePath"></param>
        /// <param name="projectDirectories"></param>
        /// <param name="isAppend"></param>
        /// <returns></returns>
        public static bool CreateSolutionFile(string solutionFilePath, List<string> projectDirectories, bool isAppend)
        {
            try
            {
                using (var streamWriter = new StreamWriter(solutionFilePath, isAppend, Encoding.UTF8))
                {
                    VisualStudioVersionWorker.WriteVisualStudioVersionInformation(isAppend, solutionFilePath, streamWriter);

                    var seenElements = new HashSet<string>();
                    foreach (var directoryPath in projectDirectories.Distinct())
                    {
                        /* Check if projects folder path is valid. If not valid, go to the next folder. */
                        if (!ValidationHelper.ValidateProperFilePath(directoryPath))
                        {
                            ConsoleOutputWorker.OutputInvalidProjectsFolderPath(directoryPath);
                            continue;
                        }

                        ConsoleOutputWorker.OutputStatement($"Going through directory - {directoryPath}");

                        DirectoryInfo dirInfo = (new DirectoryInfo(directoryPath));
                        FileInfo[] fileInfo = dirInfo.GetFiles("*.csproj", SearchOption.AllDirectories);                       

                        /* Checks if there are valid projects at the specified location */
                        ValidationHelper.ValidateFileInfo(fileInfo, directoryPath);

                        foreach (var file in fileInfo)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(file.Name);
                            if (seenElements.Add(fileName))
                            {
                                var guid = ReadGuid(file.FullName);
                                ConsoleOutputWorker.OutputStatement($"Adding project {fileName} to the solution file");
                                streamWriter.WriteLine(string.Format(@"Project(""0"") = ""{0}"", ""{1}"",""{2}""", fileName, file.FullName, guid));
                                streamWriter.WriteLine("EndProject");
                            }
                        }
                    }
                }

                ConsoleOutputWorker.OutputCompletedStatements(solutionFilePath);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

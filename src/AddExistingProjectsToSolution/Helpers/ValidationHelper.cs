using AddExistingProjectsToSolution.Workers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddExistingProjectsToSolution.Helpers
{
    public class ValidationHelper
    {
        /// <summary>
        /// Checks if entered paths are empty.
        /// </summary>
        /// <param name="solutionFilePath"></param>
        /// <param name="projectDirectories"></param>
        /// <returns></returns>
        public static void ValidateEmptyInputs(string solutionFilePath, List<string> projectDirectories, string[] args)
        {
            if (string.IsNullOrEmpty(solutionFilePath) || projectDirectories.Count == 0)
            {
                ConsoleOutputWorker.OutputPathsEmptyStatements();
                if (Console.ReadLine().ToLower() == "c")
                    Program.Main(args);
                else
                    Environment.Exit(0);
            }
        }

        /// <summary>
        /// Checks if the provided path is a valid local path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool ValidateProperFilePath(string filePath)
        {
            try
            {
                return new Uri(filePath).IsFile;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if there are valid projects at the specified location
        /// </summary>
        /// <param name="fileInfo"></param>
        public static void ValidateFileInfo(FileInfo[] fileInfo, string directoryPath)
        {
            if (fileInfo.Count() == 0)
            {
                ConsoleOutputWorker.OutputNoProjectsStatements(directoryPath);
            }
        }
    }
}

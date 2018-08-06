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
using AddExistingProjectsToSolution.Extensions;
using AddExistingProjectsToSolution.Helpers;

namespace AddExistingProjectsToSolution
{
    class Program
    {
        public static void Main(string[] args)
        {
            ConsoleOutputWorker.OutputSolutionFilePath();
            string solutionFilePath = Console.ReadLine();

            ConsoleOutputWorker.OutputProjectPath();
            List<string> projectDirectories = Console.ReadLine().Split(',').ToList();

            /* Check if entered paths are empty. */
            ValidationHelper.ValidateEmptyInputs(solutionFilePath, projectDirectories, args);            

            /* Check whether to append the projects to existing solution file or to create a new solution file. */
            bool isAppend = FileHelper.CheckIfAppend();

            /* Check if entered solution file path is valid. If not valid, create the solution path in "temp" folder */
            if (!ValidationHelper.ValidateProperFilePath(solutionFilePath))
                solutionFilePath = FileHelper.GetTempSolutionPath();

            /* Include any projects inside solution's own directory. */
            DirectoryHelper.AddCurrentSolutionDirectory(projectDirectories, solutionFilePath);

            /* Create solution file with all the projects at the specified location */
            FileWorker.CreateSolutionFile(solutionFilePath, projectDirectories, isAppend);
        }
    }
}

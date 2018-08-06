using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddExistingProjectsToSolution.Helpers
{
    public class DirectoryHelper
    {
        /// <summary>
        /// Adds directory of solution path to the project directories. This will add any projects that are present in project directory to the solution file.
        /// </summary>
        /// <param name="projectDirectories"></param>
        /// <param name="fullDirectoryPath"></param>
        public static void AddCurrentSolutionDirectory(List<string> projectDirectories, string fullDirectoryPath)
        {
            string currentSolutionDirectory = GetCurrentSolutionDirectory(fullDirectoryPath, "\\") == string.Empty ? GetCurrentSolutionDirectory(fullDirectoryPath, "/") : GetCurrentSolutionDirectory(fullDirectoryPath, "\\");
            if (!string.IsNullOrEmpty(currentSolutionDirectory) && !projectDirectories.Contains(currentSolutionDirectory))
                projectDirectories.Add(currentSolutionDirectory);
        }

        /// <summary>
        /// Gets the directory of the solution file.
        /// </summary>
        /// <param name="fullDirectoryPath"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        private static string GetCurrentSolutionDirectory(string fullDirectoryPath, string character = "//")
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

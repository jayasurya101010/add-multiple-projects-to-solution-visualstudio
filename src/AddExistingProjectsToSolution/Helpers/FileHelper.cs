using AddExistingProjectsToSolution.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddExistingProjectsToSolution.Helpers
{
    class FileHelper
    {
        public static bool CheckIfAppend()
        {
            bool isAppend = false;
            ConsoleOutputWorker.OutputIsAppendStatements();
            isAppend = Console.ReadLine().ToLower() == "a" ? true : false;
            return isAppend;
        }

        public static string GetTempSolutionPath()
        {
            return "C:\\Temp\\AutoSolutionFile.sln";
        }
    }
}

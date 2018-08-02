using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddExistingProjectsToSolution.Workers
{
    class ConsoleOutputWorker
    {
        /// <summary>
        /// Asks the user to select the intended Visual Studio version
        /// </summary>
        public static void OutputVisualStudioVersionSelection()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Select Version of Visual Studio that the solution is expected to open with");
            Console.WriteLine("1 - Visual Studio 10");
            Console.WriteLine("2 - Visual Studio 12");
            Console.WriteLine("3 - Visual Studio 13");
            Console.WriteLine("4 - Visual Studio 15");
            Console.WriteLine("5 - Visual Studio 17");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Example : Enter 4 for Visual Studio 15");
            Console.WriteLine("Version - ");
            Console.ResetColor();
        }

        /// <summary>
        /// Asks the user to input the desired solution file path
        /// </summary>
        public static void OutputSolutionFilePath()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Enter the solution file path in full. Do not use existing file name as tool will create a new file and overrides existing file if any.");
            Console.WriteLine();
            Console.WriteLine("Example - C:\\Users\\User\\Documents\\Visual Studio 2017\\Projects\\MyApplication.sln");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Solution Path - ");
            Console.ResetColor();
        }

        /// <summary>
        /// Asks the user to input the projects location
        /// </summary>
        public static void OutputProjectPath()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Enter the path of the folder containing the projects to be added. In case of projects in multiple folders, separate each folder path by a comma.");
            Console.WriteLine();
            Console.WriteLine("Example - C:\\CoreLibraries\\, C:\\DataLibraries");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Project folder/folders Path - ");
            Console.ResetColor();
        }

        public static void OutputCompletedStatements(string solutionFilePath)
        {
            Console.WriteLine($"Created solution file at {solutionFilePath}");
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }

        public static void OutputPathsEmptyStatements()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Solution file path and project directories cannot be empty.");
            Console.WriteLine("Press C to continue and re-enter the paths. Press any other key to exit.");
            Console.ResetColor();
        }

        public static void OutputStatement(string message)
        {
            Console.WriteLine(message);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddExistingProjectsToSolution.Workers
{
    public class VisualStudioVersionWorker
    {

        /// <summary>
        /// Writes mandatory visual studio version information to solution file
        /// </summary>
        /// <param name="isAppend"></param>
        /// <param name="solutionFilePath"></param>
        /// <param name="streamWriter"></param>
        public static void WriteVisualStudioVersionInformation(bool isAppend, string solutionFilePath, StreamWriter streamWriter)
        {
            if ((!isAppend && File.Exists(solutionFilePath)) || (isAppend && !File.Exists(solutionFilePath)))
                streamWriter.WriteLine("Microsoft Visual Studio Solution File, Format Version 12.00");
        }

        /// <summary>
        /// TODO : Writes the visual studio version specifc information to the solution file
        /// </summary>
        /// <param name="version"></param>
        /// <param name="writer"></param>
        public static void WriteVisualStudioVersionInformation(int version, StreamWriter writer)
        {
            switch (version)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    writer.WriteLine("Microsoft Visual Studio Solution File, Format Version 12.00");
                    writer.WriteLine("# Visual Studio 15");
                    writer.WriteLine("VisualStudioVersion = 15.0.26228.13");
                    writer.WriteLine("MinimumVisualStudioVersion = 10.0.40219.1");
                    break;
                case 5:
                    break;
                default:
                    break;
            }
        }
    }
}

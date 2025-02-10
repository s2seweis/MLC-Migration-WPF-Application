using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace RibbonDemo02.Helpers
{
    public class FileHelper
    {
        private static int counter = 0;
        public static string DetermineFileType(string currentDirectory)
        {
            if (currentDirectory.Contains("help"))
                return "hlp";
            if (currentDirectory.Contains("forms"))
                return "frm";
            if (currentDirectory.Contains("messages"))
                return "msg";
            if (currentDirectory.Contains("various"))
                return "all";

            return "unknown"; // Default fallback
        }

        public bool CheckIfFileFollowsPattern(string file)
        {
            string pattern = @"^\d+\.xml$";
            string fileName = Path.GetFileName(file);
            return Regex.IsMatch(fileName.ToLower(), pattern);
        }
    }
}

using RibbonDemo02.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RibbonDemo02.Helpers;

namespace RibbonDemo02.Service
{
    internal class RestoreFilesService
    {

        //  common naming convention in C# to indicate that the variable is a private field
        private static readonly XmlPath _xmlPath = new XmlPath(); // Initialize XMLPathsDynamic

        public RestoreFilesService()
        {
            //RestoreFiles();
        }

        public static void RestoreFiles(
            IProgress<ProgressUpdate> progress,
            out string currentPath,
            out int totalFiles,
            out int filesProcessed
            )
        {
            currentPath = _xmlPath.DynamicPath;
            filesProcessed = 0;
            totalFiles = 0;
            
            // Check if the root directory exists
            if (!Directory.Exists(_xmlPath.DynamicPath))
            {
                Console.WriteLine($"The root directory does not exist: {_xmlPath.DynamicPath}");
                return;
            }

            // Recursively iterate over all subdirectories for restoration process
            //RestoreFilesFromBackup(rootDirectory); // true indicates we are restoring files
            RestoreFilesFromBackupRecursively(_xmlPath.DynamicPath, progress, currentPath, ref totalFiles, ref filesProcessed); // true indicates we are restoring files
        }

        public static void RestoreFilesFromBackupRecursively(string currentDirectory, IProgress<ProgressUpdate> progress, string currentPath, ref int totalFiles, ref int filesProcessed )
        {
            try
            {
                // Get all subdirectories in the current directory
                string[] subdirectories = Directory.GetDirectories(currentDirectory);

                foreach (string subdirectory in subdirectories)
                {
                    // If the subdirectory ends with "Backup", it is a backup folder, so restore from it
                    if (subdirectory.EndsWith("Backup", StringComparison.OrdinalIgnoreCase))
                    {
                        string parentDirectory = RestoreFilesHelper.GetParentDirectoryForBackup(subdirectory);
                        if (parentDirectory != null)
                        {
                            // Process the files in the backup folder
                            RestoreFilesHelper.ProcessRestoreFiles(currentDirectory, parentDirectory, progress, ref totalFiles, ref filesProcessed);

                        }
                    }
                    else
                    {
                        // Recursively call RestoreFilesFromBackup for the next subdirectory
                        RestoreFilesFromBackupRecursively(subdirectory, progress, currentPath, ref totalFiles, ref filesProcessed);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing restore for directory {currentDirectory}: {ex.Message}");
            }
        }
    }
}

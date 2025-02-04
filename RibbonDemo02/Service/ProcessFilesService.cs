using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RibbonDemo02.Helpers;
using RibbonDemo02.Models;

namespace RibbonDemo02.Service
{
    internal class ProcessFilesService
    {
        //  common naming convention in C# to indicate that the variable is a private field
        private static readonly XmlPath _xmlPath = new XmlPath(); // Initialize XMLPathsDynamic

        public static void ProcessFilesMethod(List<int> selectedLanguages, List<string> selectedFolders, IProgress<double> progress, ref int totalFiles, ref int filesProcessed)
        {
            // Check if the root directory exists
            if (Directory.Exists(_xmlPath.DynamicPath))
            {
                // Process the directory recursively
                ProcessDirectoryRecursively(_xmlPath.DynamicPath, selectedLanguages, selectedFolders, progress, ref totalFiles, ref filesProcessed);

            }
            else
            {
                Console.WriteLine($"Root directory does not exist: {_xmlPath.DynamicPath}");
            }
        }

        public static void ProcessDirectoryRecursively(string currentDirectory, List<int> selectedLanguages, List<string> selectedFolders, IProgress<double> progress, ref int totalFiles, ref int filesProcessed)
        {
            try
            {
                FileHelper fileHelper = new FileHelper();

                // Get all XML files in the current directory
                string[] xmlFiles = Directory.GetFiles(currentDirectory, "*.xml");
                string[] validXmlFiles = xmlFiles.Where(file => fileHelper.CheckIfFileFollowsPattern(file)).ToArray();
                string[] pathParts = currentDirectory.TrimEnd(Path.DirectorySeparatorChar)
                                                     .Split(Path.DirectorySeparatorChar);

                if (validXmlFiles.Length > 0)
                {
                    // Extract the name of the current directory
                    string parentFolder = Path.GetFileName(currentDirectory.TrimEnd(Path.DirectorySeparatorChar));

                    // Construct the backup folder name within the current directory
                    string backupPath = Path.Combine(currentDirectory, $"{parentFolder}Backup");
                    string backupFolder = Path.GetFileName(backupPath.TrimEnd(Path.DirectorySeparatorChar));

                    // Create the backup directory
                    Directory.CreateDirectory(backupFolder);

                    string _fileType = FileHelper.DetermineFileType(currentDirectory);

                    // Initialize XmlProcessor and SqlRepository
                    XmlProcessorService processor = new XmlProcessorService();
                    SqlRepositoryService repository = new SqlRepositoryService();

                    ProcessFilesMethod(xmlFiles, _fileType, processor, repository, parentFolder, backupFolder, progress, ref totalFiles, ref filesProcessed);
                }

                // Iterate over subdirectories
                foreach (string subdirectory in Directory.GetDirectories(currentDirectory))
                {
                    string subdirectoryName = Path.GetFileName(subdirectory);

                    // Check if this subdirectory is a language folder
                    int folderLanguage = int.TryParse(subdirectoryName, out int languageNumber) ? languageNumber : 0;

                    // Process only if the subdirectory corresponds to a selected language
                    if (selectedLanguages.Contains(folderLanguage))
                    {
                        // Check if any of the selected folders match the current subdirectory
                        // Look for subdirectories named exactly as the selected folder names
                        foreach (string selectedFolder in selectedFolders)
                        {
                            string folderToProcess = Path.Combine(subdirectory, selectedFolder);

                            if (Directory.Exists(folderToProcess))
                            {
                                // Recursively process the valid subdirectory
                                ProcessDirectoryRecursively(folderToProcess, selectedLanguages, selectedFolders, progress, ref totalFiles, ref filesProcessed);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors encountered during directory processing
                LoggerHelper.Log($"Error processing directory {currentDirectory}: {ex.Message}");
            }
        }

        // ### Move the ProcessFiles method into a helper class? 

        public static void ProcessFilesMethod(string[] files, string prefix, XmlProcessorService processor, SqlRepositoryService repository, string parentFolder, string backupFolder, IProgress<double> progress, ref int totalFiles, ref int filesProcessed)
        {
            bool isAnyFileProcessed = false; // Flag to track if any file was processed
            int validXmlFileCount = 0; // Counter to track files matching the pattern
            FileHelper fileHelper = new FileHelper();

            // Count files matching the pattern using CheckIfFileFollowsPattern
            foreach (string file in files)
            {
                if (fileHelper.CheckIfFileFollowsPattern(file))
                {
                    validXmlFileCount++; // Increment count for valid XML files
                }
            }

            // Update the totalFiles count in the calling class (ViewModel)
            totalFiles = validXmlFileCount;

            // Process only valid XML files
            foreach (string file in files)
            {
                // Check if the file matches the pattern (this is where CheckIfFileFollowsPattern is used)
                if (fileHelper.CheckIfFileFollowsPattern(file))
                {
                    // here is currently a error !
                    var convertedXML = processor.ConvertXML(file, prefix, validXmlFileCount, parentFolder, backupFolder, progress);
                    if (convertedXML != null)
                    {
                        // has to be true
                        bool isSuccess = repository.SaveDataToDB(convertedXML, prefix);
                        if (isSuccess)
                        {
                            ProcessFileHelper.MoveFileToNewLocation(file); // Use helper method to move the file
                            isAnyFileProcessed = true; // Mark that a file has been processed

                            filesProcessed++; // Increment filesProcessed count
                            //progress.Report((double)filesProcessed / totalFiles * 100);
                        }
                    }
                }
            }

            // Only log the message outside the loop when a file was processed
            if (isAnyFileProcessed)
            {
                // Log success message
                //Console.WriteLine($"Successfully processed " +
                //$"{prefix.ToUpper()} " +
                //$"files.");
            }
            else
            {
                // Log the message when no valid files were processed
                LoggerHelper.Log($"No valid {prefix.ToUpper()} files were processed.");
            }
        }
    }
}
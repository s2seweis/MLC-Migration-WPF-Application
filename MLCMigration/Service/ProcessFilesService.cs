using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RibbonDemo02.Helpers;
using RibbonDemo02.Models;

namespace RibbonDemo02.Service
{
    internal class ProcessFilesService
    {
        private static readonly XmlPath _xmlPath = new XmlPath();

        public static void ProcessFilesMethod(
            List<int> selectedLanguages,
            List<string> selectedFolders,
            IProgress<ProgressUpdate> progress,
            out int totalFiles,
            out int filesProcessed,
            out int currentLanguage,
            out string currentFolderNew
            )
        {
            filesProcessed = 0;
            totalFiles = 0;
            currentLanguage = 0;
            currentFolderNew = string.Empty;

            if (Directory.Exists(_xmlPath.DynamicPath))
            {
                // Start processing from the root directory.
                ProcessDirectoryRecursively(
                    _xmlPath.DynamicPath,
                    selectedLanguages,
                    selectedFolders,
                    progress,
                    totalFiles,
                    ref filesProcessed,
                    ref currentLanguage,
                    ref currentFolderNew
                    );
            }
            else
            {
                Console.WriteLine($"Root directory does not exist: {_xmlPath.DynamicPath}");
            }
        }

        private static int CountValidFiles(string currentDirectory)
        {
            int count = 0;
            FileHelper fileHelper = new FileHelper();

            string[] xmlFiles = Directory.GetFiles(currentDirectory, "*.xml");
            count += xmlFiles.Count(file => fileHelper.CheckIfFileFollowsPattern(file));

            foreach (string subdirectory in Directory.GetDirectories(currentDirectory))
            {
                count += CountValidFiles(subdirectory);
            }
            return count;
        }

        // currentLanguage is passed by reference so that updates persist in the caller.
        private static void ProcessDirectoryRecursively(
            string currentDirectory,
            List<int> selectedLanguages,
            List<string> selectedFolders,
            IProgress<ProgressUpdate> progress,
            int totalFiles,
            ref int filesProcessed,
            ref int currentLanguage,
            ref string currentFolderNew
            )
        {
            try
            {
                FileHelper fileHelper = new FileHelper();
                string[] xmlFiles = Directory.GetFiles(currentDirectory, "*.xml");
                string[] validXmlFiles = xmlFiles.Where(file => fileHelper.CheckIfFileFollowsPattern(file)).ToArray();

                // Update currentLanguage from the parent directory name.
                // For example, if currentDirectory is "C:\Data\3\_messages", then the language is 3.
                if (int.TryParse(Path.GetFileName(Path.GetDirectoryName(currentDirectory)), out int lang))
                {
                    currentLanguage = lang;
                }

                if (validXmlFiles.Length > 0)
                {
                    string currentFolder = Path.GetFileName(currentDirectory.TrimEnd(Path.DirectorySeparatorChar));
                    currentFolderNew = currentFolder;
                    string backupPath = Path.Combine(currentDirectory, $"{currentFolder}Backup");
                    string backupFolder = Path.GetFileName(backupPath.TrimEnd(Path.DirectorySeparatorChar));
                    Directory.CreateDirectory(backupFolder);
                    string fileType = FileHelper.DetermineFileType(currentDirectory);

                    XmlProcessorService processor = new XmlProcessorService();
                    SqlRepositoryService repository = new SqlRepositoryService();

                    ProcessFilesMethod(
                        validXmlFiles,
                        fileType,
                        processor,
                        repository,
                        currentFolder,
                        backupFolder,
                        progress,
                        ref totalFiles,
                        ref filesProcessed,
                        ref currentLanguage,
                        ref currentFolderNew
                        );
                }

                foreach (string subdirectory in Directory.GetDirectories(currentDirectory))
                {
                    int folderLanguage = int.TryParse(Path.GetFileName(subdirectory), out int languageNumber) ? languageNumber : 0;
                    if (selectedLanguages.Contains(folderLanguage))
                    {
                        foreach (string selectedFolder in selectedFolders)
                        {
                            string folderToProcess = Path.Combine(subdirectory, selectedFolder);
                            if (Directory.Exists(folderToProcess))
                            {
                                totalFiles = 0;
                                filesProcessed = 0;
                                currentLanguage = 0;
                                ProcessDirectoryRecursively(
                                    folderToProcess,
                                    selectedLanguages,
                                    selectedFolders,
                                    progress,
                                    totalFiles,
                                    ref filesProcessed,
                                    ref currentLanguage,
                                    ref currentFolderNew
                                    );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.Log($"Error processing directory {currentDirectory}: {ex.Message}");
            }
        }

        // currentLanguage is passed by reference so that its updated value is available to the caller.
        private static void ProcessFilesMethod(
            string[] files,
            string prefix,
            XmlProcessorService processor,
            SqlRepositoryService repository,
            string currentFolder,
            string backupFolder,
            IProgress<ProgressUpdate> progress,
            ref int totalFiles,
            ref int filesProcessed,
            ref int currentLanguage,
            ref string currentFolderNew
            )
        {
            FileHelper fileHelper = new FileHelper();
            int validXmlFileCount = files.Count(file => fileHelper.CheckIfFileFollowsPattern(file));

            // Assign the valid XML file count to totalFiles for this batch.
            totalFiles = validXmlFileCount;

            foreach (string file in files)
            {
                if (fileHelper.CheckIfFileFollowsPattern(file))
                {
                    var convertedXML = processor.ConvertXML(file, prefix, validXmlFileCount, currentFolder, backupFolder, progress);
                    if (convertedXML != null)
                    {
                        bool isSuccess = repository.SaveDataToDB(convertedXML, prefix);
                        if (isSuccess)
                        {
                            ProcessFileHelper.MoveFileToNewLocation(file);
                            filesProcessed++;

                            double percent = totalFiles > 0 ? (double)filesProcessed / totalFiles * 100 : 0;
                            progress.Report(new ProgressUpdate
                            {
                                Percent = percent,
                                FilesProcessed = filesProcessed,
                                TotalFiles = totalFiles,
                                CurrentLanguage = currentLanguage,
                                CurrentFolderNew = currentFolderNew
                            });
                        }
                    }
                }
            }
        }
    }
}

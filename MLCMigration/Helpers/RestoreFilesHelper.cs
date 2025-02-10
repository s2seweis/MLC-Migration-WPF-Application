using RibbonDemo02.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RibbonDemo02.Helpers
{
    internal class RestoreFilesHelper
    {
        public static string GetParentDirectoryForBackup(string backupDirectory)
        {
            string parentDirectory = backupDirectory.Replace("-Backup", "");
            return Directory.Exists(parentDirectory) ? parentDirectory : null;
        }

        public static void ProcessRestoreFiles(string parentDirectory, string backupDirectory, IProgress<ProgressUpdate> progress, ref int totalFiles, ref int filesProcessed)
        {
            string[] backupFiles = Directory.GetFiles(backupDirectory);
            totalFiles = backupFiles.Length;


            string[] pathParts = backupDirectory.TrimEnd(Path.DirectorySeparatorChar)
                                                            .Split(Path.DirectorySeparatorChar);
            string firstTwoParts = string.Join(@"\", pathParts.Take(1)); // Get the first two parts

            string lastThreePartsOld = string.Join(@"\", pathParts.Skip(Math.Max(0, pathParts.Length - 7)));

            string lastThreeParts = $"{firstTwoParts}...{lastThreePartsOld}";

            if (backupFiles.Length > 0)
            {
                foreach (var file in backupFiles)
                {
                    string targetPath = Path.Combine(parentDirectory, Path.GetFileName(file));

                    if (!File.Exists(targetPath))
                    {
                        MoveFileToParentDirectory(file);
                        filesProcessed++;
                        Thread.Sleep(50);
                        double percent = totalFiles > 0 ? (double)filesProcessed / totalFiles * 100 : 0;
                        progress.Report(new ProgressUpdate
                        {

                            Percent = percent,
                            TotalFiles = totalFiles,
                            FilesProcessed = filesProcessed,
                            CurrentPath = lastThreeParts,
                            ProcessType = "Restore"
                        });
                    }

                    var repositoryHelper = new SqlRepositoryHelper();
                    repositoryHelper.ClearTables();
                }
            }
            else
            {
                Console.WriteLine($"No files to process in directory: {backupDirectory}");
            }
        }

        public static void MoveFileToParentDirectory(string file)
        {
            try
            {
                var currentDirectory = Path.GetDirectoryName(file);
                string parentDirectory = Directory.GetParent(currentDirectory)?.FullName;

                if (string.IsNullOrEmpty(parentDirectory)) return;

                string newLocation = Path.Combine(parentDirectory, Path.GetFileName(file));

                if (!Directory.Exists(parentDirectory)) return;

                File.Move(file, newLocation);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace RibbonDemo02.Helpers
{
    public static class LoggerHelper
    {
        private static readonly string _logFilePath =
            Path.Combine("C:\\Users\\SWE\\source\\repos\\11. MLC Migration WPF\\RibbonDemo02\\Data\\Logs", "exceptions.txt");

        // A HashSet to track already logged messages, preventing duplicates.
        private static readonly HashSet<string> _loggedMessages = new HashSet<string>();

        public static void Log(string message)
        {
            try
            {
                // Get the directory name (fallback to empty string if null to handle compatibility with 7.3)
                string directory = Path.GetDirectoryName(_logFilePath) ?? string.Empty;
                // Ensure the directory for the log file exists
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Avoid logging duplicate messages
                if (_loggedMessages.Contains(message))
                    return;

                // Format the message
                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | Error: {message}";

                // Write the message to the log file
                using (StreamWriter writer = new StreamWriter(_logFilePath, true))
                {
                    writer.WriteLine(logMessage);
                }

                // Add to HashSet to avoid duplicate logging
                _loggedMessages.Add(message);
            }
            catch (Exception ex)
            {
                // Handle issues writing to the log file (optional UI notification)
                MessageBox.Show($"Error writing to log file: {ex.Message}", "Logging Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void HandleError(Exception ex, string userFriendlyMessage = "An error occurred. Please try again later.")
        {
            // Log the detailed exception message
            Log(ex.Message);

            // Display a user-friendly message to the user
            MessageBox.Show(userFriendlyMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}

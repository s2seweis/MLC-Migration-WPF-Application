using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace RibbonDemo02.Helpers
{
    public static class LoggerHelper
    {
        private static readonly string _logDirectory =
            Path.Combine("C:\\Users\\SWE\\source\\repos\\11. MLC Migration WPF\\RibbonDemo02\\Data\\Logs");

        private static string _logFilePath;
        private static readonly HashSet<string> _loggedMessages = new HashSet<string>();

        static LoggerHelper()
        {
            InitializeLogFile();
        }

        private static void InitializeLogFile()
        {
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            _logFilePath = Path.Combine(_logDirectory, $"log_{currentDate}.txt");

            if (!Directory.Exists(_logDirectory))
            {
                Directory.CreateDirectory(_logDirectory);
            }

            if (!File.Exists(_logFilePath))
            {
                File.WriteAllText(_logFilePath, $"Logfile created for date: {currentDate}\n");
            }
        }

        public static void Log(string message)
        {
            try
            {
                string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                string newLogFilePath = Path.Combine(_logDirectory, $"log_{currentDate}.txt");

                if (_logFilePath != newLogFilePath)
                {
                    _logFilePath = newLogFilePath;
                    InitializeLogFile();
                }

                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | Error: {message}";

                using (StreamWriter writer = new StreamWriter(_logFilePath, true))
                {
                    writer.WriteLine(logMessage);
                }

                _loggedMessages.Add(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error writing to log file: {ex.Message}", "Logging Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void HandleError(Exception ex, string userFriendlyMessage = "An error occurred. Please try again later.")
        {
            Log(ex.Message);
            MessageBox.Show(userFriendlyMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}

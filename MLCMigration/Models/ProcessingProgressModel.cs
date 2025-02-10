using System;
using System.ComponentModel;

namespace RibbonDemo02.Models
{
    public class ProcessingProgressModel : INotifyPropertyChanged
    {
        private double _progress;
        private string _progressText;
        private int _totalFiles;
        private int _filesProcessed;
        private int _currentLanguage;
        private string _currentFolder;
        private string _currentPath;
        private string _processType;
        private bool _isCompleted;

        public double Progress
        {
            get => _progress;
            set
            {
                if (_progress != value)
                {
                    _progress = value;
                    OnPropertyChanged(nameof(Progress));
                    UpdateProgressText();

                    // Check if progress reached 100%
                    if (_progress >= 100 && !_isCompleted)
                    {
                        OnProgressCompleted();
                    }
                }
            }
        }

        public event EventHandler ProgressCompleted;

        private void OnProgressCompleted()
        {
            _isCompleted = true;
            ProgressCompleted?.Invoke(this, EventArgs.Empty);
            Progress = 0;
            // Set completion message
            ProgressText = "Bereit.";
            OnPropertyChanged(nameof(ProgressText));
        }

        public int TotalFiles
        {
            get => _totalFiles;
            set
            {
                if (_totalFiles != value)
                {
                    _totalFiles = value;
                    OnPropertyChanged(nameof(TotalFiles));
                    UpdateProgressText();
                }
            }
        }

        public int FilesProcessed
        {
            get => _filesProcessed;
            set
            {
                if (_filesProcessed != value)
                {
                    _filesProcessed = value;
                    OnPropertyChanged(nameof(FilesProcessed));
                    UpdateProgressText();
                }
            }
        }

        public int CurrentLanguage
        {
            get => _currentLanguage;
            set
            {
                if (_currentLanguage != value)
                {
                    _currentLanguage = value;
                    OnPropertyChanged(nameof(CurrentLanguage));
                    UpdateProgressText();
                }
            }
        }

        public string CurrentFolderNew
        {
            get => _currentFolder;
            set
            {
                if (_currentFolder != value)
                {
                    _currentFolder = value;
                    OnPropertyChanged(nameof(CurrentFolderNew));
                    UpdateProgressText();
                }
            }
        }

        public string CurrentPath
        {
            get => _currentPath;
            set
            {
                if (_currentPath != value)
                {
                    _currentPath = value;
                    OnPropertyChanged(nameof(CurrentPath));
                }
            }
        }

        public string ProcessType
        {
            get => _processType;
            set
            {
                if (_processType != value)
                {
                    _isCompleted = false;
                    _processType = value;
                    OnPropertyChanged(nameof(ProcessType));
                    UpdateProgressText();
                }
            }
        }

        public string ProgressText
        {
            get => _progressText;
            set
            {
                if (_progressText != value)
                {
                    _progressText = value;
                    OnPropertyChanged(nameof(ProgressText));
                }
            }
        }

        public void UpdateProgressText()
        {
            if (_isCompleted)
            {
                // If completed, set progress text to "Bereit."
                ProgressText = "Bereit.";
            }
            else
            {
                string languageName = GetLanguageName(CurrentLanguage);
                string folderName = string.IsNullOrEmpty(CurrentFolderNew) ? "N/A" : CurrentFolderNew;

                if (ProcessType == "Restore")
                {
                    ProgressText = $"{FilesProcessed}/{TotalFiles} files restored from Path: {CurrentPath} {ProcessType} ({Progress:F0}%)";
                }
                else
                {
                    ProgressText = $"{FilesProcessed}/{TotalFiles} files processed from Language: {languageName} and Folder: {folderName} ({Progress:F0}%)";
                }
            }

            OnPropertyChanged(nameof(ProgressText));
        }

        private string GetLanguageName(int language)
        {
            // Adjust the mapping as necessary.
            switch (language)
            {
                case 1:
                    return "German";
                case 2:
                    return "English"; // If Spanish also equals 2, adjust accordingly.
                case 8:
                    return "Hungarian";
                case 9:
                    return "Polish";
                case 12:
                    return "Danish";
                default:
                    return "N/A";
            }
        }

        //public void Reset()
        //{
        //    _isCompleted = false;
        //    Progress = 0;
        //    FilesProcessed = 0;
        //    TotalFiles = 0;
        //    CurrentLanguage = 0;
        //    CurrentFolderNew = string.Empty;
        //    CurrentPath = string.Empty;
        //    ProcessType = string.Empty;
        //    ProgressText = string.Empty;
        //}

        public void Reset()
        {
            _isCompleted = false;
            Progress = 0;
            FilesProcessed = 0;
            TotalFiles = 0;
            CurrentLanguage = 0;
            CurrentFolderNew = string.Empty;
            CurrentPath = string.Empty;
            ProcessType = string.Empty;
            ProgressText = string.Empty;
            OnPropertyChanged(nameof(ProgressText));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
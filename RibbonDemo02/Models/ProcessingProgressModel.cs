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
                }
            }
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

        public string ProgressText
        {
            get => _progressText;
            private set
            {
                if (_progressText != value)
                {
                    _progressText = value;
                    OnPropertyChanged(nameof(ProgressText));
                }
            }
        }

        private void UpdateProgressText()
        {
            // Convert the current language integer to a proper language name.
            string languageName = GetLanguageName(CurrentLanguage);
            string folderName = string.IsNullOrEmpty(CurrentFolderNew) ? "N/A" : CurrentFolderNew;
            ProgressText = $"{FilesProcessed}/{TotalFiles} files processed from Language: {languageName} and Folder: {folderName} ({Progress:F0}%)";
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

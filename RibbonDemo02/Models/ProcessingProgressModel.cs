using System.ComponentModel;

namespace RibbonDemo02.Models
{
    public class ProcessingProgressModel : INotifyPropertyChanged
    {
        private double _progress;
        private string _progressText;
        private int _totalFiles;
        private int _filesProcessed;

        /// <summary>
        /// Gets or sets the progress percentage (0–100).
        /// Changing this property also updates the display text.
        /// </summary>
        public double Progress
        {
            get => _progress;
            set
            {
                if (_progress != value)
                {
                    _progress = value;
                    OnPropertyChanged(nameof(Progress));
                    // Automatically update the progress text when progress changes.
                    ProgressText = $"{FilesProcessed}/{TotalFiles} files processed ({_progress:F0}%)";
                }
            }
        }

        /// <summary>
        /// Gets or sets the display text for progress.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the total number of files.
        /// </summary>
        public int TotalFiles
        {
            get => _totalFiles;
            set
            {
                if (_totalFiles != value)
                {
                    _totalFiles = value;
                    OnPropertyChanged(nameof(TotalFiles));
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of files processed.
        /// </summary>
        public int FilesProcessed
        {
            get => _filesProcessed;
            set
            {
                if (_filesProcessed != value)
                {
                    _filesProcessed = value;
                    OnPropertyChanged(nameof(FilesProcessed));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

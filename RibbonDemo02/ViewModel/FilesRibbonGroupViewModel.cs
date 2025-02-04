using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;  // For MessageBox
using RibbonDemo02.Models;
using RibbonDemo02.Service;

namespace RibbonDemo02.ViewModels
{
    public class FilesRibbonGroupViewModel : INotifyPropertyChanged
    {
        public ProcessingProgressModel ProcessingProgress { get; set; } = new ProcessingProgressModel();
        public FileSelectionModel FileSelection { get; set; } = new FileSelectionModel();
        public LanguageSelectionModel LanguageSelection { get; set; } = new LanguageSelectionModel();

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler StateChanged;
        public ICommand StartCommand { get; }

        public FilesRibbonGroupViewModel()
        {
            // Remove any default setting if you don't want a checkbox preselected.
            // FileSelection.NachrichtenSelected = true;  

            StartCommand = new RelayCommand(ExecuteStartCommand, CanExecuteStartCommand);

            FileSelection.PropertyChanged += (s, e) =>
            {
                CommandManager.InvalidateRequerySuggested();
                StateChanged?.Invoke(this, EventArgs.Empty);
            };

            LanguageSelection.PropertyChanged += (s, e) =>
            {
                StateChanged?.Invoke(this, EventArgs.Empty);
            };

            ProcessingProgress.PropertyChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(ProcessingProgress));
            };
        }

        private async void ExecuteStartCommand(object parameter)
        {
            var selectedFolders = new List<string>();
            if (FileSelection.NachrichtenSelected) selectedFolders.Add("_messages");
            if (FileSelection.HilfeSelected) selectedFolders.Add("_help");
            if (FileSelection.VariousSelected) selectedFolders.Add("_various");
            if (FileSelection.SonstigesSelected) selectedFolders.Add("_forms");

            var selectedLanguages = new List<int>();
            if (LanguageSelection.GermanSelected) selectedLanguages.Add(1);
            if (LanguageSelection.EnglishSelected) selectedLanguages.Add(2);
            if (LanguageSelection.SpanishSelected) selectedLanguages.Add(3);
            if (LanguageSelection.HungarianSelected) selectedLanguages.Add(8);
            if (LanguageSelection.PolishSelected) selectedLanguages.Add(9);
            if (LanguageSelection.DanishSelected) selectedLanguages.Add(12);

            //MessageBox.Show("Start button clicked.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            string message = $"Start button clicked.\n" +
                           $"Selected Folders: {string.Join(", ", selectedFolders)}\n" +
                           $"Selected Languages: {string.Join(", ", selectedLanguages)}";

            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Create a progress reporter that updates the ProcessingProgress model.
            var progress = new Progress<ProgressUpdate>(update =>
            {
                // These assignments occur on the UI thread.
                ProcessingProgress.Progress = update.Percent;
                ProcessingProgress.FilesProcessed = update.FilesProcessed;
                ProcessingProgress.TotalFiles = update.TotalFiles;
                //
                ProcessingProgress.CurrentLanguage = update.CurrentLanguage;
                ProcessingProgress.CurrentFolderNew = update.CurrentFolderNew;
                //
            });

            // Run file processing asynchronously.
            await Task.Run(() =>
            {
                ProcessFilesService.ProcessFilesMethod(
                    selectedLanguages,
                    selectedFolders,
                    progress,
                    out int totalFiles,
                    out int filesProcessed,
                    out int currentLanguage,
                    out string currentFolderNew
                );
            });
        }

        private bool CanExecuteStartCommand(object parameter)
        {
            return FileSelection.NachrichtenSelected ||
                   FileSelection.HilfeSelected ||
                   FileSelection.VariousSelected ||
                   FileSelection.SonstigesSelected;
        }

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

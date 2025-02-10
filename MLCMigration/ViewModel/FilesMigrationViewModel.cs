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
    public class FilesMigrationViewModel : INotifyPropertyChanged
    {
        public ProcessingProgressModel ProcessingProgress { get; set; } = new ProcessingProgressModel();
        public FileSelectionModel FileSelection { get; set; } = new FileSelectionModel();
        public LanguageSelectionModel LanguageSelection { get; set; } = new LanguageSelectionModel();

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler StateChanged;
        public ICommand StartCommand { get; }
        public ICommand RestoreCommand { get; }

        public FilesMigrationViewModel()
        {
            // Remove any default setting if you don't want a checkbox preselected.
            // FileSelection.NachrichtenSelected = true;  

            StartCommand = new RelayCommand(ExecuteStartCommand, CanExecuteStartCommand);

            // Neues RestoreCommand
            RestoreCommand = new RelayCommand(ExecuteRestoreCommand);


            FileSelection.PropertyChanged += (s, e) =>
            {
                CommandManager.InvalidateRequerySuggested();
                StateChanged?.Invoke(this, EventArgs.Empty);
            };

            LanguageSelection.PropertyChanged += (s, e) =>
            {
                StateChanged?.Invoke(this, EventArgs.Empty);
            };


            ProcessingProgress.ProgressCompleted += (s, e) =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("Der Vorgang wurde erfolgreich abgeschlossen!", "Fertig", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // UI-Refresh erzwingen
                    OnPropertyChanged(nameof(ProcessingProgress));
                });
            };


        }

        private async void ExecuteStartCommand(object parameter)
        {
            ProcessingProgress.Reset();

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

        private async void ExecuteRestoreCommand(object parameter)
        {
            ProcessingProgress.Reset();

            MessageBox.Show("Wiederherstellen wurde gestartet.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var progress = new Progress<ProgressUpdate>(update =>
            {
                ProcessingProgress.Progress = update.Percent;
                ProcessingProgress.FilesProcessed = update.FilesProcessed;
                ProcessingProgress.TotalFiles = update.TotalFiles;
                ProcessingProgress.CurrentPath = update.CurrentPath;
                ProcessingProgress.ProcessType = update.ProcessType;
            });

            await Task.Run(() =>
            {
                RestoreFilesService.RestoreFiles(
                    progress,
                    out string currentPath,
                    out int totalFiles,
                    out int filesProcessed
                    );
            });
        }

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

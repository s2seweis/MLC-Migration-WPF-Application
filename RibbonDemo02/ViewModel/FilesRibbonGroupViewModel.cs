using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Forms;  // For MessageBox
using RibbonDemo02.Models;
using RibbonDemo02.Service;

namespace RibbonDemo02.ViewModels
{
    public class FilesRibbonGroupViewModel : INotifyPropertyChanged
    {
        // Composed models for progress, file selection, and language selection.
        public ProcessingProgressModel ProcessingProgress { get; set; } = new ProcessingProgressModel();
        public FileSelectionModel FileSelection { get; set; } = new FileSelectionModel();
        public LanguageSelectionModel LanguageSelection { get; set; } = new LanguageSelectionModel();

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler StateChanged;

        // ICommand for the "Start" button.
        public ICommand StartCommand { get; }

        public FilesRibbonGroupViewModel()
        {
            // For testing or default, set one file selection to true so that the Start command is enabled.
            //FileSelection.NachrichtenSelected = true;

            StartCommand = new RelayCommand(ExecuteStartCommand, CanExecuteStartCommand);

            // Subscribe to changes in the file selection model to trigger requerying of the command.
            FileSelection.PropertyChanged += (s, e) =>
            {
                CommandManager.InvalidateRequerySuggested();
                StateChanged?.Invoke(this, EventArgs.Empty);
            };

            // Optionally subscribe to language selection changes.
            LanguageSelection.PropertyChanged += (s, e) =>
            {
                StateChanged?.Invoke(this, EventArgs.Empty);
            };

            // Optionally, if you need the view model to be notified when any nested property changes,
            // you can subscribe to the nested models’ PropertyChanged events and re-raise them:
            ProcessingProgress.PropertyChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(ProcessingProgress));
            };

            // Alternatively, you can bind directly to the composed models in your XAML.
        }

        private void ExecuteStartCommand(object parameter)
        {
            // Build list of selected folders from file selection.
            var selectedFolders = new List<string>();
            if (FileSelection.NachrichtenSelected) selectedFolders.Add("_messages");
            if (FileSelection.HilfeSelected) selectedFolders.Add("_help");
            if (FileSelection.VariousSelected) selectedFolders.Add("_various");
            if (FileSelection.SonstigesSelected) selectedFolders.Add("_forms");

            // Build list of selected languages from language selection.
            var selectedLanguages = new List<int>();
            if (LanguageSelection.GermanSelected) selectedLanguages.Add(1);
            if (LanguageSelection.EnglishSelected) selectedLanguages.Add(2);
            if (LanguageSelection.SpanishSelected) selectedLanguages.Add(3);
            if (LanguageSelection.HungarianSelected) selectedLanguages.Add(8);
            if (LanguageSelection.PolishSelected) selectedLanguages.Add(9);
            if (LanguageSelection.DanishSelected) selectedLanguages.Add(12);

            string message = $"Start button clicked.\n" +
                             $"Selected Folders: {string.Join(", ", selectedFolders)}\n" +
                             $"Selected Languages: {string.Join(", ", selectedLanguages)}";

            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Create a progress reporter that updates the ProcessingProgress model.
            var progress = new Progress<double>(p => ProcessingProgress.Progress = p);

            // Use local variables for total and processed counts (which can be passed by ref).
            int totalFiles = 0;
            int filesProcessed = 0;

            // Call your file processing service.
            ProcessFilesService.ProcessFilesMethod(
                selectedLanguages,
                selectedFolders,
                progress,
                ref totalFiles,
                ref filesProcessed
            );

            // Update the ProcessingProgress model with the final counts.
            ProcessingProgress.TotalFiles = totalFiles;
            ProcessingProgress.FilesProcessed = filesProcessed;
        }

        private bool CanExecuteStartCommand(object parameter)
        {
            // The command is enabled if at least one file type is selected.
            return FileSelection.NachrichtenSelected ||
                   FileSelection.HilfeSelected ||
                   FileSelection.VariousSelected ||
                   FileSelection.SonstigesSelected;
        }

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

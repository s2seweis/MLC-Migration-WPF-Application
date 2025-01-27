using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;

namespace RibbonDemo02.ViewModels
{
    public class FilesRibbonGroupViewModel : INotifyPropertyChanged
    {
        // State variables for File and Language sections
        private bool _alleSelected, _hilfeSelected, _nachrichtenSelected, _variousSelected, _sonstigesSelected;
        private bool _allLanguagesSelected, _englishSelected, _germanSelected, _frenchSelected, _spanishSelected, _italianSelected, _dutchSelected, _japaneseSelected, _koreanSelected;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler StateChanged;

        // Helper to notify property changes
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Method to manage state and invoke actions
        private void HandleCheckboxChange(ref bool field, bool newValue, Action onSelect, Action onDeselect, string propertyName)
        {
            if (field == newValue) return;

            field = newValue;
            OnPropertyChanged(propertyName);

            if (newValue) onSelect?.Invoke();
            else onDeselect?.Invoke();

            StateChanged?.Invoke(this, EventArgs.Empty);
            CommandManager.InvalidateRequerySuggested();
        }

        #region File Section Properties

        public bool AlleSelected
        {
            get => _alleSelected;
            set => HandleCheckboxChange(ref _alleSelected, value, SelectAllFiles, ResetAllFiles, nameof(AlleSelected));
        }

        public bool HilfeSelected
        {
            get => _hilfeSelected;
            set => HandleCheckboxChange(ref _hilfeSelected, value, CheckAllFiles, () => UpdateAlleSelected(false), nameof(HilfeSelected));
        }

        public bool NachrichtenSelected
        {
            get => _nachrichtenSelected;
            set => HandleCheckboxChange(ref _nachrichtenSelected, value, CheckAllFiles, () => UpdateAlleSelected(false), nameof(NachrichtenSelected));
        }

        public bool VariousSelected
        {
            get => _variousSelected;
            set => HandleCheckboxChange(ref _variousSelected, value, CheckAllFiles, () => UpdateAlleSelected(false), nameof(VariousSelected));
        }

        public bool SonstigesSelected
        {
            get => _sonstigesSelected;
            set => HandleCheckboxChange(ref _sonstigesSelected, value, CheckAllFiles, () => UpdateAlleSelected(false), nameof(SonstigesSelected));
        }

        private void SelectAllFiles()
        {
            HilfeSelected = NachrichtenSelected = VariousSelected = SonstigesSelected = true;
        }

        private void ResetAllFiles()
        {
            HilfeSelected = NachrichtenSelected = VariousSelected = SonstigesSelected = false;
        }

        private void CheckAllFiles() => UpdateAlleSelected(HilfeSelected && NachrichtenSelected && VariousSelected && SonstigesSelected);
        private void UpdateAlleSelected(bool state) => _alleSelected = state;

        #endregion

        #region Language Section Properties

        public bool AllLanguagesSelected
        {
            get => _allLanguagesSelected;
            set => HandleCheckboxChange(ref _allLanguagesSelected, value, SelectAllLanguages, ResetAllLanguages, nameof(AllLanguagesSelected));
        }

        public bool GermanSelected
        {
            get => _germanSelected;
            set => HandleCheckboxChange(ref _germanSelected, value, CheckAllLanguages, () => UpdateAllLanguagesSelected(false), nameof(GermanSelected));
        }

        public bool EnglishSelected
        {
            get => _englishSelected;
            set => HandleCheckboxChange(ref _englishSelected, value, CheckAllLanguages, () => UpdateAllLanguagesSelected(false), nameof(EnglishSelected));
        }

        // Other language properties similar to German and English
        public bool FrenchSelected
        {
            get => _frenchSelected;
            set => HandleCheckboxChange(ref _frenchSelected, value, CheckAllLanguages, () => UpdateAllLanguagesSelected(false), nameof(FrenchSelected));
        }

        public bool SpanishSelected
        {
            get => _spanishSelected;
            set => HandleCheckboxChange(ref _spanishSelected, value, CheckAllLanguages, () => UpdateAllLanguagesSelected(false), nameof(SpanishSelected));
        }

        public bool ItalianSelected
        {
            get => _italianSelected;
            set => HandleCheckboxChange(ref _italianSelected, value, CheckAllLanguages, () => UpdateAllLanguagesSelected(false), nameof(ItalianSelected));
        }

        public bool DutchSelected
        {
            get => _dutchSelected;
            set => HandleCheckboxChange(ref _dutchSelected, value, CheckAllLanguages, () => UpdateAllLanguagesSelected(false), nameof(DutchSelected));
        }

        public bool JapaneseSelected
        {
            get => _japaneseSelected;
            set => HandleCheckboxChange(ref _japaneseSelected, value, CheckAllLanguages, () => UpdateAllLanguagesSelected(false), nameof(JapaneseSelected));
        }

        public bool KoreanSelected
        {
            get => _koreanSelected;
            set => HandleCheckboxChange(ref _koreanSelected, value, CheckAllLanguages, () => UpdateAllLanguagesSelected(false), nameof(KoreanSelected));
        }

        private void SelectAllLanguages()
        {
            GermanSelected = EnglishSelected = FrenchSelected = SpanishSelected = ItalianSelected = DutchSelected = JapaneseSelected = KoreanSelected = true;
        }

        private void ResetAllLanguages()
        {
            GermanSelected = EnglishSelected = FrenchSelected = SpanishSelected = ItalianSelected = DutchSelected = JapaneseSelected = KoreanSelected = false;
        }

        private void CheckAllLanguages() => UpdateAllLanguagesSelected(IsAllLanguagesChecked());
        private bool IsAllLanguagesChecked() => GermanSelected && EnglishSelected && FrenchSelected && SpanishSelected &&
                                                ItalianSelected && DutchSelected && JapaneseSelected && KoreanSelected;

        private void UpdateAllLanguagesSelected(bool state) => _allLanguagesSelected = state;

        #endregion

        #region Start Command

        public ICommand StartCommand { get; }

        public FilesRibbonGroupViewModel()
        {
            StartCommand = new RelayCommand(ExecuteStartCommand, CanExecuteStartCommand);
        }

        private void ExecuteStartCommand(object parameter)
        {
            var selectedValuesFiles = new
            {
                AlleSelected,
                NachrichtenSelected,
                HilfeSelected,
                VariousSelected,
                SonstigesSelected
            };

            var selectedValuesLanguages = new
            {
                AllLanguagesSelected,
                GermanSelected,
                EnglishSelected,
                FrenchSelected,
                SpanishSelected,
                ItalianSelected,
                DutchSelected,
                JapaneseSelected,
                KoreanSelected
            };

            var message = $"Start button clicked.\n" +
                          $"File Section (IsChecked): \n" +
                          $"Alle: {selectedValuesFiles.AlleSelected}, Nachrichten: {selectedValuesFiles.NachrichtenSelected}, Hilfe: {selectedValuesFiles.HilfeSelected}, Various: {selectedValuesFiles.VariousSelected}, Sonstiges: {selectedValuesFiles.SonstigesSelected}\n" +
                          $"Language Section (IsChecked): \n" +
                          $"AllLanguages: {selectedValuesLanguages.AllLanguagesSelected}, German: {selectedValuesLanguages.GermanSelected}, English: {selectedValuesLanguages.EnglishSelected}, French: {selectedValuesLanguages.FrenchSelected}, Spanish: {selectedValuesLanguages.SpanishSelected}, Italian: {selectedValuesLanguages.ItalianSelected}, Dutch: {selectedValuesLanguages.DutchSelected}, Japanese: {selectedValuesLanguages.JapaneseSelected}, Korean: {selectedValuesLanguages.KoreanSelected}";

            MessageBox.Show(message);
        }

        private bool CanExecuteStartCommand(object parameter) => HilfeSelected || NachrichtenSelected || VariousSelected || SonstigesSelected;

        #endregion
    }
}

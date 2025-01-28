using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using RibbonDemo02.Service;

namespace RibbonDemo02.ViewModels
{
    public class FilesRibbonGroupViewModel : INotifyPropertyChanged
    {
        // For the File checkbox - Start
        private bool _alleSelected;
        private bool _nachrichtenSelected;
        private bool _hilfeSelected;
        private bool _variousSelected;
        private bool _sonstigesSelected;

        public event PropertyChangedEventHandler PropertyChanged;
        // Event to notify state change
        public event EventHandler StateChanged;
        public bool AlleSelected
        {
            get => _alleSelected;
            set
            {
                if (_alleSelected != value)
                {
                    _alleSelected = value;
                    OnPropertyChanged(nameof(AlleSelected));

                    // If "Alle" is selected, select all checkboxes
                    if (_alleSelected)
                    {
                        NachrichtenSelected = true;
                        HilfeSelected = true;
                        VariousSelected = true;
                        SonstigesSelected = true;
                    }
                    // If "Alle" is unchecked, reset individual selections
                    else
                    {
                        NachrichtenSelected = false;
                        HilfeSelected = false;
                        VariousSelected = false;
                        SonstigesSelected = false;
                    }

                    // Ensure the StartCommand is updated when state changes
                    CommandManager.InvalidateRequerySuggested();
                    StateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        // Properties for other checkboxes
        public bool NachrichtenSelected
        {
            get => _nachrichtenSelected;
            set
            {
                if (_nachrichtenSelected != value)
                {
                    _nachrichtenSelected = value;
                    OnPropertyChanged(nameof(NachrichtenSelected));

                    if (!_nachrichtenSelected)
                        AlleSelected = false;
                    else if (IsAllSelected())
                        AlleSelected = true;

                    CommandManager.InvalidateRequerySuggested(); // Refresh command availability
                    StateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public bool HilfeSelected
        {
            get => _hilfeSelected;
            set
            {
                if (_hilfeSelected != value)
                {
                    _hilfeSelected = value;
                    OnPropertyChanged(nameof(HilfeSelected));

                    if (!_hilfeSelected)
                        AlleSelected = false;
                    else if (IsAllSelected())
                        AlleSelected = true;

                    CommandManager.InvalidateRequerySuggested(); // Refresh command availability
                    StateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public bool VariousSelected
        {
            get => _variousSelected;
            set
            {
                if (_variousSelected != value)
                {
                    _variousSelected = value;
                    OnPropertyChanged(nameof(VariousSelected));

                    if (!_variousSelected)
                        AlleSelected = false;
                    else if (IsAllSelected())
                        AlleSelected = true;

                    CommandManager.InvalidateRequerySuggested(); // Refresh command availability
                    StateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public bool SonstigesSelected
        {
            get => _sonstigesSelected;
            set
            {
                if (_sonstigesSelected != value)
                {
                    _sonstigesSelected = value;
                    OnPropertyChanged(nameof(SonstigesSelected));

                    if (!_sonstigesSelected)
                        AlleSelected = false;
                    else if (IsAllSelected())
                        AlleSelected = true;

                    CommandManager.InvalidateRequerySuggested(); // Refresh command availability
                    StateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        // Helper method to check if all checkboxes are selected
        private bool IsAllSelected()
        {
            return NachrichtenSelected && HilfeSelected && VariousSelected && SonstigesSelected;
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        // For the File checkbox - End 

        // For the Language checkbox - Start

        // For the Language checkboxes
        private bool _allLanguagesSelected;
        private bool _germanSelected;
        private bool _englishSelected;
        private bool _frenchSelected;
        private bool _spanishSelected;
        private bool _italianSelected;
        private bool _dutchSelected;
        private bool _japaneseSelected;
        private bool _koreanSelected;

        //public event PropertyChangedEventHandler PropertyChanged;
        // Event to notify state change
        //public event EventHandler StateChanged;

        // "All Languages" checkbox
        public bool AllLanguagesSelected
        {
            get => _allLanguagesSelected;
            set
            {
                if (_allLanguagesSelected != value)
                {
                    _allLanguagesSelected = value;
                    OnPropertyChanged(nameof(AllLanguagesSelected));

                    // If "All" is selected, select all checkboxes
                    if (_allLanguagesSelected)
                    {
                        GermanSelected = true;
                        EnglishSelected = true;
                        FrenchSelected = true;
                        SpanishSelected = true;
                        ItalianSelected = true;
                        DutchSelected = true;
                        JapaneseSelected = true;
                        KoreanSelected = true;
                    }
                    // If "All" is unchecked, reset individual selections
                    else
                    {
                        GermanSelected = false;
                        EnglishSelected = false;
                        FrenchSelected = false;
                        SpanishSelected = false;
                        ItalianSelected = false;
                        DutchSelected = false;
                        JapaneseSelected = false;
                        KoreanSelected = false;
                    }

                    StateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        // Properties for other language checkboxes
        // #######################################################
        // #######################################################


        public bool GermanSelected
        {
            get => _germanSelected;
            set
            {
                if (_germanSelected != value)
                {
                    _germanSelected = value;
                    OnPropertyChanged(nameof(GermanSelected));

                    if (!_germanSelected) AllLanguagesSelected = false;
                    else if (IsAllLanguagesSelected()) AllLanguagesSelected = true;

                    StateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public bool EnglishSelected
        {
            get => _englishSelected;
            set
            {
                if (_englishSelected != value)
                {
                    _englishSelected = value;
                    OnPropertyChanged(nameof(EnglishSelected));

                    if (!_englishSelected) AllLanguagesSelected = false;
                    else if (IsAllLanguagesSelected()) AllLanguagesSelected = true;

                    StateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public bool FrenchSelected
        {
            get => _frenchSelected;
            set
            {
                if (_frenchSelected != value)
                {
                    _frenchSelected = value;
                    OnPropertyChanged(nameof(FrenchSelected));

                    if (!_frenchSelected) AllLanguagesSelected = false;
                    else if (IsAllLanguagesSelected()) AllLanguagesSelected = true;

                    StateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public bool SpanishSelected
        {
            get => _spanishSelected;
            set
            {
                if (_spanishSelected != value)
                {
                    _spanishSelected = value;
                    OnPropertyChanged(nameof(SpanishSelected));

                    if (!_spanishSelected) AllLanguagesSelected = false;
                    else if (IsAllLanguagesSelected()) AllLanguagesSelected = true;

                    StateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public bool ItalianSelected
        {
            get => _italianSelected;
            set
            {
                if (_italianSelected != value)
                {
                    _italianSelected = value;
                    OnPropertyChanged(nameof(ItalianSelected));

                    if (!_italianSelected) AllLanguagesSelected = false;
                    else if (IsAllLanguagesSelected()) AllLanguagesSelected = true;

                    StateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public bool DutchSelected
        {
            get => _dutchSelected;
            set
            {
                if (_dutchSelected != value)
                {
                    _dutchSelected = value;
                    OnPropertyChanged(nameof(DutchSelected));

                    if (!_dutchSelected) AllLanguagesSelected = false;
                    else if (IsAllLanguagesSelected()) AllLanguagesSelected = true;

                    StateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public bool JapaneseSelected
        {
            get => _japaneseSelected;
            set
            {
                if (_japaneseSelected != value)
                {
                    _japaneseSelected = value;
                    OnPropertyChanged(nameof(JapaneseSelected));

                    if (!_japaneseSelected) AllLanguagesSelected = false;
                    else if (IsAllLanguagesSelected()) AllLanguagesSelected = true;

                    StateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public bool KoreanSelected
        {
            get => _koreanSelected;
            set
            {
                if (_koreanSelected != value)
                {
                    _koreanSelected = value;
                    OnPropertyChanged(nameof(KoreanSelected));

                    if (!_koreanSelected) AllLanguagesSelected = false;
                    else if (IsAllLanguagesSelected()) AllLanguagesSelected = true;

                    StateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        // Helper method to check if all languages are selected
        private bool IsAllLanguagesSelected()
        {
            return GermanSelected && EnglishSelected && FrenchSelected && SpanishSelected &&
                   ItalianSelected && DutchSelected && JapaneseSelected && KoreanSelected;
        }

        // For the Language checkbox - End


        // ####################################################### - Command Part
        // #######################################################


        // ICommand for the "Start" button
        public ICommand StartCommand { get; }

        public FilesRibbonGroupViewModel()
        {
            StartCommand = new RelayCommand(ExecuteStartCommand, CanExecuteStartCommand);
        }

        // Method for Start button command
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
                EnglishSelected,
                FrenchSelected,
                SpanishSelected,
                ItalianSelected,
                GermanSelected,
                DutchSelected,
                KoreanSelected,
                JapaneseSelected
            };

            var message = $"Start button clicked.\n" +
                  $"File Section (IsChecked): \n" +
                  $"Alle: {selectedValuesFiles.AlleSelected}, Nachrichten: {selectedValuesFiles.NachrichtenSelected}, Hilfe: {selectedValuesFiles.HilfeSelected}, Various: {selectedValuesFiles.VariousSelected}, Sonstiges: {selectedValuesFiles.SonstigesSelected}\n" +
                  $"Language Section (IsChecked): \n" +
                  $"AllLanguages: {selectedValuesLanguages.AllLanguagesSelected}, German: {selectedValuesLanguages.GermanSelected}, English: {selectedValuesLanguages.EnglishSelected}, French: {selectedValuesLanguages.FrenchSelected}, Spanish: {selectedValuesLanguages.SpanishSelected}, Italian: {selectedValuesLanguages.ItalianSelected}, Dutch: {selectedValuesLanguages.DutchSelected}, Japanese: {selectedValuesLanguages.JapaneseSelected}, Korean: {selectedValuesLanguages.KoreanSelected}";

            //MessageBox.Show(message);
            ProcessFilesService.ProcessFilesMethod();

        }

        // Method to determine if StartCommand can execute (ensuring at least one checkbox is selected)
        private bool CanExecuteStartCommand(object parameter)
        {
            return NachrichtenSelected || HilfeSelected || VariousSelected || SonstigesSelected;
        }


    }
}

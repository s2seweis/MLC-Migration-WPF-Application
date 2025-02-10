using System.ComponentModel;

namespace RibbonDemo02.Models
{
    public class LanguageSelectionModel : INotifyPropertyChanged
    {
        private bool _allLanguagesSelected;
        private bool _germanSelected;
        private bool _englishSelected;
        private bool _spanishSelected;
        private bool _hungarianSelected;
        private bool _polishSelected;
        private bool _danishSelected;

        public bool AllLanguagesSelected
        {
            get => _allLanguagesSelected;
            set
            {
                if (_allLanguagesSelected != value)
                {
                    _allLanguagesSelected = value;
                    OnPropertyChanged(nameof(AllLanguagesSelected));
                    if (_allLanguagesSelected)
                    {
                        GermanSelected = true;
                        EnglishSelected = true;
                        SpanishSelected = true;
                        HungarianSelected = true;
                        PolishSelected = true;
                        DanishSelected = true;
                    }
                    else
                    {
                        GermanSelected = false;
                        EnglishSelected = false;
                        SpanishSelected = false;
                        HungarianSelected = false;
                        PolishSelected = false;
                        DanishSelected = false;
                    }
                }
            }
        }

        public bool GermanSelected
        {
            get => _germanSelected;
            set
            {
                if (_germanSelected != value)
                {
                    _germanSelected = value;
                    OnPropertyChanged(nameof(GermanSelected));
                    if (!_germanSelected)
                        AllLanguagesSelected = false;
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
                    if (!_englishSelected)
                        AllLanguagesSelected = false;
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
                    if (!_spanishSelected)
                        AllLanguagesSelected = false;
                }
            }
        }

        public bool HungarianSelected
        {
            get => _hungarianSelected;
            set
            {
                if (_hungarianSelected != value)
                {
                    _hungarianSelected = value;
                    OnPropertyChanged(nameof(HungarianSelected));
                    if (!_hungarianSelected)
                        AllLanguagesSelected = false;
                }
            }
        }

        public bool PolishSelected
        {
            get => _polishSelected;
            set
            {
                if (_polishSelected != value)
                {
                    _polishSelected = value;
                    OnPropertyChanged(nameof(PolishSelected));
                    if (!_polishSelected)
                        AllLanguagesSelected = false;
                }
            }
        }

        public bool DanishSelected
        {
            get => _danishSelected;
            set
            {
                if (_danishSelected != value)
                {
                    _danishSelected = value;
                    OnPropertyChanged(nameof(DanishSelected));
                    if (!_danishSelected)
                        AllLanguagesSelected = false;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

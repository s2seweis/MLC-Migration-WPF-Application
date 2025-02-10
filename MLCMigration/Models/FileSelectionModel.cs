using System.ComponentModel;

namespace RibbonDemo02.Models
{
    public class FileSelectionModel : INotifyPropertyChanged
    {
        private bool _alleSelected;
        private bool _nachrichtenSelected;
        private bool _hilfeSelected;
        private bool _variousSelected;
        private bool _sonstigesSelected;

        /// <summary>
        /// Gets or sets whether all file selections are checked.
        /// When set to true, all individual selections are also checked.
        /// </summary>
        public bool AlleSelected
        {
            get => _alleSelected;
            set
            {
                if (_alleSelected != value)
                {
                    _alleSelected = value;
                    OnPropertyChanged(nameof(AlleSelected));

                    // If "Alle" is checked, mark all as selected.
                    if (_alleSelected)
                    {
                        NachrichtenSelected = true;
                        HilfeSelected = true;
                        VariousSelected = true;
                        SonstigesSelected = true;
                    }
                    else
                    {
                        NachrichtenSelected = false;
                        HilfeSelected = false;
                        VariousSelected = false;
                        SonstigesSelected = false;
                    }
                }
            }
        }

        public bool NachrichtenSelected
        {
            get => _nachrichtenSelected;
            set
            {
                if (_nachrichtenSelected != value)
                {
                    _nachrichtenSelected = value;
                    OnPropertyChanged(nameof(NachrichtenSelected));
                    // If an individual checkbox is unchecked, uncheck "Alle".
                    if (!_nachrichtenSelected)
                        AlleSelected = false;
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
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

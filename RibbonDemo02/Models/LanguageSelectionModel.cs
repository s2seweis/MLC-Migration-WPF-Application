using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RibbonDemo02.Models
{
    internal class LanguageSelectionModel
    {
        public bool AllLanguagesSelected { get; set; }
        public bool GermanSelected { get; set; }
        public bool EnglishSelected { get; set; }
        public bool FrenchSelected { get; set; }
        public bool SpanishSelected { get; set; }
        public bool ItalianSelected { get; set; }
        public bool DutchSelected { get; set; }
        public bool JapaneseSelected { get; set; }
        public bool KoreanSelected { get; set; }

        public LanguageSelectionModel()
        {
            // Initialize with default values if necessary
            AllLanguagesSelected = false;
            GermanSelected = false;
            EnglishSelected = false;
            FrenchSelected = false;
            SpanishSelected = false;
            ItalianSelected = false;
            DutchSelected = false;
            JapaneseSelected = false;
            KoreanSelected = false;
        }

        // Method to reset language selections based on the "AllLanguagesSelected" flag
        public void UpdateLanguageSelections()
        {
            if (AllLanguagesSelected)
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
        }

        // Method to check if all languages are selected
        public bool IsAllLanguagesSelected()
        {
            return GermanSelected && EnglishSelected && FrenchSelected &&
                   SpanishSelected && ItalianSelected && DutchSelected &&
                   JapaneseSelected && KoreanSelected;
        }

        // Method to update "AllLanguagesSelected" when individual languages change
        public void CheckAllLanguagesSelected()
        {
            AllLanguagesSelected = IsAllLanguagesSelected();
        }
    }
}

namespace RibbonDemo02.Models
{
    public class FilesSelectionModel
    {
        public bool AlleSelected { get; set; }
        public bool NachrichtenSelected { get; set; }
        public bool HilfeSelected { get; set; }
        public bool ReminderSelected { get; set; }
        public bool VariousSelected { get; set; }
        public bool SonstigesSelected { get; set; }

        // Method to update selection based on the "Alle" checkbox
        public void UpdateAlleSelection()
        {
            if (AlleSelected)
            {
                NachrichtenSelected = true;
                HilfeSelected = true;
                ReminderSelected = true;
                VariousSelected = true;
                SonstigesSelected = true;
            }
            else
            {
                NachrichtenSelected = false;
                HilfeSelected = false;
                ReminderSelected = false;
                VariousSelected = false;
                SonstigesSelected = false;
            }
        }

        // Method to check if "Alle" should be selected based on other selections
        public void UpdateAlleIfNecessary()
        {
            AlleSelected = NachrichtenSelected && HilfeSelected && ReminderSelected && VariousSelected && SonstigesSelected;
        }
    }
}

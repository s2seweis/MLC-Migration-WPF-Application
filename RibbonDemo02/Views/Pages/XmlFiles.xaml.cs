using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using RibbonDemo02.Helpers;

namespace RibbonDemo02.Views
{
    public partial class XmlFiles : UserControl
    {
        public XmlFiles()
        {
            InitializeComponent();
            // Pfad für das Verzeichnis, das geladen werden soll
            string path = @"C:\Users\SWE\source\repos\11. MLC Migration WPF\RibbonDemo02\_mlc\dynamic\1";

            try
            {
                // Verwenden von DirectoryHelper, um die Struktur zu laden
                DirectoryHelper.LoadDirectoryStructure(path, DirectoryTreeView);
            }
            catch (DirectoryNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // Ereignis zuordnen
            DirectoryTreeView.SelectedItemChanged += DirectoryTreeView_SelectedItemChanged;
        }

        private void DirectoryTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is TreeViewItem selectedItem)
            {
                string selectedPath = selectedItem.Tag as string;

                if (selectedPath != null && File.Exists(selectedPath))
                {
                    try
                    {
                        // Dateiinhalt lesen und im AvalonEdit-TextEditor anzeigen
                        string fileContent = File.ReadAllText(selectedPath);
                        TextEditor.Text = fileContent;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Fehler beim Laden der Datei: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}

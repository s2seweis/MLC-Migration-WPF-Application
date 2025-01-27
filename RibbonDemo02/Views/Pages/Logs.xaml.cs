using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using RibbonDemo02.Helpers;

namespace RibbonDemo02.Views
{
    public partial class Logs : UserControl
    {
        public Logs()
        {
            InitializeComponent();
            // Pfad für das Verzeichnis, das geladen werden soll
            //string path = @"C:\Users\weiss\source\repos\Logs";
            string path = @"C:\Users\SWE\source\repos\wpf-mvvm-demos\RibbonDemo02\RibbonDemo02\Assets\Logs\";

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

        // Wenn ein Item im TreeView ausgewählt wird (z.B. eine Datei)
        private void DirectoryTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // Sicherstellen, dass das ausgewählte Element eine Datei ist
            if (e.NewValue is TreeViewItem selectedItem)
            {
                string selectedPath = selectedItem.Tag as string;

                if (selectedPath != null && File.Exists(selectedPath))
                {
                    try
                    {
                        // Inhalt der ausgewählten Datei lesen und im TextBox anzeigen
                        string fileContent = File.ReadAllText(selectedPath);
                        FileContentTextBox.Text = fileContent;
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

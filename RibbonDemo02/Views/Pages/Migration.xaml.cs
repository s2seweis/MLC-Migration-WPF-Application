using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using RibbonDemo02.Helpers;

namespace RibbonDemo02.Views
{
    public partial class Migration : UserControl
    {
        public Migration()
        {
            InitializeComponent();
            // Pfad für das Verzeichnis, das geladen werden soll
            string path = @"C:\Users\SWE\source\repos\11. MLC Migration WPF\RibbonDemo02\_mlc\dynamic\1\";
            try
            {
                // Verwenden von DirectoryHelper, um die Struktur zu laden
                //DirectoryHelper.LoadDirectoryStructure(path, DirectoryTreeViewMigration);
            }
            catch (DirectoryNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

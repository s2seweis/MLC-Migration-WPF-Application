using RibbonDemo02.ViewModels;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace RibbonDemo02
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Shared ViewModel instance to maintain global state across the application
        public static FilesRibbonGroupViewModel SharedViewModel { get; } = new FilesRibbonGroupViewModel();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Versuche die Dateien zu initialisieren, ignoriere Fehler, wenn sie nicht gefunden werden
            try
            {
                InitializeApp();
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung, falls beim Starten Probleme auftreten
                MessageBox.Show($"Fehler beim Initialisieren der Anwendung: {ex.Message}",
                                "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            // Hauptfenster der Anwendung anzeigen
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void InitializeApp()
        {
            // Beispielhafte Datei und Ordnersuche
            string xmlDirectory = @"C:\Path\To\Files\";

            // Überprüfen, ob das Verzeichnis existiert, und eine Warnung ausgeben, wenn es nicht vorhanden ist
            if (!Directory.Exists(xmlDirectory))
            {
                MessageBox.Show("Das benötigte Verzeichnis wurde nicht gefunden. Die Anwendung wird mit reduzierten Funktionen gestartet.",
                                "Verzeichnis fehlt", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                // Optional: Weitere Initialisierungslogik hinzufügen, um die benötigten Dateien zu laden
                // Diese Logik kann angepasst werden, um mit der verschobenen Ordnerstruktur umzugehen
            }
        }
    }

    // Application-wide state for managing admin rights
    public class AppState : INotifyPropertyChanged
    {
        // Private backing field for the IsAdmin property
        private static bool _isAdmin;

        /// <summary>
        /// Indicates whether admin rights are enabled.
        /// </summary>
        public static bool IsAdmin
        {
            get => _isAdmin;
            set
            {
                if (_isAdmin != value) // Only raise the event if the value changes
                {
                    _isAdmin = value;
                    OnStaticPropertyChanged(nameof(IsAdmin));
                }
            }
        }

        /// <summary>
        /// Event triggered when a static property changes.
        /// </summary>
        public static event PropertyChangedEventHandler StaticPropertyChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the StaticPropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        private static void OnStaticPropertyChanged(string propertyName)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }
    }
}

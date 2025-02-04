using RibbonDemo02.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;
using RibbonDemo02.Helpers; // Logger importieren

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

            // Logger initialisieren und ersten Eintrag schreiben
            LoggerHelper.Log("Application started.");
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
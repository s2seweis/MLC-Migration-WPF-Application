using RibbonDemo02.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;

namespace RibbonDemo02
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = App.SharedViewModel;

        }

        private void ViewModel_StateChanged(object sender, EventArgs e)
        {
            // Logic when state changes (checkbox selections)
            MessageBox.Show("State changed!");  // Just as an example, replace with real logic
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is Ribbon)
            {
                var selectedTab = (RibbonTab)((Ribbon)sender).SelectedItem;
                switch (selectedTab.Header)
                {
                    case "Übertragung":
                        ContentControlArea.Content = new Views.Migration();
                        break;
                    case "XML Editor":
                        ContentControlArea.Content = new Views.XmlFiles();
                        break;
                    case "Log":
                        ContentControlArea.Content = new Views.Logs();
                        break;
                    case "Datenbank":
                        ContentControlArea.Content = new Views.DatabaseView();
                        break;

                }
            }
        }
        private void RibbonMenuItem_SettingsClick(object sender, RoutedEventArgs e)
        {
        }
        private void RibbonMenuItem_CloseClick(object sender, RoutedEventArgs e)
        {
        }
    }
}


// Xml files will not be added to the projected,
// just copy and paste to the project,
// by this the program will not miss them when they moved to an other location
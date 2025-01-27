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
            // Access the DataContext (FilesRibbonGroupViewModel)
            //var viewModel = new FilesRibbonGroupViewModel();

            //viewModel.StateChanged += ViewModel_StateChanged;

            // Subscribe to the StaticPropertyChanged event to catch changes in IsAdmin
            AppState.StaticPropertyChanged += AppState_StaticPropertyChanged;

            //this.DataContext = this; // Set data context to bind the command
            //this.DataContext = new FilesRibbonGroupViewModel();

            // Use the shared ViewModel
            DataContext = App.SharedViewModel;

        }

        private void AppState_StaticPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AppState.IsAdmin))
            {
                // Manually update UI bindings to reflect the IsAdmin change
                Dispatcher.Invoke(() =>
                {
                    // Refreshing the UI by re-setting the DataContext
                    this.DataContext = this.DataContext;
                });
            }
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
                    case "Migration":
                        ContentControlArea.Content = new Views.Migration();
                        break;
                    case "XML Files":
                        ContentControlArea.Content = new Views.XmlFiles();
                        break;
                    case "Logs":
                        ContentControlArea.Content = new Views.Logs();
                        break;
                    case "Database":
                        ContentControlArea.Content = new Views.DatabaseView();
                        break;

                }
            }
        }

        private void RibbonMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Prompt for password
            string password = PromptForPassword("Enter admin password:", "Admin Authentication");

            if (password == "1234") // Replace with your actual admin password logic
            {
                // Display the admin rights toggle dialog
                ToggleAdminRights();
            }
            else
            {
                MessageBox.Show("Invalid password.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RibbonMenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            // Display a confirmation MessageBox with Yes and No options.
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to exit?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            // Check the result of the MessageBox
            if (result == MessageBoxResult.Yes)
            {
                // Close the application if the user clicks "Yes"
                Application.Current.Shutdown();
            }
            // If "No" is clicked, simply do nothing and the MessageBox will close.
        }

        private string PromptForPassword(string message, string title)
        {
            // Create an input dialog box
            var inputBox = new System.Windows.Controls.TextBox
            {
                Margin = new Thickness(10),
                Width = 200,
                MaxLength = 20,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Left,
            };

            var panel = new StackPanel();
            panel.Children.Add(new TextBlock { Text = message });
            panel.Children.Add(inputBox);

            var window = new Window
            {
                Title = title,
                Content = panel,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize,
            };

            var okButton = new Button { Content = "OK", IsDefault = true, Margin = new Thickness(5) };
            okButton.Click += (s, e) => window.DialogResult = true;
            panel.Children.Add(okButton);

            return window.ShowDialog() == true ? inputBox.Text : string.Empty;
        }

        private void ToggleAdminRights()
        {
            StackPanel panel = new StackPanel { Margin = new Thickness(10) };

            CheckBox checkBox = new CheckBox
            {
                Content = "Enable Admin Rights",
                IsChecked = AppState.IsAdmin,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(5)
            };

            panel.Children.Add(checkBox);

            Button okButton = new Button
            {
                Content = "OK",
                IsDefault = true,
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Right,
                Padding = new Thickness(10, 5, 10, 5)
            };

            panel.Children.Add(okButton);

            Window dialog = new Window
            {
                Title = "Admin Rights",
                Content = panel,
                Width = 300,
                Height = 150,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize
            };

            okButton.Click += (s, e) =>
            {
                AppState.IsAdmin = checkBox.IsChecked ?? false;
                dialog.Close();
            };

            dialog.ShowDialog();
        }
    }
}

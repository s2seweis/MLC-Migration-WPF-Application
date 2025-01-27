using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RibbonDemo02.Views.UserControls
{
    /// <summary>
    /// Interaktionslogik für FilesRibbonGroup.xaml
    /// </summary>
    public partial class FilesRibbonGroup : UserControl
    {
        public FilesRibbonGroup()
        {
            InitializeComponent();

            DataContext = App.SharedViewModel;

        }
    }
}

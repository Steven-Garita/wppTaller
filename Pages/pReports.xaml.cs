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

namespace wpp_Taller.Pages
{
    /// <summary>
    /// Lógica de interacción para pReports.xaml
    /// </summary>
    public partial class pReports : Page
    {
        public pReports()
        {
            InitializeComponent();
        }

        private void btnCarsRepared_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new pViewReports());
        }

        private void btnInventorySold_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new pInvetorySoldReport());
        }

        private void btnPorforms_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

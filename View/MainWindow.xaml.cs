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
using wpp_Taller.View;
using System.ComponentModel;
using wpp_Taller.Pages;
using wpp_Taller.Model;
using System.Windows.Media.Animation;

namespace wpp_Taller
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow(int licenseUser)
        {
            InitializeComponent();
            if (licenseUser != 1)
            {
                btnUsers0.Visibility = Visibility.Hidden;
                btnUsers1.Visibility = Visibility.Hidden;
            }
        }

        #region btnFunctions

        private void btnStart()
        {
            grdPrincipal.Opacity = 0.35;
            frPage.Visibility = Visibility.Hidden;
        }

        private void btnCars()
        {
            frPage.Visibility = Visibility.Visible;
            frPage.NavigationService.Navigate(new pCarPending());
            grdPrincipal.Opacity = 100;
        }

        private void btnClients()
        {
            frPage.Visibility = Visibility.Visible;
            frPage.NavigationService.Navigate(new pClients());
            grdPrincipal.Opacity = 100;
        }
        private void btnInventory()
        {
            frPage.Visibility = Visibility.Visible;
            frPage.NavigationService.Navigate(new pInventory());
            grdPrincipal.Opacity = 100;
        }
        public void btnUsers()
        {
            frPage.Visibility = Visibility.Visible;
            frPage.NavigationService.Navigate(new pUsers());
            grdPrincipal.Opacity = 100;
        }

        public void btnReports()
        {
            frPage.Visibility = Visibility.Visible;
            frPage.NavigationService.Navigate(new pReports());
            grdPrincipal.Opacity = 100;
        }

        #endregion

        #region btns
        private void btnStart0_Click(object sender, RoutedEventArgs e)
        {
            btnStart();
        }

        private void btnStart1_Click(object sender, RoutedEventArgs e)
        {
            btnStart();
        }

        private void btnCars0_Click(object sender, RoutedEventArgs e)
        {
            btnCars();
        }

        private void btnCars1_Click(object sender, RoutedEventArgs e)
        {
            btnCars();
        }

        private void btnClients0_Click(object sender, RoutedEventArgs e)
        {
            btnClients();
        }

        private void btnClients1_Click(object sender, RoutedEventArgs e)
        {
            btnClients();
        }
        private void btnInventory0_Click(object sender, RoutedEventArgs e)
        {
            btnInventory(); 
        }

        private void btnInventory1_Click(object sender, RoutedEventArgs e)
        {
            btnInventory();
        }
        private void btnUsers0_Click(object sender, RoutedEventArgs e)
        {
            btnUsers();
        }

        private void btnUsers1_Click(object sender, RoutedEventArgs e)
        {
            btnUsers();
        }
        #endregion

        private void btnReports0_Click(object sender, RoutedEventArgs e)
        {
            btnReports();
        }

        private void btnReports1_Click(object sender, RoutedEventArgs e)
        {
            btnReports();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            LoginView lv = new LoginView();
            lv.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(1)
            };

            this.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
        }
    }
}

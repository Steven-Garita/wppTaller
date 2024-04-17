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
using wpp_Taller.Pages;
using MySql.Data.MySqlClient;
using System.Data;
using wpp_Taller.Model;
using wpp_Taller.View;


namespace wpp_Taller.Pages
{
    /// <summary>
    /// Lógica de interacción para pCarReady.xaml
    /// </summary>
    public partial class pCarReady : Page
    {
        string idAuto;
        int cont = 2;
        clsCars Cars = new clsCars();

        public pCarReady(string id)
        {
            InitializeComponent();
            idAuto = id;
        }
        

        private void btnAddSpare_Click(object sender, RoutedEventArgs e)
        {
            if (cont <= 15)
            {
                string txtNameSpare = "txtSpare" + cont;
                string txtPrice = "txtPrice" + cont;

                TextBox textBoxSpare = (TextBox)FindName(txtNameSpare);
                textBoxSpare.Visibility = Visibility.Visible;
                textBoxSpare.VerticalContentAlignment = VerticalAlignment.Center;
                textBoxSpare.HorizontalContentAlignment = HorizontalAlignment.Center;

                TextBox textBoxPrice = (TextBox)FindName(txtPrice);
                textBoxPrice.Visibility = Visibility.Visible;
                textBoxPrice.VerticalContentAlignment = VerticalAlignment.Center;
                textBoxPrice.HorizontalContentAlignment = HorizontalAlignment.Center;

                cont++;
            }
        }

        private void btnConfirmCarReady_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection conn = ClsConnection.conect();

                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_RemoveCar", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                for (int i = 1; i < 16; i++)
                {
                    string txtPrice = "txtPrice" + i;
                    TextBox textBoxPrice = (TextBox)FindName(txtPrice);

                    if (textBoxPrice.Text == "")
                    {
                        textBoxPrice.Text = "0";
                    }
                }

                Cars.idAuto = int.Parse(idAuto);
                Cars.repuesto1 = txtSpare1.Text;
                Cars.repuesto2 = txtSpare2.Text;
                Cars.repuesto3 = txtSpare3.Text;
                Cars.repuesto4 = txtSpare4.Text;
                Cars.repuesto5 = txtSpare5.Text;
                Cars.repuesto6 = txtSpare6.Text;
                Cars.repuesto7 = txtSpare7.Text;
                Cars.repuesto8 = txtSpare8.Text;
                Cars.repuesto9 = txtSpare9.Text;
                Cars.repuesto10 = txtSpare10.Text;
                Cars.repuesto11 = txtSpare11.Text;
                Cars.repuesto12 = txtSpare12.Text;
                Cars.repuesto13 = txtSpare13.Text;
                Cars.repuesto14 = txtSpare14.Text;
                Cars.repuesto15 = txtSpare15.Text;
                Cars.precio1 = int.Parse(txtPrice1.Text);
                Cars.precio2 = int.Parse(txtPrice2.Text);
                Cars.precio3 = int.Parse(txtPrice3.Text);
                Cars.precio4 = int.Parse(txtPrice4.Text);
                Cars.precio5 = int.Parse(txtPrice5.Text);
                Cars.precio6 = int.Parse(txtPrice6.Text);
                Cars.precio7 = int.Parse(txtPrice7.Text);
                Cars.precio8 = int.Parse(txtPrice8.Text);
                Cars.precio9 = int.Parse(txtPrice9.Text);
                Cars.precio10 = int.Parse(txtPrice10.Text);
                Cars.precio11 = int.Parse(txtPrice11.Text);
                Cars.precio12 = int.Parse(txtPrice12.Text);
                Cars.precio13 = int.Parse(txtPrice13.Text);
                Cars.precio14 = int.Parse(txtPrice14.Text);
                Cars.precio15 = int.Parse(txtPrice15.Text);
                Cars.reparacionesRealizadas1 = txtRepairsPerformed1.Text;
                Cars.reparacionesRealizadas2 = txtRepairsPerformed2.Text;
                Cars.reparacionesRealizadas3 = txtRepairsPerformed3.Text;
                Cars.reparacionesRealizadas4 = txtRepairsPerformed4.Text;
                Cars.reparacionesRealizadas5 = txtRepairsPerformed5.Text;

                if (txtLaborPrice.Text == "")
                {
                    Cars.precioManoObra = 0;
                }
                else
                {
                    Cars.precioManoObra = int.Parse(txtLaborPrice.Text);
                }

                cmd.Parameters.AddWithValue("@pId", Cars.idAuto);
                cmd.Parameters.AddWithValue("@pReparacionesRealizadas1", Cars.reparacionesRealizadas1);
                cmd.Parameters.AddWithValue("@pReparacionesRealizadas2", Cars.reparacionesRealizadas2);
                cmd.Parameters.AddWithValue("@pReparacionesRealizadas3", Cars.reparacionesRealizadas3);
                cmd.Parameters.AddWithValue("@pReparacionesRealizadas4", Cars.reparacionesRealizadas4);
                cmd.Parameters.AddWithValue("@pReparacionesRealizadas5", Cars.reparacionesRealizadas5);
                cmd.Parameters.AddWithValue("@pManoObra", Cars.precioManoObra);
                cmd.Parameters.AddWithValue("@pRepuesto1", Cars.repuesto1);
                cmd.Parameters.AddWithValue("@pRepuesto2", Cars.repuesto2);
                cmd.Parameters.AddWithValue("@pRepuesto3", Cars.repuesto3);
                cmd.Parameters.AddWithValue("@pRepuesto4", Cars.repuesto4);
                cmd.Parameters.AddWithValue("@pRepuesto5", Cars.repuesto5);
                cmd.Parameters.AddWithValue("@pRepuesto6", Cars.repuesto6);
                cmd.Parameters.AddWithValue("@pRepuesto7", Cars.repuesto7);
                cmd.Parameters.AddWithValue("@pRepuesto8", Cars.repuesto8);
                cmd.Parameters.AddWithValue("@pRepuesto9", Cars.repuesto9);
                cmd.Parameters.AddWithValue("@pRepuesto10", Cars.repuesto10);
                cmd.Parameters.AddWithValue("@pRepuesto11", Cars.repuesto11);
                cmd.Parameters.AddWithValue("@pRepuesto12", Cars.repuesto12);
                cmd.Parameters.AddWithValue("@pRepuesto13", Cars.repuesto13);
                cmd.Parameters.AddWithValue("@pRepuesto14", Cars.repuesto14);
                cmd.Parameters.AddWithValue("@pRepuesto15", Cars.repuesto15);
                cmd.Parameters.AddWithValue("@pPrecio1", Cars.precio1);
                cmd.Parameters.AddWithValue("@pPrecio2", Cars.precio2);
                cmd.Parameters.AddWithValue("@pPrecio3", Cars.precio3);
                cmd.Parameters.AddWithValue("@pPrecio4", Cars.precio4);
                cmd.Parameters.AddWithValue("@pPrecio5", Cars.precio5);
                cmd.Parameters.AddWithValue("@pPrecio6", Cars.precio6);
                cmd.Parameters.AddWithValue("@pPrecio7", Cars.precio7);
                cmd.Parameters.AddWithValue("@pPrecio8", Cars.precio8);
                cmd.Parameters.AddWithValue("@pPrecio9", Cars.precio9);
                cmd.Parameters.AddWithValue("@pPrecio10", Cars.precio10);
                cmd.Parameters.AddWithValue("@pPrecio11", Cars.precio11);
                cmd.Parameters.AddWithValue("@pPrecio12", Cars.precio12);
                cmd.Parameters.AddWithValue("@pPrecio13", Cars.precio13);
                cmd.Parameters.AddWithValue("@pPrecio14", Cars.precio14);
                cmd.Parameters.AddWithValue("@pPrecio15", Cars.precio15);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Auto registrado como reparado.");
                conn.Close();

                NavigationService?.Navigate(new pCarPending());
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new pCarPending());
        }
    }
}

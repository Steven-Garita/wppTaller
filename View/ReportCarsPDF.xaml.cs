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
using System.Windows.Shapes;
using wpp_Taller.Pages;
using MySql.Data.MySqlClient;
using System.Data;
using wpp_Taller.Model;
using wpp_Taller.View;

namespace wpp_Taller.View
{
    /// <summary>
    /// Lógica de interacción para ReportCarsPDF.xaml
    /// </summary>
    public partial class ReportCarsPDF : Window
    {
        int idCar;
        int idClient;
        string aux;

        public ReportCarsPDF(int idCa, int idCl)
        {
            InitializeComponent();
            idCar = idCa;
            idClient = idCl;
            fillReport();
        }

        private void fillReport()
        {
            MySqlConnection conn = ClsConnection.conect();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_fillReportClient", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pIdClient", idClient);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txbClient.Text = reader["Cliente"].ToString();
                    txbPhone.Text = reader["numeroTelefono"].ToString();

                    aux = txbClient.Text;
                }
                
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            conn.Close();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_fillReportCar", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pIdCar", idCar);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txbBrandCar.Text = reader["marca"].ToString();
                    txbModelCar.Text = reader["modelo"].ToString();
                    txbEntryDate.Text = reader["fechaDeIngreso"].ToString();
                    txbExitDate.Text = reader["fechaDeRetiro"].ToString();
                    txbRepair1.Text = reader["reparacionesRealizadas1"].ToString();
                    txbRepair2.Text = reader["reparacionesRealizadas2"].ToString();
                    txbRepair3.Text = reader["reparacionesRealizadas3"].ToString();
                    txbRepair4.Text = reader["reparacionesRealizadas4"].ToString();
                    txbRepair5.Text = reader["reparacionesRealizadas5"].ToString();

                    string labor = reader["precioManoObra"].ToString();
                    decimal laborDecimal = Convert.ToDecimal(labor);
                    string colonLabor = string.Format("₡ {0:N2}", laborDecimal);
                    txbLabor.Text = colonLabor;

                    string total = reader["totalGeneral"].ToString();
                    decimal totalDecimal = Convert.ToDecimal(total);
                    string colonTotal = string.Format("₡ {0:N2}", totalDecimal);
                    txbTotal.Text = colonTotal;
                }

                fillSpares();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            conn.Close();
        }

        private void fillSpares()
        {
            MySqlConnection conn = ClsConnection.conect();

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("sp_fillReportCar", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pIdCar", idCar);

                MySqlDataReader reader = cmd.ExecuteReader();

                TextBlock[] txbsSpares = new TextBlock[15];
                txbsSpares[0] = txbSpare1;
                txbsSpares[1] = txbSpare2;
                txbsSpares[2] = txbSpare3;
                txbsSpares[3] = txbSpare4;
                txbsSpares[4] = txbSpare5;
                txbsSpares[5] = txbSpare6;
                txbsSpares[6] = txbSpare7;
                txbsSpares[7] = txbSpare8;
                txbsSpares[8] = txbSpare9;
                txbsSpares[9] = txbSpare10;
                txbsSpares[10] = txbSpare11;
                txbsSpares[11] = txbSpare12;
                txbsSpares[12] = txbSpare13;
                txbsSpares[13] = txbSpare14;
                txbsSpares[14] = txbSpare15;

                int contSpares = 0;

                for (int value = 1; value < 16; value++)
                {
                    reader.Read();
                    string spare = reader["repuesto" + value].ToString();
                    txbsSpares[contSpares].Text = spare;
                    contSpares++;
                }

                TextBlock[] txbsPrices = new TextBlock[15];
                txbsPrices[0] = txbPrice1;
                txbsPrices[1] = txbPrice2;
                txbsPrices[2] = txbPrice3;
                txbsPrices[3] = txbPrice4;
                txbsPrices[4] = txbPrice5;
                txbsPrices[5] = txbPrice6;
                txbsPrices[6] = txbPrice7;
                txbsPrices[7] = txbPrice8;
                txbsPrices[8] = txbPrice9;
                txbsPrices[9] = txbPrice10;
                txbsPrices[10] = txbPrice11;
                txbsPrices[11] = txbPrice12;
                txbsPrices[12] = txbPrice13;
                txbsPrices[13] = txbPrice14;
                txbsPrices[14] = txbPrice15;

                int contPrices = 0;

                for (int value = 1; value < 16; value++)
                {
                    reader.Read();
                    string price = reader["precio" + value].ToString();
                    decimal priceDecimal = Convert.ToDecimal(price);
                    string colonPrice = string.Format("₡ {0:N2}", priceDecimal);
                    if (price == "0.00")
                    {
                        txbsPrices[contPrices].Text = "";
                    }
                    else
                    {
                        txbsPrices[contPrices].Text = colonPrice;
                    }
                    contPrices++;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error" + ex.ToString());
            }
            conn.Close();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                grdButton.Visibility = Visibility.Hidden;
                this.IsEnabled = false;
                PrintDialog print = new PrintDialog();
                if (print.ShowDialog() == true)
                {
                    print.PrintVisual(grdReport, aux);
                }
                this.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

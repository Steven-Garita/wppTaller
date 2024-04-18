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
using wpp_Taller;
using MySql.Data.MySqlClient;
using System.Data;

namespace wpp_Taller.View
{
    /// <summary>
    /// Lógica de interacción para ReportProfromPDF.xaml
    /// </summary>
    public partial class ReportProfromPDF : Window
    {
        int idProform;
        public ReportProfromPDF(int idP)
        {
            InitializeComponent();
            idProform = idP;
        }

        private void fillReport()
        {
            MySqlConnection conn = ClsConnection.conect();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("fillReportProform", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pIdProform", idProform);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txbClient.Text = reader["nombreCliente"].ToString();
                    txbBrandCar.Text = reader["marcaAuto"].ToString();
                    txbModelCar.Text = reader["modelCar"].ToString();
                    txbDate.Text = reader["fechaProforma"].ToString();
                    txbPhone.Text = reader["telefono"].ToString();
                    txbRepair1.Text = reader["reparacionesRealizar1"].ToString();
                    txbRepair2.Text = reader["reparacionesRealizar2"].ToString();
                    txbRepair3.Text = reader["reparacionesRealizar3"].ToString();
                    txbRepair4.Text = reader["reparacionesRealizar4"].ToString();
                    txbRepair5.Text = reader["reparacionesRealizar5"].ToString();

                    string labor = reader["precioManoObra"].ToString();
                    decimal laborDecimal = Convert.ToDecimal(labor);
                    string colonLabor = string.Format("₡ {0:N2}", laborDecimal);
                    txbLabor.Text = colonLabor;

                    string total = reader["totalGeneral"].ToString();
                    decimal totalDecimal = Convert.ToDecimal(total);
                    string colonTotal = string.Format("₡ {0:N2}", totalDecimal);
                    txbTotal.Text = colonTotal;

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

                    int contSpares = 0;

                    for (int value = 1; value < 11; value++)
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

                    int contPrices = 0;

                    for (int value = 1; value < 11; value++)
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
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error " + ex.ToString());
            }
            conn.Close();
        }

    }
}

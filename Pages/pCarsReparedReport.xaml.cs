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
    /// Lógica de interacción para pViewReports.xaml
    /// </summary>
    public partial class pViewReports : Page
    {
        clsCars Cars = new clsCars();
        public pViewReports()
        {
            InitializeComponent();
            showCarsRepared();
        }

        private void showCarsRepared()
        {
            MySqlConnection conn = ClsConnection.conect();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_ShowCarRepared", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dtgCarsRepared.ItemsSource = dt.DefaultView;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            conn.Close();
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            ReportCarsPDF report = new ReportCarsPDF(Cars.idAuto, Cars.idCliente);

            report.ShowDialog();
        }

        private void dtgCarsRepared_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgCarsRepared.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)dtgCarsRepared.SelectedItem;

                Cars.idAuto = Convert.ToInt32(selectedRow[0].ToString());
                Cars.idCliente = Convert.ToInt32((string)selectedRow[7].ToString());
            }
        }

        private void dtgCarsRepared_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Height = 35;
            e.Row.FontSize = 16;
        }
    }
}

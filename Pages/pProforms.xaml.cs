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
    /// Lógica de interacción para pProforms.xaml
    /// </summary>
    public partial class pProforms : Page
    {
        clsProforms Proforms = new clsProforms();

        public pProforms()
        {
            InitializeComponent();
            showProforms();
        }

        private void showProforms()
        {
            MySqlConnection conn = ClsConnection.conect();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_showProforms", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dtgProforms.ItemsSource = dt.DefaultView;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            conn.Close();
        }

        private void btnViewProform_Click(object sender, RoutedEventArgs e)
        {
            ReportProfromPDF reportProformPDF = new ReportProfromPDF(Proforms.idProforma);
            reportProformPDF.showDialog();
        }

        private void dtgProforms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgProforms.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)dtgProforms.SelectedItem;
                Proforms.idProforma = Convert.ToInt32(selectedRow[0].ToString());
            }
        }

        private void dtgProforms_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Height = 35;
            e.Row.FontSize = 16;
        }
    }
}

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
using MySql.Data.MySqlClient;
using System.Data;
using wpp_Taller.Model;
using wpp_Taller.View;

namespace wpp_Taller.Pages
{
    /// <summary>
    /// Lógica de interacción para pInvetorySoldReport.xaml
    /// </summary>
    public partial class pInvetorySoldReport : Page
    {
        public pInvetorySoldReport()
        {
            InitializeComponent();
            showInventorySold();
        }

        public void showInventorySold()
        {
            MySqlConnection conn = ClsConnection.conect();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_ShowInventorySold", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dtgInventory.ItemsSource = dt.DefaultView;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            conn.Close();
        }

        private void dtgInventory_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Height = 35;
            e.Row.FontSize = 16;
        }
    }
}

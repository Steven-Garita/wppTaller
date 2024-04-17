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
using MySql.Data.MySqlClient;
using System.Data;
using wpp_Taller.Model;
using wpp_Taller.View;
using wpp_Taller.Pages;

namespace wpp_Taller.View
{
    /// <summary>
    /// Lógica de interacción para ModalView.xaml
    /// </summary>
    public partial class ModalView : Window
    {
        string idInventario;
        int undsInv;
        pInventory pInventory = new pInventory();
        public ModalView(string id, int unds)
        {
            InitializeComponent();
            idInventario = id;
            undsInv = unds;
        }

        public void showInventory()
        {
            MySqlConnection conn = ClsConnection.conect();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_ShowInventory", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                pInventory.dtgInventory.ItemsSource = dt.DefaultView;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            conn.Close();
        }

        private void btnConfirmPrice_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = ClsConnection.conect();

            clsInventory Inventory = new clsInventory();
            ModalView mdView = new ModalView(idInventario, undsInv);
            DataRowView selectedRow = (DataRowView)pInventory.dtgInventory.SelectedItem;

            if (txtPrice.Text != "" && txtUnits.Text != "")
            {
                int finalPrice = 0;
                finalPrice = Convert.ToInt32(txtPrice.Text);
                int unitsSold = 0;
                unitsSold = Convert.ToInt32(txtUnits.Text);

                if (unitsSold <= undsInv)
                {
                    try
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("sp_SellInventory", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        Inventory.idInventario = int.Parse(idInventario);
                        cmd.Parameters.AddWithValue("@pId", Inventory.idInventario);
                        cmd.Parameters.AddWithValue("@pPrecio", finalPrice);
                        cmd.Parameters.AddWithValue("@pUnidades", unitsSold);
                        cmd.ExecuteNonQuery();

                        this.Close();

                        MessageBox.Show("Repuesto vendido con éxito");
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error" + ex.ToString());
                    }
                    showInventory();
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("Inventario insuficiente");
                }
            }
            else
            {
                MessageBox.Show("Los datos de la venta de inventario no pueden ser Nulos.");
            }
            
        }

        private void btnCancelPrice_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

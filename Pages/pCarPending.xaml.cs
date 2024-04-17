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
    /// Lógica de interacción para pCarPending.xaml
    /// </summary>
    public partial class pCarPending : Page
    {
        clsCars Cars = new clsCars();
        int idClient = -1;
        bool cmbSafe = true;
        string idAuto;
        public pCarPending()
        {
            InitializeComponent();
            showCarsPending();
            loadClientsCMB();
        }

        private void showCarsPending()
        {
            MySqlConnection conn = ClsConnection.conect();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_ShowCarPending", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dtgCarsPending.ItemsSource = dt.DefaultView;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            conn.Close();
        }

        private void loadClientsCMB()
        {
            MySqlConnection conn = ClsConnection.conect(); 
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_ShowClientCMB", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string clientCMB = reader["Cliente"].ToString();
                        cmbClients.Items.Add(clientCMB);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            conn.Close();
        }

        private void dtgCarsPending_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Height = 35;
            e.Row.FontSize = 16;
        }

        private void dtgCarsPending_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgCarsPending.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)dtgCarsPending.SelectedItem;
                btnSaveCar.Content = "Modificar";
                btnCheck.Visibility = Visibility.Visible;

                idAuto = selectedRow[0].ToString();
                Cars.idAuto = int.Parse(idAuto);
                Cars.marca = selectedRow[1].ToString();
                Cars.modelo = selectedRow[2].ToString();
                Cars.detalles = selectedRow[3].ToString();
                Cars.reparacion = selectedRow[4].ToString();
                Cars.cliente = selectedRow[7].ToString();

                cmbClients.Text = Cars.cliente;
                txtBrand.Text = Cars.marca;
                txtModel.Text = Cars.modelo;
                txtDetails.Text = Cars.detalles;
                txtRepair.Text = Cars.reparacion;
                ChangeBtnColor();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            cmbSafe = false;

            cmbClients.Text = null;
            txtBrand.Text = null;
            txtModel.Text = null;
            txtDetails.Text = null;
            txtRepair.Text = null;

            btnSaveCar.Content = "Guardar";
            btnCheck.Visibility = Visibility.Hidden;
            ChangeBtnColor();
        }

        private void btnDeleteCar_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = ClsConnection.conect();
            cmbSafe = false;

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_DropCar", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                DataRowView selectedRow = (DataRowView)dtgCarsPending.SelectedItem;


                string idCar = selectedRow[0].ToString();
                Cars.idAuto = int.Parse(idCar);
                cmd.Parameters.AddWithValue("@pIdCar", Cars.idAuto);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Auto eliminado con éxito");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            showCarsPending();
            cmbClients.Text = null;
            txtBrand.Text = null;
            txtDetails.Text = null;
            txtRepair.Text = null;
            txtModel.Text = null;
            btnSaveCar.Content = "Guardar";
            btnCheck.Visibility = Visibility.Hidden;
            ChangeBtnColor();

        }

        private void btnSaveCar_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = ClsConnection.conect();
            cmbSafe = false;

            if (btnSaveCar.Content is "Guardar")
            {
                if (cmbClients.Text != "" && txtBrand.Text != "")
                {
                    try
                    {
                        if (txtBrand.Text == "" || cmbClients.Text == null)
                        {
                            MessageBox.Show("Los campos *Cliente* y *Marca* deben rellenarse.");
                        }
                        else
                        {
                            conn.Open();
                            MySqlCommand cmd = new MySqlCommand("sp_AddNewCar", conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            Cars.cliente = cmbClients.Text;
                            Cars.marca = txtBrand.Text;
                            Cars.modelo = txtModel.Text;
                            Cars.detalles = txtDetails.Text;
                            Cars.reparacion = txtRepair.Text;
                            Cars.idCliente = idClient;

                            cmd.Parameters.AddWithValue("@marca", Cars.marca);
                            cmd.Parameters.AddWithValue("@modelo", Cars.modelo);
                            cmd.Parameters.AddWithValue("@detalles", Cars.detalles);
                            cmd.Parameters.AddWithValue("@reparacion", Cars.reparacion);
                            cmd.Parameters.AddWithValue("@idCliente", Cars.idCliente);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Auto ingresado con éxito");

                            showCarsPending();
                            conn.Close();
                            cmbClients.Text = null;
                            txtBrand.Text = null;
                            txtDetails.Text = null;
                            txtRepair.Text = null;
                            txtModel.Text = null;
                        }
                        
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error," + ex.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Los campos *Cliente* y *Marca* deben rellenarse.");
                }
            }

            if (btnSaveCar.Content is "Modificar")
            {
                if (cmbClients.Text != "" && txtBrand.Text != "")
                {
                    try
                    {
                        if (txtBrand.Text == "" || cmbClients.Text == null)
                        {
                            MessageBox.Show("Los campos *Cliente* y *Marca* deben rellenarse.");
                        }
                        else
                        {
                            DataRowView selectedRow = (DataRowView)dtgCarsPending.SelectedItem;

                            string SelectIdCar = selectedRow[0].ToString();
                            int idCar = int.Parse(SelectIdCar);
                            Cars.idAuto = idCar;
                            Cars.idCliente = idClient;
                            Cars.marca = txtBrand.Text;
                            Cars.modelo = txtModel.Text;
                            Cars.detalles = txtDetails.Text;
                            Cars.detalles = txtDetails.Text;
                            Cars.reparacion = txtRepair.Text;

                            conn.Open();
                            MySqlCommand cmd = new MySqlCommand("sp_UpdateCarPending", conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@pId", Cars.idAuto);
                            cmd.Parameters.AddWithValue("@pMarca", Cars.marca);
                            cmd.Parameters.AddWithValue("@pModelo", Cars.modelo);
                            cmd.Parameters.AddWithValue("@pDetalles", Cars.detalles);
                            cmd.Parameters.AddWithValue("@pReparacion", Cars.reparacion);
                            cmd.Parameters.AddWithValue("@pIdCliente", Cars.idCliente);

                            cmd.ExecuteNonQuery();

                            showCarsPending();
                            conn.Close();
                            txtBrand.Text = "";
                            txtModel.Text = "";
                            cmbClients.Text = "";
                            txtDetails.Text = "";
                            txtRepair.Text = "";
                            btnSaveCar.Content = "Guardar";
                            btnCheck.Visibility = Visibility.Hidden;
                            ChangeBtnColor();
                            MessageBox.Show("Auto Modificado con éxito");
                        }                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error" + ex.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Los campos *Cliente* y *Marca* deben rellenarse.");
                }
            }
            ChangeBtnColor();
        }
        private int loadIdClient(string clientCMB)
        {
            MySqlConnection conn = ClsConnection.conect();

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("sp_SaveIdClient", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pNombreCliente", clientCMB);

                object firstColumn = cmd.ExecuteScalar();
                idClient = Convert.ToInt32(firstColumn);
            }

            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }

            conn.Close();
            return idClient;
        }

        private void cmbClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbSafe)
            {
                string selectedClientName = cmbClients.SelectedItem.ToString();
                idClient = loadIdClient(selectedClientName);
            }
            else
            {
                cmbSafe = true;
            }
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new pCarReady(idAuto));
        }

        private void ChangeBtnColor()
        {
            if (btnSaveCar.Content is "Modificar")
            {
                Color blue = (Color)ColorConverter.ConvertFromString("#FF19B0CC");
                SolidColorBrush btnBlue = new SolidColorBrush(blue);
                btnSaveCar.Background = btnBlue;
            }
            else
            {
                Color green = (Color)ColorConverter.ConvertFromString("#FF00BB25");
                SolidColorBrush btnGreen = new SolidColorBrush(green);
                btnSaveCar.Background = btnGreen;
            }
        }
    }
}

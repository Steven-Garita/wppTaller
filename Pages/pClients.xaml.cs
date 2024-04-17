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
using MySqlX.XDevAPI;

namespace wpp_Taller.Pages
{
    /// <summary>
    /// Lógica de interacción para pClients.xaml
    /// </summary>
    public partial class pClients : Page
    {
        bool actionClients = true;
        int idsClients;
        List<int> listIdsClients = new List<int>();

        public pClients()
        {
            InitializeComponent();
            showClients();
            checkClientCarPending();
            checkClientCarRepared();
            dtgClients.LoadingRow += dtgClients_LoadingRow;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dtgClients.SelectionChanged += dtgClients_SelectionChanged;
        }

        private void checkClientCarPending()
        {
            MySqlConnection conn = ClsConnection.conect();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_ShowCarPending", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                clsClient Client = new clsClient();

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    idsClients = dr.GetInt32(6);
                    listIdsClients.Add(idsClients);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            conn.Close();
        }

        private void checkClientCarRepared()
        {
            MySqlConnection conn = ClsConnection.conect();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_ShowCarRepared", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                clsClient Client = new clsClient();

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    idsClients = dr.GetInt32(7);
                    if (listIdsClients.Contains(idsClients))
                    {

                    }
                    else
                    {
                        listIdsClients.Add(idsClients);
                    }
                    
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            conn.Close();
        }

        private void showClients()
        {
            MySqlConnection conn = ClsConnection.conect();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_ShowClients", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dtgClients.ItemsSource = dt.DefaultView;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            conn.Close();
            btnInactiveClients.Content = "Inactivos";
            btnSaveClient.Content = "Guardar";
            actionClients = true;
            btnCheck.Visibility = Visibility.Hidden;

            txtNameClient.Visibility = Visibility.Visible;
            txtLastNameClient.Visibility = Visibility.Visible;
            txtPhoneClient.Visibility = Visibility.Visible;
            btnSaveClient.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
            lblNameClient.Visibility = Visibility.Visible;
            lblLastNameCliente.Visibility = Visibility.Visible;
            lblPhoneClient.Visibility = Visibility.Visible;

            lbClientsTitle.Content = "CLIENTES ACTIVOS";

            DataGridColumn column = dtgClients.Columns[3];
            column.Visibility = Visibility.Visible;
        }

        private void showInactiveClients()
        {
            MySqlConnection conn = ClsConnection.conect();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_ShowInactiveClients", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dtgClients.ItemsSource = dt.DefaultView;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            conn.Close();
            btnInactiveClients.Content = "Activos";
            btnCheck.Visibility = Visibility.Visible;
            actionClients = false;

            txtNameClient.Visibility = Visibility.Hidden;
            txtLastNameClient.Visibility = Visibility.Hidden;
            txtPhoneClient.Visibility = Visibility.Hidden;
            btnSaveClient.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;
            lblNameClient.Visibility = Visibility.Hidden;
            lblLastNameCliente.Visibility = Visibility.Hidden;
            lblPhoneClient.Visibility= Visibility.Hidden;

            lbClientsTitle.Content = "CLIENTES INACTIVOS";

            DataGridColumn column = dtgClients.Columns[3];
            column.Visibility = Visibility.Hidden;
        }

        private void btnSaveClient_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = ClsConnection.conect();

            if (btnSaveClient.Content is "Guardar")
            {
                if (string.IsNullOrWhiteSpace(txtNameClient.Text) && string.IsNullOrWhiteSpace(txtLastNameClient.Text) && string.IsNullOrWhiteSpace(txtPhoneClient.Text))
                {
                    MessageBox.Show("Rellene los campos para ingresar un cliente.");
                }
                else
                {
                    try
                    {
                        if (txtNameClient.Text == "")
                        {
                            MessageBox.Show("Rellene el nombre del cliente para registrarlo");
                        }
                        else
                        {
                            conn.Open();
                            MySqlCommand cmd = new MySqlCommand("sp_AddClient", conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            clsClient Client = new clsClient();
                            Client.nombreCliente = txtNameClient.Text;
                            Client.apellidosCliente = txtLastNameClient.Text;
                            Client.numeroTelefono = txtPhoneClient.Text;

                            cmd.Parameters.AddWithValue("@nombre", Client.nombreCliente);
                            cmd.Parameters.AddWithValue("@apellidos", Client.apellidosCliente);
                            cmd.Parameters.AddWithValue("@telefono", Client.numeroTelefono);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Cliente guardado con éxito");

                            showClients();
                            conn.Close();
                            txtNameClient.Text = "";
                            txtLastNameClient.Text = "";
                            txtPhoneClient.Text = "";
                        }
                        
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error" + ex.ToString());
                    }
                }
            }

            if (btnSaveClient.Content is "Modificar")
            {
                if (string.IsNullOrWhiteSpace(txtNameClient.Text) && string.IsNullOrWhiteSpace(txtLastNameClient.Text) && string.IsNullOrWhiteSpace(txtPhoneClient.Text))
                {
                    MessageBox.Show("Todos los campos deben completarse.");
                }
                else
                {
                    try
                    {
                        if (txtNameClient.Text == "")
                        {
                            MessageBox.Show("Rellene el nombre del cliente para registrarlo");
                        }
                        else
                        {
                            DataRowView selectedRow = (DataRowView)dtgClients.SelectedItem;

                            clsClient Client = new clsClient();
                            string idCliente = selectedRow[0].ToString();
                            int idClient = int.Parse(idCliente);
                            Client.nombreCliente = txtNameClient.Text;
                            Client.apellidosCliente = txtLastNameClient.Text;
                            Client.numeroTelefono = txtPhoneClient.Text;

                            conn.Open();
                            MySqlCommand cmd = new MySqlCommand("sp_UpdateClient", conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@id", idClient);
                            cmd.Parameters.AddWithValue("@nombre", Client.nombreCliente);
                            cmd.Parameters.AddWithValue("@apellidos", Client.apellidosCliente);
                            cmd.Parameters.AddWithValue("@telefono", Client.numeroTelefono);

                            cmd.ExecuteNonQuery();

                            showClients();
                            conn.Close();
                            txtNameClient.Text = "";
                            txtLastNameClient.Text = "";
                            txtPhoneClient.Text = "";
                            MessageBox.Show("Cliente Modificado con éxito");
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error" + ex.ToString());
                    }
                }
            }
            ChangeBtnColor();
        }

        private void dtgClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (dtgClients.SelectedItem != null)
            {
                btnSaveClient.Content = "Modificar";

                DataRowView selectedRow = (DataRowView)dtgClients.SelectedItem;

                clsClient Client = new clsClient();

                string idCliente = selectedRow[0].ToString();
                Client.idCliente = int.Parse(idCliente);
                Client.nombreCliente = selectedRow[1].ToString();
                Client.apellidosCliente = selectedRow[2].ToString();
                Client.numeroTelefono = selectedRow[3].ToString();

                txtNameClient.Text = Client.nombreCliente;
                txtLastNameClient.Text = Client.apellidosCliente;
                txtPhoneClient.Text = Client.numeroTelefono;
                ChangeBtnColor();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            txtNameClient.Text = null;
            txtLastNameClient.Text = null;
            txtPhoneClient.Text = null;

            btnSaveClient.Content = "Guardar";
            ChangeBtnColor();
        }

        private void btnDeleteClient_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = ClsConnection.conect();

            if (actionClients == false)
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_DropClient", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    DataRowView selectedRow = (DataRowView)dtgClients.SelectedItem;

                    clsClient Client = new clsClient();

                    string idCliente = selectedRow[0].ToString();
                    Client.idCliente = int.Parse(idCliente);

                    if (listIdsClients.Contains(Client.idCliente))
                    {
                        MessageBox.Show("El cliente no se puede eliminar debido a que se encuentra ligado a un vehículo pendiente o a algún reporte");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@id", Client.idCliente);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Cliente eliminado con éxito");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error" + ex.ToString());
                }
                showInactiveClients();
                txtNameClient.Text = "";
                txtLastNameClient.Text = "";
                txtPhoneClient.Text = "";
            }
            ChangeBtnColor();
        }

        private void dtgClients_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Height = 35;
            e.Row.FontSize = 18;
        }

        private void btnInactiveClients_Click(object sender, RoutedEventArgs e)
        {
            if (actionClients)
            {
                showInactiveClients();
            }
            else
            {
                showClients();
            }
            ChangeBtnColor();
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = ClsConnection.conect();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_ActiveClient", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                DataRowView selectedRow = (DataRowView)dtgClients.SelectedItem;

                if (selectedRow == null)
                {
                    MessageBox.Show("Seleccione el cliente que desea Activar.");
                    showInactiveClients();
                }
                else
                {
                    clsClient Client = new clsClient();

                    string idCliente = selectedRow[0].ToString();
                    Client.idCliente = int.Parse(idCliente);
                    cmd.Parameters.AddWithValue("@pId", Client.idCliente);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cliente activado con éxito");
                    showClients();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            txtNameClient.Text = "";
            txtLastNameClient.Text = "";
            txtPhoneClient.Text = "";
            ChangeBtnColor();
        }

        private void btnMinimizeClient_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = ClsConnection.conect();

            if (actionClients)
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_MinimizeClient", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    DataRowView selectedRow = (DataRowView)dtgClients.SelectedItem;

                    clsClient Client = new clsClient();

                    string idCliente = selectedRow[0].ToString();
                    Client.idCliente = int.Parse(idCliente);
                    cmd.Parameters.AddWithValue("@id", Client.idCliente);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cliente inactivado con éxito");
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error" + ex.ToString());
                }
                showClients();
                txtNameClient.Text = "";
                txtLastNameClient.Text = "";
                txtPhoneClient.Text = "";

                ChangeBtnColor();
            }
        }

        private void ChangeBtnColor()
        {
            if (btnSaveClient.Content is "Modificar")
            {
                Color blue = (Color)ColorConverter.ConvertFromString("#FF19B0CC");
                SolidColorBrush btnBlue = new SolidColorBrush(blue);
                btnSaveClient.Background = btnBlue;
            }
            else
            {
                Color green = (Color)ColorConverter.ConvertFromString("#FF00BB25");
                SolidColorBrush btnGreen = new SolidColorBrush(green);
                btnSaveClient.Background = btnGreen;
            }
        }
    }
}

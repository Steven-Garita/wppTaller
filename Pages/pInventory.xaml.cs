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
    /// Lógica de interacción para pInventory.xaml
    /// </summary>
    public partial class pInventory : Page
    {
        public pInventory()
        {
            InitializeComponent();
            showInventory();
        }

        clsInventory Inventory = new clsInventory();

        string idInventario;
        string unidadesInv;

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
                dtgInventory.ItemsSource = dt.DefaultView;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            conn.Close();
        }

        public void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgInventory.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)dtgInventory.SelectedItem;
                btnSaveInventory.Content = "Modificar";

                idInventario = selectedRow[0].ToString();
                Inventory.idInventario = int.Parse(idInventario);
                Inventory.nombreRpuesto = selectedRow[1].ToString();
                Inventory.disponibilidad = selectedRow[2].ToString();
                Inventory.estadoRepuesto = selectedRow[3].ToString();
                Inventory.marcaAuto = selectedRow[4].ToString();
                Inventory.modeloAuto = selectedRow[5].ToString();
                Inventory.otros = selectedRow[6].ToString();
                string precioInventario = selectedRow[7].ToString();
                Inventory.precio = int.Parse(precioInventario);
                unidadesInv = selectedRow[8].ToString();
                Inventory.unidades = int.Parse(unidadesInv);

                txtBrand.Text = Inventory.marcaAuto;
                txtModel.Text = Inventory.modeloAuto;
                txtOther.Text = Inventory.otros;
                txtPrice.Text = Convert.ToString(Inventory.precio);
                txtSpare.Text = Inventory.nombreRpuesto;
                txtState.Text = Inventory.estadoRepuesto;
                txtUnits.Text = Convert.ToString(Inventory.unidades);

                ChangeBtnColor();
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            txtSpare.Text = null;
            txtBrand.Text = null;
            txtModel.Text = null;
            txtState.Text = null;
            txtOther.Text = null;
            txtPrice.Text = null;
            txtUnits.Text= null;

            btnSaveInventory.Content = "Guardar";

            ChangeBtnColor();
            showInventory();
        }

        private void btnSaveInventory_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = ClsConnection.conect();

            if (btnSaveInventory.Content is "Guardar")
            {
                try
                {
                    if (txtSpare.Text == "" || txtUnits.Text == "")
                    {
                        MessageBox.Show("Los campos *Repuesto* y *Unidades* deben rellenarse");
                    }
                    else
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("sp_AddInventory", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        clsInventory Inventory = new clsInventory();
                        Inventory.nombreRpuesto = txtSpare.Text;
                        Inventory.modeloAuto = txtModel.Text;
                        Inventory.marcaAuto = txtBrand.Text;
                        Inventory.estadoRepuesto = txtState.Text;
                        Inventory.otros = txtOther.Text;
                        if (txtPrice.Text == "")
                        {
                            Inventory.precio = 0;
                        }
                        else
                        {
                            Inventory.precio = Convert.ToInt32(txtPrice.Text);
                        }
                        Inventory.unidades = Convert.ToInt32(txtUnits.Text);

                        cmd.Parameters.AddWithValue("@pNombreRepuesto", Inventory.nombreRpuesto);
                        cmd.Parameters.AddWithValue("@pEstadoRepuesto", Inventory.estadoRepuesto);
                        cmd.Parameters.AddWithValue("@pMarcaAuto", Inventory.marcaAuto);
                        cmd.Parameters.AddWithValue("@pModeloAuto", Inventory.modeloAuto);
                        cmd.Parameters.AddWithValue("@pOtros", Inventory.otros);
                        cmd.Parameters.AddWithValue("@pPrecio", Inventory.precio);
                        cmd.Parameters.AddWithValue("@pUnidades", Inventory.unidades);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Repuesto guardado con éxito");

                        showInventory();
                        conn.Close();
                        txtSpare.Text = "";
                        txtBrand.Text = "";
                        txtModel.Text = "";
                        txtState.Text = "";
                        txtOther.Text = "";
                        txtPrice.Text = "";
                        txtUnits.Text = "";
                    }
                    
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error" + ex.ToString());
                }
            }

            if (btnSaveInventory.Content is "Modificar")
            {
                try
                {
                    if (txtSpare.Text == "" || txtUnits.Text == "")
                    {
                        MessageBox.Show("Los campos *Repuesto* y *Unidades* deben rellenarse");
                    }
                    else
                    {
                        DataRowView selectedRow = (DataRowView)dtgInventory.SelectedItem;
                        clsInventory Inventory = new clsInventory();

                        string SelectIdInventory = selectedRow[0].ToString();
                        int idInventory = int.Parse(SelectIdInventory);
                        Inventory.idInventario = idInventory;
                        Inventory.nombreRpuesto = txtSpare.Text;
                        Inventory.estadoRepuesto = txtState.Text;
                        Inventory.marcaAuto = txtBrand.Text;
                        Inventory.modeloAuto = txtModel.Text;
                        Inventory.otros = txtOther.Text;
                        if (txtPrice.Text == "")
                        {
                            Inventory.precio = 0;
                        }
                        else
                        {
                            Inventory.precio = Convert.ToInt32(txtPrice.Text);
                        }
                        Inventory.unidades = Convert.ToInt32(txtUnits.Text);

                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("sp_UpdateInventory", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@pId", Inventory.idInventario);
                        cmd.Parameters.AddWithValue("@pNombreRepuesto", Inventory.nombreRpuesto);
                        cmd.Parameters.AddWithValue("@pEstadoRepuesto", Inventory.estadoRepuesto);
                        cmd.Parameters.AddWithValue("@pMarcaAuto", Inventory.marcaAuto);
                        cmd.Parameters.AddWithValue("@pModeloAuto", Inventory.modeloAuto);
                        cmd.Parameters.AddWithValue("@pOtros", Inventory.otros);
                        cmd.Parameters.AddWithValue("@pPrecio", Inventory.precio);
                        cmd.Parameters.AddWithValue("@pUnidades", Inventory.unidades);

                        cmd.ExecuteNonQuery();

                        btnSaveInventory.Content = "Guardar";

                        ChangeBtnColor();
                        showInventory();
                        conn.Close();
                        txtBrand.Text = "";
                        txtModel.Text = "";
                        txtOther.Text = "";
                        txtPrice.Text = "";
                        txtSpare.Text = "";
                        txtState.Text = "";
                        txtUnits.Text = "";
                        MessageBox.Show("Repuesto Modificado con éxito");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error" + ex.ToString());
                }
            }
        }

        private void dtgInventory_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Height = 35;
            e.Row.FontSize = 17;
        }

        private void btnSold_Click(object sender, RoutedEventArgs e)
        {
            int undsInv = 0;
            undsInv = Convert.ToInt32(unidadesInv);
            ModalView mdView = new ModalView(idInventario, undsInv);

            mdView.ShowDialog();

            txtBrand.Text = null;
            txtModel.Text = null;
            txtOther.Text = null;
            txtPrice.Text = null;
            txtSpare.Text = null;
            txtState.Text = null;
            txtUnits.Text= null;
            btnSaveInventory.Content = "Guardar";

            ChangeBtnColor();
            showInventory();
        }

        private void ChangeBtnColor()
        {
            if (btnSaveInventory.Content is "Modificar")
            {
                Color blue = (Color)ColorConverter.ConvertFromString("#FF19B0CC");
                SolidColorBrush btnBlue = new SolidColorBrush(blue);
                btnSaveInventory.Background = btnBlue;
            }
            else
            {
                Color green = (Color)ColorConverter.ConvertFromString("#FF00BB25");
                SolidColorBrush btnGreen = new SolidColorBrush(green);
                btnSaveInventory.Background = btnGreen;
            }
        }
    }
}

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
using wpp_Taller.Pages;

namespace wpp_Taller.Pages
{
    /// <summary>
    /// Lógica de interacción para pUsers.xaml
    /// </summary>
    public partial class pUsers : Page
    {
        public pUsers()
        {
            InitializeComponent();
            showUsers();

        }

        clsUsers Users = new clsUsers();
        string passwordConfirmation;
        bool userLicensses;
        string idUsuario;
        private void showUsers()
        {
            MySqlConnection conn = ClsConnection.conect();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_ShowUsers", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dtgUsers.ItemsSource = dt.DefaultView;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            conn.Close();
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = ClsConnection.conect();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_DropUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                DataRowView selectedRow = (DataRowView)dtgUsers.SelectedItem;

                clsUsers Users = new clsUsers();

                string idUser = selectedRow[0].ToString();
                Users.idUsuarios = int.Parse(idUser);

                if (Users.idUsuarios == 1)
                {
                    MessageBox.Show("El usuario administrador no puede ser eliminado.");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pId", Users.idUsuarios);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Usuario eliminado con éxito");
                    pbPasswordConfirm.Visibility = Visibility.Visible;
                    txbConfirmPs.Visibility = Visibility.Visible;

                    showUsers();
                    txbUser.Text = null;
                    pbPassword.Password = null;
                    pbPasswordConfirm.Password = null;
                    btnSaveUser.Content = "Guardar";
                    ChangeBtnColor();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }

        private void dtgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgUsers.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)dtgUsers.SelectedItem;
                btnSaveUser.Content = "Confirmar";
                ChangeBtnColor();
;

                idUsuario = selectedRow[0].ToString();
                Users.idUsuarios = int.Parse(idUsuario);
                Users.usuario = selectedRow[1].ToString();
                Users.contrasena = selectedRow[2].ToString();
                passwordConfirmation = Users.contrasena;

                txbUser.Text = Users.usuario;
                pbPassword.Password = null;

                pbPasswordConfirm.Visibility = Visibility.Hidden;
                txbConfirmPs.Visibility = Visibility.Hidden;

                MessageBox.Show("Para modificar el usuario, favor digite su contraseña.");
            }
        }

        private void dtgUsers_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Height = 38;
            e.Row.FontSize = 18;
        }

        private void btnSaveUser_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = ClsConnection.conect();
            clsUsers Users = new clsUsers();
            userLicensses = true;

            if (btnSaveUser.Content is "Guardar")
            {
                if (txbUser.Text == "")
                {
                    MessageBox.Show("Rellene el campo *Usuario* para registrarlo.");
                    userLicensses = false;
                }
                else
                {
                    if (pbPassword.Password == pbPasswordConfirm.Password)
                    {
                        try
                        {
                            conn.Open();
                            MySqlCommand cmd = new MySqlCommand("sp_AddUser", conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            Users.usuario = txbUser.Text;
                            Users.contrasena = pbPasswordConfirm.Password;

                            cmd.Parameters.AddWithValue("@pUsuario", Users.usuario);
                            cmd.Parameters.AddWithValue("@pContrasena", Users.contrasena);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Usuario añadido con éxito");

                            showUsers();
                            conn.Close();
                            txbUser.Text = null;
                            pbPassword.Password = null;
                            pbPasswordConfirm.Password = null;
                            userLicensses = false;
                        }
                        catch (MySqlException ex)
                        {
                            MessageBox.Show("Error" + ex.ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Las contraseñas no coinciden, verifique e intentelo de nuevo.");
                        userLicensses = false;
                    }
                    ChangeBtnColor();
                }
                
            }

            if (btnSaveUser.Content is "Confirmar")
            {
                if (txbUser.Text == "")
                {
                    MessageBox.Show("Rellene el campo *Usuario* para registrarlo.");
                }
                else
                {
                    if (passwordConfirmation == pbPassword.Password)
                    {
                        MessageBox.Show("Contraseña Correcta. Permisos para modificar el usuario concedidos.");
                        userLicensses = false;
                        btnSaveUser.Content = "Modificar";

                        pbPasswordConfirm.Visibility = Visibility.Visible;
                        txbConfirmPs.Visibility = Visibility.Visible;
                        txbNewPassword.Text = "Nueva Contraseña";

                        pbPassword.Password = "";
                        pbPasswordConfirm.Password = "";
                    }
                    else
                    {
                        MessageBox.Show("Contraseña incorrecta");
                        userLicensses = false;
                    }
                    ChangeBtnColor();
                }
            }

            if (userLicensses)
            {
                try
                {
                    if (txbUser.Text == "")
                    {
                        MessageBox.Show("Rellene el campo *Usuario* para registrarlo.");
                    }
                    else
                    {
                        if (pbPassword.Password == pbPasswordConfirm.Password)
                        {
                            DataRowView selectedRow = (DataRowView)dtgUsers.SelectedItem;

                            string SelectIdUser = idUsuario;
                            int idUser = int.Parse(SelectIdUser);
                            Users.idUsuarios = idUser;
                            Users.usuario = txbUser.Text;
                            Users.contrasena = pbPassword.Password;

                            conn.Open();
                            MySqlCommand cmd = new MySqlCommand("sp_UpdateUser", conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@pId", Users.idUsuarios);
                            cmd.Parameters.AddWithValue("@pUsuario", Users.usuario);
                            cmd.Parameters.AddWithValue("@pContrasena", Users.contrasena);

                            cmd.ExecuteNonQuery();
                            ChangeBtnColor();
                            MessageBox.Show("Usuario Modificado con éxito");
                            txbUser.Text = "";
                            pbPassword.Password = "";
                            pbPasswordConfirm.Password = "";
                            txbNewPassword.Text = "Contraseña";
                            btnSaveUser.Content = "Guardar";

                            showUsers();
                            conn.Close();
                            ChangeBtnColor();
                        }
                        else
                        {
                            MessageBox.Show("Las contraseñas no coinciden, intente de nuevo.");
                            txbNewPassword.Text = "Nueva Contraseña";
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error" + ex.ToString());
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            txbUser.Text = null;
            pbPassword.Password = null;
            pbPasswordConfirm.Password = null;

            btnSaveUser.Content = "Guardar";
            ChangeBtnColor();
            NavigationService?.Navigate(new pUsers());
        }

        private void ChangeBtnColor()
        {
            if (btnSaveUser.Content is "Modificar" || btnSaveUser.Content is "Confirmar")
            {
                Color blue = (Color)ColorConverter.ConvertFromString("#FF19B0CC");
                SolidColorBrush btnBlue = new SolidColorBrush(blue);
                btnSaveUser.Background = btnBlue;
            }
            else
            {
                Color green = (Color)ColorConverter.ConvertFromString("#FF00BB25");
                SolidColorBrush btnGreen = new SolidColorBrush(green);
                btnSaveUser.Background = btnGreen;
            }
        }
    }
}

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
using System.Windows.Media.Animation;

namespace wpp_Taller.View
{
    /// <summary>
    /// Lógica de interacción para LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        int licensesUser;
        public WindowState Windowstate { get; private set; }

        public LoginView()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton==MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            clsUsers User = new clsUsers();

            MySqlConnection conn = ClsConnection.conect();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_Login", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                User.usuario = txtUser.Text;
                User.contrasena = txtPassword.Password;

                cmd.Parameters.AddWithValue("@pUsuario", User.usuario);
                cmd.Parameters.AddWithValue("@pContrasena", User.contrasena);

                object firstColumn = cmd.ExecuteScalar();
                User.idUsuarios = Convert.ToInt32(firstColumn);

                licensesUser = User.idUsuarios;

                MySqlDataReader dr = cmd.ExecuteReader();
                LoginView lv = new LoginView();

                if (dr.Read())
                {
                    this.Hide();
                    MainWindow mw = new MainWindow(licensesUser);
                    lv.Hide();
                    mw.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Usuario o Contraseña Incorrecto.");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(1)
            };

            this.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace wpp_Taller
{
    internal class ClsConnection
    {
        public static MySqlConnection conect()
        {
            string server = "server=localhost; database=autotronica_bd; User Id=root; Password=Garita90";

            MySqlConnection conn = new MySqlConnection(server);

            try
            {
                return conn;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+ex.StackTrace);
                return null;
            }

            
        }
    }
}

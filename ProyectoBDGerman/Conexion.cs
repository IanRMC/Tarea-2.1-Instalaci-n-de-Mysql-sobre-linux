using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBDGerman
{
    public class Conexion
    {

        public static MySqlConnection conexion;

        public void newSesion(string host, string user, string pass)
        {
            Properties.Settings.Default.Host = host;
            Properties.Settings.Default.Usuario = user;
            Properties.Settings.Default.Pass = pass;
        }

        public static bool Conectar()
        {


            try
            {
                if (conexion != null && conexion.State == System.Data.ConnectionState.Open) return true;

                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                builder.Server = Properties.Settings.Default.Host;
                builder.UserID = Properties.Settings.Default.Usuario;
                builder.Password = Properties.Settings.Default.Pass;
                builder.Database = "compras";

                conexion = new MySqlConnection(builder.ConnectionString);


                conexion.Open();

                return true;

            }
            catch (MySqlException ex)
            {
                return false;

            }
            catch (Exception ex)
            {

                return false;
            }

        }

        public static void Desconectar()
        {
            if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
                conexion.Close();

        }

    }
}

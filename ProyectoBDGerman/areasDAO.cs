using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBDGerman
{
    public class areasDAO
    {
        public oAreas obtenerUsuario(int id)
        {
            oAreas ar = null;
            //Conectarme
            if (Conexion.Conectar())
            {
                try
                {
                    String select = @"SELECT * FROM areas WHERE id=@id";


                    //Definir un datatable para que sea llenado
                    DataTable dt = new DataTable();
                    //Crear el dataadapter
                    MySqlCommand sentencia = new MySqlCommand(select);
                    //Asignar los parámetros

                    sentencia.Parameters.AddWithValue("@id", id);

                    sentencia.Connection = Conexion.conexion;

                    MySqlDataAdapter da = new MySqlDataAdapter(sentencia);
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow fila = dt.Rows[0];
                        ar = new oAreas(Convert.ToInt32(fila["id"]), fila["nombre"].ToString(), fila["ubicacion"].ToString());

                    }

                    return ar;
                }
                finally
                {
                    Conexion.Desconectar();
                }
            }
            else
            {
                return null;
            }

        }

        public List<oAreas> GetAll()
        {
            List<oAreas> lista = new List<oAreas>();
            //Conectarme
            if (Conexion.Conectar())
            {
                try
                {
                    //Crear la sentencia a ejecutar (SELECT)
                    String select = "SELECT * FROM areas";
                    //Definir un datatable para que sea llenado
                    DataTable dt = new DataTable();
                    //Crear el dataadapter
                    MySqlCommand sentencia = new MySqlCommand();
                    sentencia.CommandText = select;
                    sentencia.Connection = Conexion.conexion;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = sentencia;
                    //Llenar el datatable
                    da.Fill(dt);
                    //Crear un objeto categoría por cada fila de la tabla y añadirlo a la lista
                    foreach (DataRow fila in dt.Rows)
                    {
                        oAreas area = new oAreas(
                            Convert.ToInt32(fila["id"]),
                            fila["nombre"].ToString(),
                            fila["ubicacion"].ToString()                            
                            );
                        
                        lista.Add(area);
                    }

                    return lista;
                }
                finally
                {
                    Conexion.Desconectar();
                }
            }
            else
            {
                return null;
            }

        }
    }
}

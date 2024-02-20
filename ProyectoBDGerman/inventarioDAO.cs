using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBDGerman
{
    public class inventarioDAO
    {
        public List<oInventario> GetAll()
        {
            List<oInventario> lista = new List<oInventario>();
            //Conectarme
            if (Conexion.Conectar())
            {
                try
                {
                    //Crear la sentencia a ejecutar (SELECT)
                    String select = "SELECT * FROM Inventario";
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
                        int id = Convert.ToInt32(fila["Areas_id"]);
                        oInventario inventario = new oInventario(
                            Convert.ToInt32(fila["id"]),
                            fila["nombreCorto"].ToString(),
                            fila["descripcion"].ToString(),
                            fila["serie"].ToString(),
                            fila["color"].ToString(),
                            fila["fechaAdquisicion"].ToString(),
                            fila["tipoAdquisicion"].ToString(),
                            fila["observaciones"].ToString(),
                            id
                            );
                        inventario.area = new areasDAO().obtenerUsuario( id );
                        inventario.nom_area = inventario.area.name;
                        lista.Add(inventario);
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

        public oInventario obtenerCompra(int id)
        {
            oInventario inv = null;
            //Conectarme
            if (Conexion.Conectar())
            {
                try
                {
                    String select = @"SELECT * FROM Inventario WHERE id=@id";


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
                        int id2 = Convert.ToInt32(fila["Areas_id"]);
                        inv = new oInventario(
                            Convert.ToInt32(fila["id"]),
                            fila["nombreCorto"].ToString(),
                            fila["descripcion"].ToString(),
                            fila["serie"].ToString(),
                            fila["color"].ToString(),
                            fila["fechaAdquisicion"].ToString(),
                            fila["tipoAdquisicion"].ToString(),
                            fila["observaciones"].ToString(),
                            id2
                            );
                        inv.area = new areasDAO().obtenerUsuario(id2);
                        inv.nom_area = inv.area.name;

                    }

                    return inv;
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

        public int Insert(oInventario compra)
        {
            //Conectarme

            if (Conexion.Conectar())
            {
                MySqlTransaction tran = Conexion.conexion.BeginTransaction();
                try
                {
                    String select = @"INSERT INTO Inventario 
                                    VALUES(
                                        0, @nom, @des, @ser, @col, @fec, @tipo, @ob, @aid); 
                                    select last_insert_id();";

                    // Se modifico la manera de crear la sentencia de ejecución porque por alguna razón
                    // generaba una excepción al signar nulos
                    MySqlCommand sentencia = new MySqlCommand(select, Conexion.conexion);
                    //sentencia.CommandText = select;

                    sentencia.Parameters.AddWithValue("@nom", compra.nombreCorto);
                    sentencia.Parameters.AddWithValue("@des", compra.descripcion);
                    sentencia.Parameters.AddWithValue("@ser", compra.serie);
                    sentencia.Parameters.AddWithValue("@col", compra.color);
                    sentencia.Parameters.AddWithValue("@fec", compra.fechaAd);
                    sentencia.Parameters.AddWithValue("@tipo", compra.tipoAd);
                    sentencia.Parameters.AddWithValue("@ob", compra.observaciones);
                    sentencia.Parameters.AddWithValue("@aid", compra.id_area);
                    sentencia.Connection = Conexion.conexion;

                    //Ejercutar el comando 
                    //Cuando nos interesa obtener un valor adicional en el comando (como en el ejemplo de arriba que obtiene el último id generado por autoincrement podemos usar ExecuteScalar
                    int prod = Convert.ToInt32(sentencia.ExecuteScalar());
                    int id = prod;
                    

                    return prod;
                }
                finally
                {
                    tran.Commit();
                    Conexion.Desconectar();
                }
            }
            else
            {
                //Devolvemos un cero indicando que no se insertó nada
                return 0;
            }
        }

        public void Update(oInventario compra)
        {
            //Conectarme

            if (Conexion.Conectar())
            {
                MySqlTransaction tran = Conexion.conexion.BeginTransaction();
                try
                {
                    String select = @"UPDATE Inventario SET
                                    nombreCorto = @nom, descripcion = @des, serie = @ser, color = @col, 
                                    fechaAdquisicion = @fec, tipoAdquisicion = @tipo, observaciones = @ob, Areas_id = @aid
                                    WHERE id = @id;";

                    // Se modifico la manera de crear la sentencia de ejecución porque por alguna razón
                    // generaba una excepción al signar nulos
                    MySqlCommand sentencia = new MySqlCommand(select, Conexion.conexion);
                    //sentencia.CommandText = select;

                    sentencia.Parameters.AddWithValue("@nom", compra.nombreCorto);
                    sentencia.Parameters.AddWithValue("@des", compra.descripcion);
                    sentencia.Parameters.AddWithValue("@ser", compra.serie);
                    sentencia.Parameters.AddWithValue("@col", compra.color);
                    sentencia.Parameters.AddWithValue("@fec", compra.fechaAd);
                    sentencia.Parameters.AddWithValue("@tipo", compra.tipoAd);
                    sentencia.Parameters.AddWithValue("@ob", compra.observaciones);
                    sentencia.Parameters.AddWithValue("@aid", compra.id_area);
                    sentencia.Parameters.AddWithValue("@id", compra.id);
                    sentencia.Connection = Conexion.conexion;
                    sentencia.ExecuteScalar();
                    //Ejercutar el comando 
                    //Cuando nos interesa obtener un valor adicional en el comando (como en el ejemplo de arriba que obtiene el último id generado por autoincrement podemos usar ExecuteScalar
                    
                }
                finally
                {
                    tran.Commit();
                    Conexion.Desconectar();
                }
            }
            else
            {
                //Devolvemos un cero indicando que no se insertó nada
                return ;
            }
        }

        public void Delete(int id)
        {
            //Conectarme

            if (Conexion.Conectar())
            {
                MySqlTransaction tran = Conexion.conexion.BeginTransaction();
                try
                {
                    String select = @"DELETE FROM Inventario WHERE id = @id;";

                    // Se modifico la manera de crear la sentencia de ejecución porque por alguna razón
                    // generaba una excepción al signar nulos
                    MySqlCommand sentencia = new MySqlCommand(select, Conexion.conexion);
                    //sentencia.CommandText = select;

                    
                    sentencia.Parameters.AddWithValue("@id", id);
                    sentencia.Connection = Conexion.conexion;
                    sentencia.ExecuteScalar();
                    //Ejercutar el comando 
                    //Cuando nos interesa obtener un valor adicional en el comando (como en el ejemplo de arriba que obtiene el último id generado por autoincrement podemos usar ExecuteScalar

                }
                finally
                {
                    tran.Commit();
                    Conexion.Desconectar();
                }
            }
            else
            {
                //Devolvemos un cero indicando que no se insertó nada
                return;
            }
        }

    }
}

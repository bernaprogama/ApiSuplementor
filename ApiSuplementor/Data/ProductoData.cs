// Capa de datos de productos para SUPLEMENTOR
// Catálogo para invitados y gestión de productos (crear, actualizar, eliminar) para administradores

using ApiSuplementor.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiSuplementor.Data
{
    /// <summary>
    /// Clase que gestiona los datos de los productos (catálogo y administración).
    /// </summary>
    public class ProductoData
    {
        ConexionBD conexionBD = new ConexionBD();

        /// <summary>
        /// Devuelve el catálogo de productos activos (visible para invitados y usuarios).
        /// </summary>
        public List<Producto> ObtenerCatalogo()
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection conexion = conexionBD.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("usp_catalogo_productos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                SqlDataReader lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    Producto producto = new Producto();
                    producto.IdProducto = Convert.ToInt32(lector["IdProducto"]);
                    producto.Nombre = lector["Nombre"].ToString();
                    producto.Descripcion = lector["Descripcion"].ToString();
                    producto.Marca = lector["Marca"].ToString();
                    producto.Categoria = lector["Categoria"].ToString();
                    producto.Precio = Convert.ToDecimal(lector["Precio"]);
                    producto.Stock = Convert.ToInt32(lector["Stock"]);
                    producto.Activo = Convert.ToBoolean(lector["Activo"]);
                    lista.Add(producto);
                }
            }
            return lista;
        }

        /// <summary>
        /// Crea un producto nuevo en el catálogo (rol administrador).
        /// </summary>
        public bool Crear(Producto producto)
        {
            try
            {
                using (SqlConnection conexion = conexionBD.ObtenerConexion())
                {
                    SqlCommand comando = new SqlCommand("usp_crear_producto", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@nombre", producto.Nombre);
                    comando.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                    comando.Parameters.AddWithValue("@marca", producto.Marca);
                    comando.Parameters.AddWithValue("@categoria", producto.Categoria);
                    comando.Parameters.AddWithValue("@precio", producto.Precio);
                    comando.Parameters.AddWithValue("@stock", producto.Stock);
                    int filas = comando.ExecuteNonQuery();
                    return filas > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Actualiza los datos de un producto existente (rol administrador).
        /// </summary>
        public bool Actualizar(Producto producto)
        {
            try
            {
                using (SqlConnection conexion = conexionBD.ObtenerConexion())
                {
                    SqlCommand comando = new SqlCommand("usp_actualizar_producto", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@idproducto", producto.IdProducto);
                    comando.Parameters.AddWithValue("@nombre", producto.Nombre);
                    comando.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                    comando.Parameters.AddWithValue("@marca", producto.Marca);
                    comando.Parameters.AddWithValue("@categoria", producto.Categoria);
                    comando.Parameters.AddWithValue("@precio", producto.Precio);
                    comando.Parameters.AddWithValue("@stock", producto.Stock);
                    int filas = comando.ExecuteNonQuery();
                    return filas > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Elimina un producto del catálogo (eliminación lógica: pone Activo = 0).
        /// El producto se conserva en el historial de ventas.
        /// </summary>
        public bool Eliminar(int idProducto)
        {
            try
            {
                using (SqlConnection conexion = conexionBD.ObtenerConexion())
                {
                    SqlCommand comando = new SqlCommand("usp_eliminar_producto", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@idproducto", idProducto);
                    int filas = comando.ExecuteNonQuery();
                    return filas > 0;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
// Capa de datos de perfil físico para SUPLEMENTOR
// Guarda el perfil físico del usuario y genera la recomendación de suplementos

using ApiSuplementor.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiSuplementor.Data
{
    /// <summary>
    /// Clase que gestiona el perfil físico y el recomendador de suplementos.
    /// </summary>
    public class PerfilData
    {
        ConexionBD conexionBD = new ConexionBD();

        /// <summary>
        /// Guarda el perfil físico del usuario (estado actual y objetivo).
        /// </summary>
        public bool GuardarPerfil(PerfilFisico perfil)
        {
            try
            {
                using (SqlConnection conexion = conexionBD.ObtenerConexion())
                {
                    SqlCommand comando = new SqlCommand("usp_guardar_perfil", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@idusuario", perfil.IdUsuario);
                    comando.Parameters.AddWithValue("@pesokg", perfil.PesoKg);
                    comando.Parameters.AddWithValue("@alturacm", perfil.AlturaCm);
                    comando.Parameters.AddWithValue("@edad", perfil.Edad);
                    comando.Parameters.AddWithValue("@sexo", perfil.Sexo);
                    comando.Parameters.AddWithValue("@objetivo", perfil.Objetivo);
                    comando.Parameters.AddWithValue("@nivelactividad", perfil.NivelActividad);
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
        /// Recomendador: devuelve los suplementos activos recomendados
        /// según el objetivo de entrenamiento del usuario.
        /// </summary>
        public List<Producto> Recomendar(string objetivo)
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection conexion = conexionBD.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("usp_recomendacion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@objetivo", objetivo);
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
    }
}
// Clase de conexión a la base de datos de SQL Server
// Abre y cierra la conexión con DB_SUPLEMENTOR

using Microsoft.Data.SqlClient;

namespace ApiSuplementor.Data
{
    /// <summary>
    /// Clase encargada de abrir y cerrar la conexión con la base de datos de SUPLEMENTOR.
    /// </summary>
    public class ConexionBD
    {
        // Cadena de conexión: servidor, base de datos y autenticación de Windows
        private string cadenaConexion = @"Data Source=.\SQLEXPRESS;Initial Catalog=DB_SUPLEMENTOR;Integrated Security=True;TrustServerCertificate=True";

        /// <summary>
        /// Abre la conexión con la base de datos y la devuelve lista para usar.
        /// </summary>
        public SqlConnection ObtenerConexion()
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
            return conexion;
        }
    }
}
// Capa de datos de usuarios para SUPLEMENTOR
// Contiene los métodos que leen y escriben la tabla USUARIO mediante procedimientos almacenados

using ApiSuplementor.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiSuplementor.Data
{
    /// <summary>
    /// Clase que gestiona los datos de los usuarios (registro, login con rol y validación de correo).
    /// </summary>
    public class UsuarioData
    {
        ConexionBD conexionBD = new ConexionBD();

        /// <summary>
        /// Registra un usuario nuevo en la tabla USUARIO.
        /// Devuelve true si el registro fue exitoso, false en caso contrario.
        /// </summary>
        public bool Registrar(Usuario usuario)
        {
            try
            {
                using (SqlConnection conexion = conexionBD.ObtenerConexion())
                {
                    SqlCommand comando = new SqlCommand("usp_registrar_usuario", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@nombres", usuario.Nombres);
                    comando.Parameters.AddWithValue("@apellidos", usuario.Apellidos);
                    comando.Parameters.AddWithValue("@correo", usuario.Correo);
                    comando.Parameters.AddWithValue("@contrasena", usuario.Contrasena);
                    comando.Parameters.AddWithValue("@telefono", usuario.Telefono);
                    comando.Parameters.AddWithValue("@rol", usuario.Rol);
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
        /// Valida las credenciales y devuelve el usuario con su rol si la autenticación es exitosa.
        /// Devuelve null si el correo o la contraseña son incorrectos.
        /// </summary>
        public Usuario Login(string correo, string contrasena)
        {
            using (SqlConnection conexion = conexionBD.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("usp_login_usuario", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@correo", correo);
                comando.Parameters.AddWithValue("@contrasena", contrasena);
                SqlDataReader lector = comando.ExecuteReader();
                if (lector.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.IdUsuario = Convert.ToInt32(lector["IdUsuario"]);
                    usuario.Nombres = lector["Nombres"].ToString();
                    usuario.Apellidos = lector["Apellidos"].ToString();
                    usuario.Correo = lector["Correo"].ToString();
                    usuario.Rol = Convert.ToInt32(lector["Rol"]);
                    return usuario;
                }
                return null;
            }
        }

        /// <summary>
        /// Verifica si un correo ya está registrado en el sistema.
        /// Devuelve true si existe, false en caso contrario.
        /// </summary>
        public bool CorreoExiste(string correo)
        {
            using (SqlConnection conexion = conexionBD.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("usp_correo_existe", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@correo", correo);
                int cantidad = Convert.ToInt32(comando.ExecuteScalar());
                return cantidad > 0;
            }
        }
    }
}
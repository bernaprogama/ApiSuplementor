// Modelo de Usuario para SUPLEMENTOR
// Representa a una persona que se registra en la aplicación (usuario o administrador).
// El rol "invitado" no se registra en base de datos: es quien navega sin cuenta.

namespace ApiSuplementor.Models
{
    /// <summary>
    /// Clase que representa un usuario del sistema SUPLEMENTOR.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Identificador único del usuario (autoincremental en la base de datos).
        /// </summary>
        public int IdUsuario { get; set; }

        /// <summary>
        /// Nombres del usuario.
        /// </summary>
        public string Nombres { get; set; }

        /// <summary>
        /// Apellidos del usuario.
        /// </summary>
        public string Apellidos { get; set; }

        /// <summary>
        /// Correo electrónico (debe ser único, sirve para login).
        /// </summary>
        public string Correo { get; set; }

        /// <summary>
        /// Contraseña del usuario.
        /// </summary>
        public string Contrasena { get; set; }

        /// <summary>
        /// Teléfono de contacto.
        /// </summary>
        public string Telefono { get; set; }

        /// <summary>
        /// Rol del usuario: 1=Invitado, 2=Usuario registrado, 3=Administrador.
        /// </summary>
        public int Rol { get; set; }

        /// <summary>
        /// Fecha en que se registró el usuario.
        /// </summary>
        public DateTime FechaRegistro { get; set; }
    }
}
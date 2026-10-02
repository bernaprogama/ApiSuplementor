// Controlador de usuarios para SUPLEMENTOR
// Expone los servicios de registro y login (con rol)

using ApiSuplementor.Data;
using ApiSuplementor.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiSuplementor.Controllers
{
    /// <summary>
    /// Controlador que expone los servicios de usuarios (registro y login).
    /// </summary>
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        UsuarioData usuarioData = new UsuarioData();

        /// <summary>
        /// Servicio que registra un usuario nuevo en el sistema.
        /// </summary>
        [HttpPost("registrar")]
        public IActionResult Registrar([FromBody] Usuario usuario)
        {
            // Si no se indica rol, se registra como usuario registrado (2) por defecto
            if (usuario.Rol == 0) usuario.Rol = 2;

            if (usuarioData.CorreoExiste(usuario.Correo))
            {
                return BadRequest(new { mensaje = "El correo ya se encuentra registrado" });
            }

            bool registrado = usuarioData.Registrar(usuario);
            if (registrado)
            {
                return Ok(new { mensaje = "Usuario registrado correctamente" });
            }
            return BadRequest(new { mensaje = "Error: no se pudo registrar el usuario" });
        }

        /// <summary>
        /// Servicio que valida las credenciales y devuelve el usuario con su rol.
        /// </summary>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest datos)
        {
            Usuario usuario = usuarioData.Login(datos.Correo, datos.Contrasena);
            if (usuario == null)
            {
                return Unauthorized(new { mensaje = "Error en la autenticacion: correo o contrasena incorrectos" });
            }
            return Ok(new
            {
                mensaje = "Autenticacion satisfactoria",
                usuario = usuario.Nombres + " " + usuario.Apellidos,
                rol = usuario.Rol
            });
        }
    }

    /// <summary>
    /// Clase que recibe únicamente correo y contraseña para el servicio de login.
    /// </summary>
    public class LoginRequest
    {
        public string Correo { get; set; }
        public string Contrasena { get; set; }
    }
}
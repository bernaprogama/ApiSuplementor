// Controlador de recomendación para SUPLEMENTOR
// Expone el servicio de guardado de perfil físico y el servicio recomendador

using ApiSuplementor.Data;
using ApiSuplementor.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiSuplementor.Controllers
{
    /// <summary>
    /// Controlador que expone los servicios del recomendador (perfil y recomendación).
    /// </summary>
    [ApiController]
    [Route("api/recomendacion")]
    public class RecomendacionController : ControllerBase
    {
        PerfilData perfilData = new PerfilData();

        /// <summary>
        /// Servicio que guarda el perfil físico del usuario (estado actual y objetivo).
        /// </summary>
        [HttpPost("perfil")]
        public IActionResult GuardarPerfil([FromBody] PerfilFisico perfil)
        {
            bool guardado = perfilData.GuardarPerfil(perfil);
            if (guardado)
            {
                return Ok(new { mensaje = "Perfil fisico guardado correctamente" });
            }
            return BadRequest(new { mensaje = "Error: no se pudo guardar el perfil fisico" });
        }

        /// <summary>
        /// Servicio que devuelve los suplementos recomendados según el objetivo de entrenamiento.
        /// </summary>
        [HttpPost("recomendar")]
        public IActionResult Recomendar([FromBody] RecomendacionRequest datos)
        {
            // Validar que el objetivo sea uno de los cuatro válidos
            string objetivo = datos.Objetivo;
            if (objetivo != "PerderGrasa" && objetivo != "GanarMasaMuscular"
                && objetivo != "Mantenimiento" && objetivo != "Rendimiento")
            {
                return BadRequest(new { mensaje = "Objetivo no valido. Opciones: PerderGrasa, GanarMasaMuscular, Mantenimiento, Rendimiento" });
            }

            List<Producto> recomendados = perfilData.Recomendar(objetivo);
            if (recomendados.Count == 0)
            {
                return NotFound(new { mensaje = "No hay suplementos disponibles para este objetivo" });
            }
            return Ok(new { objetivo = objetivo, recomendados = recomendados });
        }
    }

    /// <summary>
    /// Clase que recibe únicamente el objetivo de entrenamiento para el servicio de recomendación.
    /// </summary>
    public class RecomendacionRequest
    {
        public string Objetivo { get; set; }
    }
}
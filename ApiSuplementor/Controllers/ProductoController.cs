// Controlador de productos para SUPLEMENTOR
// Expone el catálogo (invitado) y los servicios de administración (administrador)

using ApiSuplementor.Data;
using ApiSuplementor.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiSuplementor.Controllers
{
    /// <summary>
    /// Controlador que expone los servicios de productos (catálogo y administración).
    /// </summary>
    [ApiController]
    [Route("api/producto")]
    public class ProductoController : ControllerBase
    {
        ProductoData productoData = new ProductoData();

        /// <summary>
        /// Servicio que devuelve el catálogo de productos activos (público, para invitados).
        /// </summary>
        [HttpGet("catalogo")]
        public IActionResult Catalogo()
        {
            List<Producto> productos = productoData.ObtenerCatalogo();
            return Ok(productos);
        }

        /// <summary>
        /// Servicio que crea un producto nuevo (rol administrador).
        /// </summary>
        [HttpPost("crear")]
        public IActionResult Crear([FromBody] Producto producto)
        {
            bool creado = productoData.Crear(producto);
            if (creado)
            {
                return Ok(new { mensaje = "Producto creado correctamente" });
            }
            return BadRequest(new { mensaje = "Error: no se pudo crear el producto" });
        }

        /// <summary>
        /// Servicio que actualiza un producto existente (rol administrador).
        /// </summary>
        [HttpPut("actualizar")]
        public IActionResult Actualizar([FromBody] Producto producto)
        {
            bool actualizado = productoData.Actualizar(producto);
            if (actualizado)
            {
                return Ok(new { mensaje = "Producto actualizado correctamente" });
            }
            return BadRequest(new { mensaje = "Error: no se pudo actualizar el producto" });
        }

        /// <summary>
        /// Servicio que elimina un producto del catálogo (rol administrador, eliminación lógica).
        /// </summary>
        [HttpDelete("eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            bool eliminado = productoData.Eliminar(id);
            if (eliminado)
            {
                return Ok(new { mensaje = "Producto eliminado del catalogo" });
            }
            return BadRequest(new { mensaje = "Error: no se pudo eliminar el producto" });
        }
    }
}
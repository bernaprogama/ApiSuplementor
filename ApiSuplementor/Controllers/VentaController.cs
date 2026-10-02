// Controlador de ventas para SUPLEMENTOR
// Expone el servicio de registro de compras para usuarios registrados

using ApiSuplementor.Data;
using ApiSuplementor.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiSuplementor.Controllers
{
    /// <summary>
    /// Controlador que expone los servicios de ventas (registro de compras).
    /// </summary>
    [ApiController]
    [Route("api/venta")]
    public class VentaController : ControllerBase
    {
        VentaData ventaData = new VentaData();

        /// <summary>
        /// Servicio que registra una compra de un usuario registrado.
        /// Recibe el usuario y los productos, calcula el total y guarda todo como transacción.
        /// </summary>
        [HttpPost("registrar")]
        public IActionResult Registrar([FromBody] VentaRequest datos)
        {
            if (datos.Detalles == null || datos.Detalles.Count == 0)
            {
                return BadRequest(new { mensaje = "La compra debe tener al menos un producto" });
            }

            // El total se calcula en el servidor a partir de los detalles
            decimal total = 0;
            foreach (DetalleVenta detalle in datos.Detalles)
            {
                total += detalle.Cantidad * detalle.PrecioUnitario;
            }

            Venta venta = new Venta();
            venta.IdUsuario = datos.IdUsuario;
            venta.FechaVenta = DateTime.Now;
            venta.Total = total;
            venta.Estado = "Pagada";

            bool registrada = ventaData.RegistrarVenta(venta, datos.Detalles);
            if (registrada)
            {
                return Ok(new { mensaje = "Venta registrada correctamente", total = total });
            }
            return BadRequest(new { mensaje = "Error: no se pudo registrar la venta (verifique stock)" });
        }
    }

    /// <summary>
    /// Clase que recibe los datos de la compra: usuario y detalle de productos.
    /// </summary>
    public class VentaRequest
    {
        public int IdUsuario { get; set; }
        public List<DetalleVenta> Detalles { get; set; }
    }
}
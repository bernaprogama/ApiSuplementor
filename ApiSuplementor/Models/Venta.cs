// Modelo de Venta para SUPLEMENTOR
// Representa una compra completa realizada por un usuario registrado.

namespace ApiSuplementor.Models
{
    /// <summary>
    /// Clase que representa una venta (compra) en SUPLEMENTOR.
    /// </summary>
    public class Venta
    {
        /// <summary>
        /// Identificador único de la venta (autoincremental en la base de datos).
        /// </summary>
        public int IdVenta { get; set; }

        /// <summary>
        /// Identificador del usuario registrado que realizó la compra.
        /// </summary>
        public int IdUsuario { get; set; }

        /// <summary>
        /// Fecha y hora en que se realizó la venta.
        /// </summary>
        public DateTime FechaVenta { get; set; }

        /// <summary>
        /// Valor total de la compra en pesos colombianos.
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// Estado de la venta: Pagada, Pendiente o Cancelada.
        /// </summary>
        public string Estado { get; set; }
    }
}
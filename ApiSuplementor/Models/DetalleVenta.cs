// Modelo de DetalleVenta para SUPLEMENTOR
// Representa cada línea de una venta: qué producto, cuántas unidades y a qué precio.

namespace ApiSuplementor.Models
{
    /// <summary>
    /// Clase que representa el detalle (productos) de una venta.
    /// </summary>
    public class DetalleVenta
    {
        /// <summary>
        /// Identificador único del detalle (autoincremental en la base de datos).
        /// </summary>
        public int IdDetalle { get; set; }

        /// <summary>
        /// Identificador de la venta a la que pertenece este detalle.
        /// </summary>
        public int IdVenta { get; set; }

        /// <summary>
        /// Identificador del producto (suplemento) vendido.
        /// </summary>
        public int IdProducto { get; set; }

        /// <summary>
        /// Cantidad de unidades compradas de ese producto.
        /// </summary>
        public int Cantidad { get; set; }

        /// <summary>
        /// Precio unitario del producto al momento de la compra.
        /// </summary>
        public decimal PrecioUnitario { get; set; }
    }
}
// Modelo de Producto para SUPLEMENTOR
// Representa un suplemento deportivo del catálogo (proteínas, creatina, vitaminas, etc.)

namespace ApiSuplementor.Models
{
    /// <summary>
    /// Clase que representa un producto (suplemento deportivo) del catálogo.
    /// </summary>
    public class Producto
    {
        /// <summary>
        /// Identificador único del producto (autoincremental en la base de datos).
        /// </summary>
        public int IdProducto { get; set; }

        /// <summary>
        /// Nombre comercial del suplemento.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Descripción del producto (contenido, sabor, presentación).
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Marca del suplemento.
        /// </summary>
        public string Marca { get; set; }

        /// <summary>
        /// Categoría: Proteina, Creatina, Vitaminas, Aminoacidos, etc.
        /// </summary>
        public string Categoria { get; set; }

        /// <summary>
        /// Precio de venta en pesos colombianos.
        /// </summary>
        public decimal Precio { get; set; }

        /// <summary>
        /// Unidades disponibles en inventario.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Indica si el producto está activo (true) u oculto (false) en el catálogo.
        /// </summary>
        public bool Activo { get; set; }
    }
}
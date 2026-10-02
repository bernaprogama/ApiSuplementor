// Capa de datos de ventas para SUPLEMENTOR
// Registra la compra completa (venta + detalles) y descuenta el stock de los productos

using ApiSuplementor.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiSuplementor.Data
{
    /// <summary>
    /// Clase que gestiona el registro de ventas como una transacción:
    /// si algún paso falla, todo se reversa para proteger los datos.
    /// </summary>
    public class VentaData
    {
        ConexionBD conexionBD = new ConexionBD();

        /// <summary>
        /// Registra una venta completa: crea la venta, agrega cada detalle
        /// y descuenta el stock de los productos. Usa transacción para proteger los datos.
        /// Devuelve true si la compra quedó registrada con éxito.
        /// </summary>
        public bool RegistrarVenta(Venta venta, List<DetalleVenta> detalles)
        {
            using (SqlConnection conexion = conexionBD.ObtenerConexion())
            {
                SqlTransaction transaccion = conexion.BeginTransaction();
                try
                {
                    // 1. Crear la venta y obtener el consecutivo que asigna la base de datos
                    SqlCommand comandoVenta = new SqlCommand("usp_registrar_venta", conexion, transaccion);
                    comandoVenta.CommandType = CommandType.StoredProcedure;
                    comandoVenta.Parameters.AddWithValue("@idusuario", venta.IdUsuario);
                    comandoVenta.Parameters.AddWithValue("@total", venta.Total);
                    comandoVenta.Parameters.AddWithValue("@estado", venta.Estado);
                    int idVenta = Convert.ToInt32(comandoVenta.ExecuteScalar());

                    // 2. Agregar cada producto de la compra y descontar su stock
                    foreach (DetalleVenta detalle in detalles)
                    {
                        SqlCommand comandoDetalle = new SqlCommand("usp_agregar_detalle", conexion, transaccion);
                        comandoDetalle.CommandType = CommandType.StoredProcedure;
                        comandoDetalle.Parameters.AddWithValue("@idventa", idVenta);
                        comandoDetalle.Parameters.AddWithValue("@idproducto", detalle.IdProducto);
                        comandoDetalle.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                        comandoDetalle.Parameters.AddWithValue("@preciounitario", detalle.PrecioUnitario);
                        comandoDetalle.ExecuteNonQuery();

                        SqlCommand comandoStock = new SqlCommand("usp_descontar_stock", conexion, transaccion);
                        comandoStock.CommandType = CommandType.StoredProcedure;
                        comandoStock.Parameters.AddWithValue("@idproducto", detalle.IdProducto);
                        comandoStock.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                        comandoStock.ExecuteNonQuery();
                    }

                    // 3. Si llegó hasta aquí sin errores, confirma todo
                    transaccion.Commit();
                    return true;
                }
                catch
                {
                    // Si algo falló, reversa toda la compra (no queda nada guardado a medias)
                    transaccion.Rollback();
                    return false;
                }
            }
        }
    }
}
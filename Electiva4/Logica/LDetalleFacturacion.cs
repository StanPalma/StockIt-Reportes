using Electiva4.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Electiva4.Logica
{
    public class LDetalleFacturacion
    {
        WSStockIt.WebServiceSI WS = new WSStockIt.WebServiceSI();

        public List<EDetalleFacturacion> SeleccionarDetalleFacturacionByIdEncabezadoFacturacion(int idEncabezadoFacturacion)
        {
            List<EDetalleFacturacion> eDetalleFacturacionList = new List<EDetalleFacturacion>();
            try
            {
                DataSet ds = WS.seleccionarDetalleFacturacionByIdEncabezadoFacturacion(idEncabezadoFacturacion);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    EDetalleFacturacion eDetalleFacturacion = new EDetalleFacturacion();
                    eDetalleFacturacion.NombreProducto = row["NOMBRE_PRODUCTO"].ToString();
                    eDetalleFacturacion.Cantidad = int.Parse(row["CANTIDAD"].ToString());
                    eDetalleFacturacion.Precio = double.Parse(row["PRECIO"].ToString());
                    eDetalleFacturacion.MontoDetalleFacturacion = double.Parse(row["MONTO_DETALLE_FACTURACION"].ToString());
                    eDetalleFacturacionList.Add(eDetalleFacturacion);
                }

                return eDetalleFacturacionList;
            }
            catch (Exception)
            {
                return eDetalleFacturacionList;
            }
        }

        public List<EReporteFacturacionDetalle> DetalleReporteVentaProductosFiltros(int idProducto, DateTime fechaInicio, DateTime fechaFinal, int idCategoria, int idUsuario)
        {
            List<EReporteFacturacionDetalle> lista = new List<EReporteFacturacionDetalle>();
            try
            {
                DataSet ds = WS.detalleReporteVentaProductosFiltros(idProducto, fechaInicio, fechaFinal, idCategoria, idUsuario);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    EReporteFacturacionDetalle eReporteFacturacionDetalle = new EReporteFacturacionDetalle();
                    eReporteFacturacionDetalle.IdEncabezadoFacturacion = int.Parse(row["ID_ENCABEZADO_FACTURACION"].ToString());
                    eReporteFacturacionDetalle.IdProducto = int.Parse(row["ID_PRODUCTO"].ToString());
                    eReporteFacturacionDetalle.NombreProducto = row["NOMBRE_PRODUCTO"].ToString();
                    eReporteFacturacionDetalle.Cantidad = int.Parse(row["CANTIDAD"].ToString());
                    eReporteFacturacionDetalle.Precio = double.Parse(row["PRECIO"].ToString());
                    eReporteFacturacionDetalle.MontoDetalleFacturacion = double.Parse(row["MONTO_DETALLE_FACTURACION"].ToString());
                    eReporteFacturacionDetalle.Categoria = row["CATEGORIA"].ToString();
                    eReporteFacturacionDetalle.NombreProveedor = row["NOMBRE_PROVEEDOR"].ToString();
                    eReporteFacturacionDetalle.FechaFacturacion = DateTime.Parse(row["FECHA_FACTURACION"].ToString());
                    lista.Add(eReporteFacturacionDetalle);
                }

                return lista;
            }
            catch (Exception)
            {
                return lista;
            }
        }
    }
}
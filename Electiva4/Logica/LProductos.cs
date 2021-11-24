using Electiva4.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Electiva4.Logica
{
    public class LProductos
    {
        WSStockIt.WebServiceSI WS = new WSStockIt.WebServiceSI();

        //Método para generar Encabezados Compra de Productos
        public List<EReporteProductosEncabezado> EncabezadosReporteCompraProductos(DateTime fechaInicio, DateTime fechaFinal, int idUsuario)
        {
            List<EReporteProductosEncabezado> lista = new List<EReporteProductosEncabezado>();
            try
            {
                DataSet ds = WS.encabezadosReporteCompraProductos(fechaInicio, fechaFinal, idUsuario);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    EReporteProductosEncabezado eReportePRoductosEncabezado = new EReporteProductosEncabezado();
                    eReportePRoductosEncabezado.IdEncCompraProductos = int.Parse(row["ID_ENC_COMPRA_PRODUCTOS"].ToString());
                    eReportePRoductosEncabezado.NombreProveedor = row["NOMBRE_PROVEEDOR"].ToString();
                    eReportePRoductosEncabezado.FechaIngreso = DateTime.Parse(row["FECHA_INGRESO"].ToString());
                    eReportePRoductosEncabezado.Monto = double.Parse(row["MONTO"].ToString());
                    lista.Add(eReportePRoductosEncabezado);
                }

                return lista;
            }
            catch (Exception)
            {
                return lista;
            }
        }

        //Método para generar Detalle Compra de Productos de una compra en específico
        public List<EReporteProductosDetalle> DetalleReporteCompraProductos(int idEncabezadoCompra)
        {
            List<EReporteProductosDetalle> lista = new List<EReporteProductosDetalle>();
            try
            {
                DataSet ds = WS.detalleReporteCompraProductos(idEncabezadoCompra);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    EReporteProductosDetalle eReporteProductosDetalle = new EReporteProductosDetalle();
                    eReporteProductosDetalle.IdEncCompraProductos = int.Parse(row["ID_ENC_COMPRA_PRODUCTOS"].ToString());
                    eReporteProductosDetalle.IdProducto = int.Parse(row["ID_PRODUCTO"].ToString());
                    eReporteProductosDetalle.NombreProducto = row["NOMBRE_PRODUCTO"].ToString();
                    eReporteProductosDetalle.Cantidad = int.Parse(row["CANTIDAD"].ToString());
                    eReporteProductosDetalle.PrecioLote = double.Parse(row["PRECIO_LOTE"].ToString());
                    eReporteProductosDetalle.PrecioUnitario = double.Parse(row["PRECIO_UNITARIO"].ToString());
                    eReporteProductosDetalle.PrecioVenta = double.Parse(row["PRECIO_REAL"].ToString());
                    eReporteProductosDetalle.Categoria = row["CATEGORIA"].ToString();
                    lista.Add(eReporteProductosDetalle);
                }

                return lista;
            }
            catch (Exception)
            {
                return lista;
            }
        }

        //Método para generar Reporte de Productos Específicado
        public List<EReporteProductosDetalle> DetalleReporteCompraProductosFiltros(int idProducto, DateTime fechaInicio, DateTime fechaFinal, int idCategoria, int idUsuario)
        {
            List<EReporteProductosDetalle> lista = new List<EReporteProductosDetalle>();
            try
            {
                DataSet ds = WS.detalleReporteCompraProductosFiltros(idProducto, fechaInicio, fechaFinal, idCategoria, idUsuario);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    EReporteProductosDetalle eReporteProductosDetalle = new EReporteProductosDetalle();
                    eReporteProductosDetalle.IdProducto = int.Parse(row["ID_PRODUCTO"].ToString());
                    eReporteProductosDetalle.NombreProducto = row["NOMBRE_PRODUCTO"].ToString();
                    eReporteProductosDetalle.Cantidad = int.Parse(row["CANTIDAD"].ToString());
                    eReporteProductosDetalle.PrecioLote = double.Parse(row["PRECIO_LOTE"].ToString());
                    eReporteProductosDetalle.PrecioUnitario = double.Parse(row["PRECIO_UNITARIO"].ToString());
                    eReporteProductosDetalle.PrecioVenta = double.Parse(row["PRECIO_REAL"].ToString());
                    eReporteProductosDetalle.NombreProveedor = row["NOMBRE_PROVEEDOR"].ToString();
                    eReporteProductosDetalle.FechaIngreso = DateTime.Parse(row["FECHA_INGRESO"].ToString());
                    lista.Add(eReporteProductosDetalle);
                }

                return lista;
            }
            catch (Exception)
            {
                return lista;
            }
        }

        //Método para llenar DropDownList de Productos en CompraProductosEsp
        public DataTable seleccionarProductosByIdCategoria(int idCategoria)
        {
            try
            {
                DataSet ds = WS.seleccionarProductosByIdCategoria(idCategoria);

                return ds.Tables[0];
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }

        public DataTable SeleccionarProductosByIdUsuarioFechasAndIdCategoriaForReporte(int idUsuario, DateTime fechaInicio, DateTime fechaFinal, int idCategoria)
        {
            try
            {
                DataSet ds = WS.seleccionarProductosByIdUsuarioFechasAndIdCategoriaForReporte(idUsuario, fechaInicio, fechaFinal, idCategoria);

                return ds.Tables[0];
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }

        public List<EProducto> ListSeleccionarProductosByIdUsuarioFechasAndIdCategoriaForReporte(int idUsuario, DateTime fechaInicio, DateTime fechaFinal, int idCategoria)
        {
            List<EProducto> lista = new List<EProducto>();
            try
            {
                DataSet ds = WS.seleccionarProductosByIdUsuarioFechasAndIdCategoriaForReporte(idUsuario, fechaInicio, fechaFinal, idCategoria);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    EProducto eProducto = new EProducto();
                    eProducto.IdProducto = int.Parse(row["ID_PRODUCTO"].ToString());
                    eProducto.NombreProducto = row["NOMBRE_PRODUCTO"].ToString();
                    lista.Add(eProducto);
                }

                return lista;
            }
            catch (Exception)
            {
                return lista;
            }
        }

        public DataTable SeleccionarProductosByIdUsuarioFechasAndIdCategoriaForReportePE(int idUsuario, DateTime fechaInicio, DateTime fechaFinal, int idCategoria)
        {
            try
            {
                DataSet ds = WS.seleccionarProductosByIdUsuarioFechasAndIdCategoriaForReportePE(idUsuario, fechaInicio, fechaFinal, idCategoria);

                return ds.Tables[0];
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }

        public List<EProducto> ListSeleccionarProductosByIdUsuarioFechasAndIdCategoriaForReportePE(int idUsuario, DateTime fechaInicio, DateTime fechaFinal, int idCategoria)
        {
            List<EProducto> lista = new List<EProducto>();
            try
            {
                DataSet ds = WS.seleccionarProductosByIdUsuarioFechasAndIdCategoriaForReportePE(idUsuario, fechaInicio, fechaFinal, idCategoria);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    EProducto eProducto = new EProducto();
                    eProducto.IdProducto = int.Parse(row["ID_PRODUCTO"].ToString());
                    eProducto.NombreProducto = row["NOMBRE_PRODUCTO"].ToString();
                    lista.Add(eProducto);
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
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
    }
}
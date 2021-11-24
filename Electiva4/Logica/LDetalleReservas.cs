using Electiva4.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Electiva4.Logica
{
    public class LDetalleReservas
    {
        WSStockIt.WebServiceSI WS = new WSStockIt.WebServiceSI();

        public List<EReporteReservasDetalle> DetalleReporteReservas(int idEncabezadoReserva)
        {
            List<EReporteReservasDetalle> lista = new List<EReporteReservasDetalle>();
            try
            {
                DataSet ds = WS.detalleReporteReservas(idEncabezadoReserva);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    EReporteReservasDetalle eReporteReservasDetalle = new EReporteReservasDetalle();
                    eReporteReservasDetalle.IdEncabezadoReserva = int.Parse(row["ID_ENCABEZADO_RESERVAS"].ToString());
                    eReporteReservasDetalle.IdProducto = int.Parse(row["ID_PRODUCTO"].ToString());
                    eReporteReservasDetalle.NombreProducto = row["NOMBRE_PRODUCTO"].ToString();
                    eReporteReservasDetalle.Cantidad = int.Parse(row["CANTIDAD"].ToString());
                    eReporteReservasDetalle.PrecioProducto = double.Parse(row["PRECIO"].ToString());
                    eReporteReservasDetalle.Monto = double.Parse(row["MONTO_DETALLE_RESERVA"].ToString());
                    lista.Add(eReporteReservasDetalle);
                }

                return lista;
            }
            catch (Exception)
            {
                return lista;
            }
        }

        public List<EEstadoReserva> ListSeleccionarEstadosReservaByIdUsuarioAndFechasForReporte(int idUsuario, DateTime fechaInicio, DateTime fechaFinal)
        {
            List<EEstadoReserva> lista = new List<EEstadoReserva>();
            try
            {
                DataSet ds = WS.seleccionarEstadosReservaByIdUsuarioAndFechasForReporte(idUsuario, fechaInicio, fechaFinal);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    EEstadoReserva eEstadoReserva = new EEstadoReserva();
                    eEstadoReserva.EstadoReserva = row["ESTADO_RESERVA"].ToString();
                    eEstadoReserva.NombreEstadoReserva = row["NOMBRE_ESTADO"].ToString();
                    lista.Add(eEstadoReserva);
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
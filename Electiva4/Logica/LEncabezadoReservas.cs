using Electiva4.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Electiva4.Logica
{
    public class LEncabezadoReservas
    {
        WSStockIt.WebServiceSI WS = new WSStockIt.WebServiceSI();

        private string ESTADO_CANCELADA_CLIENTE = "C";

        public List<EReporteReservasEncabezado> EncabezadosReporteReservas(DateTime fechaInicio, DateTime fechaFinal, int idUsuario, string estadoReserva,
            int idCliente)
        {
            List<EReporteReservasEncabezado> lista = new List<EReporteReservasEncabezado>();
            try
            {
                DataSet ds = WS.encabezadosReporteReservas(fechaInicio, fechaFinal, idUsuario, estadoReserva, idCliente);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    EReporteReservasEncabezado eReporteReservasEncabezado = new EReporteReservasEncabezado();
                    eReporteReservasEncabezado.IdEncabezadoReserva = int.Parse(row["ID_ENCABEZADO_RESERVAS"].ToString());
                    eReporteReservasEncabezado.NombreCliente = row["NOMBRE_CLIENTE"].ToString();
                    eReporteReservasEncabezado.ApellidoCliente = row["APELLIDO_CLIENTE"].ToString();
                    eReporteReservasEncabezado.TelefonoCliente = row["TELEFONO_CLIENTE"].ToString();
                    eReporteReservasEncabezado.FechaReserva = DateTime.Parse(row["FECHA_RESERVA"].ToString());
                    eReporteReservasEncabezado.FechaPromesaEntrega = DateTime.Parse(row["FECHA_PROMESA_RESERVA"].ToString());
                    eReporteReservasEncabezado.MontoEncabezadoReserva = double.Parse(row["MONTO_ENCABEZADO_RESERVA"].ToString());
                    eReporteReservasEncabezado.EstadoReserva = row["ESTADO_RESERVA"].ToString() == ESTADO_CANCELADA_CLIENTE
                        ? "CANCELADA POR EL CLIENTE"
                        : "RESERVA EXPIRADA";
                    eReporteReservasEncabezado.Comentarios = row["COMENTARIOS"].ToString();
                    lista.Add(eReporteReservasEncabezado);
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
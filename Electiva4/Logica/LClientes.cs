using Electiva4.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Electiva4.Logica
{
    public class LClientes
    {
        WSStockIt.WebServiceSI WS = new WSStockIt.WebServiceSI();
        public List<ECliente> SeleccionarClientesActivosByIdUsuarioForReporte(int idUsuario, DateTime fechaInicio, DateTime fechaFinal)
        {
            List<ECliente> lista = new List<ECliente>();
            try
            {
                DataSet ds = WS.seleccionarClientesActivosByIdUsuarioForReporte(idUsuario, fechaInicio, fechaFinal);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ECliente eCliente = new ECliente();
                    eCliente.IdCliente = int.Parse(row["ID_CLIENTE"].ToString());
                    eCliente.NombreCliente = row["CLIENTE"].ToString();
                    lista.Add(eCliente);
                }

                return lista;
            }
            catch (Exception)
            {
                return lista;
            }
        }

        public List<ECliente> SeleccionarClientesByIdUsuarioFechasAndEstadoReservaForReporte(int idUsuario, DateTime fechaInicio, DateTime fechaFinal,
            string estadoReserva)
        {
            List<ECliente> lista = new List<ECliente>();
            try
            {
                DataSet ds = WS.seleccionarClientesByIdUsuarioFechasAndEstadoReservaForReporte(idUsuario, fechaInicio, fechaFinal, estadoReserva);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ECliente eCliente = new ECliente();
                    eCliente.IdCliente = int.Parse(row["ID_CLIENTE"].ToString());
                    eCliente.NombreCliente = row["CLIENTE"].ToString();
                    lista.Add(eCliente);
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
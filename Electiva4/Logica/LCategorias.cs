using Electiva4.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Electiva4.Logica
{
    public class LCategorias
    {
        WSStockIt.WebServiceSI WS = new WSStockIt.WebServiceSI();
        public DataTable SeleccionarCategoriasActivasByIdUsuarioAndFechasForReporteVE(int idUsuario, DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                DataSet ds = WS.seleccionarCategoriasActivasByIdUsuarioAndFechasForReporte(idUsuario, fechaInicio, fechaFinal);

                return ds.Tables[0];
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }

        public DataTable SeleccionarCategoriasActivasByIdUsuarioAndFechasForReportePE(int idUsuario, DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                DataSet ds = WS.seleccionarCategoriasActivasByIdUsuarioAndFechasForReportePE(idUsuario, fechaInicio, fechaFinal);

                return ds.Tables[0];
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }

        public List<ECategoria> ListSeleccionarCategoriasActivasByIdUsuarioAndFechasForReportePE(int idUsuario, DateTime fechaInicio, DateTime fechaFinal)
        {
            List<ECategoria> lista = new List<ECategoria>();
            try
            {
                DataSet ds = WS.seleccionarCategoriasActivasByIdUsuarioAndFechasForReportePE(idUsuario, fechaInicio, fechaFinal);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ECategoria eCategoria = new ECategoria();
                    eCategoria.IdCategoria = int.Parse(row["ID_CATEGORIA"].ToString());
                    eCategoria.Categoria = row["CATEGORIA"].ToString();
                    lista.Add(eCategoria);
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

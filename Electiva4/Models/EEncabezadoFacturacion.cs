using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electiva4.Models
{
    public class EEncabezadoFacturacion : EUsuario
    {
        private int idEncabezadoFacturacion;
        private int idCliente;
        private int idReserva;
        DateTime fechaFacturacion;
        double montoEncabezadoFacturacion;
        //Solo para generar factura (PDF)
        private string nombreCliente;
        private string apellidoCliente;

        public int IdEncabezadoFacturacion { get => idEncabezadoFacturacion; set => idEncabezadoFacturacion = value; }
        public int IdCliente { get => idCliente; set => idCliente = value; }
        public int IdReserva { get => idReserva; set => idReserva = value; }
        public DateTime FechaFacturacion { get => fechaFacturacion; set => fechaFacturacion = value; }
        public double MontoEncabezadoFacturacion { get => montoEncabezadoFacturacion; set => montoEncabezadoFacturacion = value; }
        public string NombreCliente { get => nombreCliente; set => nombreCliente = value; }
        public string ApellidoCliente { get => apellidoCliente; set => apellidoCliente = value; }
    }
}
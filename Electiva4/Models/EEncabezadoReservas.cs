using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electiva4.Models
{
    public class EEncabezadoReservas
    {
        private int idEncabezadoReserva;
        private int idCliente;
        private DateTime fechaReserva;
        private DateTime fechaPromesaEntrega;
        private double montoEncabezadoReserva;
        private string estadoReserva;
        private string comentarios;

        public int IdEncabezadoReserva { get => idEncabezadoReserva; set => idEncabezadoReserva = value; }
        public int IdCliente { get => idCliente; set => idCliente = value; }
        public DateTime FechaReserva { get => fechaReserva; set => fechaReserva = value; }
        public DateTime FechaPromesaEntrega { get => fechaPromesaEntrega; set => fechaPromesaEntrega = value; }
        public double MontoEncabezadoReserva { get => montoEncabezadoReserva; set => montoEncabezadoReserva = value; }
        public string EstadoReserva { get => estadoReserva; set => estadoReserva = value; }
        public string Comentarios { get => comentarios; set => comentarios = value; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electiva4.Models
{
    public class EDetalleReservas
    {
        private int idDetalleReserva;
        private int idEncabezadoReserva;
        private int idProducto;
        private int cantidad;
        private double precioProducto;
        private double monto;

        public int IdDetalleReserva { get => idDetalleReserva; set => idDetalleReserva = value; }
        public int IdEncabezadoReserva { get => idEncabezadoReserva; set => idEncabezadoReserva = value; }
        public int IdProducto { get => idProducto; set => idProducto = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public double PrecioProducto { get => precioProducto; set => precioProducto = value; }
        public double Monto { get => monto; set => monto = value; }
    }
}
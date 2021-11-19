using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electiva4.Models
{
    public class EEncabezadoCompraProductos
    {
        private int idEncCompraProductos;
        private int idProveedor;
        private DateTime fechaIngreso;
        private double monto;

        public int IdEncCompraProductos { get => idEncCompraProductos; set => idEncCompraProductos = value; }
        public int IdProveedor { get => idProveedor; set => idProveedor = value; }
        public DateTime FechaIngreso { get => fechaIngreso; set => fechaIngreso = value; }
        public double Monto { get => monto; set => monto = value; }
    }
}
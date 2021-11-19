using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electiva4.Models
{
    public class EReporteProductos
    {
        private string nombreProducto;
        private string nombreProveedor;
        private int existencia;
        private double precioUnitario;
        private double precioVenta;

        public string NombreProducto { get => nombreProducto; set => nombreProducto = value; }
        public string NombreProveedor { get => nombreProveedor; set => nombreProveedor = value; }
        public int Existencia { get => existencia; set => existencia = value; }
        public double PrecioUnitario { get => precioUnitario; set => precioUnitario = value; }
        public double PrecioVenta { get => precioVenta; set => precioVenta = value; }
    }
}
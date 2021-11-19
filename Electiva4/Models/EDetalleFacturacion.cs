using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electiva4.Models
{
    public class EDetalleFacturacion
    {
        private int idDetalleFacturacion;
        private int idEncabezadoFacturacion;
        private int idProducto;
        private int cantidad;
        private double precio;
        private double montoDetalleFacturacion;
        //Solo para generar factura (PDF)
        private string nombreProducto;

        public int IdDetalleFacturacion { get => idDetalleFacturacion; set => idDetalleFacturacion = value; }
        public int IdEncabezadoFacturacion { get => idEncabezadoFacturacion; set => idEncabezadoFacturacion = value; }
        public int IdProducto { get => idProducto; set => idProducto = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public double Precio { get => precio; set => precio = value; }
        public double MontoDetalleFacturacion { get => montoDetalleFacturacion; set => montoDetalleFacturacion = value; }
        public string NombreProducto { get => nombreProducto; set => nombreProducto = value; }
    }
}
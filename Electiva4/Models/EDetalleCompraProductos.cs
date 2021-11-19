using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electiva4.Models
{
    public class EDetalleCompraProductos
    {
        private int idDetCompraProductos;
        private int idEncCompraProductos;
        private int idProducto;
        private int cantidad;
        private double precioLote;
        private double precioUnitario;
        private double precioVenta; //precioReal
        private double porcentajeGanancia;

        public int IdDetCompraProductos { get => idDetCompraProductos; set => idDetCompraProductos = value; }
        public int IdEncCompraProductos { get => idEncCompraProductos; set => idEncCompraProductos = value; }
        public int IdProducto { get => idProducto; set => idProducto = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public double PrecioLote { get => precioLote; set => precioLote = value; }
        public double PrecioUnitario { get => precioUnitario; set => precioUnitario = value; }
        public double PrecioVenta { get => precioVenta; set => precioVenta = value; }
        public double PorcentajeGanancia { get => porcentajeGanancia; set => porcentajeGanancia = value; }
    }
}
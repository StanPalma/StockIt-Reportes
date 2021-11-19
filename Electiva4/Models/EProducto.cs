using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electiva4.Models
{
    public class EProducto
    {
        private int idProducto;
        private int idCategoria;
        private int idUsuario;
        private string nombreProducto;
        private double precio;
        private int existencia;
        private byte[] img;
        private string detalles;
        private string estadoProducto;
        public int IdProducto { get => idProducto; set => idProducto = value; }
        public int IdCategoria { get => idCategoria; set => idCategoria = value; }
        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public string NombreProducto { get => nombreProducto; set => nombreProducto = value; }
        public double Precio { get => precio; set => precio = value; }
        public int Existencia { get => existencia; set => existencia = value; }
        public byte[] Img { get => img; set => img = value; }
        public string Detalles { get => detalles; set => detalles = value; }
        public string EstadoProducto { get => estadoProducto; set => estadoProducto = value; }
    }
}
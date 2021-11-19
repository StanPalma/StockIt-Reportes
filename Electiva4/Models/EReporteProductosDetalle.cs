using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electiva4.Models
{
    public class EReporteProductosDetalle : EDetalleCompraProductos
    {
        private string nombreProducto;
        private string categoria;
        private string nombreProveedor;
        private DateTime fechaIngreso;

        public string NombreProducto { get => nombreProducto; set => nombreProducto = value; }
        public string Categoria { get => categoria; set => categoria = value; }
        public string NombreProveedor { get => nombreProveedor; set => nombreProveedor = value; }
        public DateTime FechaIngreso { get => fechaIngreso; set => fechaIngreso = value; }
    }
}
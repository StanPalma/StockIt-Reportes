using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electiva4.Models
{
    public class EReporteProductosEncabezado : EEncabezadoCompraProductos
    {
        private string nombreProveedor;

        public string NombreProveedor { get => nombreProveedor; set => nombreProveedor = value; }
    }
}
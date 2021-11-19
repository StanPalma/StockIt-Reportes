using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electiva4.Models
{
    public class EReporteReservasDetalle : EDetalleReservas
    {
        private string nombreProducto;

        public string NombreProducto { get => nombreProducto; set => nombreProducto = value; }
    }
}
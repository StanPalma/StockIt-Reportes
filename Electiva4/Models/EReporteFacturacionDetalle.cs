using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electiva4.Models
{
    public class EReporteFacturacionDetalle : EDetalleFacturacion
    {
        private string categoria;
        private string nombreProveedor;
        private DateTime fechaFacturacion;

        public string Categoria { get => categoria; set => categoria = value; }
        public string NombreProveedor { get => nombreProveedor; set => nombreProveedor = value; }
        public DateTime FechaFacturacion { get => fechaFacturacion; set => fechaFacturacion = value; }
    }
}
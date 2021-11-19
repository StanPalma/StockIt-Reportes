using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electiva4.Models
{
    public class EReporteFacturacionEncabezado : EEncabezadoFacturacion
    {
        private string telefonoCliente;

        public string TelefonoCliente { get => telefonoCliente; set => telefonoCliente = value; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electiva4.Models
{
    public class EEstadoReserva
    {
        string estadoReserva;
        string nombreEstadoReserva;

        public string EstadoReserva { get => estadoReserva; set => estadoReserva = value; }
        public string NombreEstadoReserva { get => nombreEstadoReserva; set => nombreEstadoReserva = value; }
    }
}
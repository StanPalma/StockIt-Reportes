using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electiva4.Models
{
    public class ECliente
    {
        private int idCliente;
        private int idUsuario;//Usuario que registró el cliente
        private string nombreCliente;
        private string apellidoCliente;
        private string sexoCliente;
        private string telefonoCliente;
        private string correoCliente;
        private string estadoCliente;

        public int IdCliente { get => idCliente; set => idCliente = value; }
        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public string NombreCliente { get => nombreCliente; set => nombreCliente = value; }
        public string ApellidoCliente { get => apellidoCliente; set => apellidoCliente = value; }
        public string SexoCliente { get => sexoCliente; set => sexoCliente = value; }
        public string TelefonoCliente { get => telefonoCliente; set => telefonoCliente = value; }
        public string CorreoCliente { get => correoCliente; set => correoCliente = value; }
        public string EstadoCliente { get => estadoCliente; set => estadoCliente = value; }
    }
}
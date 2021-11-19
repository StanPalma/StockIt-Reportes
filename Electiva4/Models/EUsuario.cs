using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electiva4.Models
{
    public class EUsuario
    {
        private int idUsuario;
        private string usuario;
        private string nombres;
        private string apellidos;
        private string nombreEmpresa;
        private string correo;
        private string password;
        private string estadoUsuario;
        private int passwordTemporal;

        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public string Nombres { get => nombres; set => nombres = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public string NombreEmpresa { get => nombreEmpresa; set => nombreEmpresa = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Password { get => password; set => password = value; }
        public string EstadoUsuario { get => estadoUsuario; set => estadoUsuario = value; }
        public int PasswordTemporal { get => passwordTemporal; set => passwordTemporal = value; }
    }
}
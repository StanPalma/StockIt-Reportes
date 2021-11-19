using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electiva4.Models
{
    public class ECategoria
    {
        private int idCategoria;
        private int idUsuario;
        private string categoria;
        private string estadoCategoria;

        public int IdCategoria { get => idCategoria; set => idCategoria = value; }
        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public string Categoria { get => categoria; set => categoria = value; }
        public string EstadoCategoria { get => estadoCategoria; set => estadoCategoria = value; }
    }
}
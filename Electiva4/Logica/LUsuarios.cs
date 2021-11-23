using Electiva4.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Electiva4.Logica
{
    public class LUsuarios
    {
        WSStockIt.WebServiceSI WS = new WSStockIt.WebServiceSI();

        public EUsuario seleccionarUsuarioByCorreo(string correo)
        {
            EUsuario eUsuario = new EUsuario();

            try
            {
                DataSet ds = WS.seleccionarUsuarioByCorreo(correo);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    eUsuario.IdUsuario = int.Parse(row["ID_USUARIO"].ToString());
                    eUsuario.Usuario = row["USUARIO"].ToString();
                    eUsuario.Nombres = row["NOMBRE_USUARIO"].ToString();
                    eUsuario.Apellidos = row["APELLIDO_USUARIO"].ToString();
                    eUsuario.NombreEmpresa = row["NOMBRE_EMPRESA"].ToString();
                    eUsuario.Correo = row["CORREO_USUARIO"].ToString();
                    eUsuario.EstadoUsuario = row["ESTADO_USUARIO"].ToString();
                }

                return eUsuario;
            }
            catch (Exception)
            {
                return eUsuario;
            }
        }
    }
}
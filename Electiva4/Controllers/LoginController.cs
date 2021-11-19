using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Electiva4.Models;
using System.Web.Mvc;
namespace Electiva4.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            if (Session["UserCorreo"] != null)
            {
                return Redirect("~/Menu");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult Verify(string email, string password)
        {
            EUsuario eUsuario = new EUsuario();
            eUsuario.Correo = email;
            eUsuario.Password = password;

            WSStockIt.WebServiceSI WS = new WSStockIt.WebServiceSI();

            int r = WS.login(eUsuario.Correo, eUsuario.Password);

            if (r > 0)
            {
                //Creamos las sesiones si el usuario ingresó los datos correctamente
                Session["UserCorreo"] = eUsuario.Correo;
                Session["UserNombre"] = eUsuario.Correo;
            }

            return Json(r.ToString());
        }

        public RedirectResult Logout()
        {
            if (Session["UserCorreo"] != null)
            {
                //Limpiamos las sesiones
                Session["UserCorreo"] = null;
                Session["UserNombre"] = null;
                return Redirect("~/Login/Login");
            }
            else
            {
                return Redirect("~/Login/Login");
            }
        }
    }
}
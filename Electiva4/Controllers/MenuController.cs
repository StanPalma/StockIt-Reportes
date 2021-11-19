using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Electiva4.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            if (Session["UserCorreo"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("~/Login/Login");
            }
            
        }

        public ActionResult Productos()
        {
            List<SelectListItem> categorias = new List<SelectListItem>();
            categorias.Add(new SelectListItem() { Text = "categorias 1", Value = "1" });
            categorias.Add(new SelectListItem() { Text = "categorias 2", Value = "2" });
            categorias.Add(new SelectListItem() { Text = "categorias 3", Value = "3" });

            List<SelectListItem> estado = new List<SelectListItem>();
            estado.Add(new SelectListItem() { Text = "estado 1", Value = "1" });
            estado.Add(new SelectListItem() { Text = "estado 2", Value = "2" });
            estado.Add(new SelectListItem() { Text = "estado 3", Value = "3" });

            // Enviar lista a ViewBag para recibir en la Vista
            ViewBag.categorias = categorias;
            ViewBag.estado = estado;
            return View();
        }

        public ActionResult Clientes()
        {
            return View();
        }

        public ActionResult Reservas()
        {
            return View();
        }

        public ActionResult Ventas()
        {
            List<SelectListItem> clientes = new List<SelectListItem>();
            clientes.Add(new SelectListItem() { Text = "Cliente 1", Value = "1"});
            clientes.Add(new SelectListItem() { Text = "Cliente 2", Value = "2" });
            clientes.Add(new SelectListItem() { Text = "Cliente 3", Value = "3" });

            // Enviar lista a ViewBag para recibir en la Vista
            ViewBag.clientes = clientes;

            return View();
        }
    }
}
using Electiva4.Logica;
using Electiva4.Models;
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
        WSStockIt.WebServiceSI WS = new WSStockIt.WebServiceSI();
        int idUsuario = 0;

        public ActionResult Index()
        {
            if (Session["UserCorreo"] != null)
            {
                idUsuario = int.Parse(Session["UserId"].ToString());
                return View();
            }
            else
            {
                idUsuario = 0;
                return Redirect("~/Login/Login");
            }
            
        }

        #region Métodos para CompraProductos
        public ActionResult CompraProductos()
        {
            if (Session["UserCorreo"] != null)
            {
                return View();
            }
            else
            {
                idUsuario = 0;
                return Redirect("~/Login/Login");
            }
        }

        public JsonResult LlenarTableCP(string fechaInicioP, string fechaFinalP)
        {
            List<EReporteProductosEncabezado> eReporteProductosEncabezadoList = new List<EReporteProductosEncabezado>();

            if (Session["UserId"] != null)
            {
                idUsuario = int.Parse(Session["UserId"].ToString());
            }
            else
            {
                idUsuario = 0;
            }
            
            if (idUsuario > 0)
            {
                DateTime fechaInicio = DateTime.Parse(new LUtils().fechaHoraActual()).Date;
                DateTime fechaFinal = DateTime.Parse(new LUtils().fechaHoraActual()).Date;

                if ((fechaInicioP != "" && fechaFinalP != ""))
                {
                    fechaInicio = DateTime.Parse(fechaInicioP).Date;
                    fechaFinal = DateTime.Parse(fechaFinalP).Date;
                }

                eReporteProductosEncabezadoList = new LProductos().EncabezadosReporteCompraProductos(fechaInicio, fechaFinal, idUsuario);
            }
            else
            {
                //Agregar un objeto
                Redirect("~/Login/Login");
            }

            return Json(eReporteProductosEncabezadoList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult CompraProductosEsp()
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
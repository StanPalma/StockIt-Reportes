using Electiva4.Logica;
using Electiva4.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
        string fechaInicioCadena = "";
        string fechaFinalCadena = "";

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

                if (idUsuario > 0)
                {
                    DateTime fechaInicio = DateTime.Parse(new LUtils().fechaHoraActual()).Date;
                    DateTime fechaFinal = DateTime.Parse(new LUtils().fechaHoraActual()).Date;

                    if ((fechaInicioP != "" && fechaFinalP != ""))
                    {
                        fechaInicio = DateTime.Parse(fechaInicioP).Date;
                        fechaFinal = DateTime.Parse(fechaFinalP).Date;
                    }

                    fechaInicioCadena = fechaInicio.ToString("dd-MM-yyyy");
                    fechaFinalCadena = fechaFinal.ToString("dd-MM-yyyy");

                    eReporteProductosEncabezadoList = new LProductos().EncabezadosReporteCompraProductos(fechaInicio, fechaFinal, idUsuario);

                    //Guardamos los datos en las variables de sesión que serán utilizadas para imprimir el reporte
                    Session["FechaInicio"] = fechaInicioCadena;
                    Session["FechaFinal"] = fechaFinalCadena;
                    Session["DatosEncabezadoList"] = eReporteProductosEncabezadoList;
                }
                else
                {
                    //Agregar un objeto
                    Redirect("~/Login/Login");
                }
            }
            else
            {
                fechaInicioCadena = "";
                fechaFinalCadena = "";
                idUsuario = 0;
                //Agregar un objeto
                Redirect("~/Login/Login");
            }

            return Json(eReporteProductosEncabezadoList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LlenarTableCPD(string idEncabezado, string nomProveedor, string fechaCompra, string monto)
        {
            List<EReporteProductosDetalle> eReporteProductosDetalleList = new List<EReporteProductosDetalle>();
            if (Session["UserId"] != null)
            {
                idUsuario = int.Parse(Session["UserId"].ToString());

                if (idUsuario > 0)
                {
                    int idEncabezadoCompraProductos = int.Parse(idEncabezado);

                    //Obtenemos los datos para el encabezado de la compra
                    EReporteProductosEncabezado eReporteProductosEncabezado = new EReporteProductosEncabezado();
                    eReporteProductosEncabezado.NombreProveedor = nomProveedor;
                    eReporteProductosEncabezado.FechaIngreso = DateTime.Parse(fechaCompra);
                    eReporteProductosEncabezado.Monto = Double.Parse(monto.Replace("$", ""));

                    eReporteProductosDetalleList = new LProductos().DetalleReporteCompraProductos(idEncabezadoCompraProductos);

                    Session["IdEncabezado"] = idEncabezadoCompraProductos;
                    Session["DatosEncabezado"] = eReporteProductosEncabezado;
                    Session["DatosDetalleList"] = eReporteProductosDetalleList;
                }
                else
                {
                    //Agregar un objeto
                    Redirect("~/Login/Login");
                }
            }
            else
            {
                idUsuario = 0;
                //Agregar un objeto
                Redirect("~/Login/Login");
            }

            return Json(eReporteProductosDetalleList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImprimirRCP()
        {
            try
            {
                ReporteCompraProductosController reporteCompraProductosController = new ReporteCompraProductosController();

                int idEncabezadoCompraProductos = Session["IdEncabezado"] != null && Session["IdEncabezado"].ToString() != ""
                    ? int.Parse(Session["IdEncabezado"].ToString())
                    : 0;

                List<EReporteProductosEncabezado> eReporteProductosEncabezadoList = Session["DatosEncabezadoList"] != null 
                    ? Session["DatosEncabezadoList"] as List<EReporteProductosEncabezado>
                    : new List<EReporteProductosEncabezado>();

                List<EReporteProductosDetalle> eReporteProductosDetalleList = Session["DatosDetalleList"] != null 
                    ? Session["DatosDetalleList"] as List<EReporteProductosDetalle>
                    : new List<EReporteProductosDetalle>();

                EReporteProductosEncabezado eReporteProductosEncabezado = Session["DatosEncabezado"] != null 
                    ? Session["DatosEncabezado"] as EReporteProductosEncabezado
                    : new EReporteProductosEncabezado();
                string fechaInicio = Session["FechaInicio"] != null ? Session["FechaInicio"].ToString() : "00-00-0000";
                string fechaFinal = Session["FechaFinal"] != null ? Session["FechaFinal"].ToString() : "00-00-0000";


                return reporteCompraProductosController.generarReporte(idEncabezadoCompraProductos, eReporteProductosEncabezadoList, eReporteProductosDetalleList, eReporteProductosEncabezado, 
                    fechaInicio, fechaFinal, Session["UserCorreo"].ToString(), this.Response, this.Server);
            }
            catch (Exception)
            {

                return null;
            }
        }

        #endregion

        #region Métodos para CompraProductosEsp
        public ActionResult CompraProductosEsp()
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

        public JsonResult LlenarDDLCategorias(string fechaInicioP, string fechaFinalP)
        {
            List<ECategoria> categoriasList = new List<ECategoria>();
            if (Session["UserId"] != null)
            {
                idUsuario = int.Parse(Session["UserId"].ToString());

                if ((fechaInicioP != "" && fechaFinalP != ""))
                {
                    DateTime fechaInicio = DateTime.Parse(fechaInicioP).Date;
                    DateTime fechaFinal = DateTime.Parse(fechaFinalP).Date;

                    categoriasList = new LCategorias().ListSeleccionarCategoriasActivasByIdUsuarioAndFechasForReportePE(idUsuario, fechaInicio, fechaFinal);
                }
            }

            return Json(categoriasList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LlenarDDLProductos(string fechaInicioP, string fechaFinalP, string idCategoriaP)
        {
            List<EProducto> productosList = new List<EProducto>();
            if (Session["UserId"] != null)
            {
                idUsuario = int.Parse(Session["UserId"].ToString());
                int idCategoria = int.Parse(idCategoriaP);

                if ((fechaInicioP != "" && fechaFinalP != ""))
                {
                    DateTime fechaInicio = DateTime.Parse(fechaInicioP).Date;
                    DateTime fechaFinal = DateTime.Parse(fechaFinalP).Date;

                    productosList = new LProductos().ListSeleccionarProductosByIdUsuarioFechasAndIdCategoriaForReportePE(idUsuario, fechaInicio, fechaFinal, idCategoria);
                }
            }

            return Json(productosList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LlenarTableCPE(string idProductoP, string fechaInicioP, string fechaFinalP, string idCategoriaP)
        {
            List<EReporteProductosDetalle> eReporteProductosDetalleList = new List<EReporteProductosDetalle>();
            if (Session["UserId"] != null)
            {
                idUsuario = int.Parse(Session["UserId"].ToString());

                if (idUsuario > 0)
                {
                    DateTime fechaInicio = DateTime.Parse(new LUtils().fechaHoraActual()).Date;
                    DateTime fechaFinal = DateTime.Parse(new LUtils().fechaHoraActual()).Date;

                    if ((fechaInicioP != "" && fechaFinalP != ""))
                    {
                        fechaInicio = DateTime.Parse(fechaInicioP).Date;
                        fechaFinal = DateTime.Parse(fechaFinalP).Date;
                    }

                    int idProducto = idProductoP != null && idProductoP != "" ? int.Parse(idProductoP) : 0;
                    fechaInicioCadena = fechaInicio.ToString("dd-MM-yyyy");
                    fechaFinalCadena = fechaFinal.ToString("dd-MM-yyyy");
                    int idCategoria = idCategoriaP != null && idCategoriaP != "" ? int.Parse(idCategoriaP) : 0;

                    eReporteProductosDetalleList = new LProductos().DetalleReporteCompraProductosFiltros(idProducto, fechaInicio, fechaFinal, idCategoria, idUsuario);

                    //Guardamos los datos en las variables de sesión que serán utilizadas para imprimir el reporte
                    Session["IdProducto"] = idProductoP;
                    Session["FechaInicio"] = fechaInicioCadena;
                    Session["FechaFinal"] = fechaFinalCadena;
                    Session["IdProducto"] = idCategoriaP;
                    Session["DatosDetalleList"] = eReporteProductosDetalleList;
                }
                else
                {
                    //Agregar un objeto
                    Redirect("~/Login/Login");
                }
            }
            else
            {
                fechaInicioCadena = "";
                fechaFinalCadena = "";
                idUsuario = 0;
                //Agregar un objeto
                Redirect("~/Login/Login");
            }

            return Json(eReporteProductosDetalleList, JsonRequestBehavior.AllowGet);
        }

        #endregion

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
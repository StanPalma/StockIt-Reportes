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

        public JsonResult LlenarTableCPE(string idProductoP, string fechaInicioP, string fechaFinalP, string idCategoriaP, string nomProductoP, string nomCategoriaP)
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
                    Session["NomProducto"] = nomProductoP;
                    Session["FechaInicio"] = fechaInicioCadena;
                    Session["FechaFinal"] = fechaFinalCadena;
                    Session["IdCategoria"] = idCategoriaP;
                    Session["NomCategoria"] = nomCategoriaP;
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

        public ActionResult ImprimirRCPEsp()
        {
            try
            {
                ReporteCompraProductosEspController reporteCompraProductosEspController = new ReporteCompraProductosEspController();

                int idCategoria = Session["IdCategoria"] != null && Session["IdCategoria"].ToString() != ""
                    ? int.Parse(Session["IdCategoria"].ToString())
                    : 0;

                string nombreCategoria = Session["NomCategoria"] != null && Session["NomCategoria"].ToString() != ""
                    ? Session["NomCategoria"].ToString()
                    : "TODAS";

                int idProducto = Session["IdProducto"] != null && Session["IdProducto"].ToString() != ""
                    ? int.Parse(Session["IdProducto"].ToString())
                    : 0;

                string nombreProducto = Session["NomProducto"] != null && Session["NomProducto"].ToString() != ""
                    ? Session["NomProducto"].ToString()
                    : "TODOS";

                List<EReporteProductosDetalle> eReporteProductosDetalleList = Session["DatosDetalleList"] != null
                    ? Session["DatosDetalleList"] as List<EReporteProductosDetalle>
                    : new List<EReporteProductosDetalle>();

                string fechaInicio = Session["FechaInicio"] != null ? Session["FechaInicio"].ToString() : "00-00-0000";
                string fechaFinal = Session["FechaFinal"] != null ? Session["FechaFinal"].ToString() : "00-00-0000";


                return reporteCompraProductosEspController.generarReporte(idCategoria, idProducto, fechaInicio, fechaFinal, eReporteProductosDetalleList, nombreCategoria, 
                    nombreProducto, Session["UserCorreo"].ToString(), this.Response, this.Server);
            }
            catch (Exception)
            {

                return null;
            }
        }

        #endregion

        #region Métodos para Reservas
        public ActionResult Reservas()
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

        public JsonResult LlenarDDLEstadosR(string fechaInicioP, string fechaFinalP)
        {
            List<EEstadoReserva> estadosList = new List<EEstadoReserva>();
            if (Session["UserId"] != null)
            {
                idUsuario = int.Parse(Session["UserId"].ToString());

                if ((fechaInicioP != "" && fechaFinalP != ""))
                {
                    DateTime fechaInicio = DateTime.Parse(fechaInicioP).Date;
                    DateTime fechaFinal = DateTime.Parse(fechaFinalP).Date;

                    estadosList = new LDetalleReservas().ListSeleccionarEstadosReservaByIdUsuarioAndFechasForReporte(idUsuario, fechaInicio, fechaFinal);
                }
            }

            return Json(estadosList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LlenarDDLClientesR(string fechaInicioP, string fechaFinalP, string estadoP)
        {
            List<ECliente> clientesList = new List<ECliente>();
            if (Session["UserId"] != null)
            {
                idUsuario = int.Parse(Session["UserId"].ToString());

                if ((fechaInicioP != "" && fechaFinalP != ""))
                {
                    DateTime fechaInicio = DateTime.Parse(fechaInicioP).Date;
                    DateTime fechaFinal = DateTime.Parse(fechaFinalP).Date;

                    clientesList = new LClientes().SeleccionarClientesByIdUsuarioFechasAndEstadoReservaForReporte(idUsuario, fechaInicio, fechaFinal, estadoP);
                }
            }

            return Json(clientesList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LlenarTableRE(string fechaInicioP, string fechaFinalP, string estadoP, string idClienteP)
        {
            List<EReporteReservasEncabezado> eReporteReservasEncabezadoList = new List<EReporteReservasEncabezado>();
            if (Session["UserId"] != null)
            {
                idUsuario = int.Parse(Session["UserId"].ToString());

                if (idUsuario > 0)
                {
                    DateTime fechaInicio = DateTime.Parse(new LUtils().fechaHoraActual()).Date;
                    DateTime fechaFinal = DateTime.Parse(new LUtils().fechaHoraActual()).Date;

                    int idCliente = idClienteP != null && idClienteP != "" ? int.Parse(idClienteP) : 0;

                    if ((fechaInicioP != "" && fechaFinalP != ""))
                    {
                        fechaInicio = DateTime.Parse(fechaInicioP).Date;
                        fechaFinal = DateTime.Parse(fechaFinalP).Date;
                    }

                    fechaInicioCadena = fechaInicio.ToString("dd-MM-yyyy");
                    fechaFinalCadena = fechaFinal.ToString("dd-MM-yyyy");

                    eReporteReservasEncabezadoList = new LEncabezadoReservas().EncabezadosReporteReservas(fechaInicio, fechaFinal, idUsuario, estadoP, idCliente);

                    //Guardamos los datos en las variables de sesión que serán utilizadas para imprimir el reporte
                    Session["FechaInicio"] = fechaInicioCadena;
                    Session["FechaFinal"] = fechaFinalCadena;
                    Session["EstadoReserva"] = estadoP;
                    Session["IdCliente"] = idClienteP;
                    Session["DatosEncabezadoList"] = eReporteReservasEncabezadoList;
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

            return Json(eReporteReservasEncabezadoList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LlenarTableRD(string idEncabezado, string nomProveedor, string fechaCompra, string monto)
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
        #endregion

        #region Métodos para Ventas
        public ActionResult Ventas()
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

        public JsonResult LlenarDDLClientes(string fechaInicioP, string fechaFinalP)
        {
            List<ECliente> clientesList = new List<ECliente>();
            if (Session["UserId"] != null)
            {
                idUsuario = int.Parse(Session["UserId"].ToString());

                if ((fechaInicioP != "" && fechaFinalP != ""))
                {
                    DateTime fechaInicio = DateTime.Parse(fechaInicioP).Date;
                    DateTime fechaFinal = DateTime.Parse(fechaFinalP).Date;

                    clientesList = new LClientes().SeleccionarClientesActivosByIdUsuarioForReporte(idUsuario, fechaInicio, fechaFinal);
                }
            }

            return Json(clientesList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LlenarTableV(string fechaInicioP, string fechaFinalP, string idClienteP, string nombreClienteP)
        {
            List<EReporteFacturacionEncabezado> eReporteFacturacionEncabezadoList = new List<EReporteFacturacionEncabezado>();
            if (Session["UserId"] != null)
            {
                idUsuario = int.Parse(Session["UserId"].ToString());

                if (idUsuario > 0)
                {
                    int idCliente = idClienteP != null && idClienteP != "" ? int.Parse(idClienteP) : 0;
                    DateTime fechaInicio = DateTime.Parse(new LUtils().fechaHoraActual()).Date;
                    DateTime fechaFinal = DateTime.Parse(new LUtils().fechaHoraActual()).Date;

                    if ((fechaInicioP != "" && fechaFinalP != ""))
                    {
                        fechaInicio = DateTime.Parse(fechaInicioP).Date;
                        fechaFinal = DateTime.Parse(fechaFinalP).Date;
                    }

                    fechaInicioCadena = fechaInicio.ToString("dd-MM-yyyy");
                    fechaFinalCadena = fechaFinal.ToString("dd-MM-yyyy");

                    eReporteFacturacionEncabezadoList = new LEncabezadoFacturacion().EncabezadosReporteFacturacion(fechaInicio, fechaFinal, idUsuario, idCliente);

                    //Guardamos los datos en las variables de sesión que serán utilizadas para imprimir el reporte
                    Session["FechaInicio"] = fechaInicioCadena;
                    Session["FechaFinal"] = fechaFinalCadena;
                    Session["IdCliente"] = idClienteP;
                    Session["NomCliente"] = nombreClienteP;
                    Session["DatosEncabezadoList"] = eReporteFacturacionEncabezadoList;
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

            return Json(eReporteFacturacionEncabezadoList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LlenarTableVD(string idEncabezadoP, string nombreClienteP, string telefonoClienteP, string fechaFacturacionP, string montoEncabezadoFacturacionP)
        {
            List<EDetalleFacturacion> eDetalleFacturacionList = new List<EDetalleFacturacion>();
            if (Session["UserId"] != null)
            {
                idUsuario = int.Parse(Session["UserId"].ToString());

                if (idUsuario > 0)
                {
                    int idEncabezado = int.Parse(idEncabezadoP);

                    //Obtenemos los datos para el encabezado de la compra
                    EReporteFacturacionEncabezado eReporteFacturacionEncabezado = new EReporteFacturacionEncabezado();
                    eReporteFacturacionEncabezado.NombreCliente = nombreClienteP;
                    eReporteFacturacionEncabezado.TelefonoCliente = telefonoClienteP;
                    eReporteFacturacionEncabezado.FechaFacturacion = DateTime.Parse(fechaFacturacionP);
                    eReporteFacturacionEncabezado.MontoEncabezadoFacturacion = Double.Parse(montoEncabezadoFacturacionP.Replace("$", ""));

                    eDetalleFacturacionList = new LDetalleFacturacion().SeleccionarDetalleFacturacionByIdEncabezadoFacturacion(idEncabezado);

                    Session["IdEncabezado"] = idEncabezado;
                    Session["DatosEncabezado"] = eReporteFacturacionEncabezado;
                    Session["DatosDetalleList"] = eDetalleFacturacionList;
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

            return Json(eDetalleFacturacionList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImprimirRV()
        {
            try
            {
                ReporteVentasController reporteVentasController = new ReporteVentasController();

                int idEncabezado = Session["IdEncabezado"] != null && Session["IdEncabezado"].ToString() != ""
                    ? int.Parse(Session["IdEncabezado"].ToString())
                    : 0;

                List<EReporteFacturacionEncabezado> eReporteFacturacionEncabezadoList = Session["DatosEncabezadoList"] != null
                    ? Session["DatosEncabezadoList"] as List<EReporteFacturacionEncabezado>
                    : new List<EReporteFacturacionEncabezado>();

                List<EDetalleFacturacion> eDetalleFacturacionList = Session["DatosDetalleList"] != null
                    ? Session["DatosDetalleList"] as List<EDetalleFacturacion>
                    : new List<EDetalleFacturacion>();

                EReporteFacturacionEncabezado eReporteFacturacionEncabezado = Session["DatosEncabezado"] != null
                    ? Session["DatosEncabezado"] as EReporteFacturacionEncabezado
                    : new EReporteFacturacionEncabezado();
                string fechaInicio = Session["FechaInicio"] != null ? Session["FechaInicio"].ToString() : "00-00-0000";
                string fechaFinal = Session["FechaFinal"] != null ? Session["FechaFinal"].ToString() : "00-00-0000";
                string nombreCliente = Session["NomCliente"] != null ? Session["NomCliente"].ToString() : "TODOS";


                return reporteVentasController.generarReporte(idEncabezado, eReporteFacturacionEncabezadoList, eDetalleFacturacionList, eReporteFacturacionEncabezado,
                    fechaInicio, fechaFinal, nombreCliente, Session["UserCorreo"].ToString(), this.Response, this.Server);
            }
            catch (Exception)
            {

                return null;
            }
        }
        #endregion

        #region Métodos para VentasEsp
        public ActionResult VentasEsp()
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

        public JsonResult LlenarDDLCategoriasV(string fechaInicioP, string fechaFinalP)
        {
            List<ECategoria> categoriasList = new List<ECategoria>();
            if (Session["UserId"] != null)
            {
                idUsuario = int.Parse(Session["UserId"].ToString());

                if ((fechaInicioP != "" && fechaFinalP != ""))
                {
                    DateTime fechaInicio = DateTime.Parse(fechaInicioP).Date;
                    DateTime fechaFinal = DateTime.Parse(fechaFinalP).Date;

                    categoriasList = new LCategorias().ListSeleccionarCategoriasActivasByIdUsuarioAndFechasForReporteVE(idUsuario, fechaInicio, fechaFinal);
                }
            }

            return Json(categoriasList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LlenarDDLProductosV(string fechaInicioP, string fechaFinalP, string idCategoriaP)
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

                    productosList = new LProductos().ListSeleccionarProductosByIdUsuarioFechasAndIdCategoriaForReporte(idUsuario, fechaInicio, fechaFinal, idCategoria);
                }
            }

            return Json(productosList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LlenarTableVE(string idProductoP, string fechaInicioP, string fechaFinalP, string idCategoriaP, string nomProductoP, string nomCategoriaP)
        {
            List<EReporteFacturacionDetalle> eReporteFacturacionDetalleList = new List<EReporteFacturacionDetalle>();
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

                    eReporteFacturacionDetalleList = new LDetalleFacturacion().DetalleReporteVentaProductosFiltros(idProducto, fechaInicio, fechaFinal, idCategoria, idUsuario);

                    //Guardamos los datos en las variables de sesión que serán utilizadas para imprimir el reporte
                    Session["IdProducto"] = idProductoP;
                    Session["NomProducto"] = nomProductoP;
                    Session["FechaInicio"] = fechaInicioCadena;
                    Session["FechaFinal"] = fechaFinalCadena;
                    Session["IdCategoria"] = idCategoriaP;
                    Session["NomCategoria"] = nomCategoriaP;
                    Session["DatosDetalleList"] = eReporteFacturacionDetalleList;
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

            return Json(eReporteFacturacionDetalleList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImprimirRVEsp()
        {
            try
            {
                ReporteVentasEspController reporteVentasEspController = new ReporteVentasEspController();

                int idCategoria = Session["IdCategoria"] != null && Session["IdCategoria"].ToString() != ""
                    ? int.Parse(Session["IdCategoria"].ToString())
                    : 0;

                string nombreCategoria = Session["NomCategoria"] != null && Session["NomCategoria"].ToString() != ""
                    ? Session["NomCategoria"].ToString()
                    : "TODAS";

                int idProducto = Session["IdProducto"] != null && Session["IdProducto"].ToString() != ""
                    ? int.Parse(Session["IdProducto"].ToString())
                    : 0;

                string nombreProducto = Session["NomProducto"] != null && Session["NomProducto"].ToString() != ""
                    ? Session["NomProducto"].ToString()
                    : "TODOS";

                List<EReporteFacturacionDetalle> eReporteFacturacionDetalleList = Session["DatosDetalleList"] != null
                    ? Session["DatosDetalleList"] as List<EReporteFacturacionDetalle>
                    : new List<EReporteFacturacionDetalle>();

                string fechaInicio = Session["FechaInicio"] != null ? Session["FechaInicio"].ToString() : "00-00-0000";
                string fechaFinal = Session["FechaFinal"] != null ? Session["FechaFinal"].ToString() : "00-00-0000";


                return reporteVentasEspController.generarReporte(idCategoria, idProducto, fechaInicio, fechaFinal, eReporteFacturacionDetalleList, nombreCategoria,
                    nombreProducto, Session["UserCorreo"].ToString(), this.Response, this.Server);
            }
            catch (Exception)
            {

                return null;
            }
        }
        #endregion
    }
}
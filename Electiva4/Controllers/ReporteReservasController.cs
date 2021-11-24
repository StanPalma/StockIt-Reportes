using Electiva4.Logica;
using Electiva4.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Electiva4.Controllers
{
    public class ReporteReservasController : Controller
    {
        // GET: ReporteReservas
        public ActionResult Index()
        {
            return View();
        }

        private List<EReporteReservasEncabezado> eReporteReservasEncabezadoList;
        private List<EReporteReservasDetalle> eDetalleReservasList;
        private EReporteReservasEncabezado eReporteReservasEncabezado;
        private string fechaInicio, fechaFinal;
        private string nombreCliente;
        private string estadoReserva;
        public ActionResult generarReporte(int idEncabezadoFacturacion, List<EReporteReservasEncabezado> eReporteReservasEncabezadoList,
            List<EReporteReservasDetalle> eDetalleReservasList, EReporteReservasEncabezado eReporteReservasEncabezado,
            string fechaInicio, string fechaFinal, string estadoReserva, string nombreCliente, string correo, HttpResponseBase response, HttpServerUtilityBase server)
        {
            this.eReporteReservasEncabezadoList = eReporteReservasEncabezadoList;
            this.eDetalleReservasList = eDetalleReservasList;
            this.eReporteReservasEncabezado = eReporteReservasEncabezado;
            this.fechaInicio = fechaInicio;
            this.fechaFinal = fechaFinal;
            this.nombreCliente = nombreCliente;
            this.estadoReserva = estadoReserva;

            if (idEncabezadoFacturacion == 0)
            {
                return generarReporteReservaGeneral(correo, response, server);
            }
            else
            {
                return generarReporteReservaDetalle(correo, response, server);
            }
        }

        public ActionResult generarReporteReservaGeneral(string correo, HttpResponseBase response, HttpServerUtilityBase server)
        {
            try
            {
                Document document = new Document(PageSize.LETTER, 30f, 30f, 80f, 40f);

                PdfWriter writer = PdfWriter.GetInstance(document, response.OutputStream);
                writer.PageEvent = new PageEventHelperRU();//Con esto agregamos los números de página

                document.Open();

                //Tipo de fuente
                Font fuenteEmision = FontFactory.GetFont("Arial", 10, BaseColor.BLACK);
                Font fuente = FontFactory.GetFont("Arial", 10, BaseColor.BLACK);
                Font negrita = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK);
                Font title = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK);


                /* Variables para encabezado */
                string TITULO = "REPORTE GENERAL\n DE RESERVAS";//Título a mostrar en el Encabezado
                string fechaEmision = new LUtils().fechaHoraActual();//Fecha de creacion para poner en el PDF
                string concatNombre = DateTime.Parse(new LUtils().fechaHoraActual()).ToString("ddMMyyyyHHmmss");//Fecha de creacion para poner en el PDF

                //Obtenemos los datos del Encabezado
                EUsuario eUsuario = new LUsuarios().seleccionarUsuarioByCorreo(correo);

                string nombreEmisor = String.Concat(eUsuario.Nombres, " ", eUsuario.Apellidos);//Nombre del Usuario
                string nombreEmpresa = eUsuario.NombreEmpresa;//Nombre de la Empresa
                string filtroFecha = String.Concat(this.fechaInicio, " a ", this.fechaFinal);//Nombre de la Empresa
                string filtroEstadoReservas = this.estadoReserva != "" ? this.estadoReserva : "TODOS";
                string filtroCliente = this.nombreCliente != "" ? this.nombreCliente : "TODOS";
                double montoReserva = 0.0;//El monto total de la reserva

                document.AddCreationDate();

                #region Asignacion del Logo de StockIt
                string nombreImagen = "logoStockIt.jpg";//Cambiar nombre por el de la nueva imagen

                string PathImage = server.MapPath("/images/" + nombreImagen);

                //Begin image
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(PathImage);
                logo.SetAbsolutePosition(400f, 700f);
                logo.ScaleAbsolute(110f, 80f);
                float percentage = 0.0f;
                percentage = 200 / logo.Width;
                logo.ScalePercent(percentage * 100);
                document.Add(logo);
                //End image;
                #endregion

                #region Header de la Reserva
                PdfPTable tbHeader = new PdfPTable(3);

                tbHeader.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tbHeader.DefaultCell.Border = 0;
                tbHeader.AddCell(new Paragraph());

                PdfPCell _cell = new PdfPCell(new Paragraph(TITULO, title));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                _cell.Border = 0;
                tbHeader.AddCell(_cell);

                tbHeader.AddCell(new Paragraph());
                tbHeader.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetTop(document.TopMargin) + 2, writer.DirectContent);

                var fechaPhrase = new Phrase();
                fechaPhrase.Add(new Chunk("FECHA Y HORA DE EMISIÓN: ", negrita));
                fechaPhrase.Add(new Chunk(fechaEmision, fuenteEmision));

                var emisorPhrase = new Phrase();
                emisorPhrase.Add(new Chunk("NOMBRE DEL EMISOR: ", negrita));
                emisorPhrase.Add(new Chunk(nombreEmisor, fuenteEmision));

                var empresaPhrase = new Phrase();
                empresaPhrase.Add(new Chunk("EMPRESA: ", negrita));
                empresaPhrase.Add(new Chunk(nombreEmpresa, fuenteEmision));

                var filtroFechaPhrase = new Phrase();
                filtroFechaPhrase.Add(new Chunk("FILTRO DE FECHAS: ", negrita));
                filtroFechaPhrase.Add(new Chunk(filtroFecha, fuenteEmision));

                var estadoReservasPhrase = new Phrase();
                estadoReservasPhrase.Add(new Chunk("ESTADO RESERVAS: ", negrita));
                estadoReservasPhrase.Add(new Chunk(filtroEstadoReservas, fuenteEmision));

                var clientePhrase = new Phrase();
                clientePhrase.Add(new Chunk("CLIENTE: ", negrita));
                clientePhrase.Add(new Chunk(filtroCliente, fuenteEmision));

                Chunk chunk = new Chunk();
                document.Add(new Paragraph(chunk));
                document.Add(new Paragraph("                       "));
                document.Add(new Paragraph("------------------------------------------------------------------------------------------------------------------------------------------"));
                document.Add(new Paragraph(fechaPhrase));
                document.Add(new Paragraph(emisorPhrase));
                document.Add(new Paragraph(empresaPhrase));
                document.Add(new Paragraph(filtroFechaPhrase));
                document.Add(new Paragraph(estadoReservasPhrase));
                document.Add(new Paragraph(clientePhrase));
                document.Add(new Paragraph("------------------------------------------------------------------------------------------------------------------------------------------"));
                document.Add(new Paragraph("                       "));
                #endregion

                #region Listado General de las Reservas

                //Establecemos 6 celdas si se va a mostrar el EstadoReserva, y 5 si no
                int numCeldas = 6;
                if (filtroEstadoReservas.ToUpper() != "TODOS" && filtroEstadoReservas.ToUpper() != "")
                {
                    //Si el valor de "filtroEstadoReservas" es diferente de vacío y de "TODOS", indica que NO vamos a mostrar el estado
                    numCeldas = 5;
                }

                //Encabezado de la tabla
                PdfPTable table = new PdfPTable(numCeldas);

                float[] widths = new float[] { };

                if (numCeldas == 6)
                {
                    //Agregar otra columna
                    widths = new float[] { 15f, 25f, 12f, 12f, 15f, 21f };
                }
                else
                {
                    widths = new float[] { 15f, 30f, 15f, 15f, 25f };
                }

                table.SetWidths(widths);

                _cell = new PdfPCell(new Paragraph("#", negrita));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(_cell);

                _cell = new PdfPCell(new Paragraph("CLIENTE", negrita));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(_cell);

                _cell = new PdfPCell(new Paragraph("FECHA\nRESERVA", negrita));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(_cell);

                _cell = new PdfPCell(new Paragraph("FECHA\nPROMESA\nENTREGA", negrita));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(_cell);

                if (numCeldas == 6)
                {
                    _cell = new PdfPCell(new Paragraph("ESTADO", negrita));
                    _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(_cell);
                }

                _cell = new PdfPCell(new Paragraph("MONTO RESERVA", negrita));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(_cell);

                table.WidthPercentage = 100f;

                int numReserva = 1;

                foreach (EReporteReservasEncabezado eReporteReservasEncabezado in this.eReporteReservasEncabezadoList)
                {
                    _cell = new PdfPCell(new Paragraph(numReserva.ToString(), fuente));
                    _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(_cell);

                    _cell = new PdfPCell(new Paragraph(String.Concat(eReporteReservasEncabezado.NombreCliente, " ",
                        eReporteReservasEncabezado.ApellidoCliente, "\nTEL.:", eReporteReservasEncabezado.TelefonoCliente), fuente));
                    _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(_cell);

                    _cell = new PdfPCell(new Paragraph(eReporteReservasEncabezado.FechaReserva.ToString("dd-MM-yyyy"), fuente));
                    _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(_cell);

                    _cell = new PdfPCell(new Paragraph(eReporteReservasEncabezado.FechaPromesaEntrega.ToString("dd-MM-yyyy"), fuente));
                    _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(_cell);

                    if (numCeldas == 6)
                    {
                        _cell = new PdfPCell(new Paragraph(eReporteReservasEncabezado.EstadoReserva, fuente));
                        _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(_cell);
                    }

                    _cell = new PdfPCell(new Paragraph(String.Concat("$", eReporteReservasEncabezado.MontoEncabezadoReserva.ToString("0.00")), fuente));
                    _cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table.AddCell(_cell);
                    numReserva++;
                    montoReserva += eReporteReservasEncabezado.MontoEncabezadoReserva;
                }

                #region Total de Reserva

                //Establecemos el número de columnas que se van a combinar
                int numColspan = 4;
                if (numCeldas == 6)
                {
                    numColspan = 5;
                }

                _cell = new PdfPCell(new Paragraph("TOTAL", negrita)) { Colspan = numColspan };
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                _cell.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
                table.AddCell(_cell);

                _cell = new PdfPCell(new Paragraph(String.Concat("$", montoReserva.ToString("0.00")), fuente));
                _cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(_cell);

                document.Add(table);
                #endregion

                #endregion

                document.Close();

                string nomPDF = "ReporteReserva" + concatNombre;

                response.ContentType = "application/pdf";
                response.AppendHeader("content-disposition",
                    "attachment;filename=" + nomPDF + ".pdf");
                response.Write(document);
                response.Flush();
                response.End();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        private ActionResult generarReporteReservaDetalle(string correo, HttpResponseBase response, HttpServerUtilityBase server)
        {
            try
            {
                Document document = new Document(PageSize.LETTER, 30f, 30f, 80f, 40f);

                PdfWriter writer = PdfWriter.GetInstance(document, response.OutputStream);
                writer.PageEvent = new PageEventHelperRU();//Con esto agregamos los números de página

                document.Open();

                //Tipo de fuente
                Font fuenteEmision = FontFactory.GetFont("Arial", 10, BaseColor.BLACK);
                Font fuente = FontFactory.GetFont("Arial", 10, BaseColor.BLACK);
                Font negrita = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK);
                Font title = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK);


                /* Variables para encabezado */
                string TITULO = "COMPROBANTE DE RESERVA";//Título a mostrar en el Encabezado
                string fechaEmision = new LUtils().fechaHoraActual();//Fecha de creacion para poner en el PDF
                string concatNombre = DateTime.Parse(new LUtils().fechaHoraActual()).ToString("ddMMyyyyHHmmss");//Fecha de creacion para poner en el PDF

                //Obtenemos los datos del Encabezado
                EUsuario eUsuario = new LUsuarios().seleccionarUsuarioByCorreo(correo);

                string nombreEmisor = String.Concat(eUsuario.Nombres, " ", eUsuario.Apellidos);//Nombre del Usuario
                string nombreEmpresa = eUsuario.NombreEmpresa;//Nombre de la Empresa
                string fechaReserva = eReporteReservasEncabezado.FechaReserva.ToString("dd-MM-yyyy");
                string fechaPromesaEntrega = eReporteReservasEncabezado.FechaPromesaEntrega.ToString("dd-MM-yyyy");
                string filtroEstadoReservas = this.eReporteReservasEncabezado.EstadoReserva;
                string filtroCliente = this.eReporteReservasEncabezado.NombreCliente != "" ? this.eReporteReservasEncabezado.NombreCliente : "TODOS";
                double montoReserva = eReporteReservasEncabezado.MontoEncabezadoReserva;//El monto total de la reserva

                document.AddCreationDate();

                #region Asignacion del Logo de StockIt
                string nombreImagen = "logoStockIt.jpg";//Cambiar nombre por el de la nueva imagen

                string PathImage = server.MapPath("/images/" + nombreImagen);

                //Begin image
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(PathImage);
                logo.SetAbsolutePosition(400f, 700f);
                logo.ScaleAbsolute(110f, 80f);
                float percentage = 0.0f;
                percentage = 200 / logo.Width;
                logo.ScalePercent(percentage * 100);
                document.Add(logo);
                //End image;
                #endregion

                #region Header de la Reserva
                PdfPTable tbHeader = new PdfPTable(3);

                tbHeader.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tbHeader.DefaultCell.Border = 0;
                tbHeader.AddCell(new Paragraph());

                PdfPCell _cell = new PdfPCell(new Paragraph(TITULO, title));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                _cell.Border = 0;
                tbHeader.AddCell(_cell);

                tbHeader.AddCell(new Paragraph());
                tbHeader.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetTop(document.TopMargin) + 2, writer.DirectContent);

                var fechaPhrase = new Phrase();
                fechaPhrase.Add(new Chunk("FECHA Y HORA DE EMISIÓN: ", negrita));
                fechaPhrase.Add(new Chunk(fechaEmision, fuenteEmision));

                var emisorPhrase = new Phrase();
                emisorPhrase.Add(new Chunk("NOMBRE DEL EMISOR: ", negrita));
                emisorPhrase.Add(new Chunk(nombreEmisor, fuenteEmision));

                var empresaPhrase = new Phrase();
                empresaPhrase.Add(new Chunk("EMPRESA: ", negrita));
                empresaPhrase.Add(new Chunk(nombreEmpresa, fuenteEmision));

                var fechaReservaPhrase = new Phrase();
                fechaReservaPhrase.Add(new Chunk("FECHA DE RESERVA: ", negrita));
                fechaReservaPhrase.Add(new Chunk(fechaReserva, fuenteEmision));

                var fechaPromesaEntregaPhrase = new Phrase();
                fechaPromesaEntregaPhrase.Add(new Chunk("FECHA PROMESA DE ENTREGA: ", negrita));
                fechaPromesaEntregaPhrase.Add(new Chunk(fechaPromesaEntrega, fuenteEmision));

                var estadoReservasPhrase = new Phrase();
                estadoReservasPhrase.Add(new Chunk("ESTADO DE RESERVA: ", negrita));
                estadoReservasPhrase.Add(new Chunk(filtroEstadoReservas, fuenteEmision));

                var clientePhrase = new Phrase();
                clientePhrase.Add(new Chunk("CLIENTE: ", negrita));
                clientePhrase.Add(new Chunk(filtroCliente, fuenteEmision));

                Chunk chunk = new Chunk();
                document.Add(new Paragraph(chunk));
                document.Add(new Paragraph("                       "));
                document.Add(new Paragraph("------------------------------------------------------------------------------------------------------------------------------------------"));
                document.Add(new Paragraph(fechaPhrase));
                document.Add(new Paragraph(emisorPhrase));
                document.Add(new Paragraph(empresaPhrase));
                document.Add(new Paragraph(fechaReservaPhrase));
                document.Add(new Paragraph(fechaPromesaEntregaPhrase));
                document.Add(new Paragraph(estadoReservasPhrase));
                document.Add(new Paragraph(clientePhrase));
                document.Add(new Paragraph("------------------------------------------------------------------------------------------------------------------------------------------"));
                document.Add(new Paragraph("                       "));
                #endregion

                #region Listado General de las Reservas
                //Encabezado de la tabla
                PdfPTable table = new PdfPTable(5);
                float[] widths = new float[] { 15f, 30f, 15f, 15f, 25f };
                table.SetWidths(widths);

                _cell = new PdfPCell(new Paragraph("#", negrita));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(_cell);

                _cell = new PdfPCell(new Paragraph("PRODUCTO", negrita));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(_cell);

                _cell = new PdfPCell(new Paragraph("CANTIDAD", negrita));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(_cell);

                _cell = new PdfPCell(new Paragraph("PRECIO", negrita));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(_cell);

                _cell = new PdfPCell(new Paragraph("SUBTOTAL", negrita));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(_cell);

                table.WidthPercentage = 100f;

                int numProducto = 1;

                foreach (EReporteReservasDetalle eDetalleReserva in eDetalleReservasList)
                {
                    _cell = new PdfPCell(new Paragraph(numProducto.ToString(), fuente));
                    _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(_cell);

                    _cell = new PdfPCell(new Paragraph(eDetalleReserva.NombreProducto, fuente));
                    _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(_cell);

                    _cell = new PdfPCell(new Paragraph(eDetalleReserva.Cantidad.ToString(), fuente));
                    _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(_cell);

                    _cell = new PdfPCell(new Paragraph(String.Concat("$", eDetalleReserva.PrecioProducto.ToString("0.00")), fuente));
                    _cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table.AddCell(_cell);

                    _cell = new PdfPCell(new Paragraph(String.Concat("$", eDetalleReserva.Monto.ToString("0.00")), fuente));
                    _cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table.AddCell(_cell);

                    numProducto++;
                }

                #region Total de Reserva
                _cell = new PdfPCell(new Paragraph("TOTAL", negrita)) { Colspan = 4 };
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                _cell.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
                table.AddCell(_cell);

                _cell = new PdfPCell(new Paragraph(String.Concat("$", montoReserva.ToString("0.00")), fuente));
                _cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(_cell);

                document.Add(table);
                #endregion

                #endregion

                document.Close();

                string nomPDF = "ComprobanteReserva" + concatNombre;

                response.ContentType = "application/pdf";
                response.AppendHeader("content-disposition",
                    "attachment;filename=" + nomPDF + ".pdf");
                response.Write(document);
                response.Flush();
                response.End();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
    }
}
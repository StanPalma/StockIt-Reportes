using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Electiva4.Controllers
{
    public class ReporteClientesController : Controller
    {
        // GET: ReporteClientes
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult pdfClientes()
        {
            try
            {
                Document document = new Document(PageSize.LETTER, 30f, 30f, 80f, 40f);

                PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);
                writer.PageEvent = new PageEventHelperRU();//Con esto agregamos los números de página

                document.Open();

                //Tipo de fuente
                Font fuenteEmision = FontFactory.GetFont("Arial", 10, BaseColor.BLACK);
                Font fuente = FontFactory.GetFont("Arial", 10, BaseColor.BLACK);
                Font negrita = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK);
                Font title = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK);


                /* Variables para encabezado */
                string TITULO = "REPORTE GENERAL DE CLIENTES";//Título a mostrar en el Encabezado
                string fechaEmision = "10-11-2021";//new LUtils().fechaHoraActual();//Fecha de creacion para poner en el PDF

                //Obtenemos los datos del Encabezado
                //EUsuario eUsuario = new LUsuarios().seleccionarUsuarioByCorreo(new Utils().getCorreoUsuario());

                string nombreEmisor = "HUGO RODRÍGUEZ";//String.Concat(eUsuario.Nombres, " ", eUsuario.Apellidos);//Nombre del Usuario
                string nombreEmpresa = "MI EMPRESA";//eUsuario.NombreEmpresa;//Nombre de la Empresa
                string filtro = "09-11-2021 a 10-11-2021";//String.Concat(fechaInicio, " a ", fechaFinal);//Nombre de la Empresa
                

                document.AddCreationDate();

                #region Asignacion del Logo de StockIt
                /*string nombreImagen = "logoStockIt.jpg";//Cambiar nombre por el de la nueva imagen

                string PathImage = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "Resources\\" + nombreImagen);

                //Begin image
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(PathImage);
                logo.SetAbsolutePosition(400f, 700f);
                logo.ScaleAbsolute(110f, 80f);
                float percentage = 0.0f;
                percentage = 200 / logo.Width;
                logo.ScalePercent(percentage * 100);
                document.Add(logo);*/
                //End image;
                #endregion

                #region Header de la Compra
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

                var filtroPhrase = new Phrase();
                filtroPhrase.Add(new Chunk("FILTRO: ", negrita));
                filtroPhrase.Add(new Chunk(filtro, fuenteEmision));

                Chunk chunk = new Chunk();
                document.Add(new Paragraph(chunk));
                document.Add(new Paragraph("                       "));
                document.Add(new Paragraph("------------------------------------------------------------------------------------------------------------------------------------------"));
                document.Add(new Paragraph(fechaPhrase));
                document.Add(new Paragraph(emisorPhrase));
                document.Add(new Paragraph(empresaPhrase));
                document.Add(new Paragraph(filtroPhrase));
                document.Add(new Paragraph("------------------------------------------------------------------------------------------------------------------------------------------"));
                document.Add(new Paragraph("                       "));
                #endregion

                #region Listado General de Clientes
                //Encabezado de la tabla
                PdfPTable table = new PdfPTable(5);
                float[] widths = new float[] { 10f, 40f, 20f, 20f, 10f };
                table.SetWidths(widths);

                _cell = new PdfPCell(new Paragraph("#", negrita));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(_cell);

                _cell = new PdfPCell(new Paragraph("NOMBRE", negrita));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(_cell);

                _cell = new PdfPCell(new Paragraph("TELEFONO", negrita));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(_cell);

                _cell = new PdfPCell(new Paragraph("CORREO", negrita));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(_cell);

                _cell = new PdfPCell(new Paragraph("SEXO", negrita));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(_cell);

                table.WidthPercentage = 100f;

                /*foreach (EReporteProductosEncabezado eReporteProductosEncabezado in eReporteProductosEncabezadoList)
                {
                    _cell = new PdfPCell(new Paragraph(numCompra.ToString(), fuente));
                    _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(_cell);

                    _cell = new PdfPCell(new Paragraph(eReporteProductosEncabezado.NombreProveedor.ToString(), fuente));
                    _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(_cell);

                    _cell = new PdfPCell(new Paragraph(eReporteProductosEncabezado.FechaIngreso.ToString("dd-MM-yyyy"), fuente));
                    _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(_cell);

                    _cell = new PdfPCell(new Paragraph(String.Concat("$", eReporteProductosEncabezado.Monto.ToString("0.00")), fuente));
                    _cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table.AddCell(_cell);
                    numCompra++;
                    montoCompra += eReporteProductosEncabezado.Monto;
                }*/

              

                document.Add(table);
               

                #endregion

                document.Close();

                string nomPDF = "ReporteClientes";

                Response.ContentType = "application/pdf";
                Response.AppendHeader("content-disposition",
                    "attachment;filename=" + nomPDF + ".pdf");
                Response.Write(document);
                Response.Flush();
                Response.End();

                /*Utils utils = new Utils();
                utils.messageBoxOperacionExitosa("El reporte se guardó como " + Path.GetFileNameWithoutExtension(rutaArchivoFinal) + ".pdf");*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                /*Utils utils = new Utils();
                utils.messageBoxOperacionSinExito("No se pudo generar la factura. Intente más tarde.");*/
            }

            return null;
        }
    }
}
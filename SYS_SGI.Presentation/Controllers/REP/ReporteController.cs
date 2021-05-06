using SYS_SGI.Domain.Implementation.Common.Base;
using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Presentation.Models.View;
using SYS_SGI.Presentation.Utilities;
using GMD.CustomHtmlHelpers.HtmlGenericGrid;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Linq;
using System;
using System.Data;
using SYS_SGI.Infrastructure.Data.Implementation.Base;
using System.Web.Configuration;
using SYS_SGI.Application.Implementation.REP;
using SYS_SGI.Domain.Implementation.LogicalEntities.REP;
using Microsoft.Reporting.WebForms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.IO;
using SYS_SGI.Domain.Implementation.LogicalEntities.MAN;
using SYS_SGI.Application.Implementation.MAN;
using SYS_SGI.Application.Implementation.MOV;
using SYS_SGI.Domain.Implementation.LogicalEntities.MOV;
using System.Text.RegularExpressions;

namespace SYS_SGI.Presentation.Controllers.REP
{
    [AppPresentationError]
    public class ReporteController : BaseController
    {
        private readonly ReporteAppService _ReporteAppService = new ReporteAppService();
        private readonly AlineamientoEstrategicoAppService _AlineamientoEstrategicoAppService = new AlineamientoEstrategicoAppService();
        private readonly AlineamientoConfiguracionAppService _AlineamientoConfiguracionAppService = new AlineamientoConfiguracionAppService();
        private readonly IndicadorAppService _IndicadorAppService = new IndicadorAppService();
        private readonly IndicadorDetalleAppService _IndicadorDetalleAppService = new IndicadorDetalleAppService();
        private static AppResponse appResponse = new AppResponse();
        private ReporteLogic reporte = new ReporteLogic();
        public int CantidadLineamientos { get; set; }

        private int contMeta = 0;
        private int contHistorico = 0;
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult Paginacion(string arg)
        {
            var modelData = JsonConvert.DeserializeObject<GMDGridModel<ReporteLogic>>(arg);
            var paginateParams = new PaginateParams
            {
                IsPaginate = Convert.ToBoolean(WebConfigurationManager.AppSettings["IsPaginate"]),
                PageIndex = modelData.CurrentPageIndex,
                RowsPerPage = Convert.ToInt32(WebConfigurationManager.AppSettings["RowsPerPage"]),
                SortColumn = WebConfigurationManager.AppSettings["SortColumn"].ToString(),
                SortDirection = modelData.DirectionOrder
            };
            if (modelData.Filters != null)
                paginateParams.Filters = Funciones.Conversion.ListaToDatatable<GMDFilter>(modelData.Filters.ToList());


            if (modelData.isFirstLoad)
                modelData.Data = _ReporteAppService.Paginacion(paginateParams);

            
            else
                modelData.Data = new List<ReporteLogic>();

            modelData.TotalRows = paginateParams.TotalRows;

            return PartialView(modelData);
        }

        #region Anexo

        public FileResult DescargarDocumento(long codigoGuiaEmpresarial, string GuiaEmpresarial, string anexo)
        {
            AlineamientoConfiguracionLogic alineamientoConfiguracion = new AlineamientoConfiguracionLogic();
            List<AlineamientoEstrategicoLogic> listaAlineamientoEstrategico = new List<AlineamientoEstrategicoLogic>();

            bool evaluacion = false, planOperativo = false;
            if (codigoGuiaEmpresarial == Enums.GuiaEmpresarial.MatrizPEI || codigoGuiaEmpresarial == Enums.GuiaEmpresarial.PlanOperativo)
            {
                evaluacion = (codigoGuiaEmpresarial == Enums.GuiaEmpresarial.MatrizPEI);
                planOperativo = (codigoGuiaEmpresarial == Enums.GuiaEmpresarial.PlanOperativo);
                codigoGuiaEmpresarial = Enums.GuiaEmpresarial.MatrizEstrategica;                
            }
            
            if (codigoGuiaEmpresarial > 0)
            {
                alineamientoConfiguracion = _AlineamientoConfiguracionAppService.Listar().Where(x => x.CodigoTipoGuiaEmpresarial == codigoGuiaEmpresarial && x.Estado).FirstOrDefault();
                listaAlineamientoEstrategico = _AlineamientoEstrategicoAppService.Listar().Where(x => x.Estado && x.CodigoTipoGuiaEmpresarial == codigoGuiaEmpresarial).ToList();
            }

            DataTable dataLineamiento = ObtenerTabla(alineamientoConfiguracion, listaAlineamientoEstrategico, codigoGuiaEmpresarial, evaluacion, planOperativo);
            DataTable data = evaluacion ? ObtenerValores(dataLineamiento) : dataLineamiento;
            
            string rutaCompleta = CrearReporte(data, GuiaEmpresarial, CantidadLineamientos, anexo);//CORREGIR
            string ruta = Path.GetDirectoryName(rutaCompleta);
            string tipoArchivo = "application/pdf";
            string nombreArchivo = Path.GetFileName(rutaCompleta);

            return File(rutaCompleta, tipoArchivo, nombreArchivo);
        }

        public DataTable ObtenerTabla(AlineamientoConfiguracionLogic alineamientoConfiguracion, List<AlineamientoEstrategicoLogic> listaAlineamientoEstrategico, long codigoGuiaEmpresarial, bool evaluacion, bool planOperativo)
        {
            DataTable tabla = new DataTable();
            int cont = 0;
            bool removerColumnas = false;

            List<ReporteLogic> listaIndicadorDetalleMeta = new List<ReporteLogic>();
            List<ReporteLogic> listaIndicadorDetalleLineaBase = new List<ReporteLogic>();

            if (planOperativo)
            {
                listaIndicadorDetalleMeta = _ReporteAppService.ObtenerDetalleIndicador(codigoGuiaEmpresarial, Enums.TipoRegistro.Ejecucion, Enums.TipoPeriodicidad.Trimestral);//cambiar el tipo de periodicidad
            }

            if (codigoGuiaEmpresarial != Enums.GuiaEmpresarial.AlineamientoOEC)
            {
                listaIndicadorDetalleMeta = _ReporteAppService.ObtenerDetalleIndicador(codigoGuiaEmpresarial, Enums.TipoRegistro.Meta, 99);//cambiar el tipo de periodicidad
                listaIndicadorDetalleLineaBase = _ReporteAppService.ObtenerDetalleIndicador(codigoGuiaEmpresarial, Enums.TipoRegistro.LineaBase, 99);//cambiar el tipo de periodicidad
            }

            string  cabeceraCadena = "",
                    unidadMedida = "UNIDAD DE MEDIDA",
                    codigoIndicador = "CODIGO_INDICADOR",
                    formula = "FORMA DE CÁLCULO",
                    fuenteInformacion = "FUENTE DE INFORMACIÓN",
                    area = "AREA RESPONSABLE",
                    indicador = "INDICADOR",
                    anio = "AÑO-Linea Base",
                    valor = "VALOR-Linea Base",
                    codigoAlineamiento = "CODIGO_ALINEAMIENTO_ESTRATEGICO";

            if (alineamientoConfiguracion.CodigoTipoAlineamiento1 > 0)
            {
                string cabecera = alineamientoConfiguracion.TipoAlineamiento1.Split(':')[1].Trim();
                tabla.Columns.Add(cabecera, typeof(string));
                cont++;
            }

            if (alineamientoConfiguracion.CodigoTipoAlineamiento2 > 0)
            {
                tabla.Columns.Add(alineamientoConfiguracion.TipoAlineamiento2.Split(':')[1], typeof(string));
                cont++;
            }

            if (alineamientoConfiguracion.CodigoTipoAlineamiento3 > 0)
            {
                tabla.Columns.Add(alineamientoConfiguracion.TipoAlineamiento3.Split(':')[1], typeof(string));
                cont++;
            }

            if (alineamientoConfiguracion.CodigoTipoAlineamiento4 > 0)
            {
                tabla.Columns.Add(alineamientoConfiguracion.TipoAlineamiento4.Split(':')[1], typeof(string));
                cont++;
            }

            if (alineamientoConfiguracion.CodigoTipoAlineamiento5 > 0)
            {
                tabla.Columns.Add(alineamientoConfiguracion.TipoAlineamiento5.Split(':')[1], typeof(string));
                cont++;
            }

            if (alineamientoConfiguracion.CodigoTipoAlineamiento6 > 0)
            {
                tabla.Columns.Add(alineamientoConfiguracion.TipoAlineamiento6.Split(':')[1], typeof(string));
                cont++;
            }

            if (alineamientoConfiguracion.CodigoTipoAlineamiento7 > 0)
            {
                tabla.Columns.Add(alineamientoConfiguracion.TipoAlineamiento7.Split(':')[1], typeof(string));
                cont++;
            }

            if (alineamientoConfiguracion.CodigoTipoAlineamiento8 > 0)
            {
                tabla.Columns.Add(alineamientoConfiguracion.TipoAlineamiento8.Split(':')[1], typeof(string));
                cont++;
            }

            if (alineamientoConfiguracion.CodigoTipoAlineamiento9 > 0)
            {
                tabla.Columns.Add(alineamientoConfiguracion.TipoAlineamiento9.Split(':')[1], typeof(string));
                cont++;
            }

            if (alineamientoConfiguracion.CodigoTipoAlineamiento10 > 0)
            {
                tabla.Columns.Add(alineamientoConfiguracion.TipoAlineamiento10.Split(':')[1], typeof(string));
                cont++;
            }

            if (alineamientoConfiguracion.Indicador)
            {
                tabla.Columns.Add(indicador, typeof(string));
                tabla.Columns.Add(unidadMedida, typeof(string));
                tabla.Columns.Add(codigoIndicador, typeof(int));
                tabla.Columns.Add(codigoAlineamiento, typeof(int));

                removerColumnas = true;

                if (!evaluacion)
                {
                    //para linea base
                    tabla.Columns.Add(anio, typeof(string));
                    tabla.Columns.Add(valor, typeof(string));                    
                }                

                cont++;
            }

            CantidadLineamientos = cont;

            foreach (AlineamientoEstrategicoLogic item in listaAlineamientoEstrategico)
            {
                DataRow row = tabla.NewRow();

                if (item.CodigoAlineamiento1 != "0" && item.CodigoAlineamiento1 != "" && item.CodigoAlineamiento1 != null)
                {
                    row[0] = item.Alineamiento1;
                }

                if (item.CodigoAlineamiento2 != "0" && item.CodigoAlineamiento2 != "" && item.CodigoAlineamiento2 != null)
                {
                    row[1] = item.Alineamiento2;
                }

                if (item.CodigoAlineamiento3 != "0" && item.CodigoAlineamiento3 != "" && item.CodigoAlineamiento3 != null)
                {
                    row[2] = item.Alineamiento3;
                }

                if (item.CodigoAlineamiento4 != "0" && item.CodigoAlineamiento4 != "" && item.CodigoAlineamiento4 != null)
                {
                    row[3] = item.Alineamiento4;
                }

                if (item.CodigoAlineamiento5 != "0" && item.CodigoAlineamiento5 != "" && item.CodigoAlineamiento5 != null)
                {
                    row[4] = item.Alineamiento5;
                }

                if (item.CodigoAlineamiento6 != "0" && item.CodigoAlineamiento6 != "" && item.CodigoAlineamiento6 != null)
                {
                    row[5] = item.Alineamiento6;
                }

                if (item.CodigoAlineamiento7 != "0" && item.CodigoAlineamiento7 != "" && item.CodigoAlineamiento7 != null)
                {
                    row[6] = item.Alineamiento7;
                }

                if (item.CodigoAlineamiento8 != "0" && item.CodigoAlineamiento8 != "" && item.CodigoAlineamiento8 != null)
                {
                    row[7] = item.Alineamiento8;
                }

                if (item.CodigoAlineamiento9 != "0" && item.CodigoAlineamiento9 != "" && item.CodigoAlineamiento9 != null)
                {
                    row[8] = item.Alineamiento9;
                }

                if (item.CodigoAlineamiento10 != "0" && item.CodigoAlineamiento10 != "" && item.CodigoAlineamiento10 != null)
                {
                    row[9] = item.Alineamiento10;
                }

                if (item.CodigoIndicador > 0)
                {
                    row[indicador] = Regex.Replace(item.Indicador, @"[\d-\.]", string.Empty);
                    row[codigoIndicador] = item.CodigoIndicador;
                    row[codigoAlineamiento] = item.CodigoAlineamientoEstrategico;
                }

                tabla.Rows.Add(row);
            }

            if (listaIndicadorDetalleLineaBase.Count > 0 && !evaluacion)
            {
                foreach (DataRow item in tabla.Rows)
                {
                    List<ReporteLogic> info = listaIndicadorDetalleLineaBase.Where(x => x.CodigoAlineamientoEstrategico == int.Parse(item[codigoAlineamiento].ToString()) && x.CodigoIndicador == int.Parse(item[codigoIndicador].ToString())).ToList();
                    
                    foreach (ReporteLogic item1 in info)
                    {                       
                        item[anio] = item1.TipoPeriodicidad;
                        item[valor] = item1.Valor.ToString() == "" ? "-" : item1.Valor.ToString().Substring(0, item1.Valor.ToString().Length - 2);                        
                    }
                }
            }

            if (listaIndicadorDetalleMeta.Count > 0)
            {
                List<SelectListItem> dtCabeceraIndicador = listaIndicadorDetalleMeta.Select(item => new SelectListItem
                {
                    Value = item.TipoPeriodicidad.ToString(),
                    Text = item.TipoPeriodicidad
                }).DistinctBy(x => x.Value).ToList();

                cont = 0;
                foreach (SelectListItem item in dtCabeceraIndicador)
                {
                    tabla.Columns.Add(item.Text+"-Metas", typeof(string));

                    if (cont == dtCabeceraIndicador.Count - 1 && codigoGuiaEmpresarial == Enums.GuiaEmpresarial.MatrizAEI)
                    {
                        tabla.Columns.Add(formula, typeof(string));
                        tabla.Columns.Add(fuenteInformacion, typeof(string));
                        tabla.Columns.Add(area, typeof(string));
                    }

                    cont++;
                }

                foreach (DataRow item in tabla.Rows)
                {
                    List<ReporteLogic> info = listaIndicadorDetalleMeta.Where(x => x.CodigoAlineamientoEstrategico == int.Parse(item[codigoAlineamiento].ToString()) && x.CodigoIndicador == int.Parse(item[codigoIndicador].ToString())).ToList();

                    cont = 0;
                    foreach (ReporteLogic item1 in info)
                    {
                        if (cont == 0)
                        {
                            item[unidadMedida] = item1.UnidadMedida;

                            if (codigoGuiaEmpresarial == Enums.GuiaEmpresarial.MatrizAEI)
                            {
                                item[formula] = item1.Formula;
                                item[fuenteInformacion] = item1.FuenteInformacion;
                                item[area] = item1.Area;
                            }
                        }

                        item[item1.TipoPeriodicidad+"-Metas"] = item1.Valor.ToString() == "" ? "-" : item1.Valor.ToString().Substring(0, item1.Valor.ToString().Length - 2);

                        cont++;
                    }
                }
            }

            foreach (DataRow rows in tabla.Rows)
            {
                for(int i=0; i < tabla.Columns.Count; i++)
                {
                    string contenido = rows[i].ToString().Trim();

                    if (contenido == "")
                        rows[i] = "-";
                }
            }

            if (removerColumnas && !evaluacion)
            {
                tabla.Columns.Remove(codigoAlineamiento);
                tabla.Columns.Remove(codigoIndicador);
            }

            return tabla;
        }

        public DataTable ObtenerValores(DataTable dataLineamiento)
        {
            List<IndicadorDetalleLogic> listaIndicadorDetalleEjecucion = new List<IndicadorDetalleLogic>();

            if (dataLineamiento.Rows.Count > 0)
            {
                listaIndicadorDetalleEjecucion = _IndicadorDetalleAppService.Listar(0, Enums.TipoRegistro.Ejecucion, Enums.TipoPlan.PlanEjecucion, "").Where(x => x.Estado).ToList();
            }

            int cont = 0;
            bool removerColumnas = true;            
            string codigoAlineamiento = "CODIGO_ALINEAMIENTO_ESTRATEGICO",
                codigoIndicador = "CODIGO_INDICADOR",
                cumplimiento = "Cumplimiento %", 
                ejecucion = "Ejecución al año ", 
                ponderacion="Ponderación";

            dataLineamiento.Columns.Add(ejecucion, typeof(string));            
            dataLineamiento.Columns.Add(cumplimiento, typeof(string));
            dataLineamiento.Columns.Add(ponderacion, typeof(string));

            cont = 0;
            foreach (DataRow item in dataLineamiento.Rows)
            {                
                foreach (IndicadorDetalleLogic item2 in listaIndicadorDetalleEjecucion.Where(x=>x.CodigoAlineamientoEstrategico.ToString() == item[codigoAlineamiento].ToString()))
                {
                    if (item2.Valor > 0)
                    {
                        item[ejecucion] = item2.Valor;
                        item[cumplimiento] = (item[item2.Anio+"-Metas"].ToString() == "0" || item[item2.Anio + "-Metas"].ToString() == "-") ? 0 : Math.Round((item2.Valor / decimal.Parse(item[item2.Anio + "-Metas"].ToString())) * 100, 2);
                        item[ponderacion] = item2.Ponderacion;

                        if (cont == 0)
                        {
                            dataLineamiento.Columns[ejecucion].ColumnName = ejecucion + item2.Anio;
                            ejecucion = ejecucion + item2.Anio;                            
                        }
                            
                        cont++;
                    }                    
                }
            }
            
            foreach (DataRow rows in dataLineamiento.Rows)
            {
                for (int i = 0; i < dataLineamiento.Columns.Count; i++)
                {
                    string contenido = rows[i].ToString().Trim();

                    if (contenido == "")
                        rows[i] = "-";
                }
            }

            if (removerColumnas)
            {
                dataLineamiento.Columns.Remove(codigoAlineamiento);
                dataLineamiento.Columns.Remove(codigoIndicador);
            }

            return dataLineamiento;
        }

        private string CrearReporte(DataTable Data, string GuiaEmpresarial, int cantidadLineamientos, string anexo)
        {
            string rutaDescarga = WebConfigurationManager.AppSettings["RutaDescargaArchivos"].ToString();
            string ImagenReporte = WebConfigurationManager.AppSettings["ImagenReporte"].ToString();

            string rutaCompleta = rutaDescarga + GuiaEmpresarial.Replace(" ", "_") + "_" + DateTime.Now.ToString("yyyyMMdd-HHmmssfff") + ".pdf";

            int cantidadColumnas = Data.Columns.Count;
            List<SelectListItem> cabeceraReporte = cabecera(Data);

            Document doc = new Document(PageSize.A4.Rotate());

            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(rutaCompleta, FileMode.Create));
            doc.AddTitle(GuiaEmpresarial);
            doc.AddCreator("GMD");
            doc.Open();
            writer.PageEvent = new PDFFooter();

            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(ImagenReporte);
            jpg.ScaleToFit(110f, 140f);
            //jpg.SetAbsolutePosition(55f, 800f);

            doc.Add(jpg);

            //Definimos Colores
            BaseColor _BackgroundCabecera = WebColors.GetRGBColor("#fafad2");
            BaseColor _BorderColor = WebColors.GetRGBColor("#C0C0C0");
            Font _FontColorCabecera = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5.5f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Font _FontColorTituloPrincipal = FontFactory.GetFont("HELVETICA", 9.3f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Font _FontColorDetalle = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4.3f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            // Titulo del documento
            Paragraph titulo = new Paragraph();
            titulo.Add(new Phrase(anexo, _FontColorTituloPrincipal));
            titulo.Add(new Phrase("\n" + GuiaEmpresarial.Replace("_", " ") + "\n", _FontColorTituloPrincipal));
            //PdfPCell col_2 = new PdfPCell(titulo);
            //Paragraph titulo = new Paragraph(0, GuiaEmpresarial.Replace("_", " ") + "\n\n", _FontColorTituloPrincipal);

            titulo.Alignment = Element.ALIGN_CENTER;
            titulo.SpacingBefore = 16f;
            titulo.SpacingAfter = 4f;
            titulo.IndentationRight = 14f;
            doc.Add(titulo);

            PdfPTable tblPrincipal = new PdfPTable(1);
            tblPrincipal.WidthPercentage = 100f;
            tblPrincipal.HorizontalAlignment = Element.ALIGN_CENTER;
            tblPrincipal.SplitLate = false;

            PdfPCell tblCabecera = new PdfPCell();
            tblCabecera.BorderWidth = 0f;
            tblCabecera.PaddingTop = 0f;
            tblCabecera.Table = new PdfPTable(cantidadColumnas);
            tblCabecera.Table.WidthPercentage = 100;
            tblCabecera.Table.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell tblDetalle = new PdfPCell();
            tblDetalle.BorderWidth = 0f;
            tblDetalle.PaddingTop = 0f;
            tblDetalle.Table = new PdfPTable(cantidadColumnas);
            tblDetalle.Table.WidthPercentage = 100;
            tblDetalle.Table.HorizontalAlignment = Element.ALIGN_CENTER;
            
            int cont = 0;
            foreach (SelectListItem item in cabeceraReporte)
            {
                Paragraph TituloCabecera = new Paragraph();
                TituloCabecera.Add(new Phrase(item.Text, _FontColorCabecera));
                PdfPCell cabecera = new PdfPCell(TituloCabecera);
                cabecera.BorderWidth = 0.5f;
                cabecera.BackgroundColor = _BackgroundCabecera;
                cabecera.HorizontalAlignment = Element.ALIGN_CENTER;
                cabecera.VerticalAlignment = Element.ALIGN_MIDDLE;
                cabecera.BorderColor = BaseColor.BLACK;
                cabecera.PaddingBottom = 4;
                cabecera.Rowspan = item.Value.ToString() == "" ? 2 : 1;
                cabecera.Colspan = item.Value.ToString() != "" ? (item.Text == "Metas" ? 5 : 2) : 1;

                string valorAnterior = cont > 0 ? cabeceraReporte[cont - 1].Text : "";
                if ((cabecera.Colspan > 1 && valorAnterior != item.Text) || cabecera.Rowspan > 1)
                    tblCabecera.Table.AddCell(cabecera);

                cont++;
            }

            foreach (SelectListItem item in cabeceraReporte)
            {
                if (item.Value != "")
                {
                    Paragraph TituloCabecera = new Paragraph();
                    TituloCabecera.Add(new Phrase(item.Value, _FontColorCabecera));
                    PdfPCell cabecera = new PdfPCell(TituloCabecera);
                    cabecera.BorderWidth = 0.5f;
                    cabecera.BackgroundColor = _BackgroundCabecera;
                    cabecera.HorizontalAlignment = Element.ALIGN_CENTER;
                    cabecera.VerticalAlignment = Element.ALIGN_CENTER;
                    cabecera.BorderColor = BaseColor.BLACK;
                    cabecera.PaddingBottom = 4;

                    tblCabecera.Table.AddCell(cabecera);
                }
            }


            string alineamientoAnterior = "";
            string alineamientoActual = "";
            int filasRowSpan = 1;
            cont = 0;
            int cantidadC=0;
            foreach (DataRow itemDetalle in Data.Rows)
            {
                if (cont == 0)
                {
                    for (int i = 0; i < cantidadColumnas; i++)
                    {
                        filasRowSpan = 1;
                        string valorAnterior = "", valorActual = "";

                        if (i < cantidadLineamientos)
                        {
                            foreach (DataRow item in Data.Rows)
                            {
                                if (item[i].ToString() == "")
                                {
                                    break;
                                }

                                valorActual = i > 0 ? item[i - 1].ToString() + item[i].ToString() : item[i].ToString();

                                if (filasRowSpan == 1)
                                {
                                    valorAnterior = valorActual;
                                }
                                else if (valorAnterior != valorActual)
                                    break;

                                filasRowSpan++;
                            }
                        }

                        PdfPCell detalle = new PdfPCell(new Phrase(itemDetalle[i].ToString().Replace(";","\n"), _FontColorDetalle));
                        detalle.BorderWidth = 0.1f;
                        detalle.BorderColor = _BorderColor;
                        detalle.HorizontalAlignment = Element.ALIGN_CENTER;
                        detalle.VerticalAlignment = Element.ALIGN_MIDDLE;
                        detalle.PaddingTop = 0.5f;
                        detalle.PaddingBottom = 2f;
                        detalle.PaddingLeft = 2f;
                        detalle.Rowspan = filasRowSpan - 1;

                        tblDetalle.Table.AddCell(detalle);
                    }
                }
                else
                {
                    for (int i = 0; i < cantidadColumnas; i++)
                    {
                        alineamientoAnterior = i > 0 ? Data.Rows[cont - 1][i - 1].ToString() + "-" + Data.Rows[cont - 1][i].ToString() : Data.Rows[cont - 1][i].ToString();
                        alineamientoActual = i > 0 ? itemDetalle[i - 1].ToString() + "-" + itemDetalle[i].ToString() : itemDetalle[i].ToString();

                        filasRowSpan = 1;
                        string valorAnterior = "", valorActual = "";

                        if (itemDetalle[i].ToString() == "" || i >= cantidadLineamientos)
                        {
                            PdfPCell detalle = new PdfPCell(new Phrase(itemDetalle[i].ToString().Replace(";", "\n"), _FontColorDetalle));
                            detalle.BorderWidth = 0.1f;
                            detalle.BorderColor = _BorderColor;
                            detalle.HorizontalAlignment = Element.ALIGN_CENTER;
                            detalle.VerticalAlignment = Element.ALIGN_MIDDLE;
                            detalle.PaddingTop = 0.5f;
                            detalle.PaddingBottom = 2f;
                            detalle.PaddingLeft = 2f;
                            detalle.Rowspan = filasRowSpan;

                            tblDetalle.Table.AddCell(detalle);
                        }
                        else if ((alineamientoAnterior != alineamientoActual) || i == cantidadColumnas - 1)
                        {
                            int contador = 0;
                            if (i < cantidadLineamientos)
                            {
                                foreach (DataRow item in Data.Rows)
                                {
                                    if (contador >= cont)
                                    {
                                        if (item[i].ToString() == "")
                                        {
                                            break;
                                        }

                                        valorActual = i > 0 ? item[i - 1].ToString() + item[i].ToString() : item[i].ToString();

                                        if (filasRowSpan == 1)
                                        {
                                            valorAnterior = valorActual;
                                        }
                                        else if (valorAnterior != valorActual)
                                            break;

                                        filasRowSpan++;
                                    }

                                    contador++;
                                }
                            }

                            PdfPCell detalle = new PdfPCell(new Phrase(itemDetalle[i].ToString().Replace(";", "\n"), _FontColorDetalle));
                            detalle.BorderWidth = 0.1f;
                            detalle.BorderColor = _BorderColor;
                            detalle.HorizontalAlignment = Element.ALIGN_CENTER;
                            detalle.VerticalAlignment = Element.ALIGN_MIDDLE;
                            detalle.PaddingTop = 0.5f;
                            detalle.PaddingBottom = 2f;
                            detalle.PaddingLeft = 2f;
                            detalle.Rowspan = filasRowSpan - 1;

                            tblDetalle.Table.AddCell(detalle);
                        }
                    }
                }

                cont++;
            }

            tblPrincipal.AddCell(tblCabecera);
            tblPrincipal.AddCell(tblDetalle);
            doc.Add(tblPrincipal);
            doc.Close();
            writer.Close();

            return rutaCompleta;
        }

        private List<SelectListItem> cabecera(DataTable data)
        {
            List<SelectListItem> listaCabecera = new List<SelectListItem>();
            SelectListItem cabacera = new SelectListItem();

            foreach (DataColumn itemCabecera in data.Columns)
            {
                string[] tituloSplit = itemCabecera.ColumnName.Split('-');
                string tituloPrincipal = tituloSplit.Count() > 1 ? tituloSplit[1].ToString() : tituloSplit[0].ToString();
                string tituloSecundario = tituloSplit.Count() > 1 ? tituloSplit[0].ToString() : "";
                cabacera = new SelectListItem();
                cabacera.Text = tituloPrincipal;
                cabacera.Value = tituloSecundario;

                listaCabecera.Add(cabacera);
            }

            return listaCabecera;
        }
        #endregion
        
    }
}
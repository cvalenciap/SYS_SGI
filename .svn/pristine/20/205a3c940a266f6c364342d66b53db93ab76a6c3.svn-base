using SYS_SGI.Application.Implementation.MOV;
using SYS_SGI.Domain.Implementation.Common.Base;
using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.MOV;
using SYS_SGI.Domain.Implementation.LogicalEntities.PAR;
using SYS_SGI.Domain.Implementation.LogicalEntities.MAN;
using SYS_SGI.Application.Implementation.PAR;
using SYS_SGI.Domain.Implementation.LogicalEntities.REP;
using SYS_SGI.Application.Implementation.REP;
using SYS_SGI.Application.Implementation.MAN;
using SYS_SGI.Presentation.Models.View;
using SYS_SGI.Presentation.Utilities;
using GMD.CustomHtmlHelpers.HtmlGenericGrid;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Linq;
using System;
using System.Data;
using System.Web.Configuration;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System.IO;

namespace SYS_SGI.Presentation.Controllers.MOV
{
    [AppPresentationError]
    public class PlanOperativoEjecucionController : BaseController
    {
        private readonly IndicadorDetalleAppService _IndicadorDetalleAppService = new IndicadorDetalleAppService();
        private readonly AlineamientoConfiguracionAppService _AlineamientoConfiguracionAppService = new AlineamientoConfiguracionAppService();
        private readonly IndicadorAppService _IndicadorAppService = new IndicadorAppService();
        private readonly ParametroDetalleAppService _ParametroDetalleAppService = new ParametroDetalleAppService();
        private readonly ReporteAppService _ReporteAppService = new ReporteAppService();
        private readonly AlineamientoEstrategicoAppService _AlineamientoEstrategicoAppService = new AlineamientoEstrategicoAppService();
        private readonly VariableAppService _VariableAppService = new VariableAppService();

        private static AppResponse appResponse = new AppResponse();
        private static VMIndicadorDetalle vmIndicadorDetalle = new VMIndicadorDetalle();
        private IndicadorDetalleLogic IndicadorDetalle = new IndicadorDetalleLogic();

        public ActionResult Index()
        {
            List<ParametroDetalleLogic> listaGuiaEmpresarial = new List<ParametroDetalleLogic>();
            listaGuiaEmpresarial.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.TipoGuiaEmpresarial).ToList().Where(x => x.Estado && (x.NombreCorto == "Anexo N°1" || x.NombreCorto == "Anexo N°3")));
            listaGuiaEmpresarial.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Seleccione });
            ViewBag.listaGuiaEmpresarial = listaGuiaEmpresarial;

            List<IndicadorLogic> listaIndicador = new List<IndicadorLogic>();
            listaIndicador.AddRange(_IndicadorAppService.Listar().ToList().Where(x => x.Estado));
            listaIndicador.Insert(0, new IndicadorLogic { CodigoIndicador = 0, Nombre = Enums.ComboDefault.Todos });
            ViewBag.listaIndicador = listaIndicador;

            return PartialView();
        }

        [HttpPost]
        public ActionResult Mantenimiento(int codigoAlineamientoEstrategico = 0, string fechaPeriodo = "")
        {
            vmIndicadorDetalle.IndicadorDetalle = new IndicadorDetalleLogic();
            vmIndicadorDetalle.IndicadorDetalle = _IndicadorDetalleAppService.Listar(codigoAlineamientoEstrategico, Enums.TipoRegistro.Ejecucion, Enums.TipoPlan.PlanOperativo, fechaPeriodo).FirstOrDefault();//.Where(x=>x.CodigoAlineamientoEstrategico==codigoAlineamientoEstrategico && x.FechaPeriodoDesc==fecha).FirstOrDefault();
            
            string pattern = Regex.Escape("[") + "(.*?)]";
            MatchCollection matches = Regex.Matches(vmIndicadorDetalle.IndicadorDetalle.Formula, pattern);
            List<string> listaVariables = new List<string>();
            vmIndicadorDetalle.IndicadorDetalle.listaVariableFormula = new List<VariableFormulaLogic>();
            foreach (var item in matches)
            {
                string nombreVariable = item.ToString().Replace("[", "").Replace("]", "");
                float codigoVariable = _VariableAppService.ObtenerByNombre(nombreVariable).CodigoVariable;
                
                vmIndicadorDetalle.IndicadorDetalle.listaVariableFormula.Add(new VariableFormulaLogic
                {
                    CodigoVariable = codigoVariable,
                    NombreVariable = nombreVariable,
                });
                listaVariables.Add(item.ToString().Replace("[", "").Replace("]", ""));
            }
            vmIndicadorDetalle.listaVariables = listaVariables;

            return PartialView(vmIndicadorDetalle);
        }

        [HttpPost]
        public JsonResult Guardar(VMIndicadorDetalle formMantenimiento)
        {
            if (base.ModelState.IsValid)
            {
                formMantenimiento.IndicadorDetalle.UsuarioCreacion = formMantenimiento.IndicadorDetalle.UsuarioModificacion = Login.Obtener.Usuario.Login();
                formMantenimiento.IndicadorDetalle.CodigoTipoRegistro = Enums.TipoRegistro.Ejecucion;
                formMantenimiento.IndicadorDetalle.CodigoTipoPlan = Enums.TipoPlan.PlanOperativo;

                IndicadorLogic indicador = _IndicadorAppService.Obtener(formMantenimiento.IndicadorDetalle.CodigoIndicador);

                //if (indicador.CodigoPeriodicidadValor == 0)
                //{
                //    formMantenimiento.IndicadorDetalle.FechaPeriodo = Convert.ToDateTime(formMantenimiento.IndicadorDetalle.Anio);
                //}
                //else if (indicador.CodigoPeriodicidadValor == 99)
                //{
                //    ParametroDetalleLogic periodicidad = _ParametroDetalleAppService.Obtener(Enums.Parametro.TipoPeriodicidad, indicador.CodigoPeriodicidad.ToString());
                //    formMantenimiento.IndicadorDetalle.FechaPeriodo = Convert.ToDateTime(periodicidad.Valor + formMantenimiento.IndicadorDetalle.Anio);
                //}
                //else
                //{
                //    ParametroDetalleLogic periodicidad = _ParametroDetalleAppService.Obtener(indicador.CodigoPeriodicidadValor, formMantenimiento.IndicadorDetalle.CodigoTipoValor.ToString());
                //    formMantenimiento.IndicadorDetalle.FechaPeriodo = Convert.ToDateTime(periodicidad.Valor + formMantenimiento.IndicadorDetalle.Anio);
                //}
                List<VariableFormulaLogic> listaVariableFormula = new List<VariableFormulaLogic>();

                if (formMantenimiento.IndicadorDetalle.CodigoIndicadorDetalle == 0)
                    appResponse = _IndicadorDetalleAppService.MantenimientoNuevo(new List<IndicadorDetalleLogic> { formMantenimiento.IndicadorDetalle }, listaVariableFormula);
                else appResponse = _IndicadorDetalleAppService.MantenimientoEditar(new List<IndicadorDetalleLogic> { formMantenimiento.IndicadorDetalle });
            }
            else
            {
                appResponse = GetModelStateErrors();
            }
            return Json(appResponse);
        }

        public JsonResult ActualizarValor(int codigoIndicadorDetalle, int codigoAlineamientoEstrategico, decimal valor, int codigoIndicador, int codigoTipoValor, int codigoTipoRegistro, int anio, string comentario, int tipoValor)
        {
            IndicadorDetalle = new IndicadorDetalleLogic();
            if (codigoIndicadorDetalle > 0)
            {
                IndicadorDetalle = _IndicadorDetalleAppService.Obtener(codigoIndicadorDetalle);
                IndicadorDetalle.CodigoIndicadorDetalle = codigoIndicadorDetalle;
            }
            else
            {
                IndicadorDetalle.CodigoIndicador = codigoIndicador;
                IndicadorDetalle.CodigoTipoValor = codigoTipoValor;
                IndicadorDetalle.CodigoTipoRegistro = codigoTipoRegistro;
                IndicadorDetalle.CodigoAlineamientoEstrategico = codigoAlineamientoEstrategico;

                //IndicadorLogic indicador = _IndicadorAppService.Obtener(IndicadorDetalle.CodigoIndicador);

                //if (indicador.CodigoPeriodicidadValor == 0)
                //{
                //    IndicadorDetalle.FechaPeriodo = Convert.ToDateTime(anio);
                //}
                //else if (indicador.CodigoPeriodicidadValor == 99)
                //{
                //    ParametroDetalleLogic periodicidad = _ParametroDetalleAppService.Obtener(Enums.Parametro.TipoPeriodicidad, indicador.CodigoPeriodicidad.ToString());
                //    IndicadorDetalle.FechaPeriodo = Convert.ToDateTime(periodicidad.Valor + anio);
                //}
                //else
                //{
                //    ParametroDetalleLogic periodicidad = _ParametroDetalleAppService.Obtener(indicador.CodigoPeriodicidadValor, IndicadorDetalle.CodigoTipoValor.ToString());
                //    IndicadorDetalle.FechaPeriodo = Convert.ToDateTime(periodicidad.Valor + anio);
                //}
                
                ParametroDetalleLogic periodicidad = _ParametroDetalleAppService.Obtener(Enums.Parametro.Trimestre, IndicadorDetalle.CodigoTipoValor.ToString());
                IndicadorDetalle.FechaPeriodo = Convert.ToDateTime(periodicidad.Valor + anio);
            }
            if (tipoValor == 1)
            {
                IndicadorDetalle.Valor = valor;
                IndicadorDetalle.Comentario = comentario;
            }
            else if (tipoValor == 2)
            {
                IndicadorDetalle.ValorAl = valor;
                IndicadorDetalle.ComentarioAl = comentario;
            }
            IndicadorDetalle.Estado = true;
            IndicadorDetalle.UsuarioCreacion = IndicadorDetalle.UsuarioModificacion = Login.Obtener.Usuario.Login();
            IndicadorDetalle.CodigoTipoPlan = Enums.TipoPlan.PlanOperativo;

            List<VariableFormulaLogic> listaVariableFormula = new List<VariableFormulaLogic>();

            if (codigoIndicadorDetalle > 0)
            {
                IndicadorDetalleHistoricoLogic objIndicadorDetalleHistorico = new IndicadorDetalleHistoricoLogic
                {
                    CodigoIndicadorDetalleHistorico = 0,
                    CodigoIndicadorDetalle = IndicadorDetalle.CodigoIndicadorDetalle,
                    Formula = IndicadorDetalle.Formula,
                    Valor = IndicadorDetalle.Valor,
                    ValorAl = IndicadorDetalle.ValorAl,
                    Comentario = IndicadorDetalle.Comentario,
                    ComentarioAl = IndicadorDetalle.ComentarioAl,
                    FechaPeriodo = IndicadorDetalle.FechaPeriodo,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = Login.Obtener.Usuario.Login(),
                    terminalCreacion = Login.Obtener.Usuario.Login()
                };

                appResponse = _IndicadorDetalleAppService.MantenimientoEliminar(new List<IndicadorDetalleLogic> { IndicadorDetalle });
                if(appResponse.Code =="S")
                    appResponse = _IndicadorDetalleAppService.MantenimientoNuevo(new List<IndicadorDetalleLogic> { IndicadorDetalle }, listaVariableFormula);
                if(appResponse.Code =="S")
                    appResponse = _IndicadorDetalleAppService.MantenimientoHistoricoNuevo(new List<IndicadorDetalleHistoricoLogic> { objIndicadorDetalleHistorico }, new List<VariableFormulaHistoricoLogic>());
            }
            else
                appResponse = _IndicadorDetalleAppService.MantenimientoNuevo(new List<IndicadorDetalleLogic> { IndicadorDetalle }, listaVariableFormula);

            return Json(appResponse);
        }


        public JsonResult Desactivar(int CodigoIndicadorDetalle)
        {
            IndicadorDetalle.CodigoIndicadorDetalle = CodigoIndicadorDetalle;
            IndicadorDetalle.UsuarioModificacion = IndicadorDetalle.UsuarioModificacion = Login.Obtener.Usuario.Login();
            IndicadorDetalle.FechaPeriodo = DateTime.Now;
            appResponse = _IndicadorDetalleAppService.MantenimientoEliminar(new List<IndicadorDetalleLogic> { IndicadorDetalle });
            return Json(appResponse);
        }

        public JsonResult Activar(int CodigoIndicadorDetalle)
        {
            IndicadorDetalle.CodigoIndicadorDetalle = CodigoIndicadorDetalle;
            IndicadorDetalle.UsuarioModificacion = IndicadorDetalle.UsuarioModificacion = Login.Obtener.Usuario.Login();
            IndicadorDetalle.FechaPeriodo = DateTime.Now;
            appResponse = _IndicadorDetalleAppService.MantenimientoActivar(new List<IndicadorDetalleLogic> { IndicadorDetalle });
            return Json(appResponse);
        }

        public ActionResult Paginacion(string arg, string codigoGuiaEmpresarial)
        {
            int codigoGuia = 0;
            bool resultado = int.TryParse(codigoGuiaEmpresarial, out codigoGuia);
            AlineamientoConfiguracionLogic alineamientoConfiguracion = new AlineamientoConfiguracionLogic();
            List<IndicadorDetalleLogic> data = new List<IndicadorDetalleLogic>();

            if (codigoGuia > 0)
            {
                alineamientoConfiguracion = _AlineamientoConfiguracionAppService.Listar().Where(x => x.CodigoTipoGuiaEmpresarial == codigoGuia && x.Estado).FirstOrDefault();
            }

            var modelData = JsonConvert.DeserializeObject<GMDGridModel<IndicadorDetalleLogic>>(arg);
            var paginateParams = new PaginateParams
            {
                IsPaginate = Convert.ToBoolean(WebConfigurationManager.AppSettings["IsPaginate"]),
                PageIndex = modelData.CurrentPageIndex,
                RowsPerPage = modelData.RowsPerPage,
                SortColumn = modelData.OrderBy,
                SortDirection = modelData.DirectionOrder
            };
            if (modelData.Filters != null)
                paginateParams.Filters = Funciones.Conversion.ListaToDatatable<GMDFilter>(modelData.Filters.ToList());

            if (modelData.isFirstLoad)
            {
                data = _IndicadorDetalleAppService.Paginacion(paginateParams, codigoGuia, Enums.TipoRegistro.Ejecucion, Enums.TipoPlan.PlanOperativo);
                if (data.Count > 0)
                    data[0].alineamientoConfiguracion = alineamientoConfiguracion;
            }
            modelData.Data = data;
            modelData.TotalRows = paginateParams.TotalRows;
            return PartialView(modelData);
        }

        public ActionResult CargarDatosIndicador(long CodigoIndicador)
        {
            IndicadorLogic indicador = _IndicadorAppService.Obtener(CodigoIndicador);
            string pattern = Regex.Escape("[") + "(.*?)]";
            MatchCollection matches = Regex.Matches(indicador.Formula, pattern);
            //foreach (Match match in matches)
            //    Console.WriteLine("{0}: {1}", ++commentNumber, match.Groups[1].Value);

            //Console.WriteLine("   {0}: {1}", ++commentNumber, match.Value);
            return Json(indicador, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerCabecera(long CodigoTipoGuiaEmpresarial)
        {
            AlineamientoConfiguracionLogic alineamientoConfiguracion = new AlineamientoConfiguracionLogic();

            if (CodigoTipoGuiaEmpresarial > 0)
            {
                alineamientoConfiguracion = _AlineamientoConfiguracionAppService.Listar().Where(x => x.CodigoTipoGuiaEmpresarial == CodigoTipoGuiaEmpresarial && x.Estado).FirstOrDefault();
            }

            return Json(alineamientoConfiguracion, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult CargarCombos(long CodigoParametro, bool Indicador, int Index, int CodigoTipoGuiaEmpresarial)
        //{
        //    List<ParametroDetalleLogic> lista = new List<ParametroDetalleLogic>();
        //    List<AlineamientoEstrategicoLogic> listaAlineamiento = new List<AlineamientoEstrategicoLogic>();
        //    listaAlineamiento = _AlineamientoEstrategicoAppService.Listar().Where(x => x.CodigoTipoGuiaEmpresarial == CodigoTipoGuiaEmpresarial && x.Estado).ToList();

        //    switch (Index)
        //    {
        //        case 1:
        //            lista = listaAlineamiento.Select(item => new ParametroDetalleLogic
        //            {
        //                CodigoElemento = item.CodigoAlineamiento1.ToString(),
        //                Nombre = item.Alineamiento1
        //            }).OrderBy(x => x.Nombre).DistinctBy(x => x.Nombre).ToList();
        //            break;
        //        case 2:
        //            listaAlineamiento = listaAlineamiento.Where(x => x.CodigoAlineamiento1 == CodigoParametro).ToList();
        //            lista = listaAlineamiento.Select(item => new ParametroDetalleLogic
        //            {
        //                CodigoElemento = Indicador ? item.CodigoIndicador.ToString() : item.CodigoAlineamiento2.ToString(),
        //                Nombre = Indicador ? item.Indicador.ToString() : item.Alineamiento2.ToString(),
        //                Valor = Indicador ? item.CodigoAlineamientoEstrategico.ToString() : "0",
        //            }).OrderBy(x => x.Nombre).DistinctBy(x => x.Nombre).ToList();
        //            break;
        //        case 3:
        //            listaAlineamiento = listaAlineamiento.Where(x => x.CodigoAlineamiento2 == CodigoParametro).ToList();
        //            lista = listaAlineamiento.Select(item => new ParametroDetalleLogic
        //            {
        //                CodigoElemento = Indicador ? item.CodigoIndicador.ToString() : item.CodigoAlineamiento3.ToString(),
        //                Nombre = Indicador ? item.Indicador.ToString() : item.Alineamiento3.ToString(),
        //                Valor = Indicador ? item.CodigoAlineamientoEstrategico.ToString() : "0",
        //            }).OrderBy(x => x.Nombre).DistinctBy(x => x.Nombre).ToList();
        //            break;
        //        case 4:
        //            listaAlineamiento = listaAlineamiento.Where(x => x.CodigoAlineamiento3 == CodigoParametro).ToList();
        //            lista = listaAlineamiento.Select(item => new ParametroDetalleLogic
        //            {
        //                CodigoElemento = Indicador ? item.CodigoIndicador.ToString() : item.CodigoAlineamiento4.ToString(),
        //                Nombre = Indicador ? item.Indicador.ToString() : item.Alineamiento4.ToString(),
        //                Valor = Indicador ? item.CodigoAlineamientoEstrategico.ToString() : "0",
        //            }).OrderBy(x => x.Nombre).DistinctBy(x => x.Nombre).ToList();
        //            break;
        //        case 5:
        //            listaAlineamiento = listaAlineamiento.Where(x => x.CodigoAlineamiento4 == CodigoParametro).ToList();
        //            lista = listaAlineamiento.Select(item => new ParametroDetalleLogic
        //            {
        //                CodigoElemento = Indicador ? item.CodigoIndicador.ToString() : item.CodigoAlineamiento5.ToString(),
        //                Nombre = Indicador ? item.Indicador.ToString() : item.Alineamiento5.ToString(),
        //                Valor = Indicador ? item.CodigoAlineamientoEstrategico.ToString() : "0",
        //            }).OrderBy(x => x.Nombre).DistinctBy(x => x.Nombre).ToList();
        //            break;
        //        case 6:
        //            listaAlineamiento = listaAlineamiento.Where(x => x.CodigoAlineamiento5 == CodigoParametro).ToList();
        //            lista = listaAlineamiento.Select(item => new ParametroDetalleLogic
        //            {
        //                CodigoElemento = Indicador ? item.CodigoIndicador.ToString() : item.CodigoAlineamiento6.ToString(),
        //                Nombre = Indicador ? item.Indicador.ToString() : item.Alineamiento6.ToString(),
        //                Valor = Indicador ? item.CodigoAlineamientoEstrategico.ToString() : "0",
        //            }).OrderBy(x => x.Nombre).DistinctBy(x => x.Nombre).ToList();
        //            break;
        //        case 7:
        //            listaAlineamiento = listaAlineamiento.Where(x => x.CodigoAlineamiento6 == CodigoParametro).ToList();
        //            lista = listaAlineamiento.Select(item => new ParametroDetalleLogic
        //            {
        //                CodigoElemento = Indicador ? item.CodigoIndicador.ToString() : item.CodigoAlineamiento7.ToString(),
        //                Nombre = Indicador ? item.Indicador.ToString() : item.Alineamiento7.ToString(),
        //                Valor = Indicador ? item.CodigoAlineamientoEstrategico.ToString() : "0",
        //            }).OrderBy(x => x.Nombre).DistinctBy(x => x.Nombre).ToList();
        //            break;
        //        case 8:
        //            listaAlineamiento = listaAlineamiento.Where(x => x.CodigoAlineamiento7 == CodigoParametro).ToList();
        //            lista = listaAlineamiento.Select(item => new ParametroDetalleLogic
        //            {
        //                CodigoElemento = Indicador ? item.CodigoIndicador.ToString() : item.CodigoAlineamiento8.ToString(),
        //                Nombre = Indicador ? item.Indicador.ToString() : item.Alineamiento8.ToString(),
        //                Valor = Indicador ? item.CodigoAlineamientoEstrategico.ToString() : "0",
        //            }).OrderBy(x => x.Nombre).DistinctBy(x => x.Nombre).ToList();
        //            break;
        //        case 9:
        //            listaAlineamiento = listaAlineamiento.Where(x => x.CodigoAlineamiento8 == CodigoParametro).ToList();
        //            lista = listaAlineamiento.Select(item => new ParametroDetalleLogic
        //            {
        //                CodigoElemento = Indicador ? item.CodigoIndicador.ToString() : item.CodigoAlineamiento9.ToString(),
        //                Nombre = Indicador ? item.Indicador.ToString() : item.Alineamiento9.ToString(),
        //                Valor = Indicador ? item.CodigoAlineamientoEstrategico.ToString() : "0",
        //            }).OrderBy(x => x.Nombre).DistinctBy(x => x.Nombre).ToList();
        //            break;
        //    }

        //    return Json(lista, JsonRequestBehavior.AllowGet);
        //}

        public FileResult DescargarReporte(int codigoTipoGuiaEmpresarial, int codigoAlineamientoEstrategico, string indicador)
        {
            DataTable dataLineamiento = ObtenerLineamiento(codigoTipoGuiaEmpresarial, codigoAlineamientoEstrategico);
            DataTable dataValores = ObtenerValores(dataLineamiento, codigoAlineamientoEstrategico);

            int cantLineamientos = cantidadLineamientos(codigoTipoGuiaEmpresarial);
            string rutaCompleta = CrearReporte(dataValores, indicador, cantLineamientos);
            string ruta = Path.GetDirectoryName(rutaCompleta);
            string tipoArchivo = "application/pdf";
            string nombreArchivo = Path.GetFileName(rutaCompleta);

            return File(rutaCompleta, tipoArchivo, nombreArchivo);
        }

        public DataTable ObtenerLineamiento(int codigoTipoGuiaEmpresarial, int codigoAlineamientoEstrategico)
        {
            AlineamientoConfiguracionLogic alineamientoConfiguracion = new AlineamientoConfiguracionLogic();
            List<IndicadorDetalleLogic> indicadorDetalle = new List<IndicadorDetalleLogic>();

            alineamientoConfiguracion = _AlineamientoConfiguracionAppService.Listar().Where(x => x.Estado && x.CodigoTipoGuiaEmpresarial == codigoTipoGuiaEmpresarial).FirstOrDefault();
            indicadorDetalle = _IndicadorDetalleAppService.Listar(codigoAlineamientoEstrategico, Enums.TipoRegistro.Ejecucion, Enums.TipoPlan.PlanOperativo, "").Where(x => x.Estado && x.CodigoIndicadorDetalle > 0).ToList();

            DataTable tabla = new DataTable();
            int cont = 0;
            bool removerColumnas = false;

            string cabeceraCadena = "",
                unidadMedida = "UNIDAD DE MEDIDA",
                indicador = "INDICADOR",
                codigoIndicadorDetalle = "CODIGO_INDICADOR_DETALLE";

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
                tabla.Columns.Add(codigoIndicadorDetalle, typeof(int));

                removerColumnas = true;

                cont++;
            }

            foreach (IndicadorDetalleLogic item in indicadorDetalle)
            {
                DataRow row = tabla.NewRow();

                if (item.Alineamiento1 != null &&  item.Alineamiento1.Trim() != "" && item.Alineamiento1.Trim() != ":")
                {
                    row[0] = item.Alineamiento1;
                }

                if (item.Alineamiento2 != null && item.Alineamiento2.Trim() != "" && item.Alineamiento2.Trim() != ":")
                {
                    row[1] = item.Alineamiento2;
                }

                if (item.Alineamiento3 != null && item.Alineamiento3.Trim() != "" && item.Alineamiento3.Trim() != ":")
                {
                    row[2] = item.Alineamiento3;
                }

                if (item.Alineamiento4 != null && item.Alineamiento4.Trim() != "" && item.Alineamiento4.Trim() != ":")
                {
                    row[3] = item.Alineamiento4;
                }

                if (item.Alineamiento5 != null && item.Alineamiento5.Trim() != "" && item.Alineamiento5.Trim() != ":")
                {
                    row[4] = item.Alineamiento5;
                }

                if (item.Alineamiento6 != null && item.Alineamiento6.Trim() != "" && item.Alineamiento6.Trim() != ":")
                {
                    row[5] = item.Alineamiento6;
                }

                if (item.Alineamiento7 != null && item.Alineamiento7.Trim() != "" && item.Alineamiento7.Trim() != ":")
                {
                    row[6] = item.Alineamiento7;
                }

                if (item.Alineamiento8 != null && item.Alineamiento8.Trim() != "" && item.Alineamiento8.Trim() != ":")
                {
                    row[7] = item.Alineamiento8;
                }

                if (item.Alineamiento9 != null && item.Alineamiento9.Trim() != "" && item.Alineamiento9.Trim() != ":")
                {
                    row[8] = item.Alineamiento9;
                }

                if (item.Alineamiento10 != null && item.Alineamiento10.Trim() != "" && item.Alineamiento10.Trim() != ":")
                {
                    row[9] = item.Alineamiento10;
                }

                if (item.CodigoIndicador > 0)
                {
                    row[indicador] = item.Indicador;
                    row[unidadMedida] = item.UnidadMedida;
                    row[codigoIndicadorDetalle] = item.CodigoIndicadorDetalle;
                }

                tabla.Rows.Add(row);
            }

            foreach (DataRow rows in tabla.Rows)
            {
                for (int i = 0; i < tabla.Columns.Count; i++)
                {
                    string contenido = rows[i].ToString().Trim();

                    if (contenido == "")
                        rows[i] = "-";
                }
            }

            return tabla;
        }

        public DataTable ObtenerValores(DataTable dataLineamiento, int codigoAlineamientoEstrategico)
        {
            List<IndicadorDetalleLogic> listaIndicadorDetalleMeta = new List<IndicadorDetalleLogic>();
            List<IndicadorDetalleLogic> listaIndicadorDetalleEjecucion = new List<IndicadorDetalleLogic>();

            if (codigoAlineamientoEstrategico > 0)
            {
                listaIndicadorDetalleMeta = _IndicadorDetalleAppService.Listar(codigoAlineamientoEstrategico, Enums.TipoRegistro.Meta, Enums.TipoPlan.PlanOperativo, "").Where(x => x.Estado).ToList();
                listaIndicadorDetalleEjecucion = _IndicadorDetalleAppService.Listar(codigoAlineamientoEstrategico, Enums.TipoRegistro.Ejecucion, Enums.TipoPlan.PlanOperativo, "").Where(x => x.Estado).ToList();
            }

            int cont = 0;
            bool removerColumnas = true;

            string codigoIndicadorDetalle = "CODIGO_INDICADOR_DETALLE", cumplimiento = "Cumplimiento %";

            List<SelectListItem> listaAniosMeta = listaIndicadorDetalleMeta.Select(item => new SelectListItem
            {
                Text = item.Anio + "-Metas"
            }).DistinctBy(x => x.Text).ToList();

            List<SelectListItem> listaAniosEjecucion = listaIndicadorDetalleEjecucion.Select(item => new SelectListItem
            {
                Text = item.Anio + "-Ejecución"
            }).DistinctBy(x => x.Text).ToList();

            foreach (SelectListItem item in listaAniosMeta)
            {
                dataLineamiento.Columns.Add(item.Text, typeof(string));
            }

            foreach (SelectListItem item in listaAniosEjecucion)
            {
                dataLineamiento.Columns.Add(item.Text, typeof(string));
            }

            dataLineamiento.Columns.Add(cumplimiento, typeof(string));
                        
            foreach (DataRow item in dataLineamiento.Rows)
            {
                foreach (IndicadorDetalleLogic item1 in listaIndicadorDetalleMeta)
                {
                    item[item1.Anio + "-Metas"] = item1.Valor.ToString() == "" ? "-" : item1.Valor.ToString().Substring(0, item1.Valor.ToString().Length - 2);
                }

                cont = 0;
                foreach (IndicadorDetalleLogic item2 in listaIndicadorDetalleEjecucion)
                {
                    item[item2.Anio + "-Ejecución"] = item2.Valor.ToString() == "" ? "-" : item2.Valor.ToString().Substring(0, item2.Valor.ToString().Length - 2);
                    
                    if (item2.Valor > 0)
                    {
                        item[cumplimiento] = listaIndicadorDetalleMeta[cont].Valor == 0 ? 0 : Math.Round((item2.Valor/listaIndicadorDetalleMeta[cont].Valor)*100,2);
                    }

                    cont++;
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
                dataLineamiento.Columns.Remove(codigoIndicadorDetalle);
            }

            return dataLineamiento;
        }

        private string CrearReporte(DataTable data, string tituloReporte, int cantidadLineamientos)
        {
            string rutaDescarga = WebConfigurationManager.AppSettings["RutaDescargaArchivos"].ToString();
            string ImagenReporte = WebConfigurationManager.AppSettings["ImagenReporte"].ToString();

            string rutaCompleta = rutaDescarga + tituloReporte + "_" + DateTime.Now.ToString("yyyyMMdd-HHmmssfff") + ".pdf";

            int cantidadColumnas = data.Columns.Count;

            List<SelectListItem> cabeceraReporte = cabecera(data);

            Document doc = new Document(PageSize.A4.Rotate());

            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(rutaCompleta, FileMode.Create));
            doc.AddTitle(tituloReporte);
            doc.AddCreator("GMD");
            doc.Open();
            writer.PageEvent = new PDFFooter();

            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(ImagenReporte);
            jpg.ScaleToFit(110f, 140f);

            doc.Add(jpg);

            //Definimos Colores
            BaseColor _BackgroundCabecera = WebColors.GetRGBColor("#fafad2");
            BaseColor _BorderColor = WebColors.GetRGBColor("#C0C0C0");
            Font _FontColorCabecera = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5.5f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Font _FontColorTituloPrincipal = FontFactory.GetFont("HELVETICA", 9.3f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Font _FontColorDetalle = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4.3f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            // Titulo del documento
            Paragraph titulo = new Paragraph();
            titulo.Add(new Phrase("\n" + tituloReporte + "\n", _FontColorTituloPrincipal));
            
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
                cabecera.Colspan = item.Value.ToString() != "" ? 5 : 1;

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
            int cantidadC;
            foreach (DataRow itemDetalle in data.Rows)
            {
                if (cont == 0)
                {
                    for (int i = 0; i < cantidadColumnas; i++)
                    {
                        filasRowSpan = 1;
                        string valorAnterior = "", valorActual = "";

                        if (i < cantidadLineamientos)
                        {
                            foreach (DataRow item in data.Rows)
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

                        PdfPCell detalle = new PdfPCell(new Phrase(itemDetalle[i].ToString(), _FontColorDetalle));
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
                        alineamientoAnterior = i > 0 ? data.Rows[cont - 1][i - 1].ToString() + ":" + data.Rows[cont - 1][i].ToString() : data.Rows[cont - 1][i].ToString();
                        alineamientoActual = i > 0 ? itemDetalle[i - 1].ToString() + ":" + itemDetalle[i].ToString() : itemDetalle[i].ToString();

                        filasRowSpan = 1;
                        string valorAnterior = "", valorActual = "";

                        if (itemDetalle[i].ToString() == "" || i >= cantidadLineamientos)
                        {
                            PdfPCell detalle = new PdfPCell(new Phrase(itemDetalle[i].ToString(), _FontColorDetalle));
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
                                foreach (DataRow item in data.Rows)
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

                            PdfPCell detalle = new PdfPCell(new Phrase(itemDetalle[i].ToString(), _FontColorDetalle));
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

        private int cantidadLineamientos(int codigoTipoGuiaEmpresarial)
        {
            AlineamientoConfiguracionLogic alineamientoConfiguracion = _AlineamientoConfiguracionAppService.Listar().Where(x => x.Estado && x.CodigoTipoGuiaEmpresarial == codigoTipoGuiaEmpresarial).FirstOrDefault();

            int cont = 0;

            if (alineamientoConfiguracion.CodigoTipoAlineamiento1 > 0)
            {
                cont++;
            }

            if (alineamientoConfiguracion.CodigoTipoAlineamiento2 > 0)
            {
                cont++;
            }

            if (alineamientoConfiguracion.CodigoTipoAlineamiento3 > 0)
            {
                cont++;
            }

            if (alineamientoConfiguracion.CodigoTipoAlineamiento4 > 0)
            {
                cont++;
            }

            if (alineamientoConfiguracion.CodigoTipoAlineamiento5 > 0)
            {
                cont++;
            }

            if (alineamientoConfiguracion.CodigoTipoAlineamiento6 > 0)
            {
                cont++;
            }

            if (alineamientoConfiguracion.CodigoTipoAlineamiento7 > 0)
            {
                cont++;
            }

            if (alineamientoConfiguracion.CodigoTipoAlineamiento8 > 0)
            {
                cont++;
            }

            if (alineamientoConfiguracion.CodigoTipoAlineamiento9 > 0)
            {
                cont++;
            }

            if (alineamientoConfiguracion.CodigoTipoAlineamiento10 > 0)
            {
                cont++;
            }

            return cont;
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
    }
}

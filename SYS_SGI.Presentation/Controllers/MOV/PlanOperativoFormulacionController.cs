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

namespace SYS_SGI.Presentation.Controllers.MOV
{
    [AppPresentationError]
    public class PlanOperativoFormulacionController : BaseController
    {
        private readonly IndicadorDetalleAppService _IndicadorDetalleAppService = new IndicadorDetalleAppService();
        private readonly AlineamientoConfiguracionAppService _AlineamientoConfiguracionAppService = new AlineamientoConfiguracionAppService();
        private readonly IndicadorAppService _IndicadorAppService = new IndicadorAppService();
        private readonly ParametroDetalleAppService _ParametroDetalleAppService = new ParametroDetalleAppService();
        private readonly ReporteAppService _ReporteAppService = new ReporteAppService();
        private readonly AlineamientoEstrategicoAppService _AlineamientoEstrategicoAppService = new AlineamientoEstrategicoAppService();

        private static AppResponse appResponse = new AppResponse();
        private static VMIndicadorDetalle vmIndicadorDetalle = new VMIndicadorDetalle();
        private IndicadorDetalleLogic IndicadorDetalle = new IndicadorDetalleLogic();
        List<IndicadorDetalleLogic> listaIndicadorDetalleSession = new List<IndicadorDetalleLogic>();
        private string SessionIndicadorDetalle = "SessionIndicadorDetalle";
        private string SessionContadorIndicadorDet = "SessionContadorIndicadorDet";
        private string SessionListaAlineamiento = "SessionListaAlineamiento";

        int contador = 0;

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
        public ActionResult Mantenimiento(long codigo = 0)
        {
            if (Session[SessionIndicadorDetalle] != null)
                Session[SessionIndicadorDetalle] = null;

            if (Session[SessionContadorIndicadorDet] != null)
                Session[SessionContadorIndicadorDet] = null;

            List<ParametroDetalleLogic> listaGuiaEmpresarial = new List<ParametroDetalleLogic>();

            if (codigo > 0)
            {
                IndicadorDetalle = _IndicadorDetalleAppService.Obtener(codigo);
                IndicadorDetalle.InfoMovs = new InfoMovs { ACCION = Enums.Action.Edit };
            }
            else
            {
                IndicadorDetalle.Estado = true;
            }

            vmIndicadorDetalle.IndicadorDetalle = IndicadorDetalle;

            List<ParametroDetalleLogic> listaTipoValor = new List<ParametroDetalleLogic>();
            listaTipoValor.AddRange(_ParametroDetalleAppService.Listar(IndicadorDetalle.CodigoPeriodicidadValor).Where(x => x.Estado).ToList());
            listaTipoValor.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Seleccione });
            ViewBag.listaTipoValor = listaTipoValor;

            List<ParametroDetalleLogic> listaTipoRegistro = new List<ParametroDetalleLogic>();
            listaTipoRegistro.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.TipoRegistro).Where(x => x.Estado).ToList());
            listaTipoRegistro.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Seleccione });
            ViewBag.listaTipoRegistro = listaTipoRegistro;

            listaGuiaEmpresarial.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.TipoGuiaEmpresarial).ToList().Where(x => x.Estado));
            listaGuiaEmpresarial.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Seleccione });
            ViewBag.listaGuiaEmpresarial = listaGuiaEmpresarial;

            return PartialView(vmIndicadorDetalle);
        }

        //[HttpPost]
        //public JsonResult Guardar(VMIndicadorDetalle formMantenimiento)
        //{
        //    if (base.ModelState.IsValid)
        //    {
        //        if (Session[SessionIndicadorDetalle] != null)
        //            listaIndicadorDetalleSession = (List<IndicadorDetalleLogic>)Session[SessionIndicadorDetalle];

        //        foreach (IndicadorDetalleLogic indicadorDetalle in listaIndicadorDetalleSession)
        //        {
        //            indicadorDetalle.InfoMovs = formMantenimiento.IndicadorDetalle.InfoMovs;
        //            indicadorDetalle.UsuarioCreacion = formMantenimiento.IndicadorDetalle.UsuarioModificacion = Login.Obtener.Usuario.Login();
        //            formMantenimiento.IndicadorDetalle = indicadorDetalle;

        //            formMantenimiento.IndicadorDetalle.UsuarioCreacion = formMantenimiento.IndicadorDetalle.UsuarioModificacion = Login.Obtener.Usuario.Login();
        //            formMantenimiento.IndicadorDetalle.CodigoTipoRegistro = Enums.TipoRegistro.Meta;

        //            IndicadorLogic indicador = _IndicadorAppService.Obtener(formMantenimiento.IndicadorDetalle.CodigoIndicador);

        //            if (indicador.CodigoPeriodicidadValor == 0)
        //            {
        //                formMantenimiento.IndicadorDetalle.FechaPeriodo = Convert.ToDateTime(formMantenimiento.IndicadorDetalle.Anio);
        //            }
        //            else if (indicador.CodigoPeriodicidadValor == 99)
        //            {
        //                ParametroDetalleLogic periodicidad = _ParametroDetalleAppService.Obtener(Enums.Parametro.TipoPeriodicidad, indicador.CodigoPeriodicidad.ToString());
        //                formMantenimiento.IndicadorDetalle.FechaPeriodo = Convert.ToDateTime(periodicidad.Valor + formMantenimiento.IndicadorDetalle.Anio);
        //            }
        //            else
        //            {
        //                ParametroDetalleLogic periodicidad = _ParametroDetalleAppService.Obtener(indicador.CodigoPeriodicidadValor, formMantenimiento.IndicadorDetalle.CodigoTipoValor.ToString());
        //                formMantenimiento.IndicadorDetalle.FechaPeriodo = Convert.ToDateTime(periodicidad.Valor + formMantenimiento.IndicadorDetalle.Anio);
        //            }

        //            if (formMantenimiento.IndicadorDetalle.InfoMovs.ACCION == Enums.Action.New)
        //                appResponse = _IndicadorDetalleAppService.MantenimientoNuevo(new List<IndicadorDetalleLogic> { formMantenimiento.IndicadorDetalle });
        //            else appResponse = _IndicadorDetalleAppService.MantenimientoEditar(new List<IndicadorDetalleLogic> { formMantenimiento.IndicadorDetalle });
        //        }
        //    }
        //    else
        //    {
        //        appResponse = GetModelStateErrors();
        //    }
        //    return Json(appResponse);
        //}

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

            if (codigoIndicadorDetalle > 0)
            {
                IndicadorDetalleHistoricoLogic objIndicadorDetalleHistorico = new IndicadorDetalleHistoricoLogic {
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
                if (appResponse.Code == "S")
                appResponse = _IndicadorDetalleAppService.MantenimientoNuevo(new List<IndicadorDetalleLogic> { IndicadorDetalle }, new List<VariableFormulaLogic>());
                if (appResponse.Code == "S")
                appResponse = _IndicadorDetalleAppService.MantenimientoHistoricoNuevo(new List<IndicadorDetalleHistoricoLogic> { objIndicadorDetalleHistorico }, new List<VariableFormulaHistoricoLogic>());
                
                //appResponse = _IndicadorDetalleAppService.MantenimientoEditar(new List<IndicadorDetalleLogic> { IndicadorDetalle });
            }
            else
                appResponse = _IndicadorDetalleAppService.MantenimientoNuevo(new List<IndicadorDetalleLogic> { IndicadorDetalle }, new List<VariableFormulaLogic>());
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
                data = _IndicadorDetalleAppService.Paginacion(paginateParams, codigoGuia, Enums.TipoRegistro.Meta, Enums.TipoPlan.PlanOperativo);
                if (data.Count > 0)
                    data[0].alineamientoConfiguracion = alineamientoConfiguracion;
            }

            modelData.Data = data;

            modelData.TotalRows = paginateParams.TotalRows;

            return PartialView(modelData);
        }

        public ActionResult PaginacionMantenimiento(string arg)
        {
            AlineamientoConfiguracionLogic alineamientoConfiguracion = new AlineamientoConfiguracionLogic();

            if (Session[SessionIndicadorDetalle] != null)
                listaIndicadorDetalleSession = (List<IndicadorDetalleLogic>)Session[SessionIndicadorDetalle];
            else
                Session[SessionIndicadorDetalle] = null;

            var modelData = JsonConvert.DeserializeObject<GMDGridModel<IndicadorDetalleLogic>>(arg);
            modelData.Data = new List<IndicadorDetalleLogic>();

            var paginateParams = new PaginateParams
            {
                IsPaginate = false,
                PageIndex = modelData.CurrentPageIndex,
                RowsPerPage = modelData.RowsPerPage,
                SortColumn = modelData.OrderBy,
                SortDirection = modelData.DirectionOrder
            };
            if (modelData.Filters != null)
                paginateParams.Filters = Funciones.Conversion.ListaToDatatable<GMDFilter>(modelData.Filters.ToList());

            if (modelData.isFirstLoad)
                modelData.Data = listaIndicadorDetalleSession;
            else
            {
                if (listaIndicadorDetalleSession.Count > 0)
                {
                    modelData.Data = listaIndicadorDetalleSession;
                }
            }

            if (modelData != null && listaIndicadorDetalleSession.Count > 0)
            {
                int codigoGuia = modelData.Data.Select(x => x.CodigoTipoGuiaEmpresarial).FirstOrDefault();
                alineamientoConfiguracion = _AlineamientoConfiguracionAppService.Listar().Where(x => x.CodigoTipoGuiaEmpresarial == codigoGuia && x.Estado).FirstOrDefault();
                listaIndicadorDetalleSession[0].alineamientoConfiguracion = alineamientoConfiguracion;
            }

            modelData.TotalRows = modelData.Data.ToList().Count;

            return PartialView(modelData);
        }

        public ActionResult CargarPeriodicidad(long CodigoIndicador, int CodigoIndicadorDetalle)
        {
            List<ParametroDetalleLogic> listaTipoValor = new List<ParametroDetalleLogic>();
            IndicadorLogic indicador = _IndicadorAppService.Obtener(CodigoIndicador);
            IndicadorDetalleLogic indicadorDetalle = new IndicadorDetalleLogic();

            if (CodigoIndicadorDetalle > 0)
                indicadorDetalle = _IndicadorDetalleAppService.Obtener(CodigoIndicadorDetalle);

            if (CodigoIndicador > 0 && indicador != null)
            {
                listaTipoValor = _ParametroDetalleAppService.Listar(indicador.CodigoPeriodicidadValor).Where(x => x.Estado).ToList();
                indicadorDetalle.listaTipoValor = listaTipoValor;
                indicadorDetalle.CodigoPeriodicidadValor = indicador.CodigoPeriodicidadValor;
                indicadorDetalle.Periodicidad = indicador.TipoPeriodicidad;
                indicadorDetalle.UnidadMedida = indicador.UnidadMedida;
            }

            return Json(indicadorDetalle, JsonRequestBehavior.AllowGet);
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

        public ActionResult CargarCombos(string CodigoParametro, bool Indicador, int Index, int CodigoTipoGuiaEmpresarial)
        {
            List<ParametroDetalleLogic> lista = new List<ParametroDetalleLogic>();
            List<AlineamientoEstrategicoLogic> listaAlineamiento = new List<AlineamientoEstrategicoLogic>();
            listaAlineamiento = _AlineamientoEstrategicoAppService.Listar().Where(x => x.CodigoTipoGuiaEmpresarial == CodigoTipoGuiaEmpresarial && x.Estado).ToList();

            switch (Index)
            {
                case 1:
                    lista = listaAlineamiento.Select(item => new ParametroDetalleLogic
                    {
                        CodigoElemento = item.CodigoAlineamiento1.ToString(),
                        Nombre = item.Alineamiento1
                    }).OrderBy(x => x.Nombre).DistinctBy(x => x.Nombre).ToList();
                    break;
                case 2:
                    listaAlineamiento = listaAlineamiento.Where(x => x.CodigoAlineamiento1 == CodigoParametro).ToList();
                    lista = listaAlineamiento.Select(item => new ParametroDetalleLogic
                    {
                        CodigoElemento = Indicador ? item.CodigoIndicador.ToString() : item.CodigoAlineamiento2.ToString(),
                        Nombre = Indicador ? item.Indicador.ToString() : item.Alineamiento2.ToString(),
                        Valor = Indicador ? item.CodigoAlineamientoEstrategico.ToString() : "0",
                    }).OrderBy(x => x.Nombre).DistinctBy(x => x.Nombre).ToList();
                    break;
                case 3:
                    listaAlineamiento = listaAlineamiento.Where(x => x.CodigoAlineamiento2 == CodigoParametro).ToList();
                    lista = listaAlineamiento.Select(item => new ParametroDetalleLogic
                    {
                        CodigoElemento = Indicador ? item.CodigoIndicador.ToString() : item.CodigoAlineamiento3.ToString(),
                        Nombre = Indicador ? item.Indicador.ToString() : item.Alineamiento3.ToString(),
                        Valor = Indicador ? item.CodigoAlineamientoEstrategico.ToString() : "0",
                    }).OrderBy(x => x.Nombre).DistinctBy(x => x.Nombre).ToList();
                    break;
                case 4:
                    listaAlineamiento = listaAlineamiento.Where(x => x.CodigoAlineamiento3 == CodigoParametro).ToList();
                    lista = listaAlineamiento.Select(item => new ParametroDetalleLogic
                    {
                        CodigoElemento = Indicador ? item.CodigoIndicador.ToString() : item.CodigoAlineamiento4.ToString(),
                        Nombre = Indicador ? item.Indicador.ToString() : item.Alineamiento4.ToString(),
                        Valor = Indicador ? item.CodigoAlineamientoEstrategico.ToString() : "0",
                    }).OrderBy(x => x.Nombre).DistinctBy(x => x.Nombre).ToList();
                    break;
                case 5:
                    listaAlineamiento = listaAlineamiento.Where(x => x.CodigoAlineamiento4 == CodigoParametro).ToList();
                    lista = listaAlineamiento.Select(item => new ParametroDetalleLogic
                    {
                        CodigoElemento = Indicador ? item.CodigoIndicador.ToString() : item.CodigoAlineamiento5.ToString(),
                        Nombre = Indicador ? item.Indicador.ToString() : item.Alineamiento5.ToString(),
                        Valor = Indicador ? item.CodigoAlineamientoEstrategico.ToString() : "0",
                    }).OrderBy(x => x.Nombre).DistinctBy(x => x.Nombre).ToList();
                    break;
                case 6:
                    listaAlineamiento = listaAlineamiento.Where(x => x.CodigoAlineamiento5 == CodigoParametro).ToList();
                    lista = listaAlineamiento.Select(item => new ParametroDetalleLogic
                    {
                        CodigoElemento = Indicador ? item.CodigoIndicador.ToString() : item.CodigoAlineamiento6.ToString(),
                        Nombre = Indicador ? item.Indicador.ToString() : item.Alineamiento6.ToString(),
                        Valor = Indicador ? item.CodigoAlineamientoEstrategico.ToString() : "0",
                    }).OrderBy(x => x.Nombre).DistinctBy(x => x.Nombre).ToList();
                    break;
                case 7:
                    listaAlineamiento = listaAlineamiento.Where(x => x.CodigoAlineamiento6 == CodigoParametro).ToList();
                    lista = listaAlineamiento.Select(item => new ParametroDetalleLogic
                    {
                        CodigoElemento = Indicador ? item.CodigoIndicador.ToString() : item.CodigoAlineamiento7.ToString(),
                        Nombre = Indicador ? item.Indicador.ToString() : item.Alineamiento7.ToString(),
                        Valor = Indicador ? item.CodigoAlineamientoEstrategico.ToString() : "0",
                    }).OrderBy(x => x.Nombre).DistinctBy(x => x.Nombre).ToList();
                    break;
                case 8:
                    listaAlineamiento = listaAlineamiento.Where(x => x.CodigoAlineamiento7 == CodigoParametro).ToList();
                    lista = listaAlineamiento.Select(item => new ParametroDetalleLogic
                    {
                        CodigoElemento = Indicador ? item.CodigoIndicador.ToString() : item.CodigoAlineamiento8.ToString(),
                        Nombre = Indicador ? item.Indicador.ToString() : item.Alineamiento8.ToString(),
                        Valor = Indicador ? item.CodigoAlineamientoEstrategico.ToString() : "0",
                    }).OrderBy(x => x.Nombre).DistinctBy(x => x.Nombre).ToList();
                    break;
                case 9:
                    listaAlineamiento = listaAlineamiento.Where(x => x.CodigoAlineamiento8 == CodigoParametro).ToList();
                    lista = listaAlineamiento.Select(item => new ParametroDetalleLogic
                    {
                        CodigoElemento = Indicador ? item.CodigoIndicador.ToString() : item.CodigoAlineamiento9.ToString(),
                        Nombre = Indicador ? item.Indicador.ToString() : item.Alineamiento9.ToString(),
                        Valor = Indicador ? item.CodigoAlineamientoEstrategico.ToString() : "0",
                    }).OrderBy(x => x.Nombre).DistinctBy(x => x.Nombre).ToList();
                    break;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Agregar(string data)
        {
            var lista = JsonConvert.DeserializeObject<List<IndicadorDetalleLogic>>(data);

            if (Session[SessionIndicadorDetalle] != null)
                listaIndicadorDetalleSession = (List<IndicadorDetalleLogic>)Session[SessionIndicadorDetalle];

            if (Session[SessionContadorIndicadorDet] != null)
                contador = (int)Session[SessionContadorIndicadorDet];

            lista[0].Item = contador + 1;
            listaIndicadorDetalleSession.AddRange(lista);

            Session[SessionIndicadorDetalle] = listaIndicadorDetalleSession;
            Session[SessionContadorIndicadorDet] = contador + 1;

            appResponse.SetSuccess(string.Empty);
            appResponse.SetError(string.Empty);
            return Json(appResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BorrarSession()
        {
            Session[SessionIndicadorDetalle] = null;
            Session[SessionContadorIndicadorDet] = null;
            appResponse.SetSuccess(string.Empty);
            appResponse.SetError(string.Empty);
            return Json(appResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Quitar(int item)
        {
            if (Session[SessionIndicadorDetalle] != null)
                listaIndicadorDetalleSession = (List<IndicadorDetalleLogic>)Session[SessionIndicadorDetalle];

            Session[SessionIndicadorDetalle] = listaIndicadorDetalleSession.Where(x => x.Item != item).ToList();
            appResponse.SetSuccess(string.Empty);
            appResponse.SetError(string.Empty);
            return Json(appResponse, JsonRequestBehavior.AllowGet);
        }
    }
}

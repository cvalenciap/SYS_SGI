using SYS_SGI.Application.Implementation.MAN;
using SYS_SGI.Domain.Implementation.Common.Base;
using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.MAN;
using SYS_SGI.Presentation.Models.View;
using SYS_SGI.Presentation.Utilities;
using GMD.CustomHtmlHelpers.HtmlGenericGrid;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Linq;
using System;
using SYS_SGI.Application.Implementation.PAR;
using SYS_SGI.Domain.Implementation.LogicalEntities.PAR;
using System.Web.Configuration;

namespace SYS_SGI.Presentation.Controllers.MAN
{
    [AppPresentationError]
    public class IndicadorController : BaseController
    {
        private readonly IndicadorAppService _IndicadorAppService = new IndicadorAppService();
        private readonly ParametroDetalleAppService _ParametroDetalleAppService = new ParametroDetalleAppService();
        private readonly ResponsableAppService _ResponsableAppService = new ResponsableAppService();
        private static AppResponse appResponse = new AppResponse();
        private static VMIndicador vmIndicador = new VMIndicador();
        private IndicadorLogic Indicador = new IndicadorLogic();

        public ActionResult Index()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Mantenimiento(long codigo = 0)
        {
            Indicador = new IndicadorLogic();

            if (codigo > 0)
            {
                Indicador = _IndicadorAppService.Obtener(codigo);
                Indicador.InfoMovs = new InfoMovs { ACCION = Enums.Action.Edit };
            }
            else
            {
                Indicador.Estado = true;
            }

            //if (Indicador.CodigoTipoIndicador != null)
            //{
            //    switch (Indicador.CodigoTipoIndicador)
            //    {
            //        case Enums.TipoIndicador.ContinuoIncremento:
            //            Indicador.bIndicadorContinuoIncremento = true;
            //            Indicador.bIndicadorContinuo = true;
            //            break;
            //        case Enums.TipoIndicador.ContinuoDisminucion:
            //            Indicador.bIndicadorContinuoDisminucion = true;
            //            Indicador.bIndicadorContinuo = true;
            //            break;
            //        case Enums.TipoIndicador.Especifico:
            //            Indicador.bIndicadorEspecifico = true;
            //            break;
            //        default:
            //            break;
            //    }
            //    if (Indicador.CodigoTipoIndicador == Enums.TipoIndicador.ContinuoIncremento)
            //    {
            //        Indicador.bIndicadorContinuoIncremento = true;
            //    }    
            //}

            vmIndicador.Indicador = Indicador;

            List<ParametroDetalleLogic> listaArea = new List<ParametroDetalleLogic>();
            listaArea.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.Area).Where(x => x.Estado).ToList());
            listaArea.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Seleccione });
            ViewBag.listaArea = listaArea;

            List<ParametroDetalleLogic> listaFuenteInformacion = new List<ParametroDetalleLogic>();
            listaFuenteInformacion.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.FuenteInformacion).Where(x => x.Estado).ToList());
            listaFuenteInformacion.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Seleccione });
            ViewBag.listaFuenteInformacion = listaFuenteInformacion;

            List<ParametroDetalleLogic> listaTipoPeriodicidad = new List<ParametroDetalleLogic>();
            listaTipoPeriodicidad.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.TipoPeriodicidad).Where(x => x.Estado).ToList());
            listaTipoPeriodicidad.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Seleccione });
            ViewBag.listaTipoPeriodicidad = listaTipoPeriodicidad;

            List<ParametroDetalleLogic> listaUnidadMedida = new List<ParametroDetalleLogic>();
            listaUnidadMedida.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.UnidadMedida).Where(x => x.Estado).ToList());
            listaUnidadMedida.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Seleccione });
            ViewBag.listaUnidadMedida = listaUnidadMedida;

            List<ParametroDetalleLogic> listaSentidoIndicador = new List<ParametroDetalleLogic>();
            listaSentidoIndicador.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.SentidoIndicador).Where(x => x.Estado).ToList());
            listaSentidoIndicador.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Seleccione });
            ViewBag.listaSentidoIndicador = listaSentidoIndicador;

            List<ParametroDetalleLogic> listaAmbitoDesempeno = new List<ParametroDetalleLogic>();
            listaAmbitoDesempeno.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.AmbitoDesempeno).Where(x => x.Estado).ToList());
            listaAmbitoDesempeno.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Seleccione });
            ViewBag.listaAmbitoDesempeno = listaAmbitoDesempeno;

            List<ParametroDetalleLogic> listaTipoIndicador = new List<ParametroDetalleLogic>();
            listaTipoIndicador.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.TipoIndicador).Where(x => x.Estado).ToList());
            listaTipoIndicador.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Seleccione });
            ViewBag.listaTipoIndicador = listaTipoIndicador;
            
            List<ResponsableLogic> listaResponsable = new List<ResponsableLogic>();
            listaResponsable.AddRange(_ResponsableAppService.Listar().Where(x => x.Estado).ToList());
            listaResponsable.Insert(0, new ResponsableLogic { CodigoResponsable = 0, Responsable = Enums.ComboDefault.Seleccione });
            ViewBag.listaResponsable = listaResponsable;

            return PartialView(vmIndicador);
        }

        [HttpPost]
        public JsonResult Guardar(VMIndicador formMantenimiento)
        {
            if (base.ModelState.IsValid)
            {
                formMantenimiento.Indicador.UsuarioCreacion = formMantenimiento.Indicador.UsuarioModificacion = Login.Obtener.Usuario.Login();
                formMantenimiento.Indicador.FechaInicio = Convert.ToDateTime(formMantenimiento.Indicador.FechaInicioDesc);
                formMantenimiento.Indicador.FechaFin = Convert.ToDateTime(formMantenimiento.Indicador.FechaFinDesc);
                if (formMantenimiento.Indicador.bIndicadorEspecifico)
                {
                    formMantenimiento.Indicador.CodigoTipoIndicador = Enums.TipoIndicador.Especifico;
                }
                else if (formMantenimiento.Indicador.bIndicadorContinuo)
                {
                    if (formMantenimiento.Indicador.bIndicadorContinuoIncremento)
                    {
                        formMantenimiento.Indicador.CodigoTipoIndicador = Enums.TipoIndicador.ContinuoIncremento;
                    }
                    else if (formMantenimiento.Indicador.bIndicadorContinuoDisminucion)
                    {
                        formMantenimiento.Indicador.CodigoTipoIndicador = Enums.TipoIndicador.ContinuoDisminucion;
                    }
                }

                if (formMantenimiento.Indicador.InfoMovs.ACCION == Enums.Action.New)
                    appResponse = _IndicadorAppService.MantenimientoNuevo(new List<IndicadorLogic> { formMantenimiento.Indicador });
                else appResponse = _IndicadorAppService.MantenimientoEditar(new List<IndicadorLogic> { formMantenimiento.Indicador });
            }
            else
            {
                appResponse = GetModelStateErrors();
            }
            return Json(appResponse);
        }

        public JsonResult Desactivar(long codigo)
        {
            Indicador.CodigoIndicador = codigo;
            Indicador.UsuarioModificacion = Indicador.UsuarioModificacion = Login.Obtener.Usuario.Login();
            appResponse = _IndicadorAppService.MantenimientoEliminar(new List<IndicadorLogic> { Indicador });
            return Json(appResponse);
        }

        public ActionResult Paginacion(string arg)
        {
            var modelData = JsonConvert.DeserializeObject<GMDGridModel<IndicadorLogic>>(arg);
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
                modelData.Data = _IndicadorAppService.Paginacion(paginateParams);
            else
                modelData.Data = new List<IndicadorLogic>();

            modelData.TotalRows = paginateParams.TotalRows;

            return PartialView(modelData);
        }
        
    }
}

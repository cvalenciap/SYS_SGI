using SYS_SGI.Application.Implementation.MOV;
using SYS_SGI.Domain.Implementation.Common.Base;
using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.MOV;
using SYS_SGI.Application.Implementation.PAR;
using SYS_SGI.Domain.Implementation.LogicalEntities.PAR;
using SYS_SGI.Application.Implementation.MAN;
using SYS_SGI.Presentation.Models.View;
using SYS_SGI.Presentation.Utilities;
using GMD.CustomHtmlHelpers.HtmlGenericGrid;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Linq;
using System;
using System.Web.Configuration;


namespace SYS_SGI.Presentation.Controllers.MOV
{
    [AppPresentationError]
    public class AlineamientoConfiguracionController : BaseController
    {
        private readonly AlineamientoConfiguracionAppService _AlineamientoConfiguracionAppService = new AlineamientoConfiguracionAppService();
        private readonly ParametroAppService _ParametroAppService = new ParametroAppService();
        private readonly ParametroDetalleAppService _ParametroDetalleAppService = new ParametroDetalleAppService();
        private readonly IndicadorAppService _IndicadorAppService = new IndicadorAppService();
        private static AppResponse appResponse = new AppResponse();
        private static VMAlineamientoConfiguracion vmAlineamientoConfiguracion = new VMAlineamientoConfiguracion();
        private AlineamientoConfiguracionLogic AlineamientoConfiguracion = new AlineamientoConfiguracionLogic();
                
        public ActionResult Index()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Mantenimiento(long codigo = 0)
        {
            AlineamientoConfiguracion = new AlineamientoConfiguracionLogic();
            List<ParametroDetalleLogic> listaGuiaEmpresarial = new List<ParametroDetalleLogic>();
            List<ParametroLogic> listaAlineamientoInicial = new List<ParametroLogic>();
            List<ParametroLogic> listaAlineamientoFinal = new List<ParametroLogic>();

            if (codigo > 0)
            {
                AlineamientoConfiguracion = _AlineamientoConfiguracionAppService.Obtener(codigo);
                AlineamientoConfiguracion.InfoMovs = new InfoMovs { ACCION = Enums.Action.Edit };
            }
            else
            {
                AlineamientoConfiguracion.Estado = true;
            }

            vmAlineamientoConfiguracion.AlineamientoConfiguracion = AlineamientoConfiguracion;
            
            listaGuiaEmpresarial.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.TipoGuiaEmpresarial).Where(x => x.Estado).ToList());
            listaGuiaEmpresarial.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Seleccione });
            ViewBag.listaGuiaEmpresarial = listaGuiaEmpresarial;
            
            return PartialView(vmAlineamientoConfiguracion);
        }

        [HttpPost]
        public JsonResult Guardar(VMAlineamientoConfiguracion formMantenimiento)
        {
            if (base.ModelState.IsValid)
            {
                formMantenimiento.AlineamientoConfiguracion.UsuarioCreacion = formMantenimiento.AlineamientoConfiguracion.UsuarioModificacion = Login.Obtener.Usuario.Login();
                if (formMantenimiento.AlineamientoConfiguracion.InfoMovs.ACCION == Enums.Action.New)
                    appResponse = _AlineamientoConfiguracionAppService.MantenimientoNuevo(new List<AlineamientoConfiguracionLogic> { formMantenimiento.AlineamientoConfiguracion });
                else appResponse = _AlineamientoConfiguracionAppService.MantenimientoEditar(new List<AlineamientoConfiguracionLogic> { formMantenimiento.AlineamientoConfiguracion });
            }
            else
            {
                appResponse = GetModelStateErrors();
            }
            return Json(appResponse);
        }

        public JsonResult Desactivar(long codigo)
        {
            AlineamientoConfiguracion.CodigoAlineamientoConfiguracion = codigo;
            AlineamientoConfiguracion.UsuarioModificacion = AlineamientoConfiguracion.UsuarioModificacion = Login.Obtener.Usuario.Login();
            appResponse = _AlineamientoConfiguracionAppService.MantenimientoEliminar(new List<AlineamientoConfiguracionLogic> { AlineamientoConfiguracion });
            return Json(appResponse);
        }

        public ActionResult Paginacion(string arg)
        {
            var modelData = JsonConvert.DeserializeObject<GMDGridModel<AlineamientoConfiguracionLogic>>(arg);
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
                modelData.Data = _AlineamientoConfiguracionAppService.Paginacion(paginateParams);
            else
                modelData.Data = new List<AlineamientoConfiguracionLogic>();

            modelData.TotalRows = paginateParams.TotalRows;

            return PartialView(modelData);
        }

        public ActionResult PaginacionAlineamientos(string arg, int CodigoAlineamientoConfiguracion)
        {
            List<ParametroLogic> lista = new List<ParametroLogic>();
            ParametroLogic indicador = new ParametroLogic();
            AlineamientoConfiguracionLogic alineamiento = new AlineamientoConfiguracionLogic();

            var modelData = JsonConvert.DeserializeObject<GMDGridModel<ParametroLogic>>(arg);
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
                indicador.Nombre = "INDICADOR";
                lista.AddRange(_ParametroAppService.Listar().Where(x => (x.Funcional || x.Valor=="GR-PRS") && x.Estado));
                
                lista.Add(indicador);
                modelData.Data = lista;
            }               
            else
                modelData.Data = new List<ParametroLogic>();

            modelData.TotalRows = modelData.Data.Count();

            return PartialView(modelData);
        }

        public ActionResult CargarAlineamientoFinal(long CodigoAlineamientoInicial)
        {
            List<ParametroLogic> listaAlineamientoFinal = new List<ParametroLogic>();

            if (CodigoAlineamientoInicial >0 )
                listaAlineamientoFinal = _ParametroAppService.Listar().Where(x => (x.Funcional || x.Valor == "GR-PRS") && x.CodigoParametro != CodigoAlineamientoInicial && x.Estado).ToList();

            return Json(listaAlineamientoFinal, JsonRequestBehavior.AllowGet);
        }
    }
}

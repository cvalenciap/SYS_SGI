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
using SYS_SGI.Domain.Implementation.LogicalEntities.PAR;
using SYS_SGI.Application.Implementation.PAR;
using System.Web.Configuration;

namespace SYS_SGI.Presentation.Controllers.MAN
{
    [AppPresentationError]
    public class VariableController : BaseController
    {
        private readonly VariableAppService _VariableAppService = new VariableAppService();
        private readonly ParametroDetalleAppService _ParametroDetalleAppService = new ParametroDetalleAppService();
        private readonly ResponsableAppService _ResponsableAppService = new ResponsableAppService();
        private static AppResponse appResponse = new AppResponse();
        private static VMVariable vmVariable = new VMVariable();
        private VariableLogic Variable = new VariableLogic();

        public ActionResult Index()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Mantenimiento(long codigo = 0)
        {
            Variable = new VariableLogic();

            if (codigo > 0)
            {
                Variable = _VariableAppService.Obtener(codigo);
                Variable.InfoMovs = new InfoMovs { ACCION = Enums.Action.Edit };
            }
            else
            {
                Variable.Estado = true;
            }

            vmVariable.Variable = Variable;

            List<ParametroDetalleLogic> listaTipoPeriodicidad = new List<ParametroDetalleLogic>();
            listaTipoPeriodicidad.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.TipoPeriodicidad).Where(x => x.Estado).ToList());
            listaTipoPeriodicidad.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Seleccione });
            ViewBag.listaTipoPeriodicidad = listaTipoPeriodicidad;

            List<ParametroDetalleLogic> listaUnidadMedida = new List<ParametroDetalleLogic>();
            listaUnidadMedida.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.UnidadMedida).Where(x => x.Estado).ToList());
            listaUnidadMedida.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Seleccione });
            ViewBag.listaUnidadMedida = listaUnidadMedida;

            List<ParametroDetalleLogic> listaTipoVariable = new List<ParametroDetalleLogic>();
            listaTipoVariable.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.TipoVariable).Where(x => x.Estado).ToList());
            listaTipoVariable.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Seleccione });
            ViewBag.listaTipoVariable = listaTipoVariable;

            List<ParametroDetalleLogic> listaArea = new List<ParametroDetalleLogic>();
            listaArea.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.Area).Where(x => x.Estado).ToList());
            listaArea.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Seleccione });
            ViewBag.listaArea = listaArea;

            List<ResponsableLogic> listaResponsable = new List<ResponsableLogic>();
            listaResponsable.AddRange(_ResponsableAppService.Listar().Where(x => x.Estado).ToList());
            listaResponsable.Insert(0, new ResponsableLogic { CodigoResponsable = 0, Responsable = Enums.ComboDefault.Seleccione });
            ViewBag.listaResponsable = listaResponsable;

            return PartialView(vmVariable);
        }

        [HttpPost]
        public JsonResult Guardar(VMVariable formMantenimiento)
        {
            if (base.ModelState.IsValid)
            {
                formMantenimiento.Variable.UsuarioCreacion = formMantenimiento.Variable.UsuarioModificacion = Login.Obtener.Usuario.Login();
                if (formMantenimiento.Variable.InfoMovs.ACCION == Enums.Action.New)
                    appResponse = _VariableAppService.MantenimientoNuevo(new List<VariableLogic> { formMantenimiento.Variable });
                else appResponse = _VariableAppService.MantenimientoEditar(new List<VariableLogic> { formMantenimiento.Variable });
            }
            else
            {
                appResponse = GetModelStateErrors();
            }
            return Json(appResponse);
        }

        public JsonResult Desactivar(long codigo)
        {
            Variable.CodigoVariable = codigo;
            Variable.UsuarioModificacion = Variable.UsuarioModificacion = Login.Obtener.Usuario.Login();
            appResponse = _VariableAppService.MantenimientoEliminar(new List<VariableLogic> { Variable });
            return Json(appResponse);
        }

        public ActionResult Paginacion(string arg)
        {
            var modelData = JsonConvert.DeserializeObject<GMDGridModel<VariableLogic>>(arg);
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
                modelData.Data = _VariableAppService.Paginacion(paginateParams);
            else
                modelData.Data = new List<VariableLogic>();

            modelData.TotalRows = paginateParams.TotalRows;

            return PartialView(modelData);
        }
    }
}

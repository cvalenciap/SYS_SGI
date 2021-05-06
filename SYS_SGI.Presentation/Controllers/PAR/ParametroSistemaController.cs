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
using SYS_SGI.Application.Implementation.PAR;
using SYS_SGI.Domain.Implementation.LogicalEntities.PAR;

namespace SYS_SGI.Presentation.Controllers.PAR
{
    [AppPresentationError]
    public class ParametroSistemaController : BaseController
    {
        private readonly ParametroAppService _parametroAppService = new ParametroAppService();
        private static AppResponse appResponse = new AppResponse();
        private static VMParametro vmParametro = new VMParametro();
        private ParametroLogic parametro = new ParametroLogic();

        public ActionResult Index()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Mantenimiento(long codigoParametro = 0)
        {
            if (codigoParametro > 0)
            {
                parametro = _parametroAppService.Obtener(codigoParametro);
                parametro.InfoMovs = new InfoMovs { ACCION = Enums.Action.Edit };
            }
            else
            {
                parametro.Estado = true;
            }

            vmParametro.parametro = parametro;
            return PartialView(vmParametro);
        }

        [HttpPost]
        public JsonResult Guardar(VMParametro formMantenimiento)
        {
            if (base.ModelState.IsValid)
            {
                formMantenimiento.parametro.UsuarioCreacion = formMantenimiento.parametro.UsuarioModificacion = Login.Obtener.Usuario.Login();
                formMantenimiento.parametro.Funcional = false;

                if (formMantenimiento.parametro.InfoMovs.ACCION == Enums.Action.New)
                    appResponse = _parametroAppService.MantenimientoNuevo(new List<ParametroLogic> { formMantenimiento.parametro });
                else appResponse = _parametroAppService.MantenimientoEditar(new List<ParametroLogic> { formMantenimiento.parametro });
            }
            else
            {
                appResponse = GetModelStateErrors();
            }
            return Json(appResponse);
        }

        public JsonResult Desactivar(long codigoParametro)
        {
            parametro.CodigoParametro = codigoParametro;
            parametro.UsuarioModificacion = parametro.UsuarioModificacion = Login.Obtener.Usuario.Login();

            appResponse = _parametroAppService.MantenimientoEliminar(new List<ParametroLogic> { parametro });
            return Json(appResponse);
        }

        public ActionResult Paginacion(string arg)
        {
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
                modelData.Data = _parametroAppService.Paginacion(paginateParams);
            else
                modelData.Data = new List<ParametroLogic>();

            modelData.TotalRows = paginateParams.TotalRows;

            return PartialView(modelData);
        }

    }
}
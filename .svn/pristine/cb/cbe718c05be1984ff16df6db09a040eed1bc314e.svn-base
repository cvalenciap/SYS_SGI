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
    public class AlertaController : BaseController
    {
        private readonly AlertaAppService _AlertaAppService = new AlertaAppService();
        private readonly ParametroDetalleAppService _ParametroDetalleAppService = new ParametroDetalleAppService();
        private static AppResponse appResponse = new AppResponse();
        private static VMAlerta vmAlerta = new VMAlerta();
        private AlertaLogic Alerta = new AlertaLogic();

        public ActionResult Index()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Mantenimiento(long codigo = 0)
        {
            Alerta = new AlertaLogic();
            List<ParametroDetalleLogic> listaTipoAlerta = new List<ParametroDetalleLogic>();

            if (codigo > 0)
            {
                Alerta = _AlertaAppService.Obtener(codigo);
                Alerta.InfoMovs = new InfoMovs { ACCION = Enums.Action.Edit };
            }
            else
            {
                Alerta.Estado = true;
            }

            vmAlerta.Alerta = Alerta;

            listaTipoAlerta.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.TipoAlerta).Where(x => x.Estado).ToList());
            listaTipoAlerta.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Seleccione });
            ViewBag.listaTipoAlerta = listaTipoAlerta;

            return PartialView(vmAlerta);
        }

        [HttpPost]
        public JsonResult Guardar(VMAlerta formMantenimiento)
        {
            if (base.ModelState.IsValid)
            {
                formMantenimiento.Alerta.UsuarioCreacion = formMantenimiento.Alerta.UsuarioModificacion = Login.Obtener.Usuario.Login();
                if (formMantenimiento.Alerta.InfoMovs.ACCION == Enums.Action.New)
                    appResponse = _AlertaAppService.MantenimientoNuevo(new List<AlertaLogic> { formMantenimiento.Alerta });
                else appResponse = _AlertaAppService.MantenimientoEditar(new List<AlertaLogic> { formMantenimiento.Alerta });
            }
            else
            {
                appResponse = GetModelStateErrors();
            }
            return Json(appResponse);
        }

        public JsonResult Desactivar(long codigo)
        {
            Alerta.CodigoAlerta = codigo;
            Alerta.UsuarioModificacion = Alerta.UsuarioModificacion = Login.Obtener.Usuario.Login();
            appResponse = _AlertaAppService.MantenimientoEliminar(new List<AlertaLogic> { Alerta });
            return Json(appResponse);
        }

        public ActionResult Paginacion(string arg)
        {
            var modelData = JsonConvert.DeserializeObject<GMDGridModel<AlertaLogic>>(arg);
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
                modelData.Data = _AlertaAppService.Paginacion(paginateParams);
            else
                modelData.Data = new List<AlertaLogic>();

            modelData.TotalRows = paginateParams.TotalRows;

            return PartialView(modelData);
        }
    }
}

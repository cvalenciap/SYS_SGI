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
    public class ResponsableController : BaseController
    {
        private readonly ResponsableAppService _ResponsableAppService = new ResponsableAppService();
        private readonly ParametroDetalleAppService _ParametroDetalleAppService = new ParametroDetalleAppService();
        private static AppResponse appResponse = new AppResponse();
        private static VMResponsable vmResponsable = new VMResponsable();
        private ResponsableLogic Responsable = new ResponsableLogic();

        public ActionResult Index()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Mantenimiento(long codigo = 0)
        {
            Responsable = new ResponsableLogic();

            if (codigo > 0)
            {
                Responsable = _ResponsableAppService.Obtener(codigo);
                Responsable.InfoMovs = new InfoMovs { ACCION = Enums.Action.Edit };
            }
            else
            {
                Responsable.Estado = true;
            }

            vmResponsable.Responsable = Responsable;

            List<ParametroDetalleLogic> listaCargo = new List<ParametroDetalleLogic>();
            listaCargo.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.Cargo).Where(x => x.Estado).ToList());
            listaCargo.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Seleccione });
            ViewBag.listaCargo = listaCargo;

            List<ParametroDetalleLogic> listaGenero = new List<ParametroDetalleLogic>();
            listaGenero.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.Genero).Where(x => x.Estado).ToList());
            listaGenero.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Seleccione });
            ViewBag.listaGenero = listaGenero;

            List<ParametroDetalleLogic> listaTipoDocumento = new List<ParametroDetalleLogic>();
            listaTipoDocumento.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.TipoDocumento).Where(x => x.Estado).ToList());
            listaTipoDocumento.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Seleccione });
            ViewBag.listaTipoDocumento = listaTipoDocumento;

            return PartialView(vmResponsable);
        }

        [HttpPost]
        public JsonResult Guardar(VMResponsable formMantenimiento)
        {
            if (base.ModelState.IsValid)
            {
                formMantenimiento.Responsable.UsuarioCreacion = formMantenimiento.Responsable.UsuarioModificacion = Login.Obtener.Usuario.Login();
                if (formMantenimiento.Responsable.InfoMovs.ACCION == Enums.Action.New)
                    appResponse = _ResponsableAppService.MantenimientoNuevo(new List<ResponsableLogic> { formMantenimiento.Responsable });
                else appResponse = _ResponsableAppService.MantenimientoEditar(new List<ResponsableLogic> { formMantenimiento.Responsable });
            }
            else
            {
                appResponse = GetModelStateErrors();
            }
            return Json(appResponse);
        }

        public JsonResult Desactivar(long codigo)
        {
            Responsable.CodigoResponsable = codigo;
            Responsable.UsuarioModificacion = Responsable.UsuarioModificacion = Login.Obtener.Usuario.Login();
            appResponse = _ResponsableAppService.MantenimientoEliminar(new List<ResponsableLogic> { Responsable });
            return Json(appResponse);
        }

        public ActionResult Paginacion(string arg)
        {
            var modelData = JsonConvert.DeserializeObject<GMDGridModel<ResponsableLogic>>(arg);
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
                modelData.Data = _ResponsableAppService.Paginacion(paginateParams);
            else
                modelData.Data = new List<ResponsableLogic>();

            modelData.TotalRows = paginateParams.TotalRows;

            return PartialView(modelData);
        }
    }
}

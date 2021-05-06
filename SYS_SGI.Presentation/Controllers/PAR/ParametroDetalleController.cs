using GMD.CustomHtmlHelpers.HtmlGenericGrid;
using Newtonsoft.Json;
using SYS_SGI.Application.Implementation.PAR;
using SYS_SGI.Domain.Implementation.Common.Base;
using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.PAR;
using SYS_SGI.Infrastructure.Data.Implementation.Base;
using SYS_SGI.Presentation.Models.View;
using SYS_SGI.Presentation.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace SYS_SGI.Presentation.Controllers.PAR
{
    [AppPresentationError]
    public class ParametroDetalleController : BaseController
    {
        private readonly ParametroDetalleAppService _parametroDetalleAppService = new ParametroDetalleAppService();
        private readonly ParametroAppService _parametroAppService = new ParametroAppService();
        private ParametroDetalleLogic parametroDetalle = new ParametroDetalleLogic();
        private static AppResponse appResponse = new AppResponse();
        private static VMParametro vmParametro = new VMParametro();
        private static VMParametroDetalle vmParametroDetalle = new VMParametroDetalle();


        public ActionResult Index(long codigoParametro = 0, string nombreParametro = "ParametroAccion", string titulo="")
        {
            vmParametro.parametro = _parametroAppService.Listar().Where(x => x.CodigoParametro == codigoParametro).FirstOrDefault();
            ViewBag.nombreParametro = nombreParametro;
            ViewBag.titulo = titulo;

            return PartialView(vmParametro);
        }

        public ActionResult Paginacion(string arg, string nombreParametro)
        {
            var modelData = JsonConvert.DeserializeObject<GMDGridModel<ParametroDetalleLogic>>(arg);

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
                modelData.Data = _parametroDetalleAppService.Paginacion(paginateParams);
            else
                modelData.Data = new List<ParametroDetalleLogic>();

            if(nombreParametro == "ParametroObjetivo" || nombreParametro == "ParametroAccion")
            {
                modelData.Data = modelData.Data.OrderBy(x=>x.Valor);
            }

            modelData.TotalRows = modelData.Data.Count();
            ViewBag.nombreParametro = nombreParametro;

            return PartialView(modelData);
        }

        [HttpPost]
        public ActionResult Mantenimiento(long codigoParametro, string codigoElemento = "0", string nombreParametro = "ParametroAccion")
        {
            ViewBag.nombreParametro = nombreParametro;

            if (Convert.ToInt32(codigoElemento) > 0)
            {
                parametroDetalle = _parametroDetalleAppService.Obtener(codigoParametro, codigoElemento);
                parametroDetalle.InfoMovs = new InfoMovs { ACCION = Enums.Action.Edit };
            }
            else
            {
                parametroDetalle.InfoMovs = new InfoMovs { ACCION = Enums.Action.New };
                parametroDetalle.Estado = true;
            }
            parametroDetalle.CodigoParametro = codigoParametro;
            parametroDetalle.ParametroDesc = _parametroAppService.Obtener(codigoParametro).Nombre;
            vmParametroDetalle.parametroDetalle = parametroDetalle;
            return PartialView(vmParametroDetalle);
        }

        [HttpPost]
        public JsonResult Guardar(VMParametroDetalle formMantenimiento)
        {
            if (base.ModelState.IsValid)
            {
                formMantenimiento.parametroDetalle.UsuarioCreacion = formMantenimiento.parametroDetalle.UsuarioModificacion = Login.Obtener.Usuario.Login();

                if (formMantenimiento.parametroDetalle.InfoMovs.ACCION == Enums.Action.New)
                    appResponse = _parametroDetalleAppService.MantenimientoNuevo(new List<ParametroDetalleLogic> { formMantenimiento.parametroDetalle });
                else appResponse = _parametroDetalleAppService.MantenimientoEditar(new List<ParametroDetalleLogic> { formMantenimiento.parametroDetalle });
            }
            else
            {
                appResponse = GetModelStateErrors();
            }
            return Json(appResponse);
        }

        public JsonResult Desactivar(long codigoParametro, string codigoElemento)
        {
            parametroDetalle.CodigoParametro = codigoParametro;
            parametroDetalle.CodigoElemento = codigoElemento;
            parametroDetalle.UsuarioCreacion = parametroDetalle.UsuarioModificacion = Login.Obtener.Usuario.Login();
            parametroDetalle.InfoMovs.ACCION = Enums.Action.Delete;

            appResponse = _parametroDetalleAppService.MantenimientoEliminar(new List<ParametroDetalleLogic> { parametroDetalle });
            return Json(appResponse);
        }

        public JsonResult ListarParametroDetalle(long codigoParametro)
        {
            List<ParametroDetalleLogic> listParametroDetalle = new List<ParametroDetalleLogic> { new ParametroDetalleLogic { Nombre = Enums.ComboDefault.Seleccione } };
            if (codigoParametro != null)
                listParametroDetalle.AddRange(_parametroDetalleAppService.Listar(Convert.ToInt16(codigoParametro)));
            return Json(listParametroDetalle, JsonRequestBehavior.AllowGet);
        }
    }
}
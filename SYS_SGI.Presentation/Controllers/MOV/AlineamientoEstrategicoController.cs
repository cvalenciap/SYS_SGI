using SYS_SGI.Application.Implementation.MOV;
using SYS_SGI.Domain.Implementation.Common.Base;
using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.MOV;
using SYS_SGI.Domain.Implementation.LogicalEntities.PAR;
using SYS_SGI.Application.Implementation.PAR;
using SYS_SGI.Domain.Implementation.LogicalEntities.REP;
using SYS_SGI.Application.Implementation.MAN;
using SYS_SGI.Application.Implementation.REP;
using SYS_SGI.Infrastructure.Data.Implementation.Base;
using SYS_SGI.Presentation.Models.View;
using SYS_SGI.Presentation.Utilities;
using GMD.CustomHtmlHelpers.HtmlGenericGrid;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Linq;
using System;
using System.Data;
using System.Reflection;
using System.Web.Configuration;

namespace SYS_SGI.Presentation.Controllers.MOV
{
    [AppPresentationError]
    public class AlineamientoEstrategicoController : BaseController
    {
        private readonly AlineamientoEstrategicoAppService _AlineamientoEstrategicoAppService = new AlineamientoEstrategicoAppService();
        private readonly AlineamientoConfiguracionAppService _AlineamientoConfiguracionAppService = new AlineamientoConfiguracionAppService();
        private readonly ReporteAppService _ReporteAppService = new ReporteAppService();
        private readonly ParametroDetalleAppService _ParametroDetalleAppService = new ParametroDetalleAppService();
        private readonly IndicadorAppService _IndicadorAppService = new IndicadorAppService();

        private static AppResponse appResponse = new AppResponse();
        private static VMAlineamientoEstrategico vmAlineamientoEstrategico = new VMAlineamientoEstrategico();
        private AlineamientoEstrategicoLogic AlineamientoEstrategico = new AlineamientoEstrategicoLogic();
        private string SessionAlineamientos = "SessionAlineamientos";
        List<AlineamientoEstrategicoLogic> listaAlineamientoSession = new List<AlineamientoEstrategicoLogic>();
        private string SessionContadorAlineamiento = "SessionContadorAlineamiento";
        int contador = 0;
        public ActionResult Index()
        {
            List<ParametroDetalleLogic> listaGuiaEmpresarial = new List<ParametroDetalleLogic>();
            listaGuiaEmpresarial.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.TipoGuiaEmpresarial).ToList().Where(x => x.Estado).Where(x => x.Estado && (x.NombreCorto == "Anexo N°1" || x.NombreCorto == "Anexo N°3")));
            listaGuiaEmpresarial.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Seleccione });
            ViewBag.listaGuiaEmpresarial = listaGuiaEmpresarial;

            return PartialView();
        }

        [HttpPost]
        public ActionResult Mantenimiento(long codigo = 0)
        {
            if (Session[SessionAlineamientos] != null)
                Session[SessionAlineamientos] = null;

            if (Session[SessionContadorAlineamiento] != null)
                Session[SessionContadorAlineamiento] = null;

            List<ParametroDetalleLogic> listaGuiaEmpresarial = new List<ParametroDetalleLogic>();

            if (codigo > 0)
            {
                AlineamientoEstrategico = _AlineamientoEstrategicoAppService.Obtener(codigo);
                AlineamientoEstrategico.InfoMovs = new InfoMovs { ACCION = Enums.Action.Edit };                
            }
            else
            {
                AlineamientoEstrategico.Estado = true;
            }
            
            vmAlineamientoEstrategico.AlineamientoEstrategico = AlineamientoEstrategico;

            listaGuiaEmpresarial.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.TipoGuiaEmpresarial).ToList().Where(x=>x.Estado));
            listaGuiaEmpresarial.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Seleccione });
            ViewBag.listaGuiaEmpresarial = listaGuiaEmpresarial;

            return PartialView(vmAlineamientoEstrategico);
        }

        [HttpPost]
        public JsonResult Guardar(VMAlineamientoEstrategico formMantenimiento)
        {
            if (base.ModelState.IsValid)
            {               
                if (Session[SessionAlineamientos] != null)
                    listaAlineamientoSession = (List<AlineamientoEstrategicoLogic>)Session[SessionAlineamientos];
                if (listaAlineamientoSession.Count == 0)
                {
                    appResponse.Description = "Verificar Informacion.";
                }
                foreach (AlineamientoEstrategicoLogic alineamiento in listaAlineamientoSession)
                {
                    alineamiento.InfoMovs = formMantenimiento.AlineamientoEstrategico.InfoMovs;
                    alineamiento.UsuarioCreacion = formMantenimiento.AlineamientoEstrategico.UsuarioModificacion = Login.Obtener.Usuario.Login();
                    formMantenimiento.AlineamientoEstrategico = alineamiento;
                    
                    if (formMantenimiento.AlineamientoEstrategico.InfoMovs.ACCION == Enums.Action.New)
                        appResponse = _AlineamientoEstrategicoAppService.MantenimientoNuevo(new List<AlineamientoEstrategicoLogic> { formMantenimiento.AlineamientoEstrategico });
                    else appResponse = _AlineamientoEstrategicoAppService.MantenimientoEditar(new List<AlineamientoEstrategicoLogic> { formMantenimiento.AlineamientoEstrategico });
                }
            }
            else
            {
                appResponse = GetModelStateErrors();
                
            }
            
            return Json(appResponse);
        }
               
        public JsonResult Desactivar(int CodigoAlineamientoEstrategico)
        {
            AlineamientoEstrategico.CodigoAlineamientoEstrategico = CodigoAlineamientoEstrategico;
            AlineamientoEstrategico.UsuarioModificacion = AlineamientoEstrategico.UsuarioModificacion = Login.Obtener.Usuario.Login();
            appResponse = _AlineamientoEstrategicoAppService.MantenimientoEliminar(new List<AlineamientoEstrategicoLogic> { AlineamientoEstrategico });
            return Json(appResponse);
        }

        public JsonResult Activar(int CodigoAlineamientoEstrategico)
        {
            AlineamientoEstrategico.CodigoAlineamientoEstrategico = CodigoAlineamientoEstrategico;
            AlineamientoEstrategico.UsuarioModificacion = AlineamientoEstrategico.UsuarioModificacion = Login.Obtener.Usuario.Login();
            appResponse = _AlineamientoEstrategicoAppService.MantenimientoActivar(new List<AlineamientoEstrategicoLogic> { AlineamientoEstrategico });
            return Json(appResponse);
        }

        public ActionResult Paginacion(string arg, string codigoGuiaEmpresarial)
        {
            int codigoGuia = 0;
            bool resultado = int.TryParse(codigoGuiaEmpresarial, out codigoGuia);
            AlineamientoConfiguracionLogic alineamientoConfiguracion = new AlineamientoConfiguracionLogic();
            List<AlineamientoEstrategicoLogic> data = new List<AlineamientoEstrategicoLogic>();
            
            if (codigoGuia > 0)
            {
                alineamientoConfiguracion = _AlineamientoConfiguracionAppService.Listar().Where(x=>x.CodigoTipoGuiaEmpresarial == codigoGuia && x.Estado).FirstOrDefault();
            }

            var modelData = JsonConvert.DeserializeObject<GMDGridModel<AlineamientoEstrategicoLogic>>(arg);
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
                data = _AlineamientoEstrategicoAppService.Paginacion(paginateParams, codigoGuia);
                if(data.Count>0)
                    data[0].alineamientoConfiguracion = alineamientoConfiguracion;            
            }
           
            modelData.Data = data;

            modelData.TotalRows = paginateParams.TotalRows;

            return PartialView(modelData);
        }

        public ActionResult PaginacionMantenimiento(string arg)
        {
            AlineamientoConfiguracionLogic alineamientoConfiguracion = new AlineamientoConfiguracionLogic();
            
            if (Session[SessionAlineamientos] != null)
                listaAlineamientoSession = (List<AlineamientoEstrategicoLogic>)Session[SessionAlineamientos];
            else
                Session[SessionAlineamientos] = null;

            var modelData = JsonConvert.DeserializeObject<GMDGridModel<AlineamientoEstrategicoLogic>>(arg);
            modelData.Data = new List<AlineamientoEstrategicoLogic>();
            
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
                modelData.Data = listaAlineamientoSession;
            else
            {
                if (listaAlineamientoSession.Count > 0)
                {
                    modelData.Data = listaAlineamientoSession;
                }
            }

            if (modelData != null && listaAlineamientoSession.Count > 0)
            {
                int codigoGuia = modelData.Data.Select(x => x.CodigoTipoGuiaEmpresarial).FirstOrDefault();
                alineamientoConfiguracion = _AlineamientoConfiguracionAppService.Listar().Where(x => x.CodigoTipoGuiaEmpresarial == codigoGuia && x.Estado).FirstOrDefault();
                listaAlineamientoSession[0].alineamientoConfiguracion = alineamientoConfiguracion;
            }

            modelData.TotalRows = modelData.Data.ToList().Count;

            return PartialView(modelData);
        }
                
        public ActionResult ObtenerCabecera(long CodigoTipoGuiaEmpresarial)
        {
            AlineamientoConfiguracionLogic alineamientoConfiguracion = new AlineamientoConfiguracionLogic();
            List<AlineamientoEstrategicoLogic> data = new List<AlineamientoEstrategicoLogic>();

            if (CodigoTipoGuiaEmpresarial > 0)
            {
                alineamientoConfiguracion = _AlineamientoConfiguracionAppService.Listar().Where(x => x.CodigoTipoGuiaEmpresarial == CodigoTipoGuiaEmpresarial && x.Estado).FirstOrDefault();
            }
            
            return Json(alineamientoConfiguracion, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CargarCombos(long CodigoParametro, bool Indicador)
        {
            List<ParametroDetalleLogic> lista = new List<ParametroDetalleLogic>();

            if (!Indicador)
            {                
                lista = _ParametroDetalleAppService.Listar(CodigoParametro);
            }
            else
            {
                lista.AddRange(_IndicadorAppService.Listar().Where(x => x.Estado).Select(item => new ParametroDetalleLogic
                {
                    CodigoElemento = item.CodigoIndicador.ToString(),
                    Nombre = item.Nombre
                }).OrderBy(x => x.Nombre).ToList());
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Agregar(string data)
        {
            var lista = JsonConvert.DeserializeObject<List<AlineamientoEstrategicoLogic>>(data);

            if (Session[SessionAlineamientos] != null)
                listaAlineamientoSession = (List<AlineamientoEstrategicoLogic>)Session[SessionAlineamientos];

            if (Session[SessionContadorAlineamiento] != null)
                contador = (int)Session[SessionContadorAlineamiento];

            lista[0].Item = contador + 1;
            listaAlineamientoSession.AddRange(lista);

            Session[SessionAlineamientos] = listaAlineamientoSession;
            Session[SessionContadorAlineamiento] = contador + 1;

            appResponse.SetSuccess(string.Empty);
            appResponse.SetError(string.Empty);
            return Json(appResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BorrarSession()
        {
            Session[SessionAlineamientos] = null;
            Session[SessionContadorAlineamiento] = null;
            appResponse.SetSuccess(string.Empty);
            appResponse.SetError(string.Empty);
            return Json(appResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Quitar(int item)
        {
            if (Session[SessionAlineamientos] != null)
                listaAlineamientoSession = (List<AlineamientoEstrategicoLogic>)Session[SessionAlineamientos];

            Session[SessionAlineamientos] = listaAlineamientoSession.Where(x => x.Item != item).ToList();
            appResponse.SetSuccess(string.Empty);
            appResponse.SetError(string.Empty);
            return Json(appResponse, JsonRequestBehavior.AllowGet);
        }
    }
}

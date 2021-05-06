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
    public class NegocioController : BaseController
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
            List<ParametroDetalleLogic> listaGuiaEmpresarial = new List<ParametroDetalleLogic>();
            listaGuiaEmpresarial.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.TipoGuiaEmpresarial).ToList().Where(x => x.Estado));
            listaGuiaEmpresarial.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Seleccione });
            ViewBag.listaGuiaEmpresarial = listaGuiaEmpresarial;

            return PartialView();
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

    }
}

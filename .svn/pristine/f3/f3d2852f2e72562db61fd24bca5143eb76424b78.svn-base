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
    public class VariableReporteController : BaseController
    {
        private readonly VariableAppService _VariableAppService = new VariableAppService();
        private readonly ParametroDetalleAppService _ParametroDetalleAppService = new ParametroDetalleAppService();
        private readonly ResponsableAppService _ResponsableAppService = new ResponsableAppService();
        private static AppResponse appResponse = new AppResponse();
        private static VMVariable vmVariable = new VMVariable();
        private VariableLogic Variable = new VariableLogic();

        public ActionResult Index()
        {
            List<ParametroDetalleLogic> listaTipoVariable = new List<ParametroDetalleLogic>();
            listaTipoVariable.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.TipoVariable).Where(x => x.Estado).ToList());
            listaTipoVariable.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Todos });
            ViewBag.listaTipoVariable = listaTipoVariable;

            List<ParametroDetalleLogic> listaArea = new List<ParametroDetalleLogic>();
            listaArea.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.Area).Where(x => x.Estado).ToList());
            listaArea.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Todos });
            ViewBag.listaArea = listaArea;

            return PartialView();
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

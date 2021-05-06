using GMD.CustomHtmlHelpers.HtmlGenericGrid;
using Newtonsoft.Json;
using SYS_SGI.Application.Implementation.MAN;
using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.MAN;
using SYS_SGI.Presentation.Models.View;
using SYS_SGI.Presentation.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace SYS_SGI.Presentation.Controllers.MAN
{
    [AppPresentationError]
    public class FormulaController : BaseController
    {
        private static VMFormula vmFormula = new VMFormula();
        private readonly VariableAppService _VariableAppService = new VariableAppService();

        public ActionResult Index()
        {
            return PartialView();
        }
      
        public ActionResult Mantenimiento()
        {
            vmFormula.Formula = new FormulaLogic();

            return PartialView(vmFormula);
        }
        public ActionResult PaginacionVariable(string arg)
        {
            var modelData = JsonConvert.DeserializeObject<GMDGridModel<VariableLogic>>(arg);
            modelData.isFirstLoad = true;
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
                modelData.Data = _VariableAppService.Paginacion(paginateParams).Where(X=>X.Estado);
            else
                modelData.Data = new List<VariableLogic>();

            modelData.TotalRows = paginateParams.TotalRows;

            return PartialView(modelData);
        }

    }
}

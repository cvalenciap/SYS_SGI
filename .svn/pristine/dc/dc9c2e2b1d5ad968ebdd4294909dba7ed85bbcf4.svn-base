using Elmah;
using SYS_SGI.Presentation.Utilities;
using SYS_SGI.Domain.Implementation.Common.Base;
using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Presentation.Models.View;
using SYS_SGI.Presentation.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SYS_SGI.Presentation.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Metodo para validar Session expirada
        /// </summary>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Request.Url != null) Response.RemoveOutputCacheItem(Request.Url.AbsolutePath);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            if (filterContext.HttpContext.Session[Constantes.Sesion.Usuario.Login] == null && !filterContext.IsChildAction)
            {
                var actionName = filterContext.RouteData.Values["action"].ToString();
                var controllerName = filterContext.Controller.ToString();
                var controllerType = filterContext.Controller.GetType();
                var method = controllerType.GetMethod(actionName);
                var returnType = method.ReturnType;

                if (returnType.Equals(typeof(JsonResult)))
                {
                    filterContext.Result = new JsonResult()
                    {
                        Data = new AppResponse()
                        {
                            Code = Enums.Response.SessionExpired,
                            Description = string.Concat("Ocurrió un error: <\br><\br>", string.Concat("<b>Controller:</b> <\br>", controllerName, " - ", "<b>Action: </b><\br> ", actionName, "<b>Metodo: </b><\br>", method))
                        }
                    };

                    return;
                }

                //TODO: Revisar en que casos entra a esta excepcion
                if (returnType.Equals(typeof(ActionResult)))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Error",
                        action = "SessionExpired"
                    }));

                    return;
                }
            }

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// Metodo para obtener Mensaje de validacion:
        /// Requiet
        /// Format
        /// Long
        /// etc.
        /// </summary>
        public AppResponse GetModelStateErrors()
        {
            var errosDescription = new List<string>();

            foreach (var modelState in ModelState.Values)
            {
                errosDescription.AddRange(modelState.Errors.Select(error => "-" + error.ErrorMessage));
            }

            return new AppResponse { Description = string.Join("<br/>", errosDescription) };
        }

        public ActionResult Auditoria(Audity entity)
        {
            return PartialView(new VMBase { item = entity });
        }

        /// <summary>
        /// Metodo para exportar grilla a excel
        /// </summary>
        public FileResult ExportExcel(string arg)
        {
            var byts = new ExportGridGmd().Excel(arg);
            return File(byts, System.Net.Mime.MediaTypeNames.Application.Octet, "grilla.pdf");
        }

        public FileResult ExportPdf(string arg)
        {
            var byts = new ExportGridGmd().Excel(arg);
            return File(byts, System.Net.Mime.MediaTypeNames.Application.Octet);
        }

        public string GetUrlRoot()
        {
            return Server.MapPath("~/FilesTemp/");
        }
        public int GetMaxSizeBytesFile()
        {
            try
            {
                return int.Parse(ConfigurationManager.ConnectionStrings["maxSizeFile"].ConnectionString);
            }
            catch (Exception)
            {
                return 5242880;
            }
        }
    }

    public class AppPresentationError : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            /*Registro Error Elmah*/
            var context = HttpContext.Current;
            var error = new Error(filterContext.Exception, context);
            var log = ErrorLog.GetDefault(context);
            log.Log(error);

            var actionName = filterContext.RouteData.Values["action"].ToString();
            var controllerName = filterContext.Controller.ToString();
            var controllerType = filterContext.Controller.GetType();
            var method = controllerType.GetMethod(actionName);
            var returnType = method.ReturnType;

            /*Mostrar mensaje*/
            var mensajeError = string.Concat(
                "<br><strong><u>Controller:</strong></u> ", controllerName,
                "<br><strong><u>Action:</strong></u> ", actionName,
                "<br><strong><u>Mensaje:</strong></u> ", filterContext.Exception.Message,
                "<br><strong><u>stackTrace:</strong></u> ", filterContext.Exception.StackTrace);

            if (returnType == typeof(JsonResult))
            {
                filterContext.Result = new JsonResult
                {
                    Data = new AppResponse
                    {
                        Description = string.Concat("Ocurrió un error: ", mensajeError)
                    }
                };
            }

            if (returnType == typeof(ActionResult))
            {
                var model2 = new HandleErrorInfo(filterContext.Exception, "Error", "NotFound");
                var result = new ViewResult
                {
                    ViewName = "Error",
                    MasterName = "",
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model2),
                    TempData = filterContext.Controller.TempData
                };

                filterContext.Result = result;
                filterContext.ExceptionHandled = true;
                return;
            }

            filterContext.ExceptionHandled = true;

            base.OnException(filterContext);
        }
    }
}
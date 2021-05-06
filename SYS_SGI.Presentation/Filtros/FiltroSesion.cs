using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SYS_SGI.Presentation.Filtros
{
    public class FiltroSesion : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object usuario = filterContext.HttpContext.Session["UsuarioLogueado"];//xd
            if (usuario == null)
            {
                filterContext.Result = new RedirectResult("~/Logeo/Index");
            }
        }
    }
}
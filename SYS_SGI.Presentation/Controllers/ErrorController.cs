using SYS_SGI.Domain.Implementation.Entities.BASE;
using System.Web.Configuration;
using System.Web.Mvc;

namespace SYS_SGI.Presentation.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Default(string mensaje)
        {
            return PartialView();
        }

        public JsonResult NotFound(string aspxerrorpath)
        {
            AppResponse response = new AppResponse { Code = SYS_SGI.Domain.Implementation.Common.Base.Enums.Response.Error, Description = aspxerrorpath };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AccessDenied()
        {
            return PartialView();
        }
        public ActionResult SessionExpired()
        {
            return RedirectToAction("Index","Home");           
        }

    }
}
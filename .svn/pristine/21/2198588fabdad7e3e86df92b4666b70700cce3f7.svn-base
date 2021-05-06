using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SYS_SGI.Presentation.Controllers
{
     [AppPresentationError]
    public class ConfirmacionController : BaseController
    {
        public ActionResult Index(string controlador,string accion,string parametros,string mensaje,string callBackMethod)
        {
            ViewBag.Controlador = controlador;
            ViewBag.Accion = accion;
            ViewBag.Parametros = parametros;
            ViewBag.Mensaje = mensaje;
            ViewBag.CallBackMethod = callBackMethod;
            return PartialView();
        }

    }
}

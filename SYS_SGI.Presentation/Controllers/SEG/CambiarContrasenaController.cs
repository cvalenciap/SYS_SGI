using SYS_SGI.Domain.Implementation.Common.Base;
using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.SEG;
using SYS_SGI.Presentation.Models.View;
using SYS_SGI.Presentation.Utilities;
using GMD.CustomHtmlHelpers.HtmlGenericGrid;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Web.Configuration;

namespace SYS_SGI.Presentation.Controllers
{
    [AppPresentationError]
    public class CambiarContrasenaController : BaseController
    {
        private static AppResponse appResponse = new AppResponse();
        ServiceSecurity.AccesoClient acceso = new ServiceSecurity.AccesoClient();
        ServiceSecurity.UsuariosLogic usuariosLogic = new ServiceSecurity.UsuariosLogic();

        public ActionResult Index()
        {
            ViewBag.Usuario = Login.Obtener.Usuario.Login();

            return PartialView();
        }

        [HttpPost]
        public JsonResult Guardar(string contrasenaNew)
        {
            int CodigoSistema = Convert.ToInt32(WebConfigurationManager.AppSettings["CodigoSistema"]);

            var appResponseService = acceso.CambiarContrasena(int.Parse(Login.Obtener.Usuario.Codigo()), contrasenaNew);

            appResponse.Code = appResponseService.Code;
            appResponse.Description = appResponseService.Description;
            
            //if (usuariosLogic != null)
            //    Login.Envio.Usuario(usuariosLogic.Usuario, usuariosLogic.CodigoUsuario, usuariosLogic.NombreCompleto, usuariosLogic.Foto, usuariosLogic.Email);
            //else
            //    usuariosLogic = new ServiceSecurity.UsuariosLogic();

            //return Json(usuariosLogic, JsonRequestBehavior.AllowGet);

            //if (base.ModelState.IsValid)
            //{
            //    formMantenimiento.parametro.UsuarioCreacion = formMantenimiento.parametro.UsuarioModificacion = Login.Obtener.Usuario.Login();
            //    formMantenimiento.parametro.Funcional = true;
            //    formMantenimiento.parametro.Valor = "PN-AES";

            //    if (formMantenimiento.parametro.InfoMovs.ACCION == Enums.Action.New)
            //        appResponse = _parametroAppService.MantenimientoNuevo(new List<ParametroLogic> { formMantenimiento.parametro });
            //    else appResponse = _parametroAppService.MantenimientoEditar(new List<ParametroLogic> { formMantenimiento.parametro });
            //}
            //else
            //{
            //    appResponse = GetModelStateErrors();
            //}
            return Json(appResponse);
        }
    }
}

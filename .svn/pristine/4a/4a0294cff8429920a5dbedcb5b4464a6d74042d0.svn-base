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
    public class LogeoController : Controller
    {
        #region Logeo        
        ServiceSecurity.AccesoClient acceso = new ServiceSecurity.AccesoClient();
        ServiceSecurity.UsuariosLogic usuariosLogic = new ServiceSecurity.UsuariosLogic();
        ServiceSecurity.SistemaLogic sistemaLogic = new ServiceSecurity.SistemaLogic();
        ServiceSecurity.EmpresaLogic empresaLogic = new ServiceSecurity.EmpresaLogic();

        public ActionResult Index()
        {
            int CodigoEmpresa = Convert.ToInt32(WebConfigurationManager.AppSettings["CodigoEmpresa"]);

            empresaLogic = acceso.ObtenerEmpresa(CodigoEmpresa);
            Login.Envio.Empresa(empresaLogic.CodigoEmpresa.ToString(), empresaLogic.RazonSocial, empresaLogic.Alias, empresaLogic.Ruc, empresaLogic.RutaImagenLogo, empresaLogic.RutaImagenLogoMini);

            return View();
        }
        
        public ActionResult validarSeguridad(string usuario, string contrasena)
        {           
            int CodigoSistema = Convert.ToInt32(WebConfigurationManager.AppSettings["CodigoSistema"]);

            usuariosLogic = acceso.Login(usuario, contrasena, CodigoSistema);

            if (usuariosLogic != null)
                Login.Envio.Usuario(usuariosLogic.Usuario, usuariosLogic.CodigoUsuario, usuariosLogic.NombreCompleto, usuariosLogic.Foto, usuariosLogic.Email);            
            else
                usuariosLogic = new ServiceSecurity.UsuariosLogic();

            return Json(usuariosLogic, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}

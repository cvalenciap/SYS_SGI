using SYS_SGI.Domain.Implementation.Common.Base;
using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.SEG;
using SYS_SGI.Presentation.Models.View;
using SYS_SGI.Presentation.Models.System;
using SYS_SGI.Presentation.Utilities;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System;
using System.Web.Configuration;

namespace SYS_SGI.Presentation.Controllers
{
    public class HomeController : Controller
    {
        ServiceSecurity.AccesoClient acceso = new ServiceSecurity.AccesoClient();
        ServiceSecurity.UsuariosLogic usuariosLogic = new ServiceSecurity.UsuariosLogic();
        ServiceSecurity.SistemaLogic sistemaLogic = new ServiceSecurity.SistemaLogic();

        public ActionResult Index()
        {
            if (Login.Obtener.Usuario.Login() == null)
                return Redirect(WebConfigurationManager.AppSettings["UrlLogin"].ToString());

            Session[Constantes.Sesion.Permisos.Lista_Modulos] = null;
            Session[Constantes.Sesion.Permisos.Lista_Opciones] = null;
            Session[Constantes.Sesion.Permisos.Lista_PermisosControlador] = null;

            int CodigoSistema = Convert.ToInt32(WebConfigurationManager.AppSettings["CodigoSistema"]);

            FormsAuthentication.SetAuthCookie(Login.Obtener.Usuario.Login(), false);
            sistemaLogic = acceso.Sistema_x_Token(ConfigurationManager.AppSettings["CLIENT_SITE_TOKEN"], Convert.ToInt32(Login.Obtener.Usuario.Codigo()), CodigoSistema);

            Login.Envio.Sistema(sistemaLogic.CodigoSistema, sistemaLogic.Nombre, sistemaLogic.Descripcion);
            var permisos_x_sistema = acceso.Usuario_x_Sistema(Login.Obtener.Usuario.Login(), Convert.ToInt32(Login.Obtener.Sistema.Codigo()));
            Login.Envio.Perfil(permisos_x_sistema[0].CodigoPerfil, permisos_x_sistema[0].Perfil);

            if (permisos_x_sistema != null)
            {
                Session[Constantes.Sesion.Permisos.Lista_Modulos] = permisos_x_sistema.DistinctBy(m => new { m.CodigoModulo }).ToList()
                .Select(item => new VMPermisosSystem.Modulo
                {
                    CodigoModulo = item.CodigoModulo,
                    Nombre = item.Modulo,
                    Glyphicon = item.GlyphiconModulo,
                    RutaImagen = item.RutaImagen,
                    ControladorModulo = item.ControladorModulo,
                    MetodoModulo = item.MetodoModulo
                }).OrderBy(x => x.Orden).ToList();

                Session[Constantes.Sesion.Permisos.Lista_Opciones] = permisos_x_sistema.DistinctBy(p => new { p.CodigoModulo, p.OpcionPadre, p.CodigoOpcion, p.Nombre, p.Controlador, p.Metodo }).ToList()
                .Select(item => new VMPermisosSystem.Opciones
                {
                    CodigoModulo = item.CodigoModulo,
                    OpcionPadre = item.OpcionPadre,
                    CodigoOpcion = item.CodigoOpcion,
                    Nombre = item.Nombre,
                    Controlador = item.Controlador,
                    Glyphicon = item.Glyphicon,
                    Metodo = item.Metodo
                }).OrderBy(x => x.Orden).ToList();

                Session[Constantes.Sesion.Permisos.Lista_PermisosControlador] = permisos_x_sistema.DistinctBy(p => new { p.Controlador, p.CodigoAccion }).ToList()
                .Select(item => new VMPermisosSystem.PermisoControlador
                {
                    Controlador = item.Controlador,
                    Opcion = item.Nombre,
                    CodigoAccion = item.CodigoAccion
                }).ToList();
            }

            return View();
        }
    }
}
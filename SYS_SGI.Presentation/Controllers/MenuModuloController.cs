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
using SYS_SGI.Presentation.Filtros;
using System.Collections.Generic;
using System.Web.Configuration;

namespace SYS_SGI.Presentation.Controllers.MOV
{
    public class MenuModuloController : Controller
    {
        ServiceSecurity.AccesoClient acceso = new ServiceSecurity.AccesoClient();

        public ActionResult Index(long codigoModulo)
        {
            string inicio = WebConfigurationManager.AppSettings["SITE_URL"].ToString();
            if (Login.Obtener.Usuario.Login() == null)
                return Redirect(WebConfigurationManager.AppSettings["UrlLogin"].ToString());

            Session[Constantes.Sesion.Permisos.Lista_Modulos] = null;
            Session[Constantes.Sesion.Permisos.Lista_Opciones] = null;
            Session[Constantes.Sesion.Permisos.Lista_PermisosControlador] = null;

            var permisos_x_sistema = acceso.Usuario_x_Sistema(Login.Obtener.Usuario.Login(), Convert.ToInt32(Login.Obtener.Sistema.Codigo())).Where(x => x.CodigoModulo == codigoModulo).ToList();

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
                    Metodo = item.Metodo
                }).OrderBy(x => x.Orden).ToList();

                Session[Constantes.Sesion.Permisos.Lista_PermisosControlador] = permisos_x_sistema.DistinctBy(p => new { p.Controlador, p.CodigoAccion }).ToList()
                .Select(item => new VMPermisosSystem.PermisoControlador
                {
                    Controlador = item.Controlador,
                    CodigoAccion = item.CodigoAccion
                }).ToList();
            }
            return View();
        }

    }
}

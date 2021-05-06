using SYS_SGI.Domain.Implementation.Common.Base;
using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Presentation.Models.View;
using SYS_SGI.Presentation.Utilities;
using SYS_SGI.Application.Implementation.REP;
using SYS_SGI.Domain.Implementation.LogicalEntities.REP;
using SYS_SGI.Domain.Implementation.LogicalEntities.PAR;
using SYS_SGI.Infrastructure.Data.Implementation.Base;
using SYS_SGI.Application.Implementation.PAR;
using SYS_SGI.Application.Implementation.MAN;
using SYS_SGI.Domain.Implementation.LogicalEntities.MAN;
using SYS_SGI.Application.Implementation.MOV;
using SYS_SGI.Domain.Implementation.LogicalEntities.MOV;
using GMD.CustomHtmlHelpers.HtmlGenericGrid;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Linq;
using System;
using System.Data;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Text.RegularExpressions;

namespace SYS_SGI.Presentation.Controllers.REP
{
    [AppPresentationError]
    public class DashboardController : BaseController
    {
        private readonly DashboardAppService _DashboardAppService = new DashboardAppService();
        private readonly ParametroDetalleAppService _ParametroDetalleAppService = new ParametroDetalleAppService();
        private readonly AlineamientoEstrategicoAppService _AlineamientoEstrategicoAppService = new AlineamientoEstrategicoAppService();
        private readonly AlineamientoConfiguracionAppService _AlineamientoConfiguracionAppService = new AlineamientoConfiguracionAppService();
        private readonly IndicadorAppService _IndicadorAppService = new IndicadorAppService();
        private static AppResponse appResponse = new AppResponse();
        private DashboardLogic Dashboard = new DashboardLogic();

        public ActionResult Index()
        {
            List<IndicadorLogic> listaIndicador = new List<IndicadorLogic>();
            List<ParametroDetalleLogic> listaTipoPeriodicidad = new List<ParametroDetalleLogic>();
            List<ParametroDetalleLogic> listaFiltroPeriodicidad = new List<ParametroDetalleLogic>();
            List<ParametroDetalleLogic> listaGuiaEmpresarial = new List<ParametroDetalleLogic>();
            List<ParametroDetalleLogic> listaTipoPLan = new List<ParametroDetalleLogic>();
            

            ViewBag.listaIndicador = listaIndicador;

            listaTipoPeriodicidad.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.TipoPeriodicidad).Where(x => x.Estado));
            listaTipoPeriodicidad.Insert(0, new ParametroDetalleLogic { Valor = "0", Nombre = Enums.ComboDefault.Todos });
            ViewBag.listaTipoPeriodicidad = listaTipoPeriodicidad;
            
            listaFiltroPeriodicidad.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Todos });
            ViewBag.listaFiltroPeriodicidad = listaFiltroPeriodicidad;

            listaGuiaEmpresarial.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.TipoGuiaEmpresarial).ToList().Where(x => x.Estado).Where(x => x.Estado && (x.NombreCorto == "Anexo N°1" || x.NombreCorto == "Anexo N°3")));
            listaGuiaEmpresarial.Insert(0, new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Seleccione });
            ViewBag.listaGuiaEmpresarial = listaGuiaEmpresarial;

            listaTipoPLan.AddRange(_ParametroDetalleAppService.Listar(Enums.Parametro.TipoPlan).ToList());
            ViewBag.listaTipoPLan = listaTipoPLan;

            return PartialView();
        }

        public JsonResult ObtenerFiltroPeriodicidad(string tipo)
        {
            int codigo;            
            bool numerico = int.TryParse(tipo, out codigo);

            List<ParametroDetalleLogic> listaFiltroPeriodicidad = new List<ParametroDetalleLogic>();
            listaFiltroPeriodicidad.AddRange(_ParametroDetalleAppService.Listar(codigo).Where(x => x.Estado));
            
            return Json(listaFiltroPeriodicidad, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Consultar(int CodigoTipoGuia, int codigoIndicador, int subTipoPeriodicidad, int tipoPeriodicidad, string anio, string fecha, int tipoPlan)
        {
            VMDashboard modelo = new VMDashboard();
            modelo.Dashboard = new DashboardLogic();

            List<ParametroDetalleLogic> listaFiltroPeriodicidad = new List<ParametroDetalleLogic>();
            
            modelo.dataDashboard1 = modelo.dataDashboard2  = modelo.dataDashboard3 = new List<Charts>();

            modelo.dataDashboard1 = llenarDashboard(CodigoTipoGuia, codigoIndicador, Enums.TipoRegistro.Ejecucion, subTipoPeriodicidad, tipoPeriodicidad, anio, fecha, tipoPlan);
            modelo.dataDashboard2 = llenarDashboard(CodigoTipoGuia, codigoIndicador, Enums.TipoRegistro.Meta, subTipoPeriodicidad, tipoPeriodicidad, anio, fecha, tipoPlan);
            modelo.dataDashboard3 = llenarDashboard(CodigoTipoGuia, codigoIndicador, Enums.TipoRegistro.LineaBase, subTipoPeriodicidad, tipoPeriodicidad, anio, fecha, tipoPlan);

            modelo.cantidad = modelo.dataDashboard1.Count + modelo.dataDashboard2.Count + modelo.dataDashboard3.Count;

            return Json(modelo, JsonRequestBehavior.AllowGet);
        }

        private List<Charts> llenarDashboard(int CodigoTipoGuia, int CodigoIndicador, int CodigoTipoRegistro, int subTipoPeriodicidad, int TipoPeriodicidad, string Anio, string Fecha, int tipoPlan)
        {
            List<Charts> dataDashboard = new List<Charts>();
            
            dataDashboard.AddRange(_DashboardAppService.Listar(CodigoTipoGuia, CodigoIndicador, CodigoTipoRegistro, TipoPeriodicidad, subTipoPeriodicidad, Anio, Fecha, tipoPlan).Select(x => new Charts()
            {
                label = x.TipoPeriodicidad.ToString(),
                y = x.Valor
            }));

            return dataDashboard;
        }

        public ActionResult CargarIndicadores(long CodigoGuiaEmpresarial)
        {          
            List<AlineamientoEstrategicoLogic> listaLineamientos = new List<AlineamientoEstrategicoLogic>();
            List<IndicadorLogic> listaIndicadores = new List<IndicadorLogic>();

            listaLineamientos = _AlineamientoEstrategicoAppService.Listar().Where(x => x.Estado && x.CodigoTipoGuiaEmpresarial == CodigoGuiaEmpresarial).ToList();
            listaIndicadores = listaLineamientos.Select(item => new IndicadorLogic {
                CodigoIndicador = item.CodigoIndicador,
                Nombre = Regex.Replace(item.Indicador, @"[\d-\.]", string.Empty)
            }).DistinctBy(x=>x.Nombre).OrderBy(x => x.Nombre).ToList();

            return Json(listaIndicadores, JsonRequestBehavior.AllowGet);
        }
    }
}
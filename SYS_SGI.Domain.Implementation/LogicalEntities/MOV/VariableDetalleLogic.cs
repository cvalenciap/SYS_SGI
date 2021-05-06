using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.Entities.MOV;
using SYS_SGI.Domain.Implementation.LogicalEntities.PAR;
using SYS_SGI.Domain.Implementation.LogicalEntities.REP;
using System.Collections.Generic;

namespace SYS_SGI.Domain.Implementation.LogicalEntities.MOV
{
    public class VariableDetalleLogic : VariableDetalle
    {
        public InfoMovs InfoMovs { get; set; }

        public int CantidadTotalRegistros { get; set; }

        public int NumFila { get; set; }

        public int CodigoTipoGuiaEmpresarial { get; set; }

        public int CodigoIndicador { get; set; }

        public int Item { get; set; }

        public string TipoValor { get; set; }

        public string UnidadMedida { get; set; }

        public string Periodicidad { get; set; }

        public string TipoRegistro { get; set; }

        public string FechaDesc { get; set; }

        public string Anio { get; set; }

        public int CodigoPeriodicidadValor { get; set; }

        public List<ParametroDetalleLogic> listaTipoValor { get; set; }

        public List<ReporteLogic> listaCabecera { get; set; }

        public string Variable { get; set; }      
        
        #region

        public AlineamientoConfiguracionLogic alineamientoConfiguracion { get; set; }

        public int CodigoAlineamiento1 { get; set; }

        public int CodigoAlineamiento2 { get; set; }

        public int CodigoAlineamiento3 { get; set; }

        public int CodigoAlineamiento4 { get; set; }

        public int CodigoAlineamiento5 { get; set; }

        public int CodigoAlineamiento6 { get; set; }

        public int CodigoAlineamiento7 { get; set; }

        public int CodigoAlineamiento8 { get; set; }

        public int CodigoAlineamiento9 { get; set; }

        public int CodigoAlineamiento10 { get; set; }

        public string Alineamiento1 { get; set; }

        public string Alineamiento2 { get; set; }

        public string Alineamiento3 { get; set; }

        public string Alineamiento4 { get; set; }

        public string Alineamiento5 { get; set; }

        public string Alineamiento6 { get; set; }

        public string Alineamiento7 { get; set; }

        public string Alineamiento8 { get; set; }

        public string Alineamiento9 { get; set; }

        public string Alineamiento10 { get; set; }

        public string Indicador { get; set; }
        #endregion
    }
}

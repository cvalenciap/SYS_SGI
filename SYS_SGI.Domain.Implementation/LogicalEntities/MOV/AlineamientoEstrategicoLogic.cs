using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.Entities.MOV;
using SYS_SGI.Domain.Implementation.LogicalEntities.PAR;
using SYS_SGI.Domain.Implementation.LogicalEntities.REP;
using System;
using System.Collections.Generic;

namespace SYS_SGI.Domain.Implementation.LogicalEntities.MOV
{
    public class AlineamientoEstrategicoLogic : AlineamientoEstrategico
    {
        public InfoMovs InfoMovs { get; set; }

        public int CantidadTotalRegistros { get; set; }

        public int NumFila { get; set; }

        public int Item { get; set; }
        
        public string GuiaEmpresarial { get; set; }

        public AlineamientoConfiguracionLogic alineamientoConfiguracion { get; set; }

        #region
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

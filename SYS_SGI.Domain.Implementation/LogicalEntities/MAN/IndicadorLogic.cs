using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.Entities.MAN;
using System.Collections.Generic;

namespace SYS_SGI.Domain.Implementation.LogicalEntities.MAN
{
    public class IndicadorLogic : Indicador
    {
        public InfoMovs InfoMovs { get; set; }

        public int CantidadTotalRegistros { get; set; }

        public int NumFila { get; set; }

        public string Area { get; set; }

        public string SentidoIndicador { get; set; }

        public string FuenteInformacion { get; set; }

        public string Responsable { get; set; }

        public string UnidadMedida { get; set; }

        public string TipoPeriodicidad { get; set; }

        public string FechaInicioDesc { get; set; }

        public string FechaFinDesc { get; set; }

        public int CodigoPeriodicidadValor { get; set; }

        public string variables { get; set; }

        public string AmbitoDesempeno { get; set; }

        public bool bIndicadorEspecifico { get; set; }

        public bool bIndicadorContinuoIncremento { get; set; }
        public bool bIndicadorContinuoDisminucion { get; set; }
        public bool bIndicadorContinuo { get; set; }

        //List<SelectListItem>
    }
}

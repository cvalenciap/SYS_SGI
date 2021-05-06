using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.Entities.MOV;

namespace SYS_SGI.Domain.Implementation.LogicalEntities.MOV
{
    public class AlineamientoConfiguracionLogic : AlineamientoConfiguracion
    {
        public InfoMovs InfoMovs { get; set; }

        public int CantidadTotalRegistros { get; set; }

        public int NumFila { get; set; }

        public string GuiaEmpresarial { get; set; }

        public string AlineamientoCompleto { get; set; }

        public string TipoAlineamiento1 { get; set; }

        public string TipoAlineamiento2 { get; set; }

        public string TipoAlineamiento3 { get; set; }

        public string TipoAlineamiento4 { get; set; }

        public string TipoAlineamiento5 { get; set; }

        public string TipoAlineamiento6 { get; set; }

        public string TipoAlineamiento7 { get; set; }

        public string TipoAlineamiento8 { get; set; }

        public string TipoAlineamiento9 { get; set; }

        public string TipoAlineamiento10 { get; set; }    
    }
}

using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.Entities.MAN;

namespace SYS_SGI.Domain.Implementation.LogicalEntities.MAN
{
    public class ResponsableLogic : Responsable
    {
        public InfoMovs InfoMovs { get; set; }

        public int CantidadTotalRegistros { get; set; }

        public int NumFila { get; set; }

        public string TipoDocumento { get; set; }

        public string Cargo { get; set; }

        public string Genero { get; set; }

        public string Responsable { get; set; }
    }
}

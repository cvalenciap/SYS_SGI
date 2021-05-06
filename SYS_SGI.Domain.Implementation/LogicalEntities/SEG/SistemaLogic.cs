using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.Entities.SEG;

namespace SYS_SGI.Domain.Implementation.LogicalEntities.SEG
{
    public class SistemaLogic: Sistema
    {
        public InfoMovs InfoMovs { get; set; }

        public int CantidadTotalRegistros { get; set; }

        public string Empresa { get; set; }
    }
}

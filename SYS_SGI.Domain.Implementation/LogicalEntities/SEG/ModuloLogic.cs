using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.Entities.SEG;

namespace SYS_SGI.Domain.Implementation.LogicalEntities.SEG
{
    public class ModuloLogic : Modulo
    {
        public InfoMovs InfoMovs { get; set; }

        public int CantidadTotalRegistros { get; set; }

        public string ModuloPadreDesc { get; set; }

        public string Sistema { get; set; }
    }
}

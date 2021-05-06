using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.Entities.SEG;

namespace SYS_SGI.Domain.Implementation.LogicalEntities.SEG
{
    public class AsignacionLogic : Asignacion
    {
        public InfoMovs InfoMovs { get; set; }

        public int CantidadTotalRegistros { get; set; }

        public string Sistema { get; set; }

        public string Usuario { get; set; }

        public string Perfil { get; set; }
    }
}


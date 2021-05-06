using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.Entities.MAN;

namespace SYS_SGI.Domain.Implementation.LogicalEntities.MAN
{
    public class AlertaLogic : Alerta
    {
        public InfoMovs InfoMovs { get; set; }

        public int CantidadTotalRegistros { get; set; }

        public int NumFila { get; set; }

        public string TipoAlerta { get; set; }
    }
}

using SYS_SGI.Domain.Implementation.LogicalEntities.MOV;
using System.Collections.Generic;

namespace SYS_SGI.Presentation.Models.View
{
    public class VMIndicadorDetalle
    {
        public IndicadorDetalleLogic IndicadorDetalle { get; set; }

        public List<string> listaVariables { get; set; }
    }
}

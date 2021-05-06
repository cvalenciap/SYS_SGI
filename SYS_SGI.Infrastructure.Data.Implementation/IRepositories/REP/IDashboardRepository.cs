using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.REP;
using System;
using System.Collections.Generic;

namespace SYS_SGI.Infrastructure.Data.Implementation.IRepositories.REP
{
    public interface IDashboardRepository : IDisposable
    {
        List<DashboardLogic> Listar(int CodigoTipoGuia, int CodigoIndicador, int CodigoTipoRegistro, int TipoPeriodicidad, int SubTipoPeriodicidad, string Anio, string Fecha, int tipoPlan);
    }
}
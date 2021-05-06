using SYS_SGI.Domain.Implementation.Common.Base;
using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.REP;
using SYS_SGI.Infrastructure.Data.Implementation.IRepositories.REP;
using SYS_SGI.Infrastructure.Data.Implementation.Repositories.REP;
using System.Collections.Generic;
using System;

namespace SYS_SGI.Application.Implementation.REP
{
    public class DashboardAppService : IDisposable
    {
        private readonly IDashboardRepository _DashboardRepository;

        public DashboardAppService()
        {
            _DashboardRepository = new DashboardRepository();
        }
        
        public List<DashboardLogic> Listar(int CodigoTipoGuia, int CodigoIndicador, int CodigoTipoRegistro, int TipoPeriodicidad, int SubTipoPeriodicidad, string Anio, string Fecha, int tipoPlan)
        {
            return _DashboardRepository.Listar(CodigoTipoGuia, CodigoIndicador, CodigoTipoRegistro, TipoPeriodicidad, SubTipoPeriodicidad, Anio, Fecha, tipoPlan);
        }
       
        public void Dispose()
        {
        }
    }
}
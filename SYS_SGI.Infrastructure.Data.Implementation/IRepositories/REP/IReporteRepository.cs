using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.REP;
using System;
using System.Collections.Generic;

namespace SYS_SGI.Infrastructure.Data.Implementation.IRepositories.REP
{
    public interface IReporteRepository : IDisposable
    {
        List<ReporteLogic> Paginacion(PaginateParams paginateParams);

        List<ReporteLogic> ObtenerCabecera(long codigoGuiaEmpresarial);

        List<ReporteLogic> ObtenerDetalle(long codigoGuiaEmpresarial);

        List<ReporteLogic> ObtenerDetalleIndicador(long codigoGuiaEmpresarial, long codigoTipoRegistro, long codigoTipoPeriodicidad, int tipoPlan);

        List<ReporteLogic> ObtenerHistoricoIndicador(long codigoGuiaEmpresarial, long codigoTipoRegistro, long codigoTipoPeriodicidad);

    }
}
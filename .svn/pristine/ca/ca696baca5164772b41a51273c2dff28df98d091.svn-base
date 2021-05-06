using SYS_SGI.Domain.Implementation.Common.Base;
using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.REP;
using SYS_SGI.Infrastructure.Data.Implementation.IRepositories.REP;
using SYS_SGI.Infrastructure.Data.Implementation.Repositories.REP;
using System.Collections.Generic;
using System;

namespace SYS_SGI.Application.Implementation.REP
{
    public class ReporteAppService : IDisposable
    {
        private readonly IReporteRepository _ReporteRepository;

        public ReporteAppService()
        {
            _ReporteRepository = new ReporteRepository();
        }
        
        public List<ReporteLogic> Paginacion(PaginateParams paginateParams)
        {
            return _ReporteRepository.Paginacion(paginateParams);
        }

        public List<ReporteLogic> ObtenerCabecera(long codigoGuiaEmpresarial)
        {
            return _ReporteRepository.ObtenerCabecera(codigoGuiaEmpresarial);
        }

        public List<ReporteLogic> ObtenerDetalle(long codigoGuiaEmpresarial)
        {
            return _ReporteRepository.ObtenerDetalle(codigoGuiaEmpresarial);
        }

        public List<ReporteLogic> ObtenerDetalleIndicador(long codigoGuiaEmpresarial, long codigoTipoRegistro, long codigoTipoPeriodicidad, int TipoPlan = 0)
        {
            return _ReporteRepository.ObtenerDetalleIndicador(codigoGuiaEmpresarial, codigoTipoRegistro, codigoTipoPeriodicidad, TipoPlan);
        }
        public List<ReporteLogic> ObtenerHistoricoIndicador(long codigoGuiaEmpresarial, long codigoTipoRegistro, long codigoTipoPeriodicidad)
        {
            return _ReporteRepository.ObtenerHistoricoIndicador(codigoGuiaEmpresarial, codigoTipoRegistro, codigoTipoPeriodicidad);
        }

        public void Dispose()
        {
        }
    }
}
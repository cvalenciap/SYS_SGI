using SYS_SGI.Domain.Implementation.Common.Base;
using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.MOV;
using SYS_SGI.Infrastructure.Data.Implementation.IRepositories.MOV;
using SYS_SGI.Infrastructure.Data.Implementation.Repositories.MOV;
using System.Collections.Generic;
using System;

namespace SYS_SGI.Application.Implementation.MOV
{
    public class IndicadorDetalleAppService : IDisposable
    {
        private readonly IIndicadorDetalleRepository _IndicadorDetalleRepository;

        public IndicadorDetalleAppService()
        {
            _IndicadorDetalleRepository = new IndicadorDetalleRepository();
        }

        public IndicadorDetalleLogic Obtener(float codigo)
        {
            return _IndicadorDetalleRepository.Obtener(codigo);
        }

        public List<IndicadorDetalleLogic> Listar(int CodigoAlineamientoEstrategico, int codigoTipoRegistro, int codigoTipoPlan, string fechaPeriodo)
        {
            return _IndicadorDetalleRepository.Listar(CodigoAlineamientoEstrategico, codigoTipoRegistro, codigoTipoPlan, fechaPeriodo);
        }

        public List<IndicadorDetalleLogic> Paginacion(PaginateParams paginateParams, float codigoGuiaEmpresarial, float tipoRegistro, int codigoTipoPlan)
        {
            return _IndicadorDetalleRepository.Paginacion(paginateParams, codigoGuiaEmpresarial, tipoRegistro, codigoTipoPlan);
        }

        public AppResponse MantenimientoNuevo(List<IndicadorDetalleLogic> lista, List<VariableFormulaLogic> listaFormula)
        {
            return _IndicadorDetalleRepository.Mantenimiento(lista, listaFormula, Enums.Action.New);
        }

        public AppResponse MantenimientoEditar(List<IndicadorDetalleLogic> lista)
        {
            return _IndicadorDetalleRepository.Mantenimiento(lista, new List<VariableFormulaLogic>(), Enums.Action.Edit);
        }

        public AppResponse MantenimientoEliminar(List<IndicadorDetalleLogic> lista)
        {
            return _IndicadorDetalleRepository.Mantenimiento(lista, new List<VariableFormulaLogic>() , Enums.Action.Delete);
        }

        public AppResponse MantenimientoActivar(List<IndicadorDetalleLogic> lista)
        {
            return _IndicadorDetalleRepository.Mantenimiento(lista, new List<VariableFormulaLogic>(), Enums.Action.Active);
        }

        public AppResponse MantenimientoHistoricoNuevo(List<IndicadorDetalleHistoricoLogic> listaIndicadorDetalleHistorico, List<VariableFormulaHistoricoLogic> listaVariableFormulaHistorico)
        {
            return _IndicadorDetalleRepository.MantenimientoHistorico(listaIndicadorDetalleHistorico, listaVariableFormulaHistorico, Enums.Action.New);
        }

        public void Dispose()
        {
        }
    }
}

using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.MOV;
using System.Collections.Generic;
using System;

namespace SYS_SGI.Infrastructure.Data.Implementation.IRepositories.MOV
{
    public interface IIndicadorDetalleRepository : IDisposable
    {
        IndicadorDetalleLogic Obtener(float codigo);
        List<IndicadorDetalleLogic> Listar(int CodigoAlineamientoEstrategico, int codigoTipoRegistro, int codigoTipoPlan, string fechaPeriodo);
        List<IndicadorDetalleLogic> Paginacion(PaginateParams paginateParams, float codigoGuiaEmpresarial, float tipoRegistro, int codigoTipoPlan);
        AppResponse Mantenimiento(List<IndicadorDetalleLogic> lista, List<VariableFormulaLogic> listaFormula, string accion);
        AppResponse MantenimientoHistorico(List<IndicadorDetalleHistoricoLogic> listaIndicadorDetalle, List<VariableFormulaHistoricoLogic> listaVariables, string accion);

    }
}

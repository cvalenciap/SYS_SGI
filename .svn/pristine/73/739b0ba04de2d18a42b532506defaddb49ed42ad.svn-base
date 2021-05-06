using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.MOV;
using System.Collections.Generic;
using System;

namespace SYS_SGI.Infrastructure.Data.Implementation.IRepositories.MOV
{
    public interface IVariableDetalleRepository : IDisposable
    {
        VariableDetalleLogic Obtener(float codigo);
        List<VariableDetalleLogic> Listar();
        List<VariableDetalleLogic> Paginacion(PaginateParams paginateParams, float codigoGuiaEmpresarial, float tipoRegistro);
        AppResponse Mantenimiento(List<VariableDetalleLogic> lista, string accion);
    }
}

using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.MOV;
using System.Collections.Generic;
using System;

namespace SYS_SGI.Infrastructure.Data.Implementation.IRepositories.MOV
{
    public interface IAlineamientoEstrategicoRepository : IDisposable
    {
        AlineamientoEstrategicoLogic Obtener(float codigo);
        List<AlineamientoEstrategicoLogic> Listar();
        List<AlineamientoEstrategicoLogic> Paginacion(PaginateParams paginateParams, float codigoGuiaEmpresarial);
        AppResponse Mantenimiento(List<AlineamientoEstrategicoLogic> lista, string accion);
    }
}

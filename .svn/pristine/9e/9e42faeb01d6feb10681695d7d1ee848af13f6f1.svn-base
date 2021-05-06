using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.PAR;
using System;
using System.Collections.Generic;
using System.Data;

namespace SYS_SGI.Infrastructure.Data.Implementation.IRepository.PAR
{
    public interface IParametroDetalleRepository : IDisposable
    {
        ParametroDetalleLogic Obtener(long codParametro, string codElemento);
        List<ParametroDetalleLogic> Listar(long codParametro);
        List<ParametroDetalleLogic> Paginacion(PaginateParams paginateParams);
        AppResponse Mantenimiento(List<ParametroDetalleLogic> lista, string accion);
    }
}
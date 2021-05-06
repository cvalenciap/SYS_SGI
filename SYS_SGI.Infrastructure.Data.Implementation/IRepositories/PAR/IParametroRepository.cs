using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.PAR;
using System;
using System.Collections.Generic;
using System.Data;

namespace SYS_SGI.Infrastructure.Data.Implementation.IRepositories.PAR
{
    public interface IParametroRepository : IDisposable
    {
        ParametroLogic Obtener(long codigoParametro);
        List<ParametroLogic> Listar();
        List<ParametroLogic> Paginacion(PaginateParams paginateParams);
        AppResponse Mantenimiento(List<ParametroLogic> listParametro, string accion);
    }
}
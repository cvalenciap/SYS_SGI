using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.MAN;
using System.Collections.Generic;
using System;

namespace SYS_SGI.Infrastructure.Data.Implementation.IRepositories.MAN
{
    public interface IAlertaRepository : IDisposable
    {
        AlertaLogic Obtener(float codigo);
        List<AlertaLogic> Listar();
        List<AlertaLogic> Paginacion(PaginateParams paginateParams);
        AppResponse Mantenimiento(List<AlertaLogic> lista, string accion);
    }
}

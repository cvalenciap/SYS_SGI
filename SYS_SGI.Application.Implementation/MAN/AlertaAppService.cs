using SYS_SGI.Domain.Implementation.Common.Base;
using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.MAN;
using SYS_SGI.Infrastructure.Data.Implementation.IRepositories.MAN;
using SYS_SGI.Infrastructure.Data.Implementation.Repositories.MAN;
using System.Collections.Generic;
using System;

namespace SYS_SGI.Application.Implementation.MAN
{
    public class AlertaAppService : IDisposable
    {
        private readonly IAlertaRepository _AlertaRepository;

        public AlertaAppService()
        {
            _AlertaRepository = new AlertaRepository();
        }

        public AlertaLogic Obtener(float codigo)
        {
            return _AlertaRepository.Obtener(codigo);
        }

        public List<AlertaLogic> Listar()
        {
            return _AlertaRepository.Listar();
        }

        public List<AlertaLogic> Paginacion(PaginateParams paginateParams)
        {
            return _AlertaRepository.Paginacion(paginateParams);
        }

        public AppResponse MantenimientoNuevo(List<AlertaLogic> lista)
        {
            return _AlertaRepository.Mantenimiento(lista, Enums.Action.New);
        }

        public AppResponse MantenimientoEditar(List<AlertaLogic> lista)
        {
            return _AlertaRepository.Mantenimiento(lista, Enums.Action.Edit);
        }

        public AppResponse MantenimientoEliminar(List<AlertaLogic> lista)
        {
            return _AlertaRepository.Mantenimiento(lista, Enums.Action.Delete);
        }

        public void Dispose()
        {
        }
    }
}

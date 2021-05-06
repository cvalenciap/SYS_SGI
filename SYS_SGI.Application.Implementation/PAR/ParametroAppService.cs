using SYS_SGI.Domain.Implementation.Common.Base;
using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.PAR;
using SYS_SGI.Infrastructure.Data.Implementation.IRepositories.PAR;
using SYS_SGI.Infrastructure.Data.Implementation.Repositories.PAR;
using System.Collections.Generic;
using System;
using System.Data;

namespace SYS_SGI.Application.Implementation.PAR
{
    public class ParametroAppService : IDisposable
    {
        private readonly IParametroRepository _ParametroRepository;

        public ParametroAppService()
        {
            _ParametroRepository = new ParametroRepository();
        }

        public List<ParametroLogic> Listar()
        {
            return _ParametroRepository.Listar();
        }

        public ParametroLogic Obtener(long item)
        {
            return _ParametroRepository.Obtener(item);
        }

        public AppResponse MantenimientoNuevo(List<ParametroLogic> item)
        {
            return _ParametroRepository.Mantenimiento(item, Enums.Action.New);
        }
        public AppResponse MantenimientoEditar(List<ParametroLogic> item)
        {
            return _ParametroRepository.Mantenimiento(item, Enums.Action.Edit);
        }
        public AppResponse MantenimientoEliminar(List<ParametroLogic> item)
        {
            return _ParametroRepository.Mantenimiento(item, Enums.Action.Delete);
        }

        public List<ParametroLogic> Paginacion(PaginateParams paginateParams)
        {
            return _ParametroRepository.Paginacion(paginateParams);
        }

        public void Dispose()
        {
        }
    }
}
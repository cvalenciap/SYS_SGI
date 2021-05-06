using SYS_SGI.Domain.Implementation.Common.Base;
using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.MOV;
using SYS_SGI.Infrastructure.Data.Implementation.IRepositories.MOV;
using SYS_SGI.Infrastructure.Data.Implementation.Repositories.MOV;
using System.Collections.Generic;
using System;

namespace SYS_SGI.Application.Implementation.MOV
{
    public class AlineamientoConfiguracionAppService : IDisposable
    {
        private readonly IAlineamientoConfiguracionRepository _AlineamientoConfiguracionRepository;

        public AlineamientoConfiguracionAppService()
        {
            _AlineamientoConfiguracionRepository = new AlineamientoConfiguracionRepository();
        }

        public AlineamientoConfiguracionLogic Obtener(float codigo)
        {
            return _AlineamientoConfiguracionRepository.Obtener(codigo);
        }

        public List<AlineamientoConfiguracionLogic> Listar()
        {
            return _AlineamientoConfiguracionRepository.Listar();
        }

        public List<AlineamientoConfiguracionLogic> Paginacion(PaginateParams paginateParams)
        {
            return _AlineamientoConfiguracionRepository.Paginacion(paginateParams);
        }

        public AppResponse MantenimientoNuevo(List<AlineamientoConfiguracionLogic> lista)
        {
            return _AlineamientoConfiguracionRepository.Mantenimiento(lista, Enums.Action.New);
        }

        public AppResponse MantenimientoEditar(List<AlineamientoConfiguracionLogic> lista)
        {
            return _AlineamientoConfiguracionRepository.Mantenimiento(lista, Enums.Action.Edit);
        }

        public AppResponse MantenimientoEliminar(List<AlineamientoConfiguracionLogic> lista)
        {
            return _AlineamientoConfiguracionRepository.Mantenimiento(lista, Enums.Action.Delete);
        }

        public void Dispose()
        {
        }
    }
}

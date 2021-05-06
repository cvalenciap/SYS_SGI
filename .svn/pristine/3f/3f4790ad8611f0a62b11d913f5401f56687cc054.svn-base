using SYS_SGI.Domain.Implementation.Common.Base;
using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.MOV;
using SYS_SGI.Infrastructure.Data.Implementation.IRepositories.MOV;
using SYS_SGI.Infrastructure.Data.Implementation.Repositories.MOV;
using System.Collections.Generic;
using System;

namespace SYS_SGI.Application.Implementation.MOV
{
    public class AlineamientoEstrategicoAppService : IDisposable
    {
        private readonly IAlineamientoEstrategicoRepository _AlineamientoEstrategicoRepository;

        public AlineamientoEstrategicoAppService()
        {
            _AlineamientoEstrategicoRepository = new AlineamientoEstrategicoRepository();
        }

        public AlineamientoEstrategicoLogic Obtener(float codigo)
        {
            return _AlineamientoEstrategicoRepository.Obtener(codigo);
        }

        public List<AlineamientoEstrategicoLogic> Listar()
        {
            return _AlineamientoEstrategicoRepository.Listar();
        }

        public List<AlineamientoEstrategicoLogic> Paginacion(PaginateParams paginateParams, float codigoGuiaEmpresarial)
        {
            return _AlineamientoEstrategicoRepository.Paginacion(paginateParams, codigoGuiaEmpresarial);
        }

        public AppResponse MantenimientoNuevo(List<AlineamientoEstrategicoLogic> lista)
        {
            return _AlineamientoEstrategicoRepository.Mantenimiento(lista, Enums.Action.New);
        }

        public AppResponse MantenimientoEditar(List<AlineamientoEstrategicoLogic> lista)
        {
            return _AlineamientoEstrategicoRepository.Mantenimiento(lista, Enums.Action.Edit);
        }

        public AppResponse MantenimientoEliminar(List<AlineamientoEstrategicoLogic> lista)
        {
            return _AlineamientoEstrategicoRepository.Mantenimiento(lista, Enums.Action.Delete);
        }

        public AppResponse MantenimientoActivar(List<AlineamientoEstrategicoLogic> lista)
        {
            return _AlineamientoEstrategicoRepository.Mantenimiento(lista, Enums.Action.Active);
        }

        public void Dispose()
        {
        }
    }
}

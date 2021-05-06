using SYS_SGI.Domain.Implementation.Common.Base;
using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.MOV;
using SYS_SGI.Infrastructure.Data.Implementation.IRepositories.MOV;
using SYS_SGI.Infrastructure.Data.Implementation.Repositories.MOV;
using System.Collections.Generic;
using System;

namespace SYS_SGI.Application.Implementation.MOV
{
    public class VariableDetalleAppService : IDisposable
    {
        private readonly IVariableDetalleRepository _VariableDetalleRepository;

        public VariableDetalleAppService()
        {
            _VariableDetalleRepository = new VariableDetalleRepository();
        }

        public VariableDetalleLogic Obtener(float codigo)
        {
            return _VariableDetalleRepository.Obtener(codigo);
        }

        public List<VariableDetalleLogic> Listar()
        {
            return _VariableDetalleRepository.Listar();
        }

        public List<VariableDetalleLogic> Paginacion(PaginateParams paginateParams, float codigoGuiaEmpresarial, float tipoRegistro)
        {
            return _VariableDetalleRepository.Paginacion(paginateParams, codigoGuiaEmpresarial, tipoRegistro);
        }

        public AppResponse MantenimientoNuevo(List<VariableDetalleLogic> lista)
        {
            return _VariableDetalleRepository.Mantenimiento(lista, Enums.Action.New);
        }

        public AppResponse MantenimientoEditar(List<VariableDetalleLogic> lista)
        {
            return _VariableDetalleRepository.Mantenimiento(lista, Enums.Action.Edit);
        }

        public AppResponse MantenimientoEliminar(List<VariableDetalleLogic> lista)
        {
            return _VariableDetalleRepository.Mantenimiento(lista, Enums.Action.Delete);
        }

        public AppResponse MantenimientoActivar(List<VariableDetalleLogic> lista)
        {
            return _VariableDetalleRepository.Mantenimiento(lista, Enums.Action.Active);
        }

        public void Dispose()
        {
        }
    }
}

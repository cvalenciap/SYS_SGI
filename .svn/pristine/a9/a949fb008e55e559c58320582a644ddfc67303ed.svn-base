using SYS_SGI.Domain.Implementation.Common.Base;
using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.PAR;
using SYS_SGI.Infrastructure.Data.Implementation.IRepository.PAR;
using SYS_SGI.Infrastructure.Data.Implementation.Repositories.PAR;
using System;
using System.Collections.Generic;
using System.Data;

namespace SYS_SGI.Application.Implementation.PAR
{
    public class ParametroDetalleAppService : IDisposable
    {
        private readonly IParametroDetalleRepository _ParametroDetalleRepository;

        public ParametroDetalleAppService()
        {
            _ParametroDetalleRepository = new ParametroDetalleRepository();
        }

        public List<ParametroDetalleLogic> Listar(long codParametro)
        {
            return _ParametroDetalleRepository.Listar(codParametro);
        }

        public ParametroDetalleLogic Obtener(long codParametro, string codElemento)
        {
            return _ParametroDetalleRepository.Obtener(codParametro, codElemento);
        }

        public AppResponse MantenimientoNuevo(List<ParametroDetalleLogic> item)
        {
            return _ParametroDetalleRepository.Mantenimiento(item, Enums.Action.New);
        }

        public AppResponse MantenimientoEditar(List<ParametroDetalleLogic> item)
        {
            return _ParametroDetalleRepository.Mantenimiento(item, Enums.Action.Edit);
        }

        public AppResponse MantenimientoEliminar(List<ParametroDetalleLogic> item)
        {

            return _ParametroDetalleRepository.Mantenimiento(item, Enums.Action.Delete);
        }

        public List<ParametroDetalleLogic> Paginacion(PaginateParams paginateParams)
        {
            return _ParametroDetalleRepository.Paginacion(paginateParams);
        }

        public List<ParametroDetalleLogic> FillListCombo(List<ParametroDetalleLogic> lista)
        {
            ParametroDetalleLogic parametroSeleccione = new ParametroDetalleLogic { CodigoElemento = "0", Nombre = Enums.ComboDefault.Seleccione };
            List<ParametroDetalleLogic> listaRes = new List<ParametroDetalleLogic> { parametroSeleccione };
            lista = lista ?? new List<ParametroDetalleLogic>();
            if (lista.Count == 1)
            {
                return lista;
            }
            else
            {
                listaRes.AddRange(lista);
                return listaRes;
            }
        }

        public void Dispose()
        {
        }
    }
}
using SYS_SGI.Domain.Implementation.Common.Base; 
using SYS_SGI.Domain.Implementation.Entities.BASE; 
using SYS_SGI.Domain.Implementation.LogicalEntities.MAN; 
using SYS_SGI.Infrastructure.Data.Implementation.IRepositories.MAN; 
using SYS_SGI.Infrastructure.Data.Implementation.Repositories.MAN; 
using System.Collections.Generic; 
using System; 

namespace SYS_SGI.Application.Implementation.MAN
{ 
	 public class VariableAppService : IDisposable 
	 { 
	 	 private readonly IVariableRepository _VariableRepository; 

	 	 public VariableAppService() 
	 	 { 
	 	 	 _VariableRepository = new VariableRepository(); 
	 	 } 

	 	 public VariableLogic Obtener(float codigo) 
	 	 { 
	 	 	 return _VariableRepository.Obtener(codigo); 
	 	 } 

	 	 public List<VariableLogic> Listar() 
	 	 { 
	 	 	 return _VariableRepository.Listar(); 
	 	 } 

	 	 public List<VariableLogic> Paginacion(PaginateParams paginateParams) 
	 	 { 
	 	 	 return _VariableRepository.Paginacion(paginateParams); 
	 	 } 
 
	 	 public AppResponse MantenimientoNuevo(List<VariableLogic> lista) 
	 	 { 
	 	 	 	 return _VariableRepository.Mantenimiento(lista, Enums.Action.New); 
	 	 } 
 
	 	 public AppResponse MantenimientoEditar(List<VariableLogic> lista) 
	 	 { 
	 	 	 	 return _VariableRepository.Mantenimiento(lista, Enums.Action.Edit); 
	 	 } 
 
	 	 public AppResponse MantenimientoEliminar(List<VariableLogic> lista) 
	 	 { 
	 	 	 	 return _VariableRepository.Mantenimiento(lista, Enums.Action.Delete); 
	 	 }

        public VariableLogic ObtenerByNombre(string nombreVariable)
        {
            return _VariableRepository.ObtenerByNombre(nombreVariable);
        }


        public void Dispose() 
	 	 { 
	 	 } 
	 } 
}

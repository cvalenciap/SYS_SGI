using SYS_SGI.Domain.Implementation.Common.Base; 
using SYS_SGI.Domain.Implementation.Entities.BASE; 
using SYS_SGI.Domain.Implementation.LogicalEntities.MAN; 
using SYS_SGI.Infrastructure.Data.Implementation.IRepositories.MAN; 
using SYS_SGI.Infrastructure.Data.Implementation.Repositories.MAN; 
using System.Collections.Generic; 
using System; 

namespace SYS_SGI.Application.Implementation.MAN
{ 
	 public class ResponsableAppService : IDisposable 
	 { 
	 	 private readonly IResponsableRepository _ResponsableRepository; 

	 	 public ResponsableAppService() 
	 	 { 
	 	 	 _ResponsableRepository = new ResponsableRepository(); 
	 	 } 

	 	 public ResponsableLogic Obtener(float codigo) 
	 	 { 
	 	 	 return _ResponsableRepository.Obtener(codigo); 
	 	 } 

	 	 public List<ResponsableLogic> Listar() 
	 	 { 
	 	 	 return _ResponsableRepository.Listar(); 
	 	 } 

	 	 public List<ResponsableLogic> Paginacion(PaginateParams paginateParams) 
	 	 { 
	 	 	 return _ResponsableRepository.Paginacion(paginateParams); 
	 	 } 
 
	 	 public AppResponse MantenimientoNuevo(List<ResponsableLogic> lista) 
	 	 { 
	 	 	 	 return _ResponsableRepository.Mantenimiento(lista, Enums.Action.New); 
	 	 } 
 
	 	 public AppResponse MantenimientoEditar(List<ResponsableLogic> lista) 
	 	 { 
	 	 	 	 return _ResponsableRepository.Mantenimiento(lista, Enums.Action.Edit); 
	 	 } 
 
	 	 public AppResponse MantenimientoEliminar(List<ResponsableLogic> lista) 
	 	 { 
	 	 	 	 return _ResponsableRepository.Mantenimiento(lista, Enums.Action.Delete); 
	 	 } 
 
	 	 public void Dispose() 
	 	 { 
	 	 } 
	 } 
}

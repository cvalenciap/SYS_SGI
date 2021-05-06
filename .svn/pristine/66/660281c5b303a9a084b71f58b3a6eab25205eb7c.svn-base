using SYS_SGI.Domain.Implementation.Common.Base; 
using SYS_SGI.Domain.Implementation.Entities.BASE; 
using SYS_SGI.Domain.Implementation.LogicalEntities.MAN; 
using SYS_SGI.Infrastructure.Data.Implementation.IRepositories.MAN; 
using SYS_SGI.Infrastructure.Data.Implementation.Repositories.MAN; 
using System.Collections.Generic; 
using System; 

namespace SYS_SGI.Application.Implementation.MAN
{ 
	 public class IndicadorAppService : IDisposable 
	 { 
	 	 private readonly IIndicadorRepository _IndicadorRepository; 

	 	 public IndicadorAppService() 
	 	 { 
	 	 	 _IndicadorRepository = new IndicadorRepository(); 
	 	 } 

	 	 public IndicadorLogic Obtener(float codigo) 
	 	 { 
	 	 	 return _IndicadorRepository.Obtener(codigo); 
	 	 } 

	 	 public List<IndicadorLogic> Listar() 
	 	 { 
	 	 	 return _IndicadorRepository.Listar(); 
	 	 } 

	 	 public List<IndicadorLogic> Paginacion(PaginateParams paginateParams) 
	 	 { 
	 	 	 return _IndicadorRepository.Paginacion(paginateParams); 
	 	 } 
 
	 	 public AppResponse MantenimientoNuevo(List<IndicadorLogic> lista) 
	 	 { 
	 	 	 	 return _IndicadorRepository.Mantenimiento(lista, Enums.Action.New); 
	 	 } 
 
	 	 public AppResponse MantenimientoEditar(List<IndicadorLogic> lista) 
	 	 { 
	 	 	 	 return _IndicadorRepository.Mantenimiento(lista, Enums.Action.Edit); 
	 	 } 
 
	 	 public AppResponse MantenimientoEliminar(List<IndicadorLogic> lista) 
	 	 { 
	 	 	 	 return _IndicadorRepository.Mantenimiento(lista, Enums.Action.Delete); 
	 	 } 
 
	 	 public void Dispose() 
	 	 { 
	 	 } 
	 } 
}

using SYS_SGI.Domain.Implementation.Entities.BASE; 
using SYS_SGI.Domain.Implementation.LogicalEntities.SEG; 
using System.Collections.Generic; 
using System; 

namespace SYS_SGI.Infrastructure.Data.Implementation.IRepositories.SEG
{ 
	 public interface IModuloRepository : IDisposable 
	 { 
	 	 ModuloLogic Obtener(float codigo); 
	 	 List<ModuloLogic> Listar(); 
	 	 List<ModuloLogic> Paginacion(PaginateParams paginateParams); 
	 	 AppResponse Mantenimiento(List<ModuloLogic> lista, string accion); 
	 } 
} 


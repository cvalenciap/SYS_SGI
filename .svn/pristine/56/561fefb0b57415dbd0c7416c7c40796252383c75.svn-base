using SYS_SGI.Domain.Implementation.Entities.BASE; 
using SYS_SGI.Domain.Implementation.LogicalEntities.SEG; 
using System.Collections.Generic; 
using System; 

namespace SYS_SGI.Infrastructure.Data.Implementation.IRepositories.SEG
{ 
	 public interface IAccionRepository : IDisposable 
	 { 
	 	 AccionLogic Obtener(float codigo); 
	 	 List<AccionLogic> Listar(); 
	 	 List<AccionLogic> Paginacion(PaginateParams paginateParams); 
	 	 AppResponse Mantenimiento(List<AccionLogic> lista, string accion); 
	 } 
} 


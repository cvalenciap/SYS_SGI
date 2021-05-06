using SYS_SGI.Domain.Implementation.Entities.BASE; 
using SYS_SGI.Domain.Implementation.LogicalEntities.SEG; 
using System.Collections.Generic; 
using System; 

namespace SYS_SGI.Infrastructure.Data.Implementation.IRepositories.SEG
{ 
	 public interface IOpcionRepository : IDisposable 
	 { 
	 	 OpcionLogic Obtener(float codigo); 
	 	 List<OpcionLogic> Listar(); 
	 	 List<OpcionLogic> Paginacion(PaginateParams paginateParams); 
	 	 AppResponse Mantenimiento(List<OpcionLogic> lista, string accion); 
	 } 
} 


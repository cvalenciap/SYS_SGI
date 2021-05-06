using SYS_SGI.Domain.Implementation.Entities.BASE; 
using SYS_SGI.Domain.Implementation.LogicalEntities.SEG; 
using System.Collections.Generic; 
using System; 

namespace SYS_SGI.Infrastructure.Data.Implementation.IRepositories.SEG
{ 
	 public interface ISistemaRepository : IDisposable 
	 { 
	 	 SistemaLogic Obtener(float codigo); 
	 	 List<SistemaLogic> Listar(); 
	 	 List<SistemaLogic> Paginacion(PaginateParams paginateParams); 
	 	 AppResponse Mantenimiento(List<SistemaLogic> lista, string accion); 
	 } 
} 


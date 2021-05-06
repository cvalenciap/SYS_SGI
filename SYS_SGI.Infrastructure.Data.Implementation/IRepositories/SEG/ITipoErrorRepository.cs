using SYS_SGI.Domain.Implementation.Entities.BASE; 
using SYS_SGI.Domain.Implementation.LogicalEntities.SEG; 
using System.Collections.Generic; 
using System; 

namespace SYS_SGI.Infrastructure.Data.Implementation.IRepositories.SEG
{ 
	 public interface ITipoErrorRepository : IDisposable 
	 { 
	 	 TipoErrorLogic Obtener(float codigo); 
	 	 List<TipoErrorLogic> Listar(); 
	 	 List<TipoErrorLogic> Paginacion(PaginateParams paginateParams); 
	 	 AppResponse Mantenimiento(List<TipoErrorLogic> lista, string accion); 
	 } 
} 


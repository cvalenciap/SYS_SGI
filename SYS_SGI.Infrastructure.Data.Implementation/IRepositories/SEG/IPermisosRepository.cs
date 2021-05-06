using SYS_SGI.Domain.Implementation.Entities.BASE; 
using SYS_SGI.Domain.Implementation.LogicalEntities.SEG; 
using System.Collections.Generic; 
using System; 

namespace SYS_SGI.Infrastructure.Data.Implementation.IRepositories.SEG
{ 
	 public interface IPermisosRepository : IDisposable 
	 { 
	 	 PermisosLogic Obtener(float codigo); 
	 	 List<PermisosLogic> Listar(); 
	 	 List<PermisosLogic> Paginacion(PaginateParams paginateParams); 
	 	 AppResponse Mantenimiento(List<PermisosLogic> lista, string accion); 
	 } 
} 


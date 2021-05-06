using SYS_SGI.Domain.Implementation.Entities.BASE; 
using SYS_SGI.Domain.Implementation.LogicalEntities.SEG; 
using System.Collections.Generic; 
using System; 

namespace SYS_SGI.Infrastructure.Data.Implementation.IRepositories.SEG
{ 
	 public interface IPerfilRepository : IDisposable 
	 { 
	 	 PerfilLogic Obtener(float codigo); 
	 	 List<PerfilLogic> Listar(); 
	 	 List<PerfilLogic> Paginacion(PaginateParams paginateParams); 
	 	 AppResponse Mantenimiento(List<PerfilLogic> lista, string accion); 
	 } 
} 


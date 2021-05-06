using SYS_SGI.Domain.Implementation.Entities.BASE; 
using SYS_SGI.Domain.Implementation.LogicalEntities.SEG; 
using System.Collections.Generic; 
using System; 

namespace SYS_SGI.Infrastructure.Data.Implementation.IRepositories.SEG
{ 
	 public interface IAsignacionRepository : IDisposable 
	 { 
	 	 AsignacionLogic Obtener(float codigo); 
	 	 List<AsignacionLogic> Listar(); 
	 	 List<AsignacionLogic> Paginacion(PaginateParams paginateParams); 
	 	 AppResponse Mantenimiento(List<AsignacionLogic> lista, string accion); 
	 } 
} 


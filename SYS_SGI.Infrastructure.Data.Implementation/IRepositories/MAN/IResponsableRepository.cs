using SYS_SGI.Domain.Implementation.Entities.BASE; 
using SYS_SGI.Domain.Implementation.LogicalEntities.MAN; 
using System.Collections.Generic; 
using System; 

namespace SYS_SGI.Infrastructure.Data.Implementation.IRepositories.MAN
{ 
	 public interface IResponsableRepository : IDisposable 
	 { 
	 	 ResponsableLogic Obtener(float codigo); 
	 	 List<ResponsableLogic> Listar(); 
	 	 List<ResponsableLogic> Paginacion(PaginateParams paginateParams); 
	 	 AppResponse Mantenimiento(List<ResponsableLogic> lista, string accion); 
	 } 
}

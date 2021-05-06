using SYS_SGI.Domain.Implementation.Entities.BASE; 
using SYS_SGI.Domain.Implementation.LogicalEntities.MAN; 
using System.Collections.Generic; 
using System; 

namespace SYS_SGI.Infrastructure.Data.Implementation.IRepositories.MAN
{ 
	 public interface IVariableRepository : IDisposable 
	 { 
	 	 VariableLogic Obtener(float codigo); 
	 	 List<VariableLogic> Listar(); 
	 	 List<VariableLogic> Paginacion(PaginateParams paginateParams); 
	 	 AppResponse Mantenimiento(List<VariableLogic> lista, string accion);
         VariableLogic ObtenerByNombre(string nombreCodigo);

     }
}

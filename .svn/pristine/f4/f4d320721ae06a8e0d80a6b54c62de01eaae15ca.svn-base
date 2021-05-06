using SYS_SGI.Domain.Implementation.Entities.BASE; 
using SYS_SGI.Domain.Implementation.LogicalEntities.MAN; 
using System.Collections.Generic; 
using System; 

namespace SYS_SGI.Infrastructure.Data.Implementation.IRepositories.MAN
{ 
	 public interface IIndicadorRepository : IDisposable 
	 { 
	 	 IndicadorLogic Obtener(float codigo); 
	 	 List<IndicadorLogic> Listar(); 
	 	 List<IndicadorLogic> Paginacion(PaginateParams paginateParams); 
	 	 AppResponse Mantenimiento(List<IndicadorLogic> lista, string accion); 
	 } 
}

using SYS_SGI.Domain.Implementation.Entities.BASE; 
using SYS_SGI.Domain.Implementation.LogicalEntities.SEG; 
using System.Collections.Generic; 
using System; 

namespace SYS_SGI.Infrastructure.Data.Implementation.IRepositories.SEG
{ 
	 public interface ILogErroresRepository : IDisposable 
	 { 
	 	 LogErroresLogic Obtener(float codigo); 
	 	 List<LogErroresLogic> Listar(); 
	 	 List<LogErroresLogic> Paginacion(PaginateParams paginateParams); 
	 	 AppResponse Mantenimiento(List<LogErroresLogic> lista, string accion); 
	 } 
} 


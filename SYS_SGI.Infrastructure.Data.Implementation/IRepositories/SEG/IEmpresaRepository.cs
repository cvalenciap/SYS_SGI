using SYS_SGI.Domain.Implementation.Entities.BASE; 
using SYS_SGI.Domain.Implementation.LogicalEntities.SEG; 
using System.Collections.Generic; 
using System; 

namespace SYS_SGI.Infrastructure.Data.Implementation.IRepositories.SEG
{ 
	 public interface IEmpresaRepository : IDisposable 
	 { 
	 	 EmpresaLogic Obtener(float codigo); 
	 	 List<EmpresaLogic> Listar(); 
	 	 List<EmpresaLogic> Paginacion(PaginateParams paginateParams); 
	 	 AppResponse Mantenimiento(List<EmpresaLogic> lista, string accion); 
	 } 
} 


using SYS_SGI.Domain.Implementation.Entities.BASE; 
using SYS_SGI.Domain.Implementation.LogicalEntities.MOV; 
using System.Collections.Generic; 
using System; 

namespace SYS_SGI.Infrastructure.Data.Implementation.IRepositories.MOV
{ 
	 public interface IAlineamientoConfiguracionRepository : IDisposable 
	 { 
	 	 AlineamientoConfiguracionLogic Obtener(float codigo); 
	 	 List<AlineamientoConfiguracionLogic> Listar(); 
	 	 List<AlineamientoConfiguracionLogic> Paginacion(PaginateParams paginateParams); 
	 	 AppResponse Mantenimiento(List<AlineamientoConfiguracionLogic> lista, string accion); 
	 } 
}

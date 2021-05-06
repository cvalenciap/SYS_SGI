using SYS_SGI.Domain.Implementation.Entities.BASE; 
using SYS_SGI.Domain.Implementation.Entities.SEG; 
using SYS_SGI.Domain.Implementation.LogicalEntities.SEG; 
using SYS_SGI.Infrastructure.Data.Implementation.Base; 
using SYS_SGI.Infrastructure.Data.Implementation.IRepositories.SEG; 
using Microsoft.Practices.EnterpriseLibrary.Data; 
using System.Collections.Generic; 
using System.Data.Common; 
using System.Data; 
using System.Linq; 
using System; 

namespace SYS_SGI.Infrastructure.Data.Implementation.Repositories.SEG
{ 
	 public class PermisosRepository : IPermisosRepository 
	 { 
	 	 public AppResponse oResponse = new AppResponse(); 

	 	 public PermisosLogic Obtener(float codigo) 
	 	 { 
	 	 	 PermisosLogic entidadLogic = new PermisosLogic(); 
	 	 	 try 
	 	 	 { 
	 	 	 	 SQLServer.OpenConection(); 
	 	 	 	 SQLServer.CreateCommand("SEG.USP_SEL_PERMISOS_OBTENER", CommandType.StoredProcedure, 
	 	 	 	 SQLServer.CreateParameter("CODIGO_PERMISOS", SqlDbType.BigInt, codigo)); 
	 	 	 	 using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection)) 
	 	 	 	 { 
	 	 	 	 	 entidadLogic = new GenericInstance<PermisosLogic>().readDataReader(oReader); 
	 	 	 	 } 
	 	 	 } 
	 	 	 catch (Exception ex) 
	 	 	 { 
	 	 	 	 throw ex; 
	 	 	 } 
	 	 	 finally 
	 	 	 { 
	 	 	 	 SQLServer.CloseConection(); 
	 	 	 } 
	 	 	 return entidadLogic; 
	 	 } 

	 	 public List<PermisosLogic> Listar() 
	 	 { 
	 	 	 List<PermisosLogic> listEntidadLogic = new List<PermisosLogic>(); 
	 	 	 Database oDatabase = DataBaseManager.GetDefaultDataBase(); 
	 	 	 DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SEG.USP_SEL_PERMISOS_LISTAR"); 
	 	 	 try 
	 	 	 { 
	 	 	 	 using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand)) 
	 	 	 	 { 
	 	 	 	 	 listEntidadLogic = new GenericInstance<PermisosLogic>().readDataReaderList(oReader); 
	 	 	 	 } 
	 	 	 } 
	 	 	 catch (Exception ex) 
	 	 	 { 
	 	 	 	 throw ex; 
	 	 	 } 
	 	 	 finally 
	 	 	 { 
	 	 	 	 if (oDbCommand.Connection != null && oDbCommand.Connection.State != ConnectionState.Closed) 
	 	 	 	 	 oDbCommand.Dispose(); 
	 	 	 } 
	 	 	 return listEntidadLogic; 
	 	 } 

	 	 public List<PermisosLogic> Paginacion(PaginateParams paginateParams) 
	 	 { 
	 	 	 List<PermisosLogic> listEntidadLogic = new List<PermisosLogic>(); 
	 	 	 try 
	 	 	 { 
	 	 	 	 SQLServer.OpenConection(); 
	 	 	 	 SQLServer.CreateCommand("SEG.USP_SEL_PERMISOS_PAG", CommandType.StoredProcedure, 
	 	 	 	 SQLServer.CreateParameter("SORTDIRECTION", SqlDbType.VarChar, paginateParams.SortDirection), 
	 	 	 	 SQLServer.CreateParameter("SORTCOLUMN", SqlDbType.VarChar, paginateParams.SortColumn), 
	 	 	 	 SQLServer.CreateParameter("PAGEINDEX", SqlDbType.Int, paginateParams.PageIndex), 
	 	 	 	 SQLServer.CreateParameter("ROWSPERPAGE", SqlDbType.Int, paginateParams.RowsPerPage), 
	 	 	 	 SQLServer.CreateParameter("PAGINATE", SqlDbType.Bit, paginateParams.IsPaginate), 
	 	 	 	 SQLServer.CreateParameter("FILTERS", SqlDbType.Structured, paginateParams.Filters)); 
	 	 	 	 using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection)) 
	 	 	 	 { 
	 	 	 	 	 listEntidadLogic = new GenericInstance<PermisosLogic>().readDataReaderList(oReader); 
	 	 	 	 } 
	 	 	 	 paginateParams.TotalRows = listEntidadLogic.Count > 0 ? listEntidadLogic[0].CantidadTotalRegistros : 0; 
	 	 	 } 
	 	 	 catch (Exception ex) 
	 	 	 { 
	 	 	 	 throw ex; 
	 	 	 } 
	 	 	 finally 
	 	 	 { 
	 	 	 	 SQLServer.CloseConection(); 
	 	 	 } 
	 	 	 return listEntidadLogic; 
	 	 } 
 
	 	 public AppResponse Mantenimiento(List<PermisosLogic> lista, string accion) 
	 	 { 
	 	 	 try 
	 	 	 { 
	 	 	 	 DataTable dt = new DataTable(); 
	 	 	 	 List<Permisos> listEntidad = lista.Cast<Permisos>().ToList(); 
	 	 	 	 dt = Util.ConvertListToDatatable(listEntidad); 
	 	 	 	 SQLServer.OpenConection(); 
	 	 	 	 SQLServer.CreateCommand("SEG.USP_MANT_PERMISOS", CommandType.StoredProcedure, 
	 	 	 	 SQLServer.CreateParameter("ACCION", SqlDbType.VarChar, accion), 
	 	 	 	 SQLServer.CreateParameter("PERMISOS_TYPE", SqlDbType.Structured, dt));
	 	 	 	 using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
	 	 	 	 { 
	 	 	 	 	 oResponse = new GenericInstance<AppResponse>().readDataReader(oReader); 
	 	 	 	 } 
	 	 	 } 
	 	 	 catch (Exception ex) 
	 	 	 { 
	 	 	 	 oResponse.SetException(string.Format("{ 0}: { 1}.", System.AppDomain.CurrentDomain.FriendlyName, ex.Message)); 
	 	 	 } 
	 	 	 return oResponse; 
	 	 } 
 
	 	 public void Dispose() 
	 	 { 
	 	 	 throw new NotImplementedException(); 
	 	 } 
	 } 
} 


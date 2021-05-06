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
	 public class TipoErrorRepository : ITipoErrorRepository 
	 { 
	 	 public AppResponse oResponse = new AppResponse(); 

	 	 public TipoErrorLogic Obtener(float codigo) 
	 	 { 
	 	 	 TipoErrorLogic entidadLogic = new TipoErrorLogic(); 
	 	 	 try 
	 	 	 { 
	 	 	 	 SQLServer.OpenConection(); 
	 	 	 	 SQLServer.CreateCommand("SEG.USP_SEL_TIPO_ERROR_OBTENER", CommandType.StoredProcedure, 
	 	 	 	 SQLServer.CreateParameter("CODIGO_TIPO_ERROR", SqlDbType.BigInt, codigo)); 
	 	 	 	 using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection)) 
	 	 	 	 { 
	 	 	 	 	 entidadLogic = new GenericInstance<TipoErrorLogic>().readDataReader(oReader); 
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

	 	 public List<TipoErrorLogic> Listar() 
	 	 { 
	 	 	 List<TipoErrorLogic> listEntidadLogic = new List<TipoErrorLogic>(); 
	 	 	 Database oDatabase = DataBaseManager.GetDefaultDataBase(); 
	 	 	 DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SEG.USP_SEL_TIPO_ERROR_LISTAR"); 
	 	 	 try 
	 	 	 { 
	 	 	 	 using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand)) 
	 	 	 	 { 
	 	 	 	 	 listEntidadLogic = new GenericInstance<TipoErrorLogic>().readDataReaderList(oReader); 
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

	 	 public List<TipoErrorLogic> Paginacion(PaginateParams paginateParams) 
	 	 { 
	 	 	 List<TipoErrorLogic> listEntidadLogic = new List<TipoErrorLogic>(); 
	 	 	 try 
	 	 	 { 
	 	 	 	 SQLServer.OpenConection(); 
	 	 	 	 SQLServer.CreateCommand("SEG.USP_SEL_TIPO_ERROR_PAG", CommandType.StoredProcedure, 
	 	 	 	 SQLServer.CreateParameter("SORTDIRECTION", SqlDbType.VarChar, paginateParams.SortDirection), 
	 	 	 	 SQLServer.CreateParameter("SORTCOLUMN", SqlDbType.VarChar, paginateParams.SortColumn), 
	 	 	 	 SQLServer.CreateParameter("PAGEINDEX", SqlDbType.Int, paginateParams.PageIndex), 
	 	 	 	 SQLServer.CreateParameter("ROWSPERPAGE", SqlDbType.Int, paginateParams.RowsPerPage), 
	 	 	 	 SQLServer.CreateParameter("PAGINATE", SqlDbType.Bit, paginateParams.IsPaginate), 
	 	 	 	 SQLServer.CreateParameter("FILTERS", SqlDbType.Structured, paginateParams.Filters)); 
	 	 	 	 using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection)) 
	 	 	 	 { 
	 	 	 	 	 listEntidadLogic = new GenericInstance<TipoErrorLogic>().readDataReaderList(oReader); 
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
 
	 	 public AppResponse Mantenimiento(List<TipoErrorLogic> lista, string accion) 
	 	 { 
	 	 	 try 
	 	 	 { 
	 	 	 	 DataTable dt = new DataTable(); 
	 	 	 	 List<TipoError> listEntidad = lista.Cast<TipoError>().ToList(); 
	 	 	 	 dt = Util.ConvertListToDatatable(listEntidad); 
	 	 	 	 SQLServer.OpenConection(); 
	 	 	 	 SQLServer.CreateCommand("SEG.USP_MANT_TIPO_ERROR", CommandType.StoredProcedure, 
	 	 	 	 SQLServer.CreateParameter("ACCION", SqlDbType.VarChar, accion), 
	 	 	 	 SQLServer.CreateParameter("TIPO_ERROR_TYPE", SqlDbType.Structured, dt));
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


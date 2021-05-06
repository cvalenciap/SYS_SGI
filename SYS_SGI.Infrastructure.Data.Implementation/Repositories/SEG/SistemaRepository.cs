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
	 public class SistemaRepository : ISistemaRepository 
	 { 
	 	 public AppResponse oResponse = new AppResponse(); 

	 	 public SistemaLogic Obtener(float codigo) 
	 	 { 
	 	 	 SistemaLogic entidadLogic = new SistemaLogic(); 
	 	 	 try 
	 	 	 { 
	 	 	 	 SQLServer.OpenConection(); 
	 	 	 	 SQLServer.CreateCommand("SEG.USP_SEL_SISTEMA_OBTENER", CommandType.StoredProcedure, 
	 	 	 	 SQLServer.CreateParameter("CODIGO_SISTEMA", SqlDbType.BigInt, codigo)); 
	 	 	 	 using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection)) 
	 	 	 	 { 
	 	 	 	 	 entidadLogic = new GenericInstance<SistemaLogic>().readDataReader(oReader); 
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

	 	 public List<SistemaLogic> Listar() 
	 	 { 
	 	 	 List<SistemaLogic> listEntidadLogic = new List<SistemaLogic>(); 
	 	 	 Database oDatabase = DataBaseManager.GetDefaultDataBase(); 
	 	 	 DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SEG.USP_SEL_SISTEMA_LISTAR"); 
	 	 	 try 
	 	 	 { 
	 	 	 	 using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand)) 
	 	 	 	 { 
	 	 	 	 	 listEntidadLogic = new GenericInstance<SistemaLogic>().readDataReaderList(oReader); 
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

	 	 public List<SistemaLogic> Paginacion(PaginateParams paginateParams) 
	 	 { 
	 	 	 List<SistemaLogic> listEntidadLogic = new List<SistemaLogic>(); 
	 	 	 try 
	 	 	 { 
	 	 	 	 SQLServer.OpenConection(); 
	 	 	 	 SQLServer.CreateCommand("SEG.USP_SEL_SISTEMA_PAG", CommandType.StoredProcedure, 
	 	 	 	 SQLServer.CreateParameter("SORTDIRECTION", SqlDbType.VarChar, paginateParams.SortDirection), 
	 	 	 	 SQLServer.CreateParameter("SORTCOLUMN", SqlDbType.VarChar, paginateParams.SortColumn), 
	 	 	 	 SQLServer.CreateParameter("PAGEINDEX", SqlDbType.Int, paginateParams.PageIndex), 
	 	 	 	 SQLServer.CreateParameter("ROWSPERPAGE", SqlDbType.Int, paginateParams.RowsPerPage), 
	 	 	 	 SQLServer.CreateParameter("PAGINATE", SqlDbType.Bit, paginateParams.IsPaginate), 
	 	 	 	 SQLServer.CreateParameter("FILTERS", SqlDbType.Structured, paginateParams.Filters)); 
	 	 	 	 using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection)) 
	 	 	 	 { 
	 	 	 	 	 listEntidadLogic = new GenericInstance<SistemaLogic>().readDataReaderList(oReader); 
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
 
	 	 public AppResponse Mantenimiento(List<SistemaLogic> lista, string accion) 
	 	 { 
	 	 	 try 
	 	 	 { 
	 	 	 	 DataTable dt = new DataTable(); 
	 	 	 	 List<Sistema> listEntidad = lista.Cast<Sistema>().ToList(); 
	 	 	 	 dt = Util.ConvertListToDatatable(listEntidad); 
	 	 	 	 SQLServer.OpenConection(); 
	 	 	 	 SQLServer.CreateCommand("SEG.USP_MANT_SISTEMA", CommandType.StoredProcedure, 
	 	 	 	 SQLServer.CreateParameter("ACCION", SqlDbType.VarChar, accion), 
	 	 	 	 SQLServer.CreateParameter("SISTEMA_TYPE", SqlDbType.Structured, dt));
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


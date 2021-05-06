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
	 public class EmpresaRepository : IEmpresaRepository 
	 { 
	 	 public AppResponse oResponse = new AppResponse(); 

	 	 public EmpresaLogic Obtener(float codigo) 
	 	 { 
	 	 	 EmpresaLogic entidadLogic = new EmpresaLogic(); 
	 	 	 try 
	 	 	 { 
	 	 	 	 SQLServer.OpenConection(); 
	 	 	 	 SQLServer.CreateCommand("SEG.USP_SEL_EMPRESA_OBTENER", CommandType.StoredProcedure, 
	 	 	 	 SQLServer.CreateParameter("CODIGO_EMPRESA", SqlDbType.BigInt, codigo)); 
	 	 	 	 using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection)) 
	 	 	 	 { 
	 	 	 	 	 entidadLogic = new GenericInstance<EmpresaLogic>().readDataReader(oReader); 
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

	 	 public List<EmpresaLogic> Listar() 
	 	 { 
	 	 	 List<EmpresaLogic> listEntidadLogic = new List<EmpresaLogic>(); 
	 	 	 Database oDatabase = DataBaseManager.GetDefaultDataBase(); 
	 	 	 DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SEG.USP_SEL_EMPRESA_LISTAR"); 
	 	 	 try 
	 	 	 { 
	 	 	 	 using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand)) 
	 	 	 	 { 
	 	 	 	 	 listEntidadLogic = new GenericInstance<EmpresaLogic>().readDataReaderList(oReader); 
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

	 	 public List<EmpresaLogic> Paginacion(PaginateParams paginateParams) 
	 	 { 
	 	 	 List<EmpresaLogic> listEntidadLogic = new List<EmpresaLogic>(); 
	 	 	 try 
	 	 	 { 
	 	 	 	 SQLServer.OpenConection(); 
	 	 	 	 SQLServer.CreateCommand("SEG.USP_SEL_EMPRESA_PAG", CommandType.StoredProcedure, 
	 	 	 	 SQLServer.CreateParameter("SORTDIRECTION", SqlDbType.VarChar, paginateParams.SortDirection), 
	 	 	 	 SQLServer.CreateParameter("SORTCOLUMN", SqlDbType.VarChar, paginateParams.SortColumn), 
	 	 	 	 SQLServer.CreateParameter("PAGEINDEX", SqlDbType.Int, paginateParams.PageIndex), 
	 	 	 	 SQLServer.CreateParameter("ROWSPERPAGE", SqlDbType.Int, paginateParams.RowsPerPage), 
	 	 	 	 SQLServer.CreateParameter("PAGINATE", SqlDbType.Bit, paginateParams.IsPaginate), 
	 	 	 	 SQLServer.CreateParameter("FILTERS", SqlDbType.Structured, paginateParams.Filters)); 
	 	 	 	 using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection)) 
	 	 	 	 { 
	 	 	 	 	 listEntidadLogic = new GenericInstance<EmpresaLogic>().readDataReaderList(oReader); 
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
 
	 	 public AppResponse Mantenimiento(List<EmpresaLogic> lista, string accion) 
	 	 { 
	 	 	 try 
	 	 	 { 
	 	 	 	 DataTable dt = new DataTable(); 
	 	 	 	 List<Empresa> listEntidad = lista.Cast<Empresa>().ToList(); 
	 	 	 	 dt = Util.ConvertListToDatatable(listEntidad); 
	 	 	 	 SQLServer.OpenConection(); 
	 	 	 	 SQLServer.CreateCommand("SEG.USP_MANT_EMPRESA", CommandType.StoredProcedure, 
	 	 	 	 SQLServer.CreateParameter("ACCION", SqlDbType.VarChar, accion), 
	 	 	 	 SQLServer.CreateParameter("EMPRESA_TYPE", SqlDbType.Structured, dt));
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


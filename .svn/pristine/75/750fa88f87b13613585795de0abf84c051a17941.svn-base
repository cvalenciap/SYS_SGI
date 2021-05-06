using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.Entities.MOV;
using SYS_SGI.Domain.Implementation.LogicalEntities.MOV;
using SYS_SGI.Infrastructure.Data.Implementation.Base;
using SYS_SGI.Infrastructure.Data.Implementation.IRepositories.MOV;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System;

namespace SYS_SGI.Infrastructure.Data.Implementation.Repositories.MOV
{
    public class AlineamientoEstrategicoRepository : IAlineamientoEstrategicoRepository
    {
        public AppResponse oResponse = new AppResponse();

        public AlineamientoEstrategicoLogic Obtener(float codigo)
        {
            AlineamientoEstrategicoLogic entidadLogic = new AlineamientoEstrategicoLogic();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("MOV.USP_SEL_ALINEAMIENTO_ESTRATEGICO_OBTENER", CommandType.StoredProcedure,
                SQLServer.CreateParameter("CODIGO_ALINEAMIENTO_ESTRATEGICO", SqlDbType.BigInt, codigo));
                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    entidadLogic = new GenericInstance<AlineamientoEstrategicoLogic>().readDataReader(oReader);
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

        public List<AlineamientoEstrategicoLogic> Listar()
        {
            List<AlineamientoEstrategicoLogic> listEntidadLogic = new List<AlineamientoEstrategicoLogic>();
            Database oDatabase = DataBaseManager.GetDefaultDataBase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("MOV.USP_SEL_ALINEAMIENTO_ESTRATEGICO_LISTAR");
            try
            {
                using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
                {
                    listEntidadLogic = new GenericInstance<AlineamientoEstrategicoLogic>().readDataReaderList(oReader);
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

        public List<AlineamientoEstrategicoLogic> Paginacion(PaginateParams paginateParams, float codigoGuiaEmpresarial)
        {
            List<AlineamientoEstrategicoLogic> listEntidadLogic = new List<AlineamientoEstrategicoLogic>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("MOV.USP_SEL_ALINEAMIENTO_ESTRATEGICO_PAG", CommandType.StoredProcedure,
                SQLServer.CreateParameter("CODIGO_GUIA_EMPRESARIAL", SqlDbType.Int, codigoGuiaEmpresarial),
                SQLServer.CreateParameter("SORTDIRECTION", SqlDbType.VarChar, paginateParams.SortDirection),
                SQLServer.CreateParameter("SORTCOLUMN", SqlDbType.VarChar, paginateParams.SortColumn),
                SQLServer.CreateParameter("PAGEINDEX", SqlDbType.Int, paginateParams.PageIndex),
                SQLServer.CreateParameter("ROWSPERPAGE", SqlDbType.Int, paginateParams.RowsPerPage),
                SQLServer.CreateParameter("PAGINATE", SqlDbType.Bit, paginateParams.IsPaginate),
                SQLServer.CreateParameter("FILTERS", SqlDbType.Structured, paginateParams.Filters));
                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    listEntidadLogic = new GenericInstance<AlineamientoEstrategicoLogic>().readDataReaderList(oReader);
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

        public AppResponse Mantenimiento(List<AlineamientoEstrategicoLogic> lista, string accion)
        {
            try
            {
                DataTable dt = new DataTable();
                List<AlineamientoEstrategico> listEntidad = lista.Cast<AlineamientoEstrategico>().ToList();
                dt = Util.ConvertListToDatatable(listEntidad);
                SQLServer.OpenConection();
                SQLServer.CreateCommand("MOV.USP_MANT_ALINEAMIENTO_ESTRATEGICO", CommandType.StoredProcedure,
                SQLServer.CreateParameter("ACCION", SqlDbType.VarChar, accion),
                SQLServer.CreateParameter("ALINEAMIENTO_ESTRATEGICO_TYPE", SqlDbType.Structured, dt));
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

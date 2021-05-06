using Microsoft.Practices.EnterpriseLibrary.Data;
using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.Entities.PAR;
using SYS_SGI.Domain.Implementation.LogicalEntities.PAR;
using SYS_SGI.Infrastructure.Data.Implementation.Base;
using SYS_SGI.Infrastructure.Data.Implementation.IRepository.PAR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace SYS_SGI.Infrastructure.Data.Implementation.Repositories.PAR
{
    public class ParametroDetalleRepository : IParametroDetalleRepository
    {
        public AppResponse oResponse = new AppResponse();

        public ParametroDetalleLogic Obtener(long codParametro, string codElemento)
        {
            ParametroDetalleLogic parametroDetalle = new ParametroDetalleLogic();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("PAR.USP_SEL_PARAMETRO_DETALLE_OBTENER", CommandType.StoredProcedure,
                SQLServer.CreateParameter("CODIGO_PARAMETRO", SqlDbType.BigInt, codParametro),
                SQLServer.CreateParameter("CODIGO_ELEMENTO", SqlDbType.VarChar, codElemento));
                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    parametroDetalle = new GenericInstance<ParametroDetalleLogic>().readDataReader(oReader);
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
            return parametroDetalle;
        }

        public List<ParametroDetalleLogic> Listar(long codParametro)
        {
            List<ParametroDetalleLogic> listParametroDetalle = new List<ParametroDetalleLogic>();
            Database oDatabase = DataBaseManager.GetDefaultDataBase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("PAR.USP_SEL_PARAMETRO_DETALLE_LISTAR");
            try
            {
                oDatabase.AddInParameter(oDbCommand, "@CODIGO_PARAMETRO", DbType.Int16, codParametro);
                using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
                {
                    listParametroDetalle = new GenericInstance<ParametroDetalleLogic>().readDataReaderList(oReader);
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
            return listParametroDetalle;
        }

        public List<ParametroDetalleLogic> Paginacion(PaginateParams paginateParams)
        {
            List<ParametroDetalleLogic> listPagParametroDetalle = new List<ParametroDetalleLogic>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("PAR.USP_SEL_PARAMETRO_DETALLE_PAG", CommandType.StoredProcedure,
                SQLServer.CreateParameter("SORTDIRECTION", SqlDbType.VarChar, paginateParams.SortDirection),
                SQLServer.CreateParameter("SORTCOLUMN", SqlDbType.VarChar, paginateParams.SortColumn),
                SQLServer.CreateParameter("PAGEINDEX", SqlDbType.Int, paginateParams.PageIndex),
                SQLServer.CreateParameter("ROWSPERPAGE", SqlDbType.Int, paginateParams.RowsPerPage),
                SQLServer.CreateParameter("PAGINATE", SqlDbType.Bit, paginateParams.IsPaginate),
                SQLServer.CreateParameter("FILTERS", SqlDbType.Structured, paginateParams.Filters));
                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    listPagParametroDetalle = new GenericInstance<ParametroDetalleLogic>().readDataReaderList(oReader);
                }
                paginateParams.TotalRows = listPagParametroDetalle.Count > 0 ? listPagParametroDetalle[0].CantidadTotalRegistros : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SQLServer.CloseConection();
            }
            return listPagParametroDetalle;
        }

        public AppResponse Mantenimiento(List<ParametroDetalleLogic> listParametroDetalle, string accion)
        {
            try
            {
                DataTable dtParametroDetalle = new DataTable();
                List<ParametroDetalle> listaParametroDetalle = listParametroDetalle.Cast<ParametroDetalle>().ToList();
                dtParametroDetalle = Util.ConvertListToDatatable(listaParametroDetalle);
                SQLServer.OpenConection();
                SQLServer.CreateCommand("PAR.USP_MANT_PARAMETRO_DETALLE", CommandType.StoredProcedure,
                SQLServer.CreateParameter("ACCION", SqlDbType.VarChar, accion),
                SQLServer.CreateParameter("PARAMETRO_DETALLE_TYPE", SqlDbType.Structured, dtParametroDetalle));
                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    oResponse = new GenericInstance<AppResponse>().readDataReader(oReader);
                }
            }
            catch (Exception ex)
            {
                oResponse.SetException(string.Format("{0}: {1}.", System.AppDomain.CurrentDomain.FriendlyName, ex.Message));
            }
            return oResponse;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
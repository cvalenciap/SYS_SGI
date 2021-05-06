using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.Entities.MAN;
using SYS_SGI.Domain.Implementation.LogicalEntities.MAN;
using SYS_SGI.Infrastructure.Data.Implementation.Base;
using SYS_SGI.Infrastructure.Data.Implementation.IRepositories.MAN;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System;

namespace SYS_SGI.Infrastructure.Data.Implementation.Repositories.MAN
{
    public class AlertaRepository : IAlertaRepository
    {
        public AppResponse oResponse = new AppResponse();

        public AlertaLogic Obtener(float codigo)
        {
            AlertaLogic entidadLogic = new AlertaLogic();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("MAN.USP_SEL_ALERTA_OBTENER", CommandType.StoredProcedure,
                SQLServer.CreateParameter("CODIGO_ALERTA", SqlDbType.BigInt, codigo));
                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    entidadLogic = new GenericInstance<AlertaLogic>().readDataReader(oReader);
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

        public List<AlertaLogic> Listar()
        {
            List<AlertaLogic> listEntidadLogic = new List<AlertaLogic>();
            Database oDatabase = DataBaseManager.GetDefaultDataBase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("MAN.USP_SEL_ALERTA_LISTAR");
            try
            {
                using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
                {
                    listEntidadLogic = new GenericInstance<AlertaLogic>().readDataReaderList(oReader);
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

        public List<AlertaLogic> Paginacion(PaginateParams paginateParams)
        {
            List<AlertaLogic> listEntidadLogic = new List<AlertaLogic>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("MAN.USP_SEL_ALERTA_PAG", CommandType.StoredProcedure,
                SQLServer.CreateParameter("SORTDIRECTION", SqlDbType.VarChar, paginateParams.SortDirection),
                SQLServer.CreateParameter("SORTCOLUMN", SqlDbType.VarChar, paginateParams.SortColumn),
                SQLServer.CreateParameter("PAGEINDEX", SqlDbType.Int, paginateParams.PageIndex),
                SQLServer.CreateParameter("ROWSPERPAGE", SqlDbType.Int, paginateParams.RowsPerPage),
                SQLServer.CreateParameter("PAGINATE", SqlDbType.Bit, paginateParams.IsPaginate),
                SQLServer.CreateParameter("FILTERS", SqlDbType.Structured, paginateParams.Filters));
                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    listEntidadLogic = new GenericInstance<AlertaLogic>().readDataReaderList(oReader);
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

        public AppResponse Mantenimiento(List<AlertaLogic> lista, string accion)
        {
            try
            {
                DataTable dt = new DataTable();
                List<Alerta> listEntidad = lista.Cast<Alerta>().ToList();
                dt = Util.ConvertListToDatatable(listEntidad);
                SQLServer.OpenConection();
                SQLServer.CreateCommand("MAN.USP_MANT_ALERTA", CommandType.StoredProcedure,
                SQLServer.CreateParameter("ACCION", SqlDbType.VarChar, accion),
                SQLServer.CreateParameter("ALERTA_TYPE", SqlDbType.Structured, dt));
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

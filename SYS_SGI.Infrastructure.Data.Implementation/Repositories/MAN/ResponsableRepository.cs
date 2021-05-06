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
    public class ResponsableRepository : IResponsableRepository
    {
        public AppResponse oResponse = new AppResponse();

        public ResponsableLogic Obtener(float codigo)
        {
            ResponsableLogic entidadLogic = new ResponsableLogic();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("MAN.USP_SEL_RESPONSABLE_OBTENER", CommandType.StoredProcedure,
                SQLServer.CreateParameter("CODIGO_RESPONSABLE", SqlDbType.BigInt, codigo));
                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    entidadLogic = new GenericInstance<ResponsableLogic>().readDataReader(oReader);
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

        public List<ResponsableLogic> Listar()
        {
            List<ResponsableLogic> listEntidadLogic = new List<ResponsableLogic>();
            Database oDatabase = DataBaseManager.GetDefaultDataBase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("MAN.USP_SEL_RESPONSABLE_LISTAR");
            try
            {
                using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
                {
                    listEntidadLogic = new GenericInstance<ResponsableLogic>().readDataReaderList(oReader);
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

        public List<ResponsableLogic> Paginacion(PaginateParams paginateParams)
        {
            List<ResponsableLogic> listEntidadLogic = new List<ResponsableLogic>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("MAN.USP_SEL_RESPONSABLE_PAG", CommandType.StoredProcedure,
                SQLServer.CreateParameter("SORTDIRECTION", SqlDbType.VarChar, paginateParams.SortDirection),
                SQLServer.CreateParameter("SORTCOLUMN", SqlDbType.VarChar, paginateParams.SortColumn),
                SQLServer.CreateParameter("PAGEINDEX", SqlDbType.Int, paginateParams.PageIndex),
                SQLServer.CreateParameter("ROWSPERPAGE", SqlDbType.Int, paginateParams.RowsPerPage),
                SQLServer.CreateParameter("PAGINATE", SqlDbType.Bit, paginateParams.IsPaginate),
                SQLServer.CreateParameter("FILTERS", SqlDbType.Structured, paginateParams.Filters));
                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    listEntidadLogic = new GenericInstance<ResponsableLogic>().readDataReaderList(oReader);
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

        public AppResponse Mantenimiento(List<ResponsableLogic> lista, string accion)
        {
            try
            {
                DataTable dt = new DataTable();
                List<Responsable> listEntidad = lista.Cast<Responsable>().ToList();
                dt = Util.ConvertListToDatatable(listEntidad);
                SQLServer.OpenConection();
                SQLServer.CreateCommand("MAN.USP_MANT_RESPONSABLE", CommandType.StoredProcedure,
                SQLServer.CreateParameter("ACCION", SqlDbType.VarChar, accion),
                SQLServer.CreateParameter("RESPONSABLE_TYPE", SqlDbType.Structured, dt));
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

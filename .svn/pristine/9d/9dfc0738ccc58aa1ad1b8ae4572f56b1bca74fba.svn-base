using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.Entities.PAR;
using SYS_SGI.Domain.Implementation.LogicalEntities.PAR;
using SYS_SGI.Infrastructure.Data.Implementation.Base;
using SYS_SGI.Infrastructure.Data.Implementation.IRepositories.PAR;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace SYS_SGI.Infrastructure.Data.Implementation.Repositories.PAR
{
    public class ParametroRepository : IParametroRepository
    {
        public AppResponse oResponse = new AppResponse();

        public ParametroLogic Obtener(long codigoParametro)
        {
            ParametroLogic parametro = new ParametroLogic();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("PAR.USP_SEL_PARAMETRO_OBTENER", CommandType.StoredProcedure,
                SQLServer.CreateParameter("CODIGO_PARAMETRO", SqlDbType.BigInt, codigoParametro));
                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    parametro = new GenericInstance<ParametroLogic>().readDataReader(oReader);
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
            return parametro;
        }

        public List<ParametroLogic> Listar()
        {
            List<ParametroLogic> listParametro = new List<ParametroLogic>();

            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("PAR.USP_SEL_PARAMETRO_LISTAR", CommandType.StoredProcedure);

                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    listParametro = new GenericInstance<ParametroLogic>().readDataReaderList(oReader);
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
            return listParametro;
        }

        public List<ParametroLogic> Paginacion(PaginateParams paginateParams)
        {
            List<ParametroLogic> listPagParametro = new List<ParametroLogic>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("PAR.USP_SEL_PARAMETRO_PAG", CommandType.StoredProcedure,
                SQLServer.CreateParameter("SORTDIRECTION", SqlDbType.VarChar, paginateParams.SortDirection),
                SQLServer.CreateParameter("SORTCOLUMN", SqlDbType.VarChar, paginateParams.SortColumn),
                SQLServer.CreateParameter("PAGEINDEX", SqlDbType.Int, paginateParams.PageIndex),
                SQLServer.CreateParameter("ROWSPERPAGE", SqlDbType.Int, paginateParams.RowsPerPage),
                SQLServer.CreateParameter("PAGINATE", SqlDbType.Bit, paginateParams.IsPaginate),
                SQLServer.CreateParameter("FILTERS", SqlDbType.Structured, paginateParams.Filters));

                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    listPagParametro = new GenericInstance<ParametroLogic>().readDataReaderList(oReader);
                }
                paginateParams.TotalRows = listPagParametro.Count > 0 ? listPagParametro[0].CantidadTotalRegistros : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SQLServer.CloseConection();
            }
            return listPagParametro;
        }

        public AppResponse Mantenimiento(List<ParametroLogic> listParametro, string accion)
        {
            try
            {
                DataTable dtParametro = new DataTable();
                List<Parametro> listEntidad = listParametro.Cast<Parametro>().ToList();
                dtParametro = Util.ConvertListToDatatable(listEntidad);
                SQLServer.OpenConection();
                SQLServer.CreateCommand("PAR.USP_MANT_PARAMETRO", CommandType.StoredProcedure,
                SQLServer.CreateParameter("ACCION", SqlDbType.VarChar, accion),
                SQLServer.CreateParameter("PARAMETRO_TYPE", SqlDbType.Structured, dtParametro));
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
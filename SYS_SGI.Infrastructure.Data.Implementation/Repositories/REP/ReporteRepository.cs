using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.REP;
using SYS_SGI.Infrastructure.Data.Implementation.Base;
using SYS_SGI.Infrastructure.Data.Implementation.IRepositories.REP;
using System;
using System.Collections.Generic;
using System.Data;

namespace SYS_SGI.Infrastructure.Data.Implementation.Repositories.REP
{
    public class ReporteRepository : IReporteRepository
    {
        public AppResponse oResponse = new AppResponse();

        public List<ReporteLogic> Paginacion(PaginateParams paginateParams)
        {
            List<ReporteLogic> listPagReporte = new List<ReporteLogic>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("REP.USP_SEL_REPORTE_PAG", CommandType.StoredProcedure,
                SQLServer.CreateParameter("SORTDIRECTION", SqlDbType.VarChar, paginateParams.SortDirection),
                SQLServer.CreateParameter("SORTCOLUMN", SqlDbType.VarChar, paginateParams.SortColumn),
                SQLServer.CreateParameter("PAGEINDEX", SqlDbType.Int, paginateParams.PageIndex),
                SQLServer.CreateParameter("ROWSPERPAGE", SqlDbType.Int, paginateParams.RowsPerPage),
                SQLServer.CreateParameter("PAGINATE", SqlDbType.Bit, paginateParams.IsPaginate),
                SQLServer.CreateParameter("FILTERS", SqlDbType.Structured, paginateParams.Filters));

                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    listPagReporte = new GenericInstance<ReporteLogic>().readDataReaderList(oReader);
                }
                paginateParams.TotalRows = listPagReporte.Count > 0 ? listPagReporte[0].CantidadTotalRegistros : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SQLServer.CloseConection();
            }
            return listPagReporte;
        }

        public List<ReporteLogic> ObtenerCabecera(long codigoGuiaEmpresarial)
        {
            List<ReporteLogic> reporte = new List<ReporteLogic>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("REP.USP_SEL_REPORTE_CABECERA_LISTAR", CommandType.StoredProcedure,
                SQLServer.CreateParameter("TIPO_DOCUMENTO_EMPRESARIAL", SqlDbType.BigInt, codigoGuiaEmpresarial));
                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    reporte = new GenericInstance<ReporteLogic>().readDataReaderList(oReader);
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
            return reporte;
        }

        public List<ReporteLogic> ObtenerDetalle(long codigoGuiaEmpresarial)
        {
            List<ReporteLogic> reporte = new List<ReporteLogic>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("REP.USP_SEL_REPORTE_DETALLE_LISTAR", CommandType.StoredProcedure,
                SQLServer.CreateParameter("TIPO_DOCUMENTO_EMPRESARIAL", SqlDbType.BigInt, codigoGuiaEmpresarial));
                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    reporte = new GenericInstance<ReporteLogic>().readDataReaderList(oReader);
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
            return reporte;
        }

        public List<ReporteLogic> ObtenerDetalleIndicador(long codigoGuiaEmpresarial, long codigoTipoRegistro, long codigoTipoPeriodicidad, int tipoPlan)
        {
            List<ReporteLogic> reporte = new List<ReporteLogic>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("REP.USP_SEL_REPORTE_DETALLE_INDICADOR_LISTAR", CommandType.StoredProcedure,
                SQLServer.CreateParameter("TIPO_DOCUMENTO_EMPRESARIAL", SqlDbType.Int, codigoGuiaEmpresarial),
                SQLServer.CreateParameter("TIPO_REGISTRO", SqlDbType.Int, codigoTipoRegistro),
                SQLServer.CreateParameter("TIPO_PERIODICIDAD", SqlDbType.Int, codigoTipoPeriodicidad),
                SQLServer.CreateParameter("TIPO_PLAN", SqlDbType.Int, tipoPlan));
                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    reporte = new GenericInstance<ReporteLogic>().readDataReaderList(oReader);
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
            return reporte;
        }

        
        public List<ReporteLogic> ObtenerHistoricoIndicador(long codigoGuiaEmpresarial, long codigoTipoRegistro, long codigoTipoPeriodicidad)
        {
            List<ReporteLogic> reporte = new List<ReporteLogic>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("REP.USP_SEL_REPORTE_HISTORICO_INDICADOR_LISTAR", CommandType.StoredProcedure,
                SQLServer.CreateParameter("TIPO_DOCUMENTO_EMPRESARIAL", SqlDbType.Int, codigoGuiaEmpresarial),
                SQLServer.CreateParameter("TIPO_REGISTRO", SqlDbType.Int, codigoTipoRegistro),
                SQLServer.CreateParameter("TIPO_PERIODICIDAD", SqlDbType.Int, codigoTipoPeriodicidad));
                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    reporte = new GenericInstance<ReporteLogic>().readDataReaderList(oReader);
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
            return reporte;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
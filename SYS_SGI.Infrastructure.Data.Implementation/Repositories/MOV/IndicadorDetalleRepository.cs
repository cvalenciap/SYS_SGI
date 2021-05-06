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
    public class IndicadorDetalleRepository : IIndicadorDetalleRepository
    {
        public AppResponse oResponse = new AppResponse();

        public IndicadorDetalleLogic Obtener(float codigo)
        {
            IndicadorDetalleLogic entidadLogic = new IndicadorDetalleLogic();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("MOV.USP_SEL_INDICADOR_DETALLE_OBTENER", CommandType.StoredProcedure,
                SQLServer.CreateParameter("CODIGO_INDICADOR_DETALLE", SqlDbType.BigInt, codigo));
                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    entidadLogic = new GenericInstance<IndicadorDetalleLogic>().readDataReader(oReader);
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

        public List<IndicadorDetalleLogic> Listar(int CodigoAlineamientoEstrategico, int codigoTipoRegistro, int codigoTipoPlan, string fechaPeriodo)
        {
            List<IndicadorDetalleLogic> listEntidadLogic = new List<IndicadorDetalleLogic>();
            
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("MOV.USP_SEL_INDICADOR_DETALLE_LISTAR", CommandType.StoredProcedure,
                SQLServer.CreateParameter("CODIGO_ALINEAMIENTO_ESTRATEGICO", SqlDbType.Int, CodigoAlineamientoEstrategico),
                SQLServer.CreateParameter("CODIGO_TIPO_REGISTRO", SqlDbType.Int, codigoTipoRegistro),
                SQLServer.CreateParameter("CODIGO_TIPO_PLAN", SqlDbType.Int, codigoTipoPlan),
                SQLServer.CreateParameter("FECHA_PERIODO", SqlDbType.VarChar, fechaPeriodo));
                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    listEntidadLogic = new GenericInstance<IndicadorDetalleLogic>().readDataReaderList(oReader);
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
            return listEntidadLogic;
        }

        public List<IndicadorDetalleLogic> Paginacion(PaginateParams paginateParams, float codigoGuiaEmpresarial, float tipoRegistro, int codigoTipoPlan)
        {
            List<IndicadorDetalleLogic> listEntidadLogic = new List<IndicadorDetalleLogic>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("MOV.USP_SEL_INDICADOR_DETALLE_PAG", CommandType.StoredProcedure,
                SQLServer.CreateParameter("CODIGO_TIPO_REGISTRO", SqlDbType.Int, tipoRegistro),
                SQLServer.CreateParameter("CODIGO_TIPO_PLAN", SqlDbType.Int, codigoTipoPlan),
                SQLServer.CreateParameter("CODIGO_GUIA_EMPRESARIAL", SqlDbType.Int, codigoGuiaEmpresarial),
                SQLServer.CreateParameter("SORTDIRECTION", SqlDbType.VarChar, paginateParams.SortDirection),
                SQLServer.CreateParameter("SORTCOLUMN", SqlDbType.VarChar, paginateParams.SortColumn),
                SQLServer.CreateParameter("PAGEINDEX", SqlDbType.Int, paginateParams.PageIndex),
                SQLServer.CreateParameter("ROWSPERPAGE", SqlDbType.Int, paginateParams.RowsPerPage),
                SQLServer.CreateParameter("PAGINATE", SqlDbType.Bit, paginateParams.IsPaginate),
                SQLServer.CreateParameter("FILTERS", SqlDbType.Structured, paginateParams.Filters));
                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    listEntidadLogic = new GenericInstance<IndicadorDetalleLogic>().readDataReaderList(oReader);
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

        public AppResponse Mantenimiento(List<IndicadorDetalleLogic> lista, List<VariableFormulaLogic> listaFormula, string accion)
        {
            try
            {
                DataTable dt = new DataTable();
                DataTable dtVariableFormula = new DataTable();
                List<IndicadorDetalle> listEntidad = lista.Cast<IndicadorDetalle>().ToList();
                List<VariableFormula> listaVariableFormula = listaFormula.Cast<VariableFormula>().ToList();
                dt = Util.ConvertListToDatatable(listEntidad);
                dtVariableFormula = Util.ConvertListToDatatable(listaVariableFormula);
                SQLServer.OpenConection();
                SQLServer.CreateCommand("MOV.USP_MANT_INDICADOR_DETALLE", CommandType.StoredProcedure,
                SQLServer.CreateParameter("ACCION", SqlDbType.VarChar, accion),
                SQLServer.CreateParameter("INDICADOR_DETALLE_TYPE", SqlDbType.Structured, dt),
                SQLServer.CreateParameter("VARIABLE_FORMULA_TYPE", SqlDbType.Structured, dtVariableFormula));

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
            //return new AppResponse { Code = "S",Description= "Se registro exitosamente" };
        }

        public AppResponse MantenimientoHistorico(List<IndicadorDetalleHistoricoLogic> listaIndicadorDetalle, List<VariableFormulaHistoricoLogic> listaVariables, string accion)
        {
            try
            {
                DataTable dtIndicadorDetalle = new DataTable();
                DataTable dtVariableFormula = new DataTable();

                List<IndicadorDetalleHistorico> listaEntidadIndicadorDetalle = listaIndicadorDetalle.Cast<IndicadorDetalleHistorico>().ToList();
                List<VariableFormulaHistorico> listaEntidadVariableFormula = listaVariables.Cast<VariableFormulaHistorico>().ToList();
                dtIndicadorDetalle = Util.ConvertListToDatatable(listaEntidadIndicadorDetalle);
                dtVariableFormula = Util.ConvertListToDatatable(listaEntidadVariableFormula);
                SQLServer.OpenConection();
                SQLServer.CreateCommand("MOV.USP_MANT_INDICADOR_DETALLE_HISTORICO", CommandType.StoredProcedure,
                SQLServer.CreateParameter("ACCION", SqlDbType.VarChar, accion),
                SQLServer.CreateParameter("VARIABLE_FORMULA_HISTORICO_TYPE", SqlDbType.Structured, dtVariableFormula),
                SQLServer.CreateParameter("INDICADOR_DETALLE_HISTORICO_TYPE", SqlDbType.Structured, dtIndicadorDetalle));
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

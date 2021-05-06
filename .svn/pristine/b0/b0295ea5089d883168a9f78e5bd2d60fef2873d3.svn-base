using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.REP;
using SYS_SGI.Infrastructure.Data.Implementation.Base;
using SYS_SGI.Infrastructure.Data.Implementation.IRepositories.REP;
using System;
using System.Collections.Generic;
using System.Data;

namespace SYS_SGI.Infrastructure.Data.Implementation.Repositories.REP
{
    public class DashboardRepository : IDashboardRepository
    {
        public AppResponse oResponse = new AppResponse();
        
        public List<DashboardLogic> Listar(int CodigoTipoGuia, int CodigoIndicador, int CodigoTipoRegistro, int TipoPeriodicidad, int SubTipoPeriodicidad, string Anio, string Fecha, int tipoPlan)
        {
            List<DashboardLogic> lista = new List<DashboardLogic>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("MAN.USP_DASHBOARD_SEL", CommandType.StoredProcedure,
                SQLServer.CreateParameter("CODIGO_TIPO_GUIA_EMPRESARIAL", SqlDbType.Int, CodigoTipoGuia),
                SQLServer.CreateParameter("CODIGO_INDICADOR", SqlDbType.Int, CodigoIndicador),
                SQLServer.CreateParameter("CODIGO_TIPO_REGISTRO", SqlDbType.Int, CodigoTipoRegistro),
                SQLServer.CreateParameter("TIPO_PERIODICIDAD", SqlDbType.Int, TipoPeriodicidad),
                SQLServer.CreateParameter("CODIGO_TIPO_PERIODICIDAD", SqlDbType.Int, SubTipoPeriodicidad),
                SQLServer.CreateParameter("ANIO", SqlDbType.VarChar, Anio),
                SQLServer.CreateParameter("FECHA", SqlDbType.VarChar, Fecha),
                SQLServer.CreateParameter("TIPO_PLAN", SqlDbType.VarChar, tipoPlan));
                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    lista = new GenericInstance<DashboardLogic>().readDataReaderList(oReader);
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
            return lista;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
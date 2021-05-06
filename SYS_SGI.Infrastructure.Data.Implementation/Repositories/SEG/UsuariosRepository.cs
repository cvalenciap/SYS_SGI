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
    public class UsuariosRepository : IUsuariosRepository
    {
        public AppResponse oResponse = new AppResponse();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public UsuariosLogic Login(string usuario, string contrasena)
        {
            UsuariosLogic usuarios = new UsuariosLogic();

            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("SEG.USP_LOGIN", CommandType.StoredProcedure,
                SQLServer.CreateParameter("USUARIO", SqlDbType.VarChar, usuario),
                SQLServer.CreateParameter("CONTRASENA", SqlDbType.VarChar, contrasena));
                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    usuarios = new GenericInstance<UsuariosLogic>().readDataReader(oReader);
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
            return usuarios;
        }

        public SistemaLogic Sistema_x_Token(string token, int codigo_usuario)
        {
            SistemaLogic sistema = new SistemaLogic();

            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("SEG.USP_SISTEMA_X_TOKEN", CommandType.StoredProcedure,
                SQLServer.CreateParameter("TOKEN", SqlDbType.VarChar, token),
                SQLServer.CreateParameter("CODIGO_USUARIO", SqlDbType.Int, codigo_usuario));
                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    sistema = new GenericInstance<SistemaLogic>().readDataReader(oReader);
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
            return sistema;
        }

        public List<OpcionLogic> Usuario_x_Sistema(string usuario, int codigo_sistema)
        {
            List<OpcionLogic> litaOpciones = new List<OpcionLogic>();

            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("SEG.USP_USUARIO_X_SISTEMA", CommandType.StoredProcedure,
                SQLServer.CreateParameter("USUARIO", SqlDbType.VarChar, usuario),
                SQLServer.CreateParameter("CODIGO_SISTEMA", SqlDbType.Int, codigo_sistema));
                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    litaOpciones = new GenericInstance<OpcionLogic>().readDataReaderList(oReader);
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
            return litaOpciones;
        }

        public UsuariosLogic Obtener(float codigo)
        {
            UsuariosLogic entidadLogic = new UsuariosLogic();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("SEG.USP_SEL_USUARIOS_OBTENER", CommandType.StoredProcedure,
                SQLServer.CreateParameter("CODIGO_USUARIO", SqlDbType.BigInt, codigo));
                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    entidadLogic = new GenericInstance<UsuariosLogic>().readDataReader(oReader);
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

        public List<UsuariosLogic> Listar()
        {
            List<UsuariosLogic> listEntidadLogic = new List<UsuariosLogic>();
            Database oDatabase = DataBaseManager.GetDefaultDataBase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SEG.USP_SEL_USUARIOS_LISTAR");
            try
            {
                using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
                {
                    listEntidadLogic = new GenericInstance<UsuariosLogic>().readDataReaderList(oReader);
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

        public List<UsuariosLogic> Paginacion(PaginateParams paginateParams)
        {
            List<UsuariosLogic> listEntidadLogic = new List<UsuariosLogic>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("SEG.USP_SEL_USUARIOS_PAG", CommandType.StoredProcedure,
                SQLServer.CreateParameter("SORTDIRECTION", SqlDbType.VarChar, paginateParams.SortDirection),
                SQLServer.CreateParameter("SORTCOLUMN", SqlDbType.VarChar, paginateParams.SortColumn),
                SQLServer.CreateParameter("PAGEINDEX", SqlDbType.Int, paginateParams.PageIndex),
                SQLServer.CreateParameter("ROWSPERPAGE", SqlDbType.Int, paginateParams.RowsPerPage),
                SQLServer.CreateParameter("PAGINATE", SqlDbType.Bit, paginateParams.IsPaginate),
                SQLServer.CreateParameter("FILTERS", SqlDbType.Structured, paginateParams.Filters));
                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    listEntidadLogic = new GenericInstance<UsuariosLogic>().readDataReaderList(oReader);
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

        public AppResponse Mantenimiento(List<UsuariosLogic> lista, string accion)
        {
            try
            {
                DataTable dt = new DataTable();
                List<Usuarios> listEntidad = lista.Cast<Usuarios>().ToList();
                dt = Util.ConvertListToDatatable(listEntidad);
                SQLServer.OpenConection();
                SQLServer.CreateCommand("SEG.USP_MANT_USUARIOS", CommandType.StoredProcedure,
                SQLServer.CreateParameter("ACCION", SqlDbType.VarChar, accion),
                SQLServer.CreateParameter("USUARIOS_TYPE", SqlDbType.Structured, dt));
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
    }
}

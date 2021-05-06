using System;
using System.Collections.Generic;
using System.Data;
using SYS_SGI.WindowsServices.Server.Entity;

namespace SYS_SGI.WindowsServices.Server.Data
{
    public class AlertaSGIData
    {
        public List<AlertaSGIEntity> ListarConcepto()
        {
            List<AlertaSGIEntity> lstAlerta = new List<AlertaSGIEntity>();
            AlertaSGIEntity alerta;

            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("MAN.USP_SEL_ALERTA_CORREO_LISTAR", CommandType.StoredProcedure);

                using (IDataReader oReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {

                    while (oReader.Read())
                    {
                        alerta = new AlertaSGIEntity();
                        alerta.Area = Convert.ToString(oReader["Area"]);
                        alerta.CodigoIndicador = Convert.ToInt32(oReader["CodigoIndicador"]);
                        alerta.DiasVencidos = Convert.ToInt32(oReader["DiasVencidos"]);
                        alerta.FechaFin = Convert.ToString(oReader["FechaFin"]);
                        alerta.FechaInicio = Convert.ToString(oReader["FechaInicio"]);
                        alerta.GerenteCorreo = Convert.ToString(oReader["GerenteCorreo"]);
                        alerta.GerenteNombre = Convert.ToString(oReader["GerenteNombre"]);
                        alerta.Indicador = Convert.ToString(oReader["Indicador"]);
                        alerta.JefeDirectoCorreo = Convert.ToString(oReader["JefeDirectoCorreo"]);
                        alerta.JefeDirectoNombre = Convert.ToString(oReader["JefeDirectoNombre"]);
                        alerta.Ponderacion = Convert.ToDouble(oReader["Ponderacion"]);
                        alerta.ResponsableCorreo = Convert.ToString(oReader["ResponsableCorreo"]);
                        alerta.ResponsableNombre = Convert.ToString(oReader["ResponsableNombre"]);
                        alerta.ValorEjecutado = Convert.ToDouble(oReader["ValorEjecutado"]);
                        lstAlerta.Add(alerta);
                    }
                    oReader.Close();
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
            return lstAlerta;
        }
    }
}

using SYS_SGI.WindowsServices.Server.Data;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using System.Configuration;
using Microsoft.SharePoint.Client;
using SYS_SGI.WindowsServices.Server.Entity;
using System.Net;

namespace SYS_SGI.WindowsServices.Server.Util
{
    public class Reutilizable
    {
        public string EnviarCorreo(ExchangeService exchange, string para, string asunto, string mensaje)
        {
            try
            {
                EmailMessage message = new EmailMessage(exchange);
                message.ToRecipients.Add(para);
                message.Importance = Importance.High;
                message.Subject = asunto;
                message.Body = mensaje;
                message.Send();
                return "";
            }
            catch (Exception ex)
            {
                new Log().RegistrarEvento(Propiedades.Mensaje.CorreoErrorEnvio);
                return ex.Message.ToString();
            }
        }

        public string ResponderCorreo(ExchangeService exchange, EmailMessage message, string asuntoContinuar, string mensaje)
        {
            try
            {
                ResponseMessage responseMessage = message.CreateReply(true);

                if (asuntoContinuar != "")
                    responseMessage.Subject = string.Format("{0} {1}", message.Subject, asuntoContinuar);

                responseMessage.BodyPrefix = mensaje;
                //responseMessage.BccRecipients.Add("wgonzales@gmd.com.pe");
                responseMessage.SendAndSaveCopy();

                return "";
            }
            catch (Exception ex)
            {
                new Log().RegistrarEvento(Propiedades.Mensaje.CorreoErrorResponder);
                return ex.Message.ToString();
            }
        }

        public string ReenviarCorreo(ExchangeService exchange, EmailMessage message, List<AlertaSGIEntity> lstCorreo, string asuntoContinuar, string mensaje)
        {
            try
            {
                ResponseMessage responseMessage = message.CreateForward();
                
                if (asuntoContinuar != "")
                    responseMessage.Subject = string.Format("{0} {1}", message.Subject, asuntoContinuar);

                responseMessage.BodyPrefix = mensaje;
                
                //EmailAddress[] emailAddressCc = new EmailAddress[lstCorreoCc.Count];
                //EmailAddress[] emailAddressBcc = new EmailAddress[lstCorreoBcc.Count];

                //for (int i = 0; i < lstCorreoCc.Count; i++)
                //    emailAddressCc[i] = lstCorreoCc[i].CorreoElectronico;

                //for (int i = 0; i < lstCorreoBcc.Count; i++)
                //    emailAddressBcc[i] = lstCorreoBcc[i].CorreoElectronico;

                //responseMessage.ToRecipients.AddRange(emailAddressCc);
                //responseMessage.BccRecipients.AddRange(emailAddressBcc);
                responseMessage.SendAndSaveCopy();

                return "";
            }
            catch (Exception ex)
            {
                new Log().RegistrarEvento(Propiedades.Mensaje.CorreoErrorReenvio);
                return ex.Message.ToString();
            }
        }

        public static DataTable ConvertListToDatatable<T>(IList<T> items)
        {
            var dataTable = new DataTable();
            Type itemsType = typeof(T);
            foreach (PropertyInfo prop in itemsType.GetProperties())
            {
                var column = new DataColumn(prop.Name)
                {
                    DataType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType
                };
                dataTable.Columns.Add(column);
            }
            foreach (var item in items)
            {
                int j = 0;
                object[] newRow = new object[dataTable.Columns.Count];
                foreach (PropertyInfo prop in itemsType.GetProperties())
                {
                    newRow[j] = prop.GetValue(item, null);
                    if (newRow[j] != null)
                    {
                        if (newRow[j].ToString().Length > 4000)
                        {
                            newRow[j] = newRow[j].ToString().Substring(0, 4000);
                        }
                    }
                    j++;
                }
                dataTable.Rows.Add(newRow);
            }
            return dataTable;
        }

        public static Object SetDBNull(Object Param)
        {
            if (Param == null)
            {
                return DBNull.Value;
            }
            else
            {
                string tipo = Param.GetType().FullName;
                if (tipo.Equals("System.String"))
                {
                    if (string.IsNullOrEmpty((string)Param))
                    {
                        return DBNull.Value;
                    }
                }
                return Param;
            }
        }

        //public Resultado SharePoint(string biblioteca, string rutaCompleta)
        //{
        //    Resultado result = new Resultado();
        //    string siteUrl = ConfigurationManager.AppSettings["SharePointSitio"].ToString();
        //    string spUsuario = ConfigurationManager.AppSettings["SharePointUsuario"].ToString();
        //    string spClave = ConfigurationManager.AppSettings["SharePointClave"].ToString();
        //    string spDominio = ConfigurationManager.AppSettings["SharePointDominio"].ToString();
        //    try
        //    {
        //        using (ClientContext clientContext = new ClientContext(siteUrl))
        //        {
        //            clientContext.RequestTimeout = -1;
        //            NetworkCredential credentials = new NetworkCredential(spUsuario, spClave, spDominio);
        //            clientContext.Credentials = credentials;

        //            List docs = clientContext.Web.Lists.GetByTitle(biblioteca);

        //            SubirArchivo(clientContext, docs, rutaCompleta);

        //            result.Indicador = "1";
        //            result.Mensaje = "";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        new Log().RegistrarError(ex);
        //        result.Indicador = "0";
        //    }

        //    return result;
        //}

        static void SubirArchivo(ClientContext ctx, List docs, string filePath)
        {
            FileCreationInformation newFile = new FileCreationInformation();
            newFile.Overwrite = true;
            newFile.Content = System.IO.File.ReadAllBytes(filePath);
            newFile.Url = System.IO.Path.GetFileName(filePath);

            File uploadFile = docs.RootFolder.Files.Add(newFile);
            ctx.Load(docs);
            ctx.Load(uploadFile);
            ctx.ExecuteQuery();
        }
    }
}

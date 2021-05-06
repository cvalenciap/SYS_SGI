using System;
using System.Diagnostics;
using System.IO;

namespace SYS_SGI.WindowsServices.Server.Util
{
    public class Log
    {
        private String sPath;

        public void RegistrarError(Exception exc)
        {
            try
            {
                sPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                sPath = System.IO.Path.GetDirectoryName(sPath);
                string Carpeta = "Log";
                
                string msg = "Error de Servicio: " + exc.Message + "\r\n" + @"StackTrace: " + exc.StackTrace + "\r\nHora Equipo: " + DateTime.Now.ToShortTimeString();

                //Crear directorio Log en el aplicativo
                if (!Directory.Exists(sPath + @"/" + Carpeta))
                    Directory.CreateDirectory(sPath + @"/" + Carpeta);

                string FileName = sPath + @"/" + Carpeta + @"/" + DateTime.Now.ToString("yyyyMMdd") + ".txt";

                //Crear txt y registrar el error
                using (StreamWriter sw = new StreamWriter(FileName, true))
                {
                    sw.WriteLine();
                    sw.WriteLine(new String(Convert.ToChar("="), 200));
                    sw.WriteLine(msg);
                    sw.WriteLine();
                }
            }
            catch (Exception ex)
            {
                sPath = System.Environment.CurrentDirectory;
                EventLog.WriteEntry("Error de Servicio: ", ex.Message + " | " + ex.StackTrace + "| Ruta Config: " + sPath);
            }
        }

        public void RegistrarEvento(string descripcion)
        {
            try
            {
                sPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                sPath = System.IO.Path.GetDirectoryName(sPath);

                string msg = descripcion + "\r\nHora Equipo: " + DateTime.Now.ToShortTimeString();
                string Carpeta = "Log";
                
                if (!Directory.Exists(sPath + @"/" + Carpeta))
                    Directory.CreateDirectory(sPath + @"/" + Carpeta);

                string FileName = sPath + @"/" + Carpeta + @"/" + DateTime.Now.ToString("yyyyMMdd") + ".txt";

                //Crear txt y registrar el error
                using (StreamWriter sw = new StreamWriter(FileName, true))
                {
                    sw.WriteLine();
                    sw.WriteLine(new String(Convert.ToChar("="), 200));
                    sw.WriteLine(msg);
                    sw.WriteLine();
                }
            }
            catch (Exception)
            {
                sPath = System.Environment.CurrentDirectory;
                EventLog.WriteEntry("Descripcion Evento: ", descripcion + " | Ruta Config: " + sPath);
            }
        }
    }
}

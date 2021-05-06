using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SYS_SGI.Presentation.Utilities
{
    public static class Funciones
    {
        public static class Conversion
        {
            public static DataTable ListaToDatatable<T>(IList<T> items)
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
                        j++;
                    }
                    dataTable.Rows.Add(newRow);
                }
                return dataTable;
            }

            public static DateTime? StringToDatetime(string dateformat, string dateString)
            {
                try
                {
                    DateTime dateValue;
                    if (DateTime.TryParseExact(dateString, dateformat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                        return dateValue;
                    return null;
                }
                catch
                {
                    return null;
                }
            }

            public static string ConvertirXlsxACsv(string rutaArchivo, int[] columnaFormatearFecha, bool noCabecera = true, string separadorColumna = "|", string separadorFila = "#")
            {
                DataTable tbl = new DataTable();
                using (var pck = new ExcelPackage())
                {
                    using (var stream = System.IO.File.OpenRead(rutaArchivo))
                    {
                        pck.Load(stream);
                    }
                    var ws = pck.Workbook.Worksheets.First();

                    foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                    {
                        tbl.Columns.Add(noCabecera ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                    }
                    var startRow = noCabecera ? 2 : 1;
                    int columna = 0;
                    for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                    {
                        var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                        DataRow row = tbl.Rows.Add();

                        foreach (var cell in wsRow)
                        {
                            columna = cell.Start.Column - 1;
                            for (int i = 0; i < columnaFormatearFecha.Length; i++)
                            {
                                if (columnaFormatearFecha[i].Equals(columna))
                                {
                                    cell.Style.Numberformat.Format = "dd/MM/yyyy hh:mm";
                                    break;
                                }
                            }

                            row[columna] = cell.Text;
                        }
                    }
                }

                string resultado = string.Empty;
                if (tbl != null)
                {
                    int columnas = tbl.Columns.Count;
                    int inicio = 0;
                    StringBuilder data = new StringBuilder();
                    foreach (DataRow dr in tbl.Rows)
                    {
                        for (; inicio < columnas - 1; inicio++)
                        {
                            data.Append(dr[inicio].ToString());
                            data.Append(separadorColumna);
                        }

                        if (inicio.Equals(columnas - 1))
                        {
                            data.Append(dr[inicio].ToString());
                        }

                        data.Append(separadorFila);
                        inicio = 0;
                    }
                    resultado = data.ToString();
                    resultado = resultado.Length > 0 ? resultado.Substring(0, resultado.Length - 1) : string.Empty;
                }
                return resultado;
            }

            public static string ConvertirXlsACsv(string rutaArchivo, int[] columnaFormatearFecha, int posicionFilaLectura, int posicionColumnaInicioLectura, int posicionColumnaFinLectura, string separadorColumna = "|", string separadorFila = "#")
            {
                HSSFWorkbook wb;
                HSSFSheet sh;
                String Sheet_name;

                using (var fs = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read))
                {
                    wb = new HSSFWorkbook(fs);

                    Sheet_name = wb.GetSheetAt(0).SheetName;
                }
                DataTable tbl = new DataTable();
                tbl.Rows.Clear();
                tbl.Columns.Clear();

                sh = (HSSFSheet)wb.GetSheet(Sheet_name);

                int i = 0;

                int numeroFila = 0;
                int numeroColumna = 0;
                int inicioPosicionColumnbaFormatera = 0;
                int cantidadPosicionColumnaFormatera = columnaFormatearFecha.Length;
                string[] fechaHora = null;


                while (sh.GetRow(i) != null)
                {
                    inicioPosicionColumnbaFormatera = 0;
                    if (i >= posicionFilaLectura)
                    {

                        posicionColumnaFinLectura = posicionColumnaFinLectura > sh.GetRow(i).Cells.Count ? sh.GetRow(i).Cells.Count : posicionColumnaFinLectura;

                        if (tbl.Columns.Count < sh.GetRow(i).Cells.Count)
                        {
                            for (int j = posicionColumnaInicioLectura; j <= posicionColumnaFinLectura; j++)
                            {
                                tbl.Columns.Add("", typeof(string));
                            }
                        }

                        tbl.Rows.Add();

                        numeroColumna = 0;
                        for (int j = posicionColumnaInicioLectura; j <= posicionColumnaFinLectura; j++)
                        {
                            var cell = sh.GetRow(i).GetCell(j);

                            if (cell != null)
                            {
                                if (inicioPosicionColumnbaFormatera < columnaFormatearFecha.Length && columnaFormatearFecha[inicioPosicionColumnbaFormatera].Equals(j))
                                {
                                    fechaHora = sh.GetRow(i).GetCell(j).ToString().Split(' ');
                                    tbl.Rows[numeroFila][numeroColumna] = DateTime.Parse(fechaHora[0].Split('/')[1] + "/" + fechaHora[0].Split('/')[0] + "/" + fechaHora[0].Split('/')[2]).ToString("dd/MM/yyyy") + " " + (fechaHora[1].Length.Equals(4) ? ("0" + fechaHora[1]) : fechaHora[1]);
                                    inicioPosicionColumnbaFormatera++;
                                }
                                else
                                {
                                    switch (cell.CellType)
                                    {
                                        case NPOI.SS.UserModel.CellType.Numeric:
                                            tbl.Rows[numeroFila][numeroColumna] = sh.GetRow(i).GetCell(j).NumericCellValue;
                                            break;
                                        case NPOI.SS.UserModel.CellType.String:
                                            tbl.Rows[numeroFila][numeroColumna] = sh.GetRow(i).GetCell(j).StringCellValue;
                                            break;
                                    }
                                }

                            }
                            numeroColumna++;
                        }
                        numeroFila++;

                    }
                    i++;
                }

                string resultado = string.Empty;
                if (tbl != null)
                {
                    int columnas = tbl.Columns.Count;
                    int inicio = 0;
                    StringBuilder data = new StringBuilder();
                    foreach (DataRow dr in tbl.Rows)
                    {
                        for (; inicio < columnas - 1; inicio++)
                        {
                            data.Append(dr[inicio].ToString());
                            data.Append(separadorColumna);
                        }

                        if (inicio.Equals(columnas - 1))
                        {
                            data.Append(dr[inicio].ToString());
                        }

                        data.Append(separadorFila);
                        inicio = 0;
                    }
                    resultado = data.ToString();
                    resultado = resultado.Length > 0 ? resultado.Substring(0, resultado.Length - 1) : string.Empty;
                }
                return resultado;
            }
        }

        public static int GetPaginaActual(int paginaActual, string PaginaSolicitada, int cantidadTotalFilas, int FilasPorPagina)
        {
            var page = 0;
            if (paginaActual == 0) return 1;

            if (string.IsNullOrEmpty(PaginaSolicitada)) return 1;

            switch (PaginaSolicitada)
            {
                case Constantes.Controles.Paginacion.Descripcion.Primero: page = 1; break;
                case Constantes.Controles.Paginacion.Descripcion.Anterior: page = paginaActual - 1; break;
                case Constantes.Controles.Paginacion.Descripcion.Siguieunte: page = paginaActual + 1; break;
                case Constantes.Controles.Paginacion.Descripcion.Ultimo: page = (cantidadTotalFilas / FilasPorPagina) + 1; break;
            }
            return page;
        }

        public static bool IsValidStringToDate(string dateformat, string dateString)
        {
            try
            {
                DateTime dateValue;
                return DateTime.TryParseExact(dateString, dateformat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue);
            }
            catch
            {
                return false;
            }
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
        public static string GetPathTemp(byte[] filedata, string extension = "pdf")
        {
            string filename = GetUrlRoot() + Guid.NewGuid().ToString() + extension;
            if (filedata != null)
            {
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    fs.Write(filedata, 0, filedata.Length);
                }
            }
            return filename;
        }
        public static byte[] readFileContents(HttpPostedFileBase file)
        {
            Stream fileStream = file.InputStream;
            var mStreamer = new MemoryStream();
            mStreamer.SetLength(fileStream.Length);
            fileStream.Read(mStreamer.GetBuffer(), 0, (int)fileStream.Length);
            mStreamer.Seek(0, SeekOrigin.Begin);
            byte[] fileBytes = mStreamer.GetBuffer();
            return fileBytes;
        }
        public static string GetUrlRoot()
        {
            return HttpContext.Current.Server.MapPath("~/FilesTemp/");
        }
    }
    public static class RegexUtilities
    {
        static bool invalid = false;

        public static bool IsValidEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            strIn = Regex.Replace(strIn, @"(@)(.+)$", RegexUtilities.DomainMapper);
            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn,
                   @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                   RegexOptions.IgnoreCase);
        }

        private static string DomainMapper(Match match)
        {
            IdnMapping idn = new IdnMapping();
            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
    }
}
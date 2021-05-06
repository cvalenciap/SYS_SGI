using GMD.CustomHtmlHelpers.Utils;
using Newtonsoft.Json;
using SYS_SGI.Presentation.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace SYS_SGI.Presentation.Utilities
{
    public class ExportGridGmd
    {
        /*WebConfig*/
        private const string AppExpPath = "AppExpPath";
        private const string AppExpColumn = "AppExpColumn";
        private const string AppExpRow = "AppExpRow";
        private const string AppExpShet = "AppExpShet";
        /*Const*/
        private const string SessionData = Constantes.Sesion.ExportarGrilla.SessionData;
        private const string TypeFile = "xlsx";
        /*Config*/
        private static string _path = "C:\\Windows\\Temp";
        /*Config Excel*/
        private static char _column = 'A';
        private static int _row = 1;
        private static string _sheet = "Sheet";

        public ExportGridGmd()
        {
            _path = string.IsNullOrEmpty(ConfigurationManager.AppSettings[AppExpPath]) ? _path : ConfigurationManager.AppSettings[AppExpPath];
            _column = string.IsNullOrEmpty(ConfigurationManager.AppSettings[AppExpColumn]) ? _column : Convert.ToChar(ConfigurationManager.AppSettings[AppExpColumn]);
            _row = string.IsNullOrEmpty(ConfigurationManager.AppSettings[AppExpRow]) ? _row : Convert.ToInt32(ConfigurationManager.AppSettings[AppExpRow]);
            _sheet = string.IsNullOrEmpty(ConfigurationManager.AppSettings[AppExpShet]) ? _sheet : ConfigurationManager.AppSettings[AppExpShet];
        }

        public byte[] Excel(string arg)
        {
            try
            {
                arg = JsonConvert.DeserializeObject<string>(arg);

                var listExportar = arg.Split(',')
                    .Select(item => item.Split('|'))
                    .Select(obj => new Arrangement
                    {
                        Key = obj[0],
                        Text = obj[1]
                    })
                    .ToList();

                var getSession = HttpContext.Current.Session[SessionData];

                var merge = MergeObjects(getSession, listExportar);
                return CreateFile(merge);
            }
            catch (Exception e)
            {
                return new byte[] { };
            }
        }

        private static Dictionary<KeyDic, string> MergeObjects(object objList, List<Arrangement> exportar)
        {
            var dicValue = new Dictionary<KeyDic, string>();
            foreach (var e in exportar)
            {
                if (string.IsNullOrEmpty(e.Key)) continue;
                var row = 1;
                dicValue.Add(new KeyDic { Row = row, Key = e.Key }, e.Text);

                foreach (var i in (IList)objList)
                {
                    var allAtributes = i.ToDictionary();
                    foreach (var atribute in allAtributes)
                    {
                        if (atribute.Key != e.Key) continue;
                        row++;
                        dicValue.Add(new KeyDic { Row = row, Key = e.Key }, atribute.Value.ToString());
                    }
                }
            }

            return dicValue;
        }

        private static byte[] CreateFile(Dictionary<KeyDic, string> dicValue)
        {
            var filePaht = Valid();

            try
            {
                /*var newFile = new FileInfo(filePaht);
                var columnNumber = AlphabetNumber(_column);
                var increase = false;

                using (var package = new ExcelPackage(newFile))
                {
                    var worksheet = package.Workbook.Worksheets.Add(_sheet);

                    foreach (var dic in dicValue)
                    {
                        if (dic.Key.Row == 1)
                        {
                            if (increase)
                                columnNumber = columnNumber + 1;
                            else
                                increase = true;
                        }

                        worksheet.Cells[dic.Key.Row + _row - 1, columnNumber].Value = dic.Value;
                    }

                    var rangeHeader = string.Format("{0}{1}:{2}{1}", _column, _row, GetExcelColumnName(columnNumber));

                    using (var rng = worksheet.Cells[rangeHeader])
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rng.Style.Font.Bold = false;
                        rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));
                        rng.Style.Font.Color.SetColor(Color.White);
                        //rng.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                        //rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        rng.Style.Border.BorderAround(ExcelBorderStyle.Medium, Color.Black);
                        rng.Style.Border.DiagonalDown = true;
                        rng.Style.Border.DiagonalUp = true;
                    }

                    worksheet.Cells.AutoFitColumns();
                    package.Save();
                }*/

                var byts = File.ReadAllBytes(filePaht);

                DeleteFile(filePaht);

                return byts;
            }
            catch (Exception e)
            {
                DeleteFile(filePaht);

                return new byte[] { };
            }
        }

        private static string Valid()
        {
            var fileName = string.Format("{0}", Guid.NewGuid());
            var filePath = string.Format("{0}\\{1}.{2}", _path, fileName, TypeFile);

            DeleteFile(filePath);

            return filePath;
        }

        private static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        private static int AlphabetNumber(char column)
        {
            return char.ToUpper(column) - 64;
        }

        private static string GetExcelColumnName(int columnNumber)
        {
            var dividend = columnNumber;
            var columnName = string.Empty;

            while (dividend > 0)
            {
                var modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                dividend = (dividend - modulo) / 26;
            }

            return columnName;
        }

        private class Arrangement
        {
            public string Key { get; set; }
            public string Text { get; set; }
        }

        private class KeyDic
        {
            public int Row { get; set; }
            public string Key { get; set; }
        }
    }
}
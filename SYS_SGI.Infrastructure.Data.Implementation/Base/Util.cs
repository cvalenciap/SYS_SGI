using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace SYS_SGI.Infrastructure.Data.Implementation.Base
{
    public static class Util
    {
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

        public static List<T> ConvertDataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
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
    }
}
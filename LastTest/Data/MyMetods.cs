using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Reflection;
using System.Data.Common;

namespace LastTest.Data
{
    class MyMetods
    {


        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }


        public static T GetItem<T>(DataRow dr)
        {
            System.Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        try
                        {
                            pro.SetValue(obj, dr[column.ColumnName], null);
                        }
                        catch (Exception)
                        {

                        }
                    }
                    else
                    {
                        continue;
                    }

                }
            }
            return obj;
        }
    }
}

using System;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using CNVP.Framework.Utils;

namespace CNVP.Framework.Helper
{
    public class ModelHelper<T> where T : class,new()
    {
        public static T ConvertSingleModel(IDataReader reader)
        {
            return ConvertSingleModel(reader, true);
        }
        /// <summary>
        /// 返回单个实体
        /// </summary>
        /// <param name="reader">IDataReader接口</param>
        /// <returns></returns>
        public static T ConvertSingleModel(IDataReader reader, bool autoClose)
        {
            T t = null;
            if (reader.Read())
            {
                try
                {
                    t = new T();
                    Type modelType = t.GetType();
                    int len = reader.FieldCount;
                    for (int i = 0; i < len; i++)
                    {
                        string filedName = reader.GetName(i);
                        PropertyInfo p = modelType.GetProperty(filedName);
                        if (p == null || !p.CanWrite) continue;
                        p.SetValue(t, Public.GetDefaultValue(reader[p.Name], p.PropertyType), null);
                    }
                }
                catch
                {
                    reader.Dispose();
                    reader.Close();
                }
                finally
                {
                    if (autoClose)
                    {
                        reader.Dispose();
                        reader.Close();
                    }
                }
            }
            return t;
        }

        /// <summary>
        /// 返回实体列表,自动关闭datareader
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static List<T> ConvertManyModel(IDataReader reader)
        {
            return ConvertManyModel(reader, true);
        }
        /// <summary>
        /// 返回实体列表
        /// </summary>
        /// <param name="reader">IDataReader接口</param>
        /// <param name="autoClose">手动控制是否关闭dataReader</param>
        /// <returns></returns>
        public static List<T> ConvertManyModel(IDataReader reader, bool autoClose)
        {
            List<T> list = null;
            if (!reader.IsClosed)
            {
                list = new List<T>();
                try
                {
                    while (reader.Read())
                    {
                        T obj = new T();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string fieldName = reader.GetName(i);
                            PropertyInfo p = obj.GetType().GetProperty(fieldName);
                            if (p == null || !p.CanWrite) continue;
                            p.SetValue(obj, Public.GetDefaultValue(reader[i], p.PropertyType), null);
                        }
                        list.Add(obj);
                    }
                }
                catch
                {
                    reader.Dispose();
                    reader.Close();
                }
                finally
                {
                    if (autoClose)
                    {
                        reader.Dispose();
                        reader.Close();
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 将一个dataTable
        /// 转换为实体集合
        /// 不知道性能上有
        /// 没有影响
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ConvertDtModel(DataTable dt)
        {
            List<T> list = null;
            int row = dt.Rows.Count;
            if (dt != null && row > 0)
            {
                list = new List<T>(row);
                int len = dt.Columns.Count;
                foreach (DataRow dr in dt.Rows)
                {
                    T obj = new T();
                    for (int i = 0; i < len; i++)
                    {
                        string fieldName = dt.Columns[i].ColumnName;
                        PropertyInfo p = obj.GetType().GetProperty(fieldName);
                        if (p == null || !p.CanWrite) continue;
                        p.SetValue(obj, Public.GetDefaultValue(dr[fieldName], p.PropertyType), null);
                    }
                    list.Add(obj);
                }
                dt.Dispose();
            }
            return list;
        }
    }
}

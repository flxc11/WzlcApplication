using System;
using System.Web;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using CNVP.Framework.Utils;

namespace CNVP.Framework.Helper
{
    /// <summary>
    /// 接收实体类并接收表
    /// 单数据进行赋值操作
    /// </summary>
    /// <typeparam name="T">实体类</typeparam>
    public sealed class UpdateModel<T> where T : class
    {
        #region 根据实体类赋值
        /// <summary>
        /// 根据实体类赋值
        /// </summary>
        /// <param name="objectModel"></param>
        public static void UpdateModels(T objectModel)
        {
            UpdateModels(objectModel, string.Empty);
        }
        #endregion
        #region 根据实体类赋值
        /// <summary>
        /// 根据实体类赋值
        /// </summary>
        /// <param name="objectModel">实体对象</param>
        /// <param name="preName">前缀名进行过滤</param>
        public static void UpdateModels(T objectModel,string preName)
        {
            if (objectModel == null)
                return;
            NameValueCollection _nvc = getFormCollection;
            if (_nvc.Count > 0)
            {
                Type type = objectModel.GetType();
                foreach (PropertyInfo p in type.GetProperties())
                {
                    if (!p.CanWrite) continue;
                    //反射实体属性名称
                    string modelName = p.Name;
                    object val = _nvc[modelName];
                    if (!string.IsNullOrEmpty(preName))
                    {
                        if (!modelName.StartsWith(preName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            modelName = string.Format("{0}{1}", preName, modelName);
                            val = _nvc[modelName];
                            //移除前缀
                            modelName = modelName.Remove(0, preName.Length);
                        }
                    }
                    if (val == null) continue;//如果为null跳出本次循环
                    p.SetValue(objectModel, Public.GetDefaultValue(val, p.PropertyType), null);
                }
            }
        }
        #endregion
        #region 获取实体表单值
        private static NameValueCollection getFormCollection
        {
            get
            {
                NameValueCollection nvc = HttpContext.Current.Request.Form;
                return nvc;
            }
        }
        #endregion
    }
}

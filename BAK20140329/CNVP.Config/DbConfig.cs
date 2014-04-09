using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Config
{
    public class DbConfig
    {
        /// <summary>
        /// 数据表前缀
        /// </summary>
        public static string Prefix = BaseConfig.GetConfigValue("Prefix");
        /// <summary>
        /// 数据库类型
        /// </summary>
        public static string DbType = BaseConfig.GetConfigValue("DbType");
        /// <summary>
        /// 数据库链接
        /// </summary>
        public static string DbConn = BaseConfig.GetConfigValue("DbConn");
        /// <summary>
        /// 数据库链接
        /// </summary>
        public static string OAConn = BaseConfig.GetConfigValue("OAConn");
    }
}
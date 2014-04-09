using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Config
{
    public class UIConfig
    {
        /// <summary>
        /// 客户端序号
        /// </summary>
        public static string ClientID = BaseConfig.GetConfigValue("ClientID");
        /// <summary>
        /// 软件名称
        /// </summary>
        public static string Product = VerConfig.Product;
        /// <summary>
        /// 登录Cookies名称
        /// </summary>
        public static string CookiesName = "HX_Product";
        /// <summary>
        /// 默认重置密码
        /// </summary>
        public static string ResetPwd = BaseConfig.GetConfigValue("ResetPwd");
        /// <summary>
        /// 间隔时间
        /// </summary>
        public static string Timestamp = BaseConfig.GetConfigValue("Timestamp");
        /// <summary>
        /// 通讯密钥
        /// </summary>
        public static string PrivateKey = BaseConfig.GetConfigValue("PrivateKey");
    }
}
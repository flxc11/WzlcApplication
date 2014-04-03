using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using CNVP.Framework.Helper;
using CNVP.Framework.Utils;

namespace CNVP.Data
{
    /// <summary>
    /// 应用授权类
    /// </summary>
    public class Auth
    {
        /// <summary>
        /// 验证请求
        /// </summary>
        /// <param name="AppID">门店序号</param>
        /// <param name="Method">方法名称</param>
        /// <param name="Sign">签名值</param>
        /// <param name="Timestamp">时间戳</param>
        /// <returns></returns>
        public static bool Validate(int AppID, string Method, string Sign, string Timestamp)
        {
            bool flg = false;
            try
            {
                Model.AppList model = new Data.AppList().GetAppInfo(AppID);
                if (model != null)
                {
                    string PubKey = model.AppPubKey;
                    string[] StrAry = RSAHelper.DecryptString(Sign, PubKey).Split('|');
                    if (StrAry[1] == Method && StrAry[2] == Timestamp)
                    {
                        //判断时间戳是否过期
                        Timestamp = Public.GetDateDiffTime(Timestamp);
                        if (Public.DateDiff(Convert.ToDateTime(Timestamp)) <= 1800)
                        {
                            flg = true;
                        }
                    }
                }
            }
            catch
            {

            }
            return true;
        }
    }
}
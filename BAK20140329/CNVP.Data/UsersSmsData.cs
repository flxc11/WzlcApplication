using CNVP.Config;
using CNVP.Framework.Helper;
using CNVP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;

namespace CNVP.Data
{
    public class UsersSmsData
    {
        #region "增加站内消息"
        /// <summary>
        /// 增加站内消息
        /// </summary>
        /// <param name="model"></param>
        public void AddUserSms(UsersSmsModel model)
        {
            string StrSql = "Insert Into " + DbConfig.Prefix + "UsersSms (UserID,SmsTitle,SmsContent,PostTime,UserPhone) Values (@UserID,@SmsTitle,@SmsContent,@PostTime,@UserPhone)";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@UserID",model.UserID),
                DbHelper.MakeParam("@SmsTitle",model.SmsTitle),
                DbHelper.MakeParam("@SmsContent",model.SmsContent),
                DbHelper.MakeParam("@PostTime",DateTime.Now.ToString()),
                DbHelper.MakeParam("@UserPhone",model.UserPhone)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);
        }
        #endregion

        #region "验证码登录判断"
        /// <summary>
        /// 验证码登录判断
        /// </summary>
        /// <returns></returns>
        public bool CheckLogin(string UserPhone, string SmsCode)
        {
            bool flg = false;

            //加入短信验证码有效期的判断
            //string StrSql = "Select Top 1 * From HD_UsersSms Where UserPhone=@UserPhone And SmsTitle=@SmsCode And DateDiff(d,PostTime,getdate())<7 Order By SmsID Desc";
            string StrSql = "Select Top 1 * From " + DbConfig.Prefix + "UsersSms Where UserPhone=@UserPhone And SmsTitle=@SmsCode And DateDiff(m,PostTime,getdate())<30 Order By SmsID Desc";
            //string StrSql = "Select Top 1 * From HD_UsersSms Where UserPhone=@UserPhone And SmsTitle=@SmsCode Order By SmsID Desc";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@UserPhone",UserPhone),
                DbHelper.MakeParam("@SmsCode",SmsCode)
            };
            DataTable Dt = DbHelper.ExecuteTable(StrSql, Param);
            if (Dt != null && Dt.Rows.Count > 0)
            {
                flg = true;
            }
            return flg;
        }
        #endregion

        #region "用户登录判断"
        /// <summary>
        /// 用户登录判断
        /// </summary>
        /// <returns></returns>
        public bool CheckLogin1(string UserName, string UserPhone, ref Model.AppliUsers model)
        {
            bool flg = false;
            string StrSql = "Select Top 1 * From " + DbConfig.Prefix + "AppliUsers Where AppPhone=@UserPhone And AppName=@UserName";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@UserPhone",UserPhone),
                DbHelper.MakeParam("@UserName",UserName)
            };
            DataTable Dt = DbHelper.ExecuteTable(StrSql, Param);
            if (Dt != null && Dt.Rows.Count > 0)
            {
                flg = true;
                model = DbHelper.ExecuteReader<Model.AppliUsers>(StrSql, Param);
            }
            return flg;
        }
        #endregion
        #region "用户存在判断"
        /// <summary>
        /// 用户存在判断
        /// </summary>
        /// <returns></returns>
        public bool CheckLogin2(string UserName, string UserCardID)
        {
            bool flg = false;
            string StrSql = "Select Top 1 * From " + DbConfig.Prefix + "AppliUsers Where AppCardID=@UserCardID And AppName=@UserName";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@UserCardID",UserCardID),
                DbHelper.MakeParam("@UserName",UserName)
            };
            DataTable Dt = DbHelper.ExecuteTable(StrSql, Param);
            if (Dt != null && Dt.Rows.Count > 0)
            {
                flg = true;
            }
            return flg;
        }
        #endregion
    }
}
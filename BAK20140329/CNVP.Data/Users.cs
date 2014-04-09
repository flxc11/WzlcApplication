using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CNVP.Cache;
using CNVP.Config;
using CNVP.Framework.Helper;
using CNVP.Framework.Utils;

namespace CNVP.Data
{
    public class Users
    {
        #region "用户登录判断"
        /// <summary>
        /// 用户登录判断
        /// </summary>
        /// <param name="AppID">商家序号</param>
        /// <param name="UserName">登录帐号</param>
        /// <param name="UserPass">登录密码</param>
        /// <returns></returns>
        public bool CheckLogin(int AppID, string UserName, string UserPass, out string LoginInfo)
        {
            bool flg = false;
            LoginInfo = string.Empty;

            string StrSql = "Select A.*,B.AppID,C.RoleID,C.IsAdmin From " + DbConfig.Prefix + "Users AS A LEFT OUTER JOIN " + DbConfig.Prefix + "AppUsers AS B ON A.UserID = B.UserID LEFT OUTER JOIN " + DbConfig.Prefix + "RoleUsers AS C ON A.UserID = C.UserID Where AppID=@AppID And UserName=@UserName And UserPass=@UserPass And IsLock=1";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@AppID",AppID),
                DbHelper.MakeParam("@UserName",UserName),
                DbHelper.MakeParam("@UserPass",UserPass)
            };
            DataTable Dt = DbHelper.ExecuteTable(StrSql, Param);
            if (Dt != null && Dt.Rows.Count > 0)
            {
                flg = true;
                LoginInfo = string.Format("{0}|{1}|{2}|{3}|{4}", Dt.Rows[0]["UserID"].ToString(), Dt.Rows[0]["UserName"].ToString(), Dt.Rows[0]["AppID"].ToString(), Dt.Rows[0]["RoleID"].ToString(), Dt.Rows[0]["IsAdmin"].ToString());

                //更新登录信息
                EditUserLoginInfo(Convert.ToInt32(Dt.Rows[0]["UserID"]));
            }
            return flg;
        }
        /// <summary>
        /// 原始密码判断
        /// </summary>
        /// <param name="UserName">登录帐号</param>
        /// <param name="UserPass">登录密码</param>
        /// <returns></returns>
        public bool CheckUserPass(string UserName,string UserPass)
        {
            bool flg = false;
            string StrSql = "Select * From " + DbConfig.Prefix + "Users Where UserName=@UserName And UserPass=@UserPass";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@UserName",UserName),
                DbHelper.MakeParam("@UserPass",UserPass)
            };
            DataTable Dt = DbHelper.ExecuteTable(StrSql, Param);
            if (Dt != null && Dt.Rows.Count > 0)
            {
                flg = true;
            }
            return flg;
        }
        #endregion
        #region "是否存在帐号"
        /// <summary>
        /// 是否存在帐号
        /// </summary>
        /// <param name="UserName">登录帐号</param>
        /// <returns></returns>
        public bool IsExist(string UserName)
        {
            bool flg = false;
            string StrSql = "Select Count(UserID) From " + DbConfig.Prefix + "Users Where UserName=@UserName";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@UserName",UserName)
            };
            int Count = Convert.ToInt32(DbHelper.ExecuteScalar(StrSql, Param));
            if (Count > 0)
            {
                flg = true;
            }
            return flg;
        }
        #endregion
        #region "读取帐号信息"
        /// <summary>
        /// 读取帐号信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public DataTable GetUserInfo(int UserID)
        {
            string StrSql = "SELECT A.*,B.RoleID,B.IsAdmin,C.AppID FROM " + DbConfig.Prefix + "RoleUsers AS B RIGHT OUTER JOIN " + DbConfig.Prefix + "Users AS A ON B.UserID = A.UserID LEFT OUTER JOIN " + DbConfig.Prefix + "AppUsers AS C ON A.UserID = C.UserID Where A.UserID=@UserID";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@UserID",UserID)
            };
            return DbHelper.ExecuteTable(StrSql, Param);
        }
        /// <summary>
        /// 获取用户序号
        /// </summary>
        /// <returns></returns>
        public int GetUserID()
        {
            int UserID = 0;
            string StrSql = "Select Top 1 UserID From " + DbConfig.Prefix + "Users Order By UserID Desc";
            DataTable Dt = DbHelper.ExecuteTable(StrSql);
            if (Dt != null && Dt.Rows.Count > 0)
            {
                UserID = Convert.ToInt32(Dt.Rows[0]["UserID"]);
            }
            return UserID + 1;
        }
        #endregion
        #region "增加用户帐号"
        /// <summary>
        /// 增加用户帐号
        /// </summary>
        /// <param name="Dict">键值</param>
        public void AddUserInfo(DictHelper<string> Dict)
        {
            //获取用户序号
            int UserID = GetUserID();

            List<string> StrSql = new List<string>();
            List<IDataParameter[]> Param = new List<IDataParameter[]>();

            //增加帐号信息
            string StrSql1 = "Insert Into " + DbConfig.Prefix + "Users (UserID,UserName,UserPass,UserTrueName,UserEmail,UserSex,UserMobile,UserTelPhone,UserFaxNumber,UserQQ,UserNickName,UserAddRess,UserRemark,LoginNum,LoginTime,LoginIP,IsLock,PostTime) Values (@UserID,@UserName,@UserPass,@UserTrueName,@UserEmail,@UserSex,@UserMobile,@UserTelPhone,@UserFaxNumber,@UserQQ,@UserNickName,@UserAddRess,@UserRemark,@LoginNum,@LoginTime,@LoginIP,@IsLock,@PostTime);Select SCOPE_IDENTITY()";
            IDataParameter[] Param1 = new IDataParameter[] {
                DbHelper.MakeParam("@UserID",UserID),
                DbHelper.MakeParam("@UserName",Dict.GetValue("UserName")),
                DbHelper.MakeParam("@UserPass",EncryptUtils.MD5(Dict.GetValue("UserPass"))),
                DbHelper.MakeParam("@UserTrueName",Dict.GetValue("UserTrueName")),
                DbHelper.MakeParam("@UserEMail",Dict.GetValue("UserEMail")),
                DbHelper.MakeParam("@UserSex",Dict.GetValue("UserSex")),
                DbHelper.MakeParam("@UserMobile",Dict.GetValue("UserMobile")),
                DbHelper.MakeParam("@UserTelPhone",Dict.GetValue("UserTelPhone")),
                DbHelper.MakeParam("@UserFaxNumber",Dict.GetValue("UserFaxNumber")),
                DbHelper.MakeParam("@UserQQ",Dict.GetValue("UserQQ")),
                DbHelper.MakeParam("@UserNickName",Dict.GetValue("UserNickName")),
                DbHelper.MakeParam("@UserAddRess",Dict.GetValue("UserAddRess")),
                DbHelper.MakeParam("@UserRemark",Dict.GetValue("UserRemark")),
                DbHelper.MakeParam("@LoginNum",0),
                DbHelper.MakeParam("@LoginTime",DateTime.Now.ToString()),
                DbHelper.MakeParam("@LoginIP",Public.GetUserIP()),
                DbHelper.MakeParam("@IsLock",1),
                DbHelper.MakeParam("@PostTime",DateTime.Now.ToString())
            };
            StrSql.Add(StrSql1);
            Param.Add(Param1);

            //隶属门店信息
            string StrSql2 = "Insert Into " + DbConfig.Prefix + "AppUsers (AppID,UserID,IsAdmin) Values (@AppID,@UserID,@IsAdmin)";
            IDataParameter[] Param2 = new IDataParameter[] {
                DbHelper.MakeParam("@AppID",Dict.GetValue("AppID")),
                DbHelper.MakeParam("@UserID",UserID),
                DbHelper.MakeParam("@IsAdmin",Dict.GetValue("IsAdmin"))
            };
            StrSql.Add(StrSql2);
            Param.Add(Param2);

            //隶属角色信息
            string StrSql3 = "Insert Into " + DbConfig.Prefix + "RoleUsers (RoleID,UserID,IsAdmin) Values (@RoleID,@UserID,@IsAdmin)";
            IDataParameter[] Param3 = new IDataParameter[] {
                DbHelper.MakeParam("@RoleID",Dict.GetValue("RoleID")),
                DbHelper.MakeParam("@UserID",UserID),
                DbHelper.MakeParam("@IsAdmin",Dict.GetValue("IsAdmin"))
            };
            StrSql.Add(StrSql3);
            Param.Add(Param3);

            //执行添加操作
            DbHelper.ExecuteSqlTran(StrSql, Param);
        }
        #endregion
        #region "编辑帐号信息"
        /// <summary>
        /// 编辑帐号信息
        /// </summary>
        /// <param name="Dict">键值</param>
        public void EditUserInfo(DictHelper<string> Dict)
        {
            List<string> StrSql = new List<string>();
            List<IDataParameter[]> Param = new List<IDataParameter[]>();

            //编辑帐号信息
            string StrSql1 = "Update " + DbConfig.Prefix + "Users Set UserTrueName=@UserTrueName,UserEmail=@UserEmail,UserSex=@UserSex,UserMobile=@UserMobile,UserTelPhone=@UserTelPhone,UserFaxNumber=@UserFaxNumber,UserQQ=@UserQQ,UserNickName=@UserNickName,UserAddRess=@UserAddRess,UserRemark=@UserRemark Where UserID=@UserID";
            IDataParameter[] Param1 = new IDataParameter[] { 
                DbHelper.MakeParam("@UserID",Dict.GetValue("UserID")),
                DbHelper.MakeParam("@UserTrueName",Dict.GetValue("UserTrueName")),
                DbHelper.MakeParam("@UserEMail",Dict.GetValue("UserEMail")),
                DbHelper.MakeParam("@UserSex",Dict.GetValue("UserSex")),
                DbHelper.MakeParam("@UserMobile",Dict.GetValue("UserMobile")),
                DbHelper.MakeParam("@UserTelPhone",Dict.GetValue("UserTelPhone")),
                DbHelper.MakeParam("@UserFaxNumber",Dict.GetValue("UserFaxNumber")),
                DbHelper.MakeParam("@UserQQ",Dict.GetValue("UserQQ")),
                DbHelper.MakeParam("@UserNickName",Dict.GetValue("UserNickName")),
                DbHelper.MakeParam("@UserAddRess",Dict.GetValue("UserAddRess")),
                DbHelper.MakeParam("@UserRemark",Dict.GetValue("UserRemark"))
            };
            StrSql.Add(StrSql1);
            Param.Add(Param1);

            //编辑登录密码
            string UserPass = Dict.GetValue("UserPass");
            if (!string.IsNullOrEmpty(UserPass))
            {
                string StrSql2 = "Update " + DbConfig.Prefix + "Users Set UserPass=@UserPass Where UserID=@UserID";
                IDataParameter[] Param2 = new IDataParameter[] { 
                    DbHelper.MakeParam("@UserID",Dict.GetValue("UserID")),
                    DbHelper.MakeParam("@UserPass",EncryptUtils.MD5(UserPass))
                };
                StrSql.Add(StrSql2);
                Param.Add(Param2);
            }

            //编辑门店信息
            string StrSql3 = "Update " + DbConfig.Prefix + "AppUsers Set AppID=@AppID,IsAdmin=@IsAdmin Where UserID=@UserID";
            IDataParameter[] Param3 = new IDataParameter[] { 
                DbHelper.MakeParam("@AppID",Dict.GetValue("AppID")),
                DbHelper.MakeParam("@IsAdmin",Dict.GetValue("IsAdmin")),
                DbHelper.MakeParam("@UserID",Dict.GetValue("UserID"))
            };
            StrSql.Add(StrSql3);
            Param.Add(Param3);

            //编辑角色信息
            string StrSql4 = "Update " + DbConfig.Prefix + "RoleUsers Set RoleID=@RoleID,IsAdmin=@IsAdmin Where UserID=@UserID";
            IDataParameter[] Param4 = new IDataParameter[] { 
                DbHelper.MakeParam("@RoleID",Dict.GetValue("RoleID")),
                DbHelper.MakeParam("@IsAdmin",Dict.GetValue("IsAdmin")),
                DbHelper.MakeParam("@UserID",Dict.GetValue("UserID"))
            };
            StrSql.Add(StrSql4);
            Param.Add(Param4);

            //执行修改操作
            DbHelper.ExecuteSqlTran(StrSql, Param);
        }
        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="UserPass"></param>
        public void EditUserPass(string UserName, string UserPass)
        {
            string StrSql = "Update " + DbConfig.Prefix + "Users Set UserPass=@UserPass Where UserName=@UserName";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@UserName",UserName),
                DbHelper.MakeParam("@UserPass",UserPass)
            };

            DbHelper.ExecuteNonQuery(StrSql, Param);
        }
        /// <summary>
        /// 更新登录信息
        /// </summary>
        /// <param name="UserID">帐号序号</param>
        private void EditUserLoginInfo(int UserID)
        {
            string StrSql = "Update " + DbConfig.Prefix + "Users Set LoginNum=LoginNum+1,LoginIP=@LoginIP,LoginTime=@LoginTime Where UserID=@UserID";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@LoginIP",Public.GetUserIP()),
                DbHelper.MakeParam("@LoginTime",DateTime.Now.ToString()),
                DbHelper.MakeParam("@UserID",UserID)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);
        }
        #endregion
        #region "启用禁用帐号"
        /// <summary>
        /// 启用禁用帐号
        /// </summary>
        public void IsLock(int UserID,int IsLock)
        {
            string StrSql = "Update " + DbConfig.Prefix + "Users Set IsLock=@IsLock Where UserID=@UserID";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@IsLock",IsLock),
                DbHelper.MakeParam("@UserID",UserID)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);
        }
        #endregion
        #region "帐号密码重置"
        /// <summary>
        /// 帐号密码重置
        /// </summary>
        public void IsReset(int UserID)
        {
            string StrSql = "Update " + DbConfig.Prefix + "Users Set UserPass=@UserPass Where UserID=@UserID";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@UserPass",EncryptUtils.MD5(UIConfig.ResetPwd)),
                DbHelper.MakeParam("@UserID",UserID)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);
        }
        #endregion
        #region "删除用户帐号"
        /// <summary>
        /// 删除用户帐号
        /// </summary>
        /// <param name="UserID"></param>
        public void DelUserInfo(int UserID)
        {
            List<string> StrSql = new List<string>();
            List<IDataParameter[]> Param = new List<IDataParameter[]>();

            //删除帐号信息
            string StrSql1 = "Delete From " + DbConfig.Prefix + "Users Where UserID=@UserID";
            IDataParameter[] Param1 = new IDataParameter[] { DbHelper.MakeParam("@UserID", UserID) };
            StrSql.Add(StrSql1);
            Param.Add(Param1);

            //删除门店信息
            string StrSql2 = "Delete From " + DbConfig.Prefix + "AppUsers Where UserID=@UserID";
            IDataParameter[] Param2 = new IDataParameter[] { DbHelper.MakeParam("@UserID", UserID) };
            StrSql.Add(StrSql2);
            Param.Add(Param2);

            //删除角色信息
            string StrSql3 = "Delete From " + DbConfig.Prefix + "RoleUsers Where UserID=@UserID";
            IDataParameter[] Param3 = new IDataParameter[] { DbHelper.MakeParam("@UserID", UserID) };
            StrSql.Add(StrSql3);
            Param.Add(Param3);

            //执行删除操作
            DbHelper.ExecuteSqlTran(StrSql, Param);
        }
        #endregion
    }
}
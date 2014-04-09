using CNVP.Config;
using CNVP.Framework.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CNVP.Data
{
    public class AppliUsers
    {
        public void AddAppUsers(Model.AppliUsers Model)
        {
            string StrSql = "insert into " + DbConfig.Prefix +
                "AppliUsers (AppName, AppCardID, AppPhone, AppAddress, AppEmail, PostTime) values (@AppName, @AppCardID, @AppPhone, @AppAddress, @AppEmail, @PostTime)";
            IDataParameter[] Param = new IDataParameter[] {
                DbHelper.MakeParam("@AppName", Model.AppName),
                DbHelper.MakeParam("@AppPhone", Model.AppPhone),
                DbHelper.MakeParam("@AppCardID", Model.AppCardID),
                DbHelper.MakeParam("@AppAddress", Model.AppAddress),
                DbHelper.MakeParam("@AppEmail", Model.AppEmail),
                DbHelper.MakeParam("@PostTime", Model.PostTime)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);
        }

        public void Del(int ID)
        {
            string StrSql = "Delete from " + DbConfig.Prefix + "AppliUsers Where ID=@ID";
            IDataParameter[] Param = new IDataParameter[] {
                DbHelper.MakeParam("@ID", ID)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);
        }

        #region 获取列表
        public List<Model.AppliUsers> GetAppList(string StrSql)
        {
            List<Model.AppliUsers> model = new List<Model.AppliUsers>();
            string Sql = "Select top 1 * from " + DbConfig.Prefix + "AppliUsers " + StrSql + "Order By PostTime Desc";
            model = DbHelper.ExecuteTable<Model.AppliUsers>(Sql);
            return model;
        }
        #endregion

        #region 获取内容
        public Model.AppliUsers GetUserInfo(int UserID)
        {
            string StrSql = "Select * from " + DbConfig.Prefix + "AppliUsers Where ID=@UserID";
            IDataParameter[] Param = new IDataParameter[] {
                DbHelper.MakeParam("@UserID", UserID)
            };
            return DbHelper.ExecuteReader<Model.AppliUsers>(StrSql, Param);
        }
        #endregion
    }
}

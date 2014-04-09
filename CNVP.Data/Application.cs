using CNVP.Config;
using CNVP.Framework.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CNVP.Data
{
    public class Application
    {
        public void AddApplication(Model.Application Model)
        {
            string StrSql = "insert into " + DbConfig.Prefix +
                "Application (AppType, AppPic, AppResult, PostTime, IsAudit, AppContent, AppUserID, AppThings) values (@AppType, @AppPic, @AppResult, @PostTime, @IsAudit, @AppContent, @AppUserID, @AppThings)";
            IDataParameter[] Param = new IDataParameter[] {
                DbHelper.MakeParam("@AppType", Model.AppType),
                DbHelper.MakeParam("@AppPic", Model.AppPic),
                DbHelper.MakeParam("@AppResult", Model.AppResult),
                DbHelper.MakeParam("@PostTime", Model.PostTime),
                DbHelper.MakeParam("@IsAudit", Model.IsAudit),
                DbHelper.MakeParam("@AppContent", Model.AppContent),
                DbHelper.MakeParam("@AppUserID", Model.AppUserID),
                DbHelper.MakeParam("@AppThings", Model.AppThings)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);
        }

        public void Del(int ID)
        {
            string StrSql = "Delete from " + DbConfig.Prefix + "Application Where ID=@ID";
            IDataParameter[] Param = new IDataParameter[] {
                DbHelper.MakeParam("@ID", ID)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);
        }

        public void IsLock(int ID, string IsLock)
        {
            string StrSql = "update " + DbConfig.Prefix + "Application set IsAudit=@IsLock Where ID=@ID";
            IDataParameter[] Param = new IDataParameter[] {
                DbHelper.MakeParam("@ID", ID),
                DbHelper.MakeParam("@IsLock", IsLock)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);
        }

        #region 获取列表
        public List<Model.Application> GetAppList(string StrSql)
        {
            List<Model.Application> model = new List<Model.Application>();
            string Sql = "Select * from " + DbConfig.Prefix + "Application " + StrSql + "Order By ID Desc";
            model = DbHelper.ExecuteTable<Model.Application>(Sql);
            return model;
        }
        #endregion

        #region 管理员回复
        public void AppReply(Model.Application model)
        {
            string StrSql = "update " + DbConfig.Prefix + "Application set AppReply=@AppReply, IsAudit=@IsAudit,AuditMan=@AuditMan where ID=@ID";
            IDataParameter[] Param = new IDataParameter[] {
                DbHelper.MakeParam("@AppReply", model.AppReply),
                DbHelper.MakeParam("@IsAudit", model.IsAudit),
                DbHelper.MakeParam("@AuditMan", model.AuditMan),
                DbHelper.MakeParam("@ID", model.ID)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);
        }
        #endregion

    }
}

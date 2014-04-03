using CNVP.Config;
using CNVP.Framework.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CNVP.Data
{
    public class Notice
    {
        #region 添加申请事项
        /// <summary>
        /// 添加申请事项
        /// </summary>
        /// <param name="model"></param>
        public void AppNotice(Model.Notice model)
        {
            string StrSql = "insert into " + DbConfig.Prefix +
                "Notice (NoticeTitle,NoticeContent, PostTime) values (@NoticeTitle, @NoticeContent, @PostTime)";
            IDataParameter[] Param = new IDataParameter[] {
                DbHelper.MakeParam("@NoticeTitle", model.NoticeTitle),
                DbHelper.MakeParam("@NoticeContent", model.NoticeContent),
                DbHelper.MakeParam("@PostTime", model.PostTime)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);
        }
        #endregion

        #region 删除申请事项
        /// <summary>
        /// 删除申请事项
        /// </summary>
        /// <param name="ID"></param>
        public void Del(int ID)
        {
            string StrSql = "Delete from " + DbConfig.Prefix + "Notice Where ID=@ID";
            IDataParameter[] Param = new IDataParameter[] {
                DbHelper.MakeParam("@ID", ID)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);
        }
        #endregion

        #region 事项列表
        /// <summary>
        /// 事项列表
        /// </summary>
        /// <param name="StrSql"></param>
        /// <returns></returns>
        public List<Model.Notice> GetAppList(string StrSql)
        {
            List<Model.Notice> model = new List<Model.Notice>();
            string Sql = "Select * from " + DbConfig.Prefix + "Notice " + StrSql + "Order By ID Desc";
            model = DbHelper.ExecuteTable<Model.Notice>(Sql);
            return model;
        }
        #endregion

        #region 读取事项信息
        public DataTable GetNoticeInfo(int ID)
        {
            string StrSql = "Select * from " + DbConfig.Prefix + "Notice Where ID=@ID";
            IDataParameter[] Param = new IDataParameter[] {
                DbHelper.MakeParam("@ID", ID)
            };
            return DbHelper.ExecuteTable(StrSql, Param);
        }
        #endregion

        #region 更新事项信息
        public void EditNoticeInfo(Model.Notice model)
        {
            string StrSql = "update " + DbConfig.Prefix + "Notice set NoticeTitle=@NoticeTitle, NoticeContent=@NoticeContent Where ID=@ID";
            IDataParameter[] Param = new IDataParameter[] {
                DbHelper.MakeParam("@NoticeTitle", model.NoticeTitle),
                DbHelper.MakeParam("@NoticeContent", model.NoticeContent),
                DbHelper.MakeParam("@ID", model.ID)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);
        }
        #endregion
    }
}

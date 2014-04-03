using CNVP.Config;
using CNVP.Framework.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CNVP.Data
{
    public class Type
    {
        #region 添加申请事项
        /// <summary>
        /// 添加申请事项
        /// </summary>
        /// <param name="model"></param>
        public void AppType(Model.Type model)
        {
            string StrSql = "insert into " + DbConfig.Prefix + 
                "Type (TypeName,TypeContent, PostTime) values (@TypeName, @TypeContent, @PostTime)";
            IDataParameter[] Param = new IDataParameter[] {
                DbHelper.MakeParam("@TypeName", model.TypeName),
                DbHelper.MakeParam("@TypeContent", model.TypeContent),
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
            string StrSql = "Delete from " + DbConfig.Prefix + "Type Where ID=@ID";
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
        public List<Model.Type> GetAppList(string StrSql)
        {
            List<Model.Type> model = new List<Model.Type>();
            string Sql = "Select * from " + DbConfig.Prefix + "Type " + StrSql + "Order By ID Desc";
            model = DbHelper.ExecuteTable<Model.Type>(Sql);
            return model;
        }
        #endregion

        #region 读取事项信息
        public DataTable GetTypeInfo(int ID)
        {
            string StrSql = "Select * from " + DbConfig.Prefix + "Type Where ID=@ID";
            IDataParameter[] Param = new IDataParameter[] {
                DbHelper.MakeParam("@ID", ID)
            };
            return DbHelper.ExecuteTable(StrSql, Param);
        }
        #endregion

        #region 更新事项信息
        public void EditTypeInfo(Model.Type model)
        {
            string StrSql = "update " + DbConfig.Prefix + "Type set TypeName=@TypeName, TypeContent=@TypeContent Where ID=@ID";
            IDataParameter[] Param = new IDataParameter[] {
                DbHelper.MakeParam("@TypeName", model.TypeName),
                DbHelper.MakeParam("@TypeContent", model.TypeContent),
                DbHelper.MakeParam("@ID", model.ID)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);
        }
        #endregion
    }
}

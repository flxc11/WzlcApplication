using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using CNVP.Config;
using CNVP.Framework.Utils;
using CNVP.Framework.Helper;

namespace CNVP.Client.Data
{
    public class Group
    {
        #region "读取套餐名称"
        /// <summary>
        /// 读取套餐名称
        /// </summary>
        /// <param name="GroupID">套餐序号</param>
        /// <returns></returns>
        public Model.Group GetGroupInfo(string GroupID)
        {
            string StrSql = "Select * From " + DbConfig.Prefix + "Group Where GroupID=@GroupID";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@GroupID",GroupID)
            };
            return DbHelper.ExecuteReader<Model.Group>(StrSql, Param);
        }
        #endregion
        #region "读取套餐列表"
        /// <summary>
        /// 读取套餐列表
        /// </summary>
        /// <param name="AppID"></param>
        /// <returns></returns>
        public List<Model.Group> GetGroupList(int AppID)
        {
            string StrSql = "Select * From " + DbConfig.Prefix + "Group Where AppID=@AppID Order By OrderID Desc";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@AppID",AppID)
            };
            return DbHelper.ExecuteTable<Model.Group>(StrSql, Param);
        }
        #endregion
    }
}
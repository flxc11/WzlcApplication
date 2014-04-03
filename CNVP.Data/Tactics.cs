using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CNVP.Config;
using CNVP.Framework.Utils;
using CNVP.Framework.Helper;

namespace CNVP.Data
{
    public class Tactics
    {
        #region "增加更新任务"
        /// <summary>
        /// 增加更新任务
        /// </summary>
        /// <param name="model"></param>
        public void AddTactics(Model.Tactics model)
        {
            string StrSql = "Insert Into " + DbConfig.Prefix + "Tactics (AppID,Action,SqlInfo,FilePath,Priority,IsUpdate,PostTime) Values (@AppID,@Action,@SqlInfo,@FilePath,@Priority,@IsUpdate,@PostTime)";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@AppID",model.AppID),
                DbHelper.MakeParam("@Action",model.Action),
                DbHelper.MakeParam("@SqlInfo",model.SqlInfo),
                DbHelper.MakeParam("@FilePath",model.FilePath),
                DbHelper.MakeParam("@Priority",model.Priority),
                DbHelper.MakeParam("@IsUpdate",0),
                DbHelper.MakeParam("@PostTime",DateTime.Now.ToString())
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);
        }
        #endregion
        #region "获取更新任务"
        /// <summary>
        /// 获取更新任务
        /// </summary>
        /// <param name="AppID">门店序号</param>
        public List<Model.Tactics> GetTopItem(int AppID)
        {
            return GetTactics(AppID, 1000);
        }
        /// <summary>
        /// 获取更新任务
        /// </summary>
        /// <param name="AppID">门店序号</param>
        /// <param name="Number">任务条数</param>
        /// <returns></returns>
        public List<Model.Tactics> GetTopItem(int AppID, int Number)
        {
            return GetTactics(AppID, Number);
        }
        /// <summary>
        /// 获取所有任务
        /// </summary>
        /// <param name="AppID">门店序号</param>
        /// <returns></returns>
        public List<Model.Tactics> GetAllItem(int AppID)
        {
            return GetTactics(AppID, 0);
        }
        /// <summary>
        /// 获取更新任务
        /// </summary>
        /// <param name="AppID">门店序号</param>
        /// <param name="TopNumber">任务条数</param>
        /// <returns></returns>
        private List<Model.Tactics> GetTactics(int AppID, int TopNumber)
        {
            string StrSql = "Select{0} * From " + DbConfig.Prefix + "Tactics Where IsUpdate=0 And AppID In (0,@AppID) Order By Priority Desc,TacticsID Asc";
            if (TopNumber != 0)
            {
                StrSql = string.Format(StrSql, " Top " + TopNumber);
            }

            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@AppID",AppID)
            };
            return DbHelper.ExecuteTable<Model.Tactics>(StrSql, Param);
        }
        #endregion
        #region "更新任务状态"
        /// <summary>
        /// 更新任务状态
        /// </summary>
        /// <param name="TacticsID">任务序号</param>
        /// <param name="AppID">门店序号</param>
        public void Update(int TacticsID, int AppID)
        {
            string StrSql = "Update " + DbConfig.Prefix + "Tactics Set IsUpdate=1,UpdateIP=@UpdateIP,UpdateTime=@UpdateTime Where TacticsID=@TacticsID And AppID=@AppID";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@UpdateIP",Public.GetUserIP()),
                DbHelper.MakeParam("@UpdateTime",DateTime.Now.ToString()),
                DbHelper.MakeParam("@TacticsID",TacticsID),
                DbHelper.MakeParam("@AppID",AppID)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CNVP.Config;
using CNVP.Cache;
using CNVP.Framework.Helper;

namespace CNVP.Data
{
    public class AppList
    {
        #region "是否存在门店"
        /// <summary>
        /// 是否存在门店
        /// </summary>
        /// <param name="AppName"></param>
        /// <returns></returns>
        public bool IsExist(string AppName)
        {
            bool flg=false;
            string StrSql = "Select Count(AppID) From " + DbConfig.Prefix + "AppList Where AppName=@AppName";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@AppName",AppName)
            };
            int Count = Convert.ToInt32(DbHelper.ExecuteScalar(StrSql, Param));
            if (Count > 0)
            {
                flg = true;
            }
            return flg;
        }
        /// <summary>
        /// 是否存在门店
        /// </summary>
        /// <param name="AppID">门店序号</param>
        /// <param name="AppName">门店名称</param>
        /// <returns></returns>
        public bool IsExist(int AppID, string AppName)
        {
            bool flg = false;
            string StrSql = "Select Count(AppID) From " + DbConfig.Prefix + "AppList Where AppName=@AppName And AppID<>@AppID";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@AppName",AppName),
                DbHelper.MakeParam("@AppID",AppID)
            };
            int Count = Convert.ToInt32(DbHelper.ExecuteScalar(StrSql, Param));
            if (Count > 0)
            {
                flg = true;
            }
            return flg;
        }
        #endregion
        #region "获取门店信息"
        /// <summary>
        /// 获取门店信息
        /// </summary>
        /// <returns></returns>
        public Model.AppList GetAppInfo(int AppID)
        {
            string StrSql = "Select * From " + DbConfig.Prefix + "AppList Where AppID=@AppID";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@AppID",AppID)
            };
            return DbHelper.ExecuteReader<Model.AppList>(StrSql, Param);
        }
        /// <summary>
        /// 获取门店信息
        /// </summary>
        /// <returns></returns>
        public List<Model.AppList> GetAppList()
        {
            BaseCache Cache = BaseCache.GetCacheService();
            List<Model.AppList> model = Cache.RetrieveObject(CacheKeys.StoreList) as List<Model.AppList>;
            if (model == null)
            {
                string StrSql = "Select * From " + DbConfig.Prefix + "AppList Order By AppID Asc";
                model = DbHelper.ExecuteTable<Model.AppList>(StrSql);

                //增加对象缓存
                Cache.AddObject(CacheKeys.StoreList, model);
            }
            return model;
        }
        /// <summary>
        /// 获取门店信息
        /// </summary>
        /// <param name="AppID">集团序号</param>
        /// <returns></returns>
        public List<Model.AppList> GetAppList(int AppID)
        {
            string StrSql = "Select * From " + DbConfig.Prefix + "AppList Where AppID<>1 Order By AppID Asc";
            return DbHelper.ExecuteTable<Model.AppList>(StrSql);
        }
        /// <summary>
        /// 获取使用门店
        /// </summary>
        /// <returns></returns>
        public List<Model.AppList> GetAppListUses()
        {
            string StrSql = "Select * From " + DbConfig.Prefix + "AppList Where IsUses=1 Order By AppID Asc";
            return DbHelper.ExecuteTable<Model.AppList>(StrSql);
        }
        #endregion
        #region "增加门店信息"
        /// <summary>
        /// 增加门店信息
        /// </summary>
        /// <param name="model"></param>
        public void AddAppInfo(Model.AppList model)
        {
            string StrSql = "Insert Into " + DbConfig.Prefix + "AppList (AppName,AppUrl,AppAddRess,AppTelPhone,AppServiceTelPhone,AppSOSTelPhone,AppFaxNumber,AppPubKey,AppPriKey,IsLock,PostTime) Values (@AppName,@AppUrl,@AppAddRess,@AppTelPhone,@AppServiceTelPhone,@AppSOSTelPhone,@AppFaxNumber,@AppPubKey,@AppPriKey,@IsLock,@PostTime)";
            IDataParameter[] Param = new IDataParameter[]{
                DbHelper.MakeParam("@AppName",model.AppName),
                DbHelper.MakeParam("@AppUrl",model.AppUrl),
                DbHelper.MakeParam("@AppAddRess",model.AppAddRess),
                DbHelper.MakeParam("@AppTelPhone",model.AppTelPhone),
                DbHelper.MakeParam("@AppServiceTelPhone",model.AppServiceTelPhone),
                DbHelper.MakeParam("@AppSOSTelPhone",model.AppSOSTelPhone),
                DbHelper.MakeParam("@AppFaxNumber",model.AppFaxNumber),
                DbHelper.MakeParam("@AppPubKey",model.AppPubKey),
                DbHelper.MakeParam("@AppPriKey",model.AppPriKey),
                DbHelper.MakeParam("@IsLock",model.IsLock),
                DbHelper.MakeParam("@PostTime",DateTime.Now.ToString())
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);

            //清除门店缓存
            ClearCache();
        }
        #endregion
        #region "编辑门店信息"
        /// <summary>
        /// 门店信息修改
        /// </summary>
        /// <param name="model"></param>
        public void EditAppInfo(Model.AppList model)
        {
            string StrSql = "Update " + DbConfig.Prefix + "AppList Set AppName=@AppName,AppUrl=@AppUrl,AppAddRess=@AppAddRess,AppTelPhone=@AppTelPhone,AppServiceTelPhone=@AppServiceTelPhone,AppSOSTelPhone=@AppSOSTelPhone,AppFaxNumber=@AppFaxNumber,AppPubKey=@AppPubKey,AppPriKey=@AppPriKey,IsLock=@IsLock Where AppID=@AppID";
            IDataParameter[] Param = new IDataParameter[]{
                DbHelper.MakeParam("@AppID",model.AppID),
                DbHelper.MakeParam("@AppName",model.AppName),
                DbHelper.MakeParam("@AppUrl",model.AppUrl),
                DbHelper.MakeParam("@AppAddRess",model.AppAddRess),
                DbHelper.MakeParam("@AppTelPhone",model.AppTelPhone),
                DbHelper.MakeParam("@AppServiceTelPhone",model.AppServiceTelPhone),
                DbHelper.MakeParam("@AppSOSTelPhone",model.AppSOSTelPhone),
                DbHelper.MakeParam("@AppFaxNumber",model.AppFaxNumber),
                DbHelper.MakeParam("@AppPubKey",model.AppPubKey),
                DbHelper.MakeParam("@AppPriKey",model.AppPriKey),
                DbHelper.MakeParam("@IsLock",model.IsLock)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);

            //清除门店缓存
            ClearCache();
        }
        /// <summary>
        /// 门店状态修改
        /// </summary>
        public void EditIsLock(int AppID,int IsLock)
        {
            string StrSql = "Update " + DbConfig.Prefix + "AppList Set IsLock=@IsLock Where AppID=@AppID";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@IsLock",IsLock),
                DbHelper.MakeParam("@AppID",AppID)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);

            //清除门店缓存
            ClearCache();
        }
        #endregion
        #region "删除门店信息"
        /// <summary>
        /// 删除门店信息
        /// </summary>
        /// <param name="AppID"></param>
        public void DelAppInfo(int AppID)
        {
            //删除门店信息
            string StrSql = "Delete From " + DbConfig.Prefix + "AppList Where AppID=@AppID";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@AppID",AppID)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);

            //清除门店缓存
            ClearCache();

            //删除用户信息

            //删除商品信息

            //删除订购信息
        }
        #endregion
        #region "清除门店缓存"
        /// <summary>
        /// 清除门店缓存
        /// </summary>
        private void ClearCache()
        {
            BaseCache Cache = BaseCache.GetCacheService();
            Cache.RemoveObject(CacheKeys.StoreList);
        }
        #endregion
        #region "重置门店数据"
        /// <summary>
        /// 重置门店数据
        /// </summary>
        /// <param name="AppID">门店序号</param>
        public void SiteClear(int AppID)
        {
            List<string> StrSql = new List<string>();
            List<IDataParameter[]> Param = new List<IDataParameter[]>();

            //清除服务端表
            string[] Tables = new string[] { "Tactics"};
            foreach (string a in Tables)
            {
                string DelSql = "Delete From " + DbConfig.Prefix + a + " Where AppID=@AppID";
                IDataParameter[] DelParam = new IDataParameter[] { 
                    DbHelper.MakeParam("@AppID",AppID)
                };
                StrSql.Add(DelSql);
                Param.Add(DelParam);
            }

            //清除客户端表
            Tables = new string[] { "AppCar", "CarBrand", "CarProduct", "CarType", "FileSources", "Group", "GroupProduct", "Software", "Type", "TypeProduct" };
            foreach (string a in Tables)
            {
                string TempSql = "Delete From " + DbConfig.Prefix + a + "";
                #region "增加操作日志"
                string TacticsSql = "Insert Into " + DbConfig.Prefix + "Tactics (AppID,Action,SqlInfo,FilePath,Priority,IsUpdate,PostTime) Values (@AppID,@Action,@SqlInfo,@FilePath,@Priority,@IsUpdate,@PostTime)";
                IDataParameter[] TacticsParam = new IDataParameter[] { 
                    DbHelper.MakeParam("@AppID",AppID),
                    DbHelper.MakeParam("@Action","UpdateSql"),
                    DbHelper.MakeParam("@SqlInfo",TempSql),
                    DbHelper.MakeParam("@FilePath",""),
                    DbHelper.MakeParam("@Priority",1),
                    DbHelper.MakeParam("@IsUpdate",0),
                    DbHelper.MakeParam("@PostTime",DateTime.Now.ToString())
                };
                StrSql.Add(TacticsSql);
                Param.Add(TacticsParam);
                #endregion
            }

            DbHelper.ExecuteSqlTran(StrSql, Param);
        }
        #endregion
    }
}
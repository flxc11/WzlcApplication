using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CNVP.Config;
using CNVP.Framework.Helper;
using CNVP.Cache;

namespace CNVP.Data
{
    public class Menu
    {
        #region "获取所有导航"
        /// <summary>
        /// 获取导航信息
        /// </summary>
        /// <param name="MenuID"></param>
        /// <returns></returns>
        public Model.Menu GetMenuInfo(int MenuID)
        {
            string StrSql = "Select * From " + DbConfig.Prefix + "Menu Where MenuID=@MenuID";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@MenuID",MenuID)
            };
            return DbHelper.ExecuteReader<Model.Menu>(StrSql, Param);
        }
        /// <summary>
        /// 获取所有导航
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllMenu()
        {
            //BaseCache Cache = BaseCache.GetCacheService();
            //DataTable Dt = Cache.RetrieveObject(CacheKeys.AllMenu) as DataTable;
            //if (Dt == null)
            //{
            //    string StrSql = "Select * From " + DbConfig.Prefix + "Menu Where IsEnable=1 Order By OrderID Asc";
            //    Dt = DbHelper.ExecuteTable(StrSql);

            //    //增加对象缓存
            //    Cache.AddObject(CacheKeys.AllMenu, Dt);
            //}
            string StrSql = "Select * From " + DbConfig.Prefix + "Menu Where IsEnable=1 Order By OrderID Asc";
            DataTable Dt = DbHelper.ExecuteTable(StrSql);
            return Dt;
        }
        /// <summary>
        /// 获取最大排序值
        /// </summary>
        /// <param name="MenuParent"></param>
        /// <returns></returns>
        public int GetMaxOrder(string MenuParent)
        {
            int OrderID = 0;
            string StrSql = "Select Top 1 OrderID From " + DbConfig.Prefix + "Menu Where MenuParent=@MenuParent Order By OrderID Desc";
            IDataParameter[] Param = new IDataParameter[] {
                DbHelper.MakeParam("@MenuParent",MenuParent)
            };
            DataTable Dt = DbHelper.ExecuteTable(StrSql, Param);
            if (Dt != null && Dt.Rows.Count > 0)
            {
                OrderID = Convert.ToInt32(Dt.Rows[0]["OrderID"]);
            }
            OrderID = OrderID + 1;
            return OrderID;
        }
        /// <summary>
        /// 获取所有导航
        /// </summary>
        /// <returns></returns>
        public int GetStartOrder(string MenuID, string MenuParent)
        {
            int OrderID = 1;
            string StrSql = string.Format("Select Top 1 MenuID,OrderID From " + DbConfig.Prefix + "Menu Where MenuID In ({0}) And MenuParent=@MenuParent Order By OrderID Asc", MenuID);
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@MenuParent",MenuParent)
            };
            DataTable Dt = DbHelper.ExecuteTable(StrSql, Param);
            if (Dt != null && Dt.Rows.Count>0)
            {
                OrderID = Convert.ToInt32(Dt.Rows[0]["OrderID"]);
            }
            return OrderID;
        }
        /// <summary>
        /// 获取上条序号
        /// </summary>
        /// <returns></returns>
        public List<Model.Menu> GetUpMenuInfo(int MenuID,string MenuParent)
        {
            string StrSql = "Select Top 2 MenuID,MenuParent,OrderID From " + DbConfig.Prefix + "Menu Where (OrderID <= (Select OrderID From " + DbConfig.Prefix + "Menu As a Where MenuID = @MenuID)) And (MenuParent = @MenuParent) Order By OrderID Desc";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@MenuID",MenuID),
                DbHelper.MakeParam("@MenuParent",MenuParent)
            };
            return DbHelper.ExecuteTable<Model.Menu>(StrSql, Param);
        }
        /// <summary>
        /// 获取下条记录
        /// </summary>
        /// <returns></returns>
        public List<Model.Menu> GetDownMenuInfo(int MenuID, string MenuParent)
        {
            string StrSql = "Select Top 2 MenuID,MenuParent,OrderID From " + DbConfig.Prefix + "Menu Where (OrderID >= (Select OrderID From " + DbConfig.Prefix + "Menu As a Where MenuID = @MenuID)) And (MenuParent = @MenuParent) Order By OrderID Asc";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@MenuID",MenuID),
                DbHelper.MakeParam("@MenuParent",MenuParent)
            };
            return DbHelper.ExecuteTable<Model.Menu>(StrSql, Param);
        }
        /// <summary>
        /// 是否存在子类
        /// </summary>
        /// <param name="MenuParent"></param>
        /// <returns></returns>
        public int IsExistLeaf(string MenuParent)
        {
            string StrSql = "Select Count(*) As Count From " + DbConfig.Prefix + "Menu Where MenuParent=@MenuParent";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@MenuParent",MenuParent)
            };
            return Convert.ToInt32(DbHelper.ExecuteScalar(StrSql, Param));
        }
        #endregion
        #region "增加导航菜单"
        /// <summary>
        /// 增加导航菜单
        /// </summary>
        /// <param name="model"></param>
        public void AddMenu(Model.Menu model)
        {
            string StrSql = "Insert Into " + DbConfig.Prefix + "Menu (MenuName,MenuValue,MenuUrl,MenuParent,MenuIcon,IsEnable,IsLeaf,OrderID) Values (@MenuName,@MenuValue,@MenuUrl,@MenuParent,@MenuIcon,1,0,@OrderID)";
            IDataParameter[] Param = new IDataParameter[]{
                DbHelper.MakeParam("@MenuName",model.MenuName),
                DbHelper.MakeParam("@MenuValue",model.MenuValue),
                DbHelper.MakeParam("@MenuUrl",model.MenuUrl),
                DbHelper.MakeParam("@MenuParent",model.MenuParent),
                DbHelper.MakeParam("@MenuIcon",model.MenuIcon),
                DbHelper.MakeParam("@OrderID",GetMaxOrder(model.MenuParent))
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);

            //清除菜单缓存
            ClearCache();
        }
        #endregion
        #region "编辑导航菜单"
        /// <summary>
        /// 编辑导航菜单
        /// </summary>
        /// <param name="model"></param>
        public void EditMenu(Model.Menu model)
        {
            string StrSql = "Update " + DbConfig.Prefix + "Menu Set MenuName=@MenuName,MenuValue=@MenuValue,MenuUrl=@MenuUrl,MenuIcon=@MenuIcon Where MenuID=@MenuID";
            IDataParameter[] Param = new IDataParameter[]{
                DbHelper.MakeParam("@MenuID",model.MenuID),
                DbHelper.MakeParam("@MenuName",model.MenuName),
                DbHelper.MakeParam("@MenuValue",model.MenuValue),
                DbHelper.MakeParam("@MenuUrl",model.MenuUrl),
                DbHelper.MakeParam("@MenuIcon",model.MenuIcon)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);

            //清除菜单缓存
            ClearCache();
        }
        /// <summary>
        /// 编辑导航排序
        /// </summary>
        /// <param name="MenuID"></param>
        public void EditMenuOrder(string MenuID, string MenuParent)
        {
            //获取最小排序值
            int OrderID = GetStartOrder(MenuID, MenuParent);

            List<string> SqlInfo = new List<string>();
            List<IDataParameter[]> Params = new List<IDataParameter[]>();

            string[] str = MenuID.Split(',');

            for (int i = 0; i < str.Length; i++)
            {
                string StrSql = "Update " + DbConfig.Prefix + "Menu Set OrderID=@OrderID Where MenuID=@MenuID And MenuParent=@MenuParent";
                IDataParameter[] Param = new IDataParameter[] { 
                    DbHelper.MakeParam("@OrderID",OrderID+i),
                    DbHelper.MakeParam("@MenuID",str[i]),
                    DbHelper.MakeParam("@MenuParent",MenuParent)
                };
                SqlInfo.Add(StrSql);
                Params.Add(Param);
            }

            //保存菜单排序
            DbHelper.ExecuteSqlTran(SqlInfo, Params);

            //清除菜单缓存
            ClearCache();
        }
        /// <summary>
        /// 编辑菜单排序
        /// </summary>
        /// <param name="Dt"></param>
        public void EditMenuOrder(List<Model.Menu> model)
        {
            if (model.Count == 2)
            {
                List<string> SqlInfo = new List<string>();
                List<IDataParameter[]> Params = new List<IDataParameter[]>();

                string StrSql = "Update " + DbConfig.Prefix + "Menu Set OrderID=@OrderID Where MenuID=@MenuID And MenuParent=@MenuParent";

                IDataParameter[] Param = new IDataParameter[] { 
                        DbHelper.MakeParam("@OrderID",model[1].OrderID),
                        DbHelper.MakeParam("@MenuID",model[0].MenuID),
                        DbHelper.MakeParam("@MenuParent",model[0].MenuParent)
                    };
                SqlInfo.Add(StrSql);
                Params.Add(Param);

                IDataParameter[] Param1 = new IDataParameter[] { 
                        DbHelper.MakeParam("@OrderID",model[0].OrderID),
                        DbHelper.MakeParam("@MenuID",model[1].MenuID),
                        DbHelper.MakeParam("@MenuParent",model[1].MenuParent)
                    };
                SqlInfo.Add(StrSql);
                Params.Add(Param1);

                //保存菜单排序
                DbHelper.ExecuteSqlTran(SqlInfo, Params);

                //清除菜单缓存
                ClearCache();
            }
        }
        #endregion
        #region "删除导航菜单"
        /// <summary>
        /// 删除导航菜单
        /// </summary>
        /// <param name="MenuValue"></param>
        public void DelMenu(string MenuValue)
        {
            string StrSql = "Delete From " + DbConfig.Prefix + "Menu Where MenuValue=@MenuValue Or MenuParent=@MenuValue";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@MenuValue",MenuValue)
            };

            DbHelper.ExecuteNonQuery(StrSql, Param);

            //清除菜单缓存
            ClearCache();
        }
        #endregion
        #region "清除菜单缓存"
        /// <summary>
        /// 清除菜单缓存
        /// </summary>
        private void ClearCache()
        {
            BaseCache Cache = BaseCache.GetCacheService();
            Cache.RemoveObject(CacheKeys.AllMenu);
        }
        #endregion
    }
}
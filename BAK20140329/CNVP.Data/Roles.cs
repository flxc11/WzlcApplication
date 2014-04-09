using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CNVP.Config;
using CNVP.Framework.Helper;
using CNVP.Cache;

namespace CNVP.Data
{
    public class Roles
    {
        #region "读取角色名称"
        /// <summary>
        /// 读取角色名称
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public Model.Roles GetRoles(int RoleID)
        {
            string StrSql = "Select * From " + DbConfig.Prefix + "Roles Where RoleID=@RoleID";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@RoleID",RoleID)
            };
            return DbHelper.ExecuteReader<Model.Roles>(StrSql, Param);
        }
        #endregion
        #region "读取角色列表"
        /// <summary>
        /// 读取角色列表
        /// </summary>
        /// <returns></returns>
        public List<Model.Roles> GetAllRoles()
        {
            BaseCache Cache = BaseCache.GetCacheService();
            List<Model.Roles> model = Cache.RetrieveObject(CacheKeys.AllRoles) as List<Model.Roles>;
            if (model == null)
            {
                string StrSql = "Select * From " + DbConfig.Prefix + "Roles Order By RoleID Asc";
                model = DbHelper.ExecuteTable<Model.Roles>(StrSql);

                //增加对象缓存
                Cache.AddObject(CacheKeys.AllRoles, model);
            }
            return model;
        }
        #endregion
        #region "读取角色序号"
        /// <summary>
        /// 读取角色序号
        /// </summary>
        private int GetRoleID()
        {
            int RoleID = 0;
            string StrSql = "Select Top 1 RoleID From " + DbConfig.Prefix + "Roles Order By RoleID Desc";
            DataTable Dt = DbHelper.ExecuteTable(StrSql);
            if (Dt != null && Dt.Rows.Count > 0)
            {
                RoleID = Convert.ToInt32(Dt.Rows[0]["RoleID"]);
            }
            RoleID = RoleID + 1;
            return RoleID;
        }
        #endregion
        #region "增加用户角色"
        /// <summary>
        /// 增加用户角色
        /// </summary>
        public void AddRoles(Model.Roles model)
        {
            string StrSql = "Insert Into " + DbConfig.Prefix + "Roles (RoleID,RoleName,RoleRemark) Values (@RoleID,@RoleName,@RoleRemark)";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@RoleID",GetRoleID()),
                DbHelper.MakeParam("@RoleName",model.RoleName),
                DbHelper.MakeParam("@RoleRemark",model.RoleRemark)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);

            //清除角色缓存
            ClearCache();
        }
        #endregion
        #region "编辑用户角色"
        /// <summary>
        /// 编辑用户角色
        /// </summary>
        public void EditRoles(Model.Roles model)
        {
            string StrSql = "Update " + DbConfig.Prefix + "Roles Set RoleName=@RoleName,RoleRemark=@RoleRemark Where RoleID=@RoleID";
            IDataParameter[] Param = new IDataParameter[]{
                DbHelper.MakeParam("@RoleID",model.RoleID),
                DbHelper.MakeParam("@RoleName",model.RoleName),
                DbHelper.MakeParam("@RoleRemark",model.RoleRemark)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);

            //清除角色缓存
            ClearCache();
        }
        #endregion
        #region "删除用户角色"
        /// <summary>
        /// 删除用户角色
        /// </summary>
        /// <param name="RoleID"></param>
        public void DelRoles(int RoleID)
        {
            List<string> StrSql = new List<string>();
            List<IDataParameter[]> Param = new List<IDataParameter[]>();

            //删除角色名称
            string StrSql1 = "Delete From " + DbConfig.Prefix + "Roles Where RoleID=@RoleID";
            IDataParameter[] Param1 = new IDataParameter[] {
                DbHelper.MakeParam("@RoleID",RoleID)
            };
            StrSql.Add(StrSql1);
            Param.Add(Param1);

            //删除权限信息
            string StrSql2 = "Delete From " + DbConfig.Prefix + "Permissions Where RoleID=@RoleID";
            IDataParameter[] Param2 = new IDataParameter[] {
                DbHelper.MakeParam("@RoleID",RoleID)
            };
            StrSql.Add(StrSql2);
            Param.Add(Param2);

            //执行删除操作
            DbHelper.ExecuteSqlTran(StrSql, Param);

            //清除角色缓存
            ClearCache();
        }
        #endregion
        #region "清除角色缓存"
        /// <summary>
        /// 清除门店缓存
        /// </summary>
        private void ClearCache()
        {
            BaseCache Cache = BaseCache.GetCacheService();
            Cache.RemoveObject(CacheKeys.AllRoles);
        }
        #endregion
    }
}
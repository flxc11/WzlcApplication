using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CNVP.Config;
using CNVP.Framework.Helper;
using CNVP.Framework.Utils;

namespace CNVP.Data
{
    public class Permissions
    {
        #region "操作权限判断"
        /// <summary>
        /// 操作权限判断
        /// </summary>
        /// <param name="RoleID">角色序号</param>
        /// <param name="OperateCode">操作权限</param>
        /// <returns></returns>
        public bool CheckPopDom(int RoleID, string OperateCode)
        {
            bool flg = false;

            string StrSql = "Select * From " + DbConfig.Prefix + "Permissions Where RoleID=@RoleID And OperateCode=@OperateCode";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@RoleID",RoleID),
                DbHelper.MakeParam("@OperateCode",OperateCode)
            };
            DataTable Dt = DbHelper.ExecuteTable(StrSql, Param);
            if (Dt != null && Dt.Rows.Count > 0)
            {
                flg = true;
            }
            return flg;
        }
        #endregion
        #region "获取权限信息"
        /// <summary>
        /// 获取权限信息
        /// </summary>
        /// <param name="RoleID">角色序号</param>
        /// <returns></returns>
        public List<Model.Permissions> GetPermission(int RoleID)
        {
            string StrSql = "Select * From " + DbConfig.Prefix + "Permissions Where RoleID=@RoleID";
            IDataParameter[] Param = new IDataParameter[] {
                DbHelper.MakeParam("@RoleID",RoleID)
            };
            return DbHelper.ExecuteTable<Model.Permissions>(StrSql, Param);
        }
        #endregion
        #region "保存权限信息"
        /// <summary>
        /// 保存权限信息
        /// </summary>
        public void AddPermission(int RoleID, List<string> Permissions)
        {
            List<string> StrSql = new List<string>();
            List<IDataParameter[]> Param = new List<IDataParameter[]>();

            //清除权限信息
            string StrSql1 = "Delete From " + DbConfig.Prefix + "Permissions Where RoleID=@RoleID";
            IDataParameter[] Param1 = new IDataParameter[] { 
                DbHelper.MakeParam("@RoleID",RoleID)
            };
            StrSql.Add(StrSql1);
            Param.Add(Param1);

            //增加权限信息
            foreach (string a in Permissions)
            {
                string StrSql2 = "Insert Into " + DbConfig.Prefix + "Permissions (RoleID,OperateCode) Values (@RoleID,@OperateCode)";
                IDataParameter[] Param2 = new IDataParameter[] { 
                    DbHelper.MakeParam("@RoleID",RoleID),
                    DbHelper.MakeParam("@OperateCode",a)
                };
                StrSql.Add(StrSql2);
                Param.Add(Param2);
            }

            //执行保存操作
            DbHelper.ExecuteSqlTran(StrSql, Param);
        }
        #endregion
    }
}
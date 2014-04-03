using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CNVP.Framework.Utils;
using CNVP.UI;

namespace CNVP.Admin
{
    public partial class Roles : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //判断用户权限
            base.CheckAuthority("Users");

            string Action = Request.Params["Action"];
            switch (Action)
            {
                case "RoleList":
                    RoleList();
                    break;
                case "MenuList":
                    MenuList();
                    break;
                case "GetPermission":
                    GetPermission();
                    break;
                case "SavePermission":
                    SavePermission();
                    break;
                case "GetRoles":
                    GetRoles();
                    break;
                case "AddRoles":
                    AddRoles();
                    break;
                case "EditRoles":
                    EditRoles();
                    break;
                case "DelRoles":
                    DelRoles();
                    break;
            }
        }
        #region "获取权限列表"
        /// <summary>
        /// 获取权限列表
        /// </summary>
        private void RoleList()
        {
            StringBuilder Str = new StringBuilder();
            Data.Roles bll = new Data.Roles();
            List<Model.Roles> model = bll.GetAllRoles();

            foreach (Model.Roles m in model)
            {
                Str.Append("{\"RoleID\":" + m.RoleID + ",\"RoleName\":\"" + m.RoleName + "\",\"RoleRemark\":\"" + m.RoleRemark + "\"},");
            }

            string ReturnStr = Str.ToString();
            if (!string.IsNullOrEmpty(ReturnStr))
            {
                ReturnStr = ReturnStr.Substring(0, ReturnStr.Length - 1);
            }

            Response.Write("{\"Rows\":[" + ReturnStr + "],\"Total\":\"" + model.Count + "\"}");
            Response.End();
        }
        #endregion
        #region "获取菜单列表"
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        private void MenuList()
        {
            StringBuilder Str = new StringBuilder();
            Data.Menu bll = new Data.Menu();
            DataTable Dt = bll.GetAllMenu();

            DataView RootDv = new DataView(Dt);
            RootDv.RowFilter = "MenuParent='Root'";
            RootDv.Sort = "OrderID Asc";
            DataTable RootDt = RootDv.ToTable();

            foreach (DataRow Row in RootDt.Rows)
            {
                Str.Append("{\"MenuID\":\"" + Row["MenuID"].ToString() + "\",\"MenuName\":\"" + Row["MenuName"].ToString() + "\",\"MenuValue\":\"" + Row["MenuValue"].ToString() + "\",\"MenuUrl\":\"" + Row["MenuUrl"].ToString() + "\",\"MenuParent\":\"" + Row["MenuParent"].ToString() + "\",\"MenuIcon\":\"" + Row["MenuIcon"].ToString() + "\",\"children\":[");
                if (Row["IsLeaf"].ToString() == "1")
                {
                    StringBuilder Str1 = new StringBuilder();
                    DataView ChildDv = new DataView(Dt);
                    ChildDv.RowFilter = "MenuParent='" + Row["MenuValue"].ToString() + "'";
                    ChildDv.Sort = "OrderID Asc";
                    DataTable ChildDt = ChildDv.ToTable();

                    foreach (DataRow Row1 in ChildDt.Rows)
                    {
                        Str1.Append("{\"MenuID\":\"" + Row1["MenuID"].ToString() + "\",\"MenuName\":\"" + Row1["MenuName"].ToString() + "\",\"MenuValue\":\"" + Row1["MenuValue"].ToString() + "\",\"MenuUrl\":\"" + Row1["MenuUrl"].ToString() + "\",\"MenuParent\":\"" + Row1["MenuParent"].ToString() + "\",\"MenuIcon\":\"" + Row1["MenuIcon"].ToString() + "\",\"children\":[]},");
                    }
                    string ReturnStr1 = Str1.ToString();
                    if (!string.IsNullOrEmpty(ReturnStr1))
                    {
                        ReturnStr1 = ReturnStr1.Substring(0, ReturnStr1.Length - 1);
                    }
                    Str.Append(ReturnStr1);
                }
                Str.Append("]},");
            }
            string ReturnStr = Str.ToString();
            if (!string.IsNullOrEmpty(ReturnStr))
            {
                ReturnStr = ReturnStr.Substring(0, ReturnStr.Length - 1);
            }

            Response.Write("{\"IsError\":false,\"Message\":null,\"Data\":[" + ReturnStr + "]}");
            Response.End();
        }
        #endregion
        #region "获取角色权限"
        /// <summary>
        /// 获取角色权限
        /// </summary>
        private void GetPermission()
        {
            StringBuilder Str = new StringBuilder();
            string RoleID = Request.Params["RoleID"];
            if (string.IsNullOrEmpty(RoleID) || (!Public.IsNumber(RoleID)))
            {
                RoleID = "0"; 
            }

            Data.Permissions bll = new Data.Permissions();
            List<Model.Permissions> model = bll.GetPermission(Convert.ToInt32(RoleID));
            foreach (Model.Permissions m in model)
            {
                Str.Append("{\"RoleID\":" + m.RoleID + ",\"OperateCode\":\"" + m.OperateCode + "\"},");
            }

            string ReturnStr = Str.ToString();
            if (!string.IsNullOrEmpty(ReturnStr))
            {
                ReturnStr = ReturnStr.Substring(0, ReturnStr.Length - 1);
            }

            Response.Write("{\"IsError\":false,\"Message\":null,\"Data\":[" + ReturnStr + "]}");
            Response.End();
        }
        #endregion
        #region "设置角色权限"
        /// <summary>
        /// 设置角色权限
        /// </summary>
        private void SavePermission()
        {
            string RoleID = Request.Params["RoleID"];
            string Operate = Request.Params["Operate"];

            if (string.IsNullOrEmpty(RoleID) || (!Public.IsNumber(RoleID)))
            {
                RoleID = "0"; 
            }

            List<string> OperateCode = new List<string>();
            foreach (string a in Operate.Split(','))
            {
                string b = Public.FilterSql(a);
                if (!string.IsNullOrEmpty(b))
                {
                    OperateCode.Add(b);
                }
            }

            Data.Permissions bll = new Data.Permissions();
            bll.AddPermission(Convert.ToInt32(RoleID), OperateCode);

            Response.End();
        }
        #endregion
        #region "读取角色名称"
        /// <summary>
        /// 读取角色名称
        /// </summary>
        private void GetRoles()
        {
            string RoleID = Request.Params["RoleID"];
            if (string.IsNullOrEmpty(RoleID) || (!Public.IsNumber(RoleID)))
            {
                RoleID = "0";
            }

            Data.Roles bll = new Data.Roles();
            Model.Roles model = bll.GetRoles(Convert.ToInt32(RoleID));
            if (model != null)
            {
                Response.Write("{\"IsError\":false,\"Message\":\"加载成功\",\"Data\":{\"RoleID\":\"" + model.RoleID + "\",\"RoleName\":\"" + model.RoleName + "\",\"RoleRemark\":\"" + model.RoleRemark + "\"}}");
            }
            else
            {
                Response.Write("{\"IsError\":true,\"Message\":\"找不到该记录\",\"Data\":{}}");
            }
            Response.End();
        }
        #endregion
        #region "增加角色名称"
        /// <summary>
        /// 增加角色名称
        /// </summary>
        private void AddRoles()
        {
            string RoleName = Public.FilterSql(Request.Params["RoleName"]);
            string RoleRemark = Request.Params["RoleRemark"];

            Data.Roles bll = new Data.Roles();
            Model.Roles model = new Model.Roles();
            model.RoleName = RoleName;
            model.RoleRemark = RoleRemark;
            bll.AddRoles(model);

            Response.Write("{\"IsError\":false,\"Message\":\"保存成功\"}");
            Response.End();
        }
        #endregion
        #region "编辑角色名称"
        /// <summary>
        /// 编辑角色名称
        /// </summary>
        private void EditRoles()
        {
            string RoleID = Request.Params["RoleID"];
            string RoleName = Public.FilterSql(Request.Params["RoleName"]);
            string RoleRemark = Request.Params["RoleRemark"];

            if (string.IsNullOrEmpty(RoleID)|| (!Public.IsNumber(RoleID)))
            {
                RoleID = "0"; 
            }

            Data.Roles bll = new Data.Roles();
            Model.Roles model = new Model.Roles();
            model.RoleID = Convert.ToInt32(RoleID);
            model.RoleName = RoleName;
            model.RoleRemark = RoleRemark;
            bll.EditRoles(model);

            Response.Write("{\"IsError\":false,\"Message\":\"保存成功\"}");
            Response.End();
        }
        #endregion
        #region "删除角色名称"
        /// <summary>
        /// 删除角色名称
        /// </summary>
        private void DelRoles()
        {
            string RoleID = Request.Params["RoleID"];
            foreach (string a in RoleID.Split(','))
            {
                Data.Roles bll = new Data.Roles();
                bll.DelRoles(Convert.ToInt32(a));
            }
            Response.End();
        }
        #endregion
    }
}
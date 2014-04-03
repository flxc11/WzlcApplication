using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CNVP.UI;
using System.Text;
using System.Data;
using CNVP.Framework.Utils;
using CNVP.Framework.Helper;

namespace CNVP.Admin
{
    public partial class Menu : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //判断用户权限
            base.CheckAuthority("Menu");

            string Action = Request.Params["Action"];
            switch (Action)
            {
                case "GetRoute":
                    GetRoute();
                    break;
                case "GetAllTopMenu":
                    GetAllTopMenu();
                    break;
                case "GetGridMenu":
                    GetGridMenu();
                    break;
                case "GetMenuIcons":
                    GetMenuIcons();
                    break;
                case "UpdateSort":
                    UpdateSort();
                    break;
                case "UpSort":
                    UpSort();
                    break;
                case "DownSort":
                    DownSort();
                    break;
                case "AddMenu":
                    AddMenu();
                    break;
                case "EditMenu":
                    EditMenu();
                    break;
                case "DelMenu":
                    DelMenu();
                    break;
            }
        }
        #region "获取导航路径"
        /// <summary>
        /// 获取导航路径
        /// </summary>
        private void GetRoute()
        {
            StringBuilder Str = new StringBuilder();
            Str.Append("<a href=\"javascript:f_onLoadGridMenu('Root');\">菜单导航</a>");

            string MenuValue = Request.Params["MenuValue"];

            DataView Dv = new DataView(new Data.Menu().GetAllMenu());
            Dv.RowFilter = "MenuValue='" + MenuValue + "'";
            Dv.Sort = "OrderID Asc";
            DataTable Dt = Dv.ToTable();
            if (Dt != null && Dt.Rows.Count > 0)
            {
                Str.Append(" >> ");
                Str.Append("<a href=\"javascript:f_onLoadGridMenu('" + Dt.Rows[0]["MenuValue"] + "');\">" + Dt.Rows[0]["MenuName"] + "</a>");
            }
            //Str.Append(" >> 二级菜单");
            Response.Write(Str.ToString());
            Response.End();
        }
        #endregion
        #region "获取父类菜单"
        /// <summary>
        /// 获取父类菜单
        /// </summary>
        private void GetAllTopMenu()
        {
            StringBuilder Str = new StringBuilder();
            DataView Dv = new DataView(new Data.Menu().GetAllMenu());
            Dv.RowFilter = "MenuParent='Root'";
            Dv.Sort = "OrderID Asc";
            DataTable Dt = Dv.ToTable();

            foreach (DataRow Row in Dt.Rows)
            {
                Str.Append("{\"MenuParent\":\"" + Row["MenuParent"].ToString() + "\",\"MenuValue\":\"" + Row["MenuValue"].ToString() + "\",\"MenuIcon\":\"" + Row["MenuIcon"].ToString() + "\",\"ID\":\"" + Row["MenuID"].ToString() + "\",\"MenuName\":\"" + Row["MenuName"].ToString() + "\"},");
            }

            string ReturnStr = Str.ToString();
            if (!string.IsNullOrEmpty(ReturnStr))
            {
                ReturnStr = ReturnStr.Substring(0, ReturnStr.Length - 1);
            }

            Response.Write("[{\"MenuName\":\"管理中心\",\"children\":[" + ReturnStr + "],\"MenuParent\":\"Root\",\"MenuValue\":\"Root\"}]");
            Response.End();
        }
        #endregion
        #region "获取菜单列表"
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        private void GetGridMenu()
        {
            string MenuParent = Request.Params["MenuParent"];
            StringBuilder Str = new StringBuilder();
            DataView Dv = new DataView(new Data.Menu().GetAllMenu());

            if (string.IsNullOrEmpty(MenuParent))
            {
                Dv.RowFilter = "MenuParent='Root'";
            }
            else
            {
                Dv.RowFilter = "MenuParent='" + MenuParent + "'";
            }
            Dv.Sort = "OrderID Asc";
            DataTable Dt = Dv.ToTable();

            foreach (DataRow Row in Dt.Rows)
            {
                Str.Append("{\"MenuID\":" + Row["MenuID"].ToString() + ",\"MenuValue\":\"" + Row["MenuValue"] + "\",\"MenuParent\":\"" + Row["MenuParent"] + "\",\"MenuOrder\":\"" + Row["OrderID"].ToString() + "\",\"MenuName\":\"" + Row["MenuName"].ToString() + "\",\"MenuUrl\":\"" + Row["MenuUrl"].ToString() + "\",\"MenuIcon\":\"" + Row["MenuIcon"].ToString() + "\",\"IsEnable\":\"" + Row["IsEnable"] + "\",\"IsLeaf\":\"" + Row["IsLeaf"] + "\"},");
            }

            string ReturnStr = Str.ToString();
            if (!string.IsNullOrEmpty(ReturnStr))
            {
                ReturnStr = ReturnStr.Substring(0, ReturnStr.Length - 1);
            }

            Response.Write("{\"Rows\":[" + ReturnStr + "],\"Total\":\"" + Dt.Rows.Count + "\"}");
            Response.End();
        }
        #endregion
        #region "获取菜单图标"
        /// <summary>
        /// 获取菜单图标
        /// </summary>
        private void GetMenuIcons()
        {
            StringBuilder Str = new StringBuilder();
            DataTable Dt = FileUtils.GetAllFolder("~/lib/icons/32X32/", "*.gif");
            foreach (DataRow Row in Dt.Rows)
            {
                Str.Append("{\"MenuIcon\":\"" + Row["FileName"].ToString() + "\"},");
            }

            string ReturnStr = Str.ToString();
            if (!string.IsNullOrEmpty(ReturnStr))
            {
                ReturnStr = ReturnStr.Substring(0, ReturnStr.Length - 1);
            }

            Response.Write("[" + ReturnStr + "]");
            Response.End();
        }
        #endregion
        #region "更新菜单排序"
        /// <summary>
        /// 更新菜单排序
        /// </summary>
        private void UpdateSort()
        {
            string MenuID = Request.Params["MenuID"];
            string MenuParent = Request.Params["MenuParent"];

            Data.Menu bll=new Data.Menu();
            bll.EditMenuOrder(MenuID, MenuParent);
            Response.End();
        }
        /// <summary>
        /// 更新菜单排序
        /// </summary>
        private void UpSort()
        {
            string MenuID = Request.Params["MenuID"];
            string MenuParent = Request.Params["MenuParent"];

            Data.Menu bll = new Data.Menu();
            List<Model.Menu> model = bll.GetUpMenuInfo(Convert.ToInt32(MenuID), MenuParent);
            bll.EditMenuOrder(model);

            Response.End();
        }
        /// <summary>
        /// 更新菜单排序
        /// </summary>
        private void DownSort()
        {
            string MenuID = Request.Params["MenuID"];
            string MenuParent = Request.Params["MenuParent"];

            Data.Menu bll = new Data.Menu();
            List<Model.Menu> model = bll.GetDownMenuInfo(Convert.ToInt32(MenuID), MenuParent);
            bll.EditMenuOrder(model);

            Response.End();
        }
        #endregion
        #region "增加权限菜单"
        /// <summary>
        /// 增加权限菜单
        /// </summary>
        private void AddMenu()
        {
            string MenuName = Request.Params["MenuName"];
            string MenuValue = Request.Params["MenuValue"];
            string MenuUrl = Request.Params["MenuUrl"];
            string MenuIcon = Request.Params["MenuIcon"];
            string MenuParent = Request.Params["MenuParent"];

            Data.Menu bll = new Data.Menu();
            Model.Menu model = new Model.Menu();
            model.MenuName = MenuName;
            model.MenuValue = MenuValue;
            model.MenuUrl = MenuUrl;
            model.MenuParent = MenuParent;
            model.MenuIcon = MenuIcon;
            bll.AddMenu(model);

            Response.Write("{\"mstCode\":\"1\",\"msgStr\":\"编辑菜单操作成功。\"}");
            Response.End();
        }
        #endregion
        #region "编辑权限菜单"
        /// <summary>
        /// 编辑权限菜单
        /// </summary>
        private void EditMenu()
        {
            string MenuID = Request.Params["MenuID"];
            string MenuName = Request.Params["MenuName"];
            string MenuValue = Request.Params["MenuValue"];
            string MenuUrl = Request.Params["MenuUrl"];
            string MenuIcon = Request.Params["MenuIcon"];

            Data.Menu bll = new Data.Menu();
            Model.Menu model = new Model.Menu();
            model.MenuID = Convert.ToInt32(MenuID);
            model.MenuName = MenuName;
            model.MenuValue = MenuValue;
            model.MenuUrl = MenuUrl;
            model.MenuIcon = MenuIcon;
            bll.EditMenu(model);

            Response.Write("{\"mstCode\":\"1\",\"msgStr\":\"编辑菜单操作成功。\"}");
            Response.End();
        }
        #endregion
        #region "删除权限菜单"
        /// <summary>
        /// 删除权限菜单
        /// </summary>
        private void DelMenu()
        {
            string MenuID = Request.Params["MenuID"];
            foreach (string a in MenuID.Split(','))
            {
                if ((!string.IsNullOrEmpty(a)) && (Public.IsNumber(a)))
                {
                    Data.Menu bll = new Data.Menu();
                    Model.Menu model = bll.GetMenuInfo(Convert.ToInt32(a));
                    if (!string.IsNullOrEmpty(model.MenuValue))
                    {
                        bll.DelMenu(model.MenuValue);
                    }
                }
            }
            Response.End();
        }
        #endregion
    }
}
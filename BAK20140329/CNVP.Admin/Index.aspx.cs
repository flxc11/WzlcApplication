using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CNVP.Config;
using CNVP.Framework.Helper;
using CNVP.Framework.Utils;
using CNVP.UI;

namespace CNVP.Admin
{
    public partial class Index : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Action = Request.Params["Action"];
            switch (Action)
            {
                case "ResetSite":
                    ResetSite();
                    break;
                case "CreateCode":
                    CreateCode();
                    break;
                case "GetAllMenu":
                    GetAllMenu();
                    break;
                default:
                    LitUserName.Text=Public.GetStrUpper(UserName);
                    break;
            }
        }
        #region "站点数据重置"
        /// <summary>
        /// 站点数据重置
        /// </summary>
        private void ResetSite()
        {
            Data.AppList bll = new Data.AppList();
            bll.SiteClear(AppID);
            Response.Write("{\"msgCode\":\"0\",\"msgStr\":\"门店信息重置操作成功，请耐心等待客户端更新数据。\"}");
            Response.End();
        }
        #endregion
        #region "生成程序代码"
        /// <summary>
        /// 生成程序代码
        /// </summary>
        private void CreateCode()
        {
            string TableName = Request.Params["TableName"];
            StringBuilder Str = new StringBuilder();
            Str.Append("<style>*{font-size:14px;line-height:200%}textarea {font-size:13px;font-family:Verdana;line-height:100%}</style>");

            Str.Append("表名：<select name=\"select\" id=\"select\" onchange=\"goUrl(this)\" style=\"width:200px\">");
            DataTable Dt = DbHelper.GetAllTable();
            foreach (DataRow Row in Dt.Rows)
            {
                if (string.IsNullOrEmpty(TableName))
                {
                    TableName = Row["Name"].ToString();
                }

                if (Row["Name"].ToString().ToLower() == TableName.ToLower())
                {
                    Str.Append("<option value=\"" + Row["Name"] + "\" selected=\"selected\">" + Row["Name"] + "</option>");
                }
                else
                {
                    Str.Append("<option value=\"" + Row["Name"] + "\">" + Row["Name"] + "</option>");
                }
            }
            Str.Append("</select><br/>备注：无主键表无法自动生成代码<br/>");
            
            string StrCode = EntityUtils.WriteEntity(TableName);
            StrCode = string.Format("<textarea name=\"CreateCode\" cols=\"100\" rows=\"30\">{0}</textarea>", StrCode);
            Str.Append(StrCode);

            Str.Append("<script>function goUrl(sobj) {var TableName =sobj.options[sobj.selectedIndex].value;window.location.href='Index.aspx?Action=CreateCode&TableName='+ TableName};</script>");
            
            Response.Write(Str.ToString());
            Response.End();
        }
        #endregion
        #region "获取菜单列表"
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        private void GetAllMenu()
        {
            StringBuilder Str = new StringBuilder();

            DataView Dv = new DataView(new Data.Menu().GetAllMenu());
            Dv.RowFilter = "MenuParent='Root'";
            Dv.Sort = "OrderID Asc";
            DataTable Dt = Dv.ToTable();
            foreach (DataRow Row in Dt.Rows)
            {
                if (CheckPopDom(Row["MenuValue"].ToString()))
                {
                    //用户权限判断
                    string StrRoot = string.Empty;
                    StrRoot += "{\"MenuID\":" + Row["MenuID"].ToString() + ",\"MenuName\":\"" + Row["MenuName"].ToString() + "\",\"MenuValue\":\"" + Row["MenuValue"].ToString() + "\",\"MenuUrl\":\"" + Row["MenuUrl"].ToString() + "\",\"MenuParent\":\"" + Row["MenuParent"].ToString() + "\",\"MenuIcon\":\"" + Row["MenuIcon"].ToString() + "\",\"children\":[";

                    //读取子类菜单
                    if (Row["IsLeaf"].ToString() == "1")
                    {
                        string StrSub = string.Empty;
                        DataView Dv1 = new DataView(new Data.Menu().GetAllMenu());
                        Dv1.RowFilter = "MenuParent='" + Row["MenuValue"].ToString() + "'";
                        Dv1.Sort = "OrderID Asc";
                        DataTable Dt1 = Dv1.ToTable();
                        foreach (DataRow Row1 in Dt1.Rows)
                        {
                            if (CheckPopDom(Row1["MenuValue"].ToString()))
                            {
                                StrSub += "{\"MenuID\":" + Row1["MenuID"].ToString() + ",\"MenuName\":\"" + Row1["MenuName"].ToString() + "\",\"MenuUrl\":\"" + Row1["MenuUrl"].ToString() + "\",\"MenuIcon\":\"" + Row1["MenuIcon"].ToString() + "\",\"MenuValue\":\"" + Row1["MenuValue"].ToString() + "\",\"MenuParent\":\"" + Row1["MenuParent"].ToString() + "\"},";
                            }
                        }
                        if (!string.IsNullOrEmpty(StrSub))
                        {
                            StrSub = StrSub.Substring(0, StrSub.Length - 1);
                            StrRoot += StrSub;
                        }
                    }
                    StrRoot += "]},";
                    //加入菜单列表
                    Str.Append(StrRoot);
                }
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
    }
}
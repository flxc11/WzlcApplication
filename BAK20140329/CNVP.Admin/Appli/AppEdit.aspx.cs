using CNVP.Framework.Helper;
using CNVP.Framework.Utils;
using CNVP.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CNVP.Admin.Appli
{
    public partial class AppEdit : AdminPage
    {
        public string CurrentID = string.Empty;
        public string AppName, AppCardID, AppPhone, AppEmail, AppType, AppResult, AppContent, AppAddress, AppMaterial, AppReply = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["Action"] == "Edit")
                {
                    CurrentID = Request.Params["AppID"];
                    string AppReply = Request.Params["AppReply"];
                    Data.Application bll1 = new Data.Application();
                    Model.Application model1 = new Model.Application();
                    model1.ID = Convert.ToInt32(CurrentID);
                    model1.AppReply = AppReply;
                    bll1.AppReply(model1);
                    Response.Write("<script>var win = parent || window;win.LG.closeAndReloadParent(null, 'Applist');</script>");
                    Response.End();
                }
                CurrentID = Request.Params["ID"];
                if (string.IsNullOrEmpty(CurrentID) || (!Public.IsNumber(CurrentID)))
                {
                    CurrentID = "0";
                }
                using (DataTable dt = DbHelper.ExecuteTable("select A.AppName, A.AppCardID, A.AppPhone, A.AppAddress,A.AppEmail, B.AppType, B.AppResult, B.AppReply, B.AppPic, B.AppContent, B.AppReply,B.PostTime from HX_Application as B inner join HX_AppliUsers as A on B.AppUserID=A.ID Where B.ID=" + CurrentID))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AppName = dt.Rows[0]["AppName"].ToString();
                        AppCardID = dt.Rows[0]["AppCardID"].ToString();
                        AppPhone = dt.Rows[0]["AppPhone"].ToString();
                        AppAddress = dt.Rows[0]["AppAddress"].ToString();
                        AppEmail = dt.Rows[0]["AppEmail"].ToString();
                        AppType = GetType(dt.Rows[0]["AppType"].ToString());
                        AppResult = GetResult(dt.Rows[0]["AppResult"].ToString());
                        AppContent = dt.Rows[0]["AppContent"].ToString();
                        AppMaterial = dt.Rows[0]["AppPic"].ToString();
                        AppReply = dt.Rows[0]["AppReply"].ToString();
                        StringBuilder SB = new StringBuilder();
                        if (AppMaterial.Length > 0)
                        {
                            string[] piclist = AppMaterial.Replace("|$|", "|").Split(new char[] { '|' });
                            for (int i = 0; i < piclist.Length - 1; i++)
                            {
                                SB.Append("<div class=\"divmatra\">");
                                SB.Append("    <div class=\"divpic\">");
                                SB.Append("<img src=\"" + piclist[i] + "\" id=\"preImg" + i + "\" fancyId=\"big" + i + "\" style=\"width:110px;height:110px;background-color:#ccc;border:1px solid #333 \" />");
                                SB.Append("    </div>");
                                SB.Append("    <div class=\"divtxt\">");
                                SB.Append("<a href=\"" + piclist[i] + "\" target=_blank>下载</a>");
                                SB.Append("    </div>");
                                SB.Append("</div>");
                            }
                            AppMaterial = SB.ToString();
                            SB.Length = 0;
                        }
                    }
                }
            }
        }

        #region 申请类型
        public string GetType(string _Type)
        {
            string str = "个人";
            if (_Type == "1")
            {
                str = "企业";
            }
            return str;
        }
        #endregion

        #region 结果需求
        public string GetResult(string _Type)
        {
            string str = "邮件";
            if (_Type == "1")
            {
                str = "快递（到付）";
            }
            return str;
        }
        #endregion
    }
}
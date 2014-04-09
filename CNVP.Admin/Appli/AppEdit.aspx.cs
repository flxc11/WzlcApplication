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
        public string AppName, AppCardID, AppPhone, AppEmail, AppType, AppResult, AppContent, AppAddress, AppMaterial, AppReply, AppThings, _PostTime, IsAudit = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["Action"] == "Edit")
                {
                    CurrentID = Request.Params["AppID"];
                    string AppReply = Request.Params["AppReply"];
                    string _IsAudit = Request.Params["IsAudit"];
                    string IsMeg = Request.Params["IsAudit"];
                    Data.Application bll1 = new Data.Application();
                    Model.Application model1 = new Model.Application();
                    model1.ID = Convert.ToInt32(CurrentID);
                    model1.IsAudit = _IsAudit;
                    model1.AppReply = AppReply;
                    model1.AuditMan = UserName;
                    string IsSms = Request.Params["IsSms"];
                    string _userPhone = Request.Params["AppPhone"];
                    string _userName = Request.Params["AppName"];
                    Ajax _ajax = new Ajax();
                    string _n_PostTime = Request.Params["PostTime"];
                    string _type = Request.Params["AppThings"];
                    string _rlt = string.Empty;
                    if (IsSms == "1")
                    {
                        if (_IsAudit == "1")
                        {
                            _rlt = "审核通过";
                        }
                        else
                        {
                            _rlt = "审核未通过";
                        }
                        string _content = _userName + " 您好，您在 " + _n_PostTime + " 申请的 " + _type + " 查调事项的申请结果为 " + _rlt + " ,管理员回复：" + AppReply + "！退订回复TD【鹿城档案地方志网】";
                        if (_ajax.SendSms1(_userPhone, _content) == "0")
                        {
                            bll1.AppReply(model1);
                            Response.Write("<script>var win = parent || window;win.LG.closeAndReloadParent(null, 'Applist');</script>");
                            Response.End();
                        }
                        else
                        {
                            Response.Write("<script>alert('回复失败!');var win = parent || window;win.LG.closeAndReloadParent(null, 'Applist');</script>");
                            Response.End();
                        }
                    }
                    else
                    {
                        bll1.AppReply(model1);
                        Response.Write("<script>var win = parent || window;win.LG.closeAndReloadParent(null, 'Applist');</script>");
                        Response.End();
                    }
                    
                }
                CurrentID = Request.Params["ID"];
                if (string.IsNullOrEmpty(CurrentID) || (!Public.IsNumber(CurrentID)))
                {
                    CurrentID = "0";
                }
                using (DataTable dt = DbHelper.ExecuteTable("select A.AppName, A.AppCardID, A.AppPhone, A.AppAddress,A.AppEmail, B.AppType, B.AppResult, B.AppReply, B.AppPic, B.AppContent, B.AppReply,B.PostTime,B.AppThings,B.IsAudit from HX_Application as B inner join HX_AppliUsers as A on B.AppUserID=A.ID Where B.ID=" + CurrentID))
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
                        AppThings = GetAppType(dt.Rows[0]["Appthings"].ToString());
                        IsAudit = dt.Rows[0]["IsAudit"].ToString();
                        _PostTime = Convert.ToDateTime(dt.Rows[0]["PostTime"].ToString()).ToString("yyyy-MM-dd");
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

        public string GetAppType(string TypeID)
        {
            string str = string.Empty;
            using (DataTable dt = DbHelper.ExecuteTable("select * from HX_Type where ID=" + TypeID))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    str = dt.Rows[0]["TypeName"].ToString();
                }
            }
            return str;
        }
    }
}
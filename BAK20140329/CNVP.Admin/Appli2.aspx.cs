using CNVP.Config;
using CNVP.Framework.Helper;
using CNVP.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CNVP.Admin
{
    public partial class Appli2 : System.Web.UI.Page
    {
        public string _UserName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string PageNo = string.Empty;
            if (!IsPostBack)
            {
                HttpCookie Cookie = HttpContext.Current.Request.Cookies["CNVP_HD_Users"];
                if (Cookie != null)
                {
                    try
                    {
                        string Str = Cookie.Value;
                        string StrDecrypTo = EncryptUtils.DecodeCookies(Str);
                        string[] StrInfo = StrDecrypTo.Split('|');
                        _UserName = StrInfo[1];
                        string UserID = StrInfo[0];
                        PageNo = Request.Params["PageNo"];
                        if (string.IsNullOrEmpty(PageNo) || (!Public.IsNumber(PageNo)))
                        {
                            PageNo = "1";
                        }
                        int PageSize = 10;
                        int RecordCount, PageCount;
                        string StrWhere = "" + DbConfig.Prefix + "Application As B INNER JOIN " + DbConfig.Prefix + "AppliUsers As A ON B.AppUserID=A.ID Where 1=1 And A.ID=" + UserID;
                        DataTable dt = DbHelper.ExecutePage("A.ID,A.AppName,B.AppContent,B.AppReply, B.PostTime,B.IsAudit,B.AppContent", StrWhere, "B.ID", "Order By B.PostTime Desc", Convert.ToInt32(PageNo), PageSize, out RecordCount, out PageCount);
                        rptList.DataSource = dt;
                        rptList.DataBind();
                        LitPager.Text = DbHelper.GetPageNormal(RecordCount, PageCount, PageSize, Convert.ToInt32(PageNo));
                    }
                    catch
                    {

                    }
                }
                else
                {
                    Response.Redirect("appsearch.aspx");
                    Response.End();
                }

                
            }
        }

        public string GetResult(string Audit)
        {
            string str = "未通过";
            if (Audit == "1")
            {
                str = "已通过";
            }
            return str;
        }
    }
}
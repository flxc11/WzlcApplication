using CNVP.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CNVP.Admin
{
    public partial class appsearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Action = Public.FilterSql(Request.Params["Action"]);

                switch (Action)
                {
                    case "login":
                        Login();
                        break;
                }
            }
            
        }

        private void Login()
        {
            string UserName = Public.FilterSql(Request.Form["talname"]);
            string UserPhone = Public.FilterSql(Request.Form["taltel"]);
            string CheckCode = Public.FilterSql(Request.Form["CheckCode"]);
            Data.UsersSmsData bll = new Data.UsersSmsData();
            Model.AppliUsers model = new Model.AppliUsers();
            if (bll.CheckLogin(UserPhone, CheckCode))
            {
                if (bll.CheckLogin1(UserName, UserPhone, ref model))
                {
                    string str = string.Format("{0}|{1}", model.ID, model.AppName);
                    //创建登录授权
                    HttpCookie _Cookie = new HttpCookie("CNVP_HD_Users");
                    string StrEncrypTo = EncryptUtils.EncodeCookies(str);
                    _Cookie.Value = StrEncrypTo;
                    HttpContext.Current.Response.Cookies.Add(_Cookie);
                    HttpContext.Current.Response.Redirect("Appli2.aspx");
                }
                else
                {
                    Response.Redirect("AppReg.aspx");
                }
            }
            else
            {
                MessageUtils.ShowRedirect("验证码错误，请重新输入！", "appsearch.aspx");
            }
        }
        private string Audit(string IsAudit)
        {
            string rlt = "未通过";
            if (IsAudit == "1")
            {
                rlt = "已通过";
            }
            return rlt;
        }
    }
}